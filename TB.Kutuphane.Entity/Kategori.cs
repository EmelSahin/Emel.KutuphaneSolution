using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TB.Kutuphane.Entity
{
    public class Kategori : BaseEntity
    {
        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string KategoriAdi { get; set; }

        public virtual List<Kitap> Kitaplar { get; set; }
    }
}
