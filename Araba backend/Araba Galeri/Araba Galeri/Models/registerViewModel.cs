using System.ComponentModel.DataAnnotations;

namespace Araba_Galeri.Models
{
    public class registerViewModel
    {
        [Required(ErrorMessage = "Ad alanı boş bırakılamaz.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "E-posta alanı boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Eposta { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "Şifre tekrar alanı boş bırakılamaz.")]
        [DataType(DataType.Password)]
        [Compare("Sifre", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string SifreTekrar { get; set; }
    }
}
