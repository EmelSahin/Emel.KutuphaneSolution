using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TB.Kutuphane.Entity
{
    public class Kitap : BaseEntity
    {
        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Ad { get; set; }

        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(20)]
        public string SiraNo { get; set; }

        [Required]
        public int Adet { get; set; }

        [Required]
        public DateTime EklemeTarihi { get; set; }

        public int YazarId { get; set; }
        public virtual Yazar Yazarlar { get; set; }

        public virtual List<Kategori> Kategoriler { get; set; }
    }
}
