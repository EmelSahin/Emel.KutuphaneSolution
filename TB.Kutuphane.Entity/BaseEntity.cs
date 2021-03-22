using System.ComponentModel.DataAnnotations;

namespace TB.Kutuphane.Entity
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
