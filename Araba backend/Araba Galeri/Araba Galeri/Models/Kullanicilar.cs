using System.ComponentModel;

namespace Araba_Galeri.Models
{
    public partial class Kullanicilar
    {
        [DisplayName("kullancının numara")]
        public int Id { get; set; }
        [DisplayName("kullanıcının adı")]
        public string Ad { get; set; }

        [DisplayName("kullanıcının e-postası")]
        public string Eposta { get; set; }
        [DisplayName("kullanıcının şifresi")]
        public string Sifre { get; set; }
    }
}
