using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

[Route("login")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly string connectionString;

    // IConfiguration ile appsettings.json'dan DefaultConnection okunur.
    public LoginController(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new System.Exception("Bağlantı dizesi bulunamadı veya geçersiz. Lütfen appsettings.json dosyanızı kontrol edin.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserLogin login)
    {
        if (string.IsNullOrWhiteSpace(login.Eposta) || string.IsNullOrWhiteSpace(login.Sifre))
        {
            return BadRequest(new { message = "Tüm alanlar zorunludur." });
        }

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            // Kullanıcının var olup olmadığını kontrol ediyoruz.
            string query = "SELECT COUNT(*) FROM kullanicilar WHERE Eposta = @Email AND Sifre = @Password";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", login.Eposta);
                command.Parameters.AddWithValue("@Password", login.Sifre);

                int count = (int)await command.ExecuteScalarAsync();
                if (count > 0)
                {
                    return Ok(new { message = "Giriş başarılı!" });
                }
                else
                {
                    return Unauthorized(new { message = "Geçersiz e-posta veya şifre." });
                }
            }
        }
    }
}
public class UserLogin  
{
    public string Eposta { get; set; }
    public string Sifre { get; set; }
}
