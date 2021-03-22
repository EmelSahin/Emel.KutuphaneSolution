using System;
using System.ComponentModel.DataAnnotations;

namespace TB.Kutuphane.Entity
{
    public class OduncKitap : BaseEntity
    {
        [Required]
        public DateTime AlisTarih { get; set; }

        [Required]
        public DateTime GetirecegiTarih { get; set; }

        public DateTime? GetirdigiTarih { get; set; }

        public bool GetirdiMi { get; set; }

        public int KitapId { get; set; }
        public virtual Kitap Kitaplar { get; set; }

        public int UyeId { get; set; }
        public virtual Uye Uyeler { get; set; }
    }
}
