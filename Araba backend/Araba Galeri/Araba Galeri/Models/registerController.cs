using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient; // Veya System.Data.SqlClient
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

[Route("register")]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly string connectionString;

    // IConfiguration servisini DI ile alıp bağlantı dizesini okuyorsunuz.
    public RegisterController(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException("Bağlantı dizesi bulunamadı veya geçersiz. Lütfen appsettings.json dosyanızı kontrol edin.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserRegistration registration)
    {
        if (string.IsNullOrWhiteSpace(registration.Ad) ||
            string.IsNullOrWhiteSpace(registration.Eposta) ||
            string.IsNullOrWhiteSpace(registration.Sifre))
        {
            return BadRequest(new { message = "Tüm alanlar zorunludur." });
        }

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            // E-posta kontrolü
            string emailCheckQuery = "SELECT COUNT(*) FROM kullanicilar WHERE Eposta = @Email";
            using (SqlCommand emailCheckCommand = new SqlCommand(emailCheckQuery, connection))
            {
                emailCheckCommand.Parameters.AddWithValue("@Email", registration.Eposta);
                int emailExists = (int)await emailCheckCommand.ExecuteScalarAsync();
                if (emailExists > 0)
                {
                    return BadRequest(new { message = "Bu e-posta zaten kayıtlı." });
                }
            }

            // Kayıt ekleme sorgusu
            string query = "INSERT INTO kullanicilar (Ad, Eposta, Sifre) VALUES (@username, @email, @password)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", registration.Ad);
                command.Parameters.AddWithValue("@email", registration.Eposta);
                command.Parameters.AddWithValue("@password", registration.Sifre); // Gerçek projede şifreleme eklemeniz önemli

                try
                {
                    int result = await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        return Ok(new { message = "Kayıt başarılı." });
                    }
                    else
                    {
                        return BadRequest(new { message = "Kayıt başarısız." });
                    }
                }
                catch (SqlException ex)
                {
                    return StatusCode(500, new { message = "Veritabanı hatası: " + ex.Message });
                }
            }
        }
    }
}
public class UserRegistration
{
    public string Ad { get; set; }
    public string Eposta { get; set; }
    public string Sifre { get; set; }
}
