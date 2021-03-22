using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TB.Kutuphane.Entity
{
    public class Uye : BaseEntity
    {
        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(50, ErrorMessage = "Ad alanı maksimum 50 karakter olmalıdır.")]
        [MinLength(2, ErrorMessage = "Ad alanı minimum 2 karakter olmalıdır.")]
        public string Ad { get; set; }

        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(50, ErrorMessage = "Soyad alanı maksimum 50 karakter olmalıdır.")]
        [MinLength(2, ErrorMessage = "Soyad alanı minimum 2 karakter olmalıdır.")]
        public string Soyad { get; set; }

        [Column(TypeName = "char")]
        [MaxLength(11, ErrorMessage = "TC Kimlik numarası 11 sayıdan oluşmaktadır.")]
        [MinLength(11, ErrorMessage = "TC Kimlik numarası 11 sayıdan oluşmaktadır.")]
        public string TC { get; set; }

        [Column(TypeName = "char")]
        [MaxLength(11, ErrorMessage = "Telefon numarası 11 sayıdan oluşmaktadır.")]
        [MinLength(11, ErrorMessage = "Telefon numarası 11 sayıdan oluşmaktadır.")]
        public string Telefon { get; set; }

        [Required]
        public DateTime KayitTarihi { get; set; }

        [Column(TypeName = "nvarchar")]
        [MaxLength(100)]
        public string Mail { get; set; }

        [Column(TypeName = "char")]
        [MaxLength(32)]
        [MinLength(32)]
        public string Sifre { get; set; }

        [Required]
        public int Ceza { get; set; }

        [Column(TypeName = "char")]
        [MaxLength(1)]
        [MinLength(1)]
        public string Yetki { get; set; }

        public virtual List<OduncKitap> OduncKitaplar { get; set; }
    }
}
