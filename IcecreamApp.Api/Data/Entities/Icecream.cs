using System.ComponentModel.DataAnnotations;

namespace IcecreamApp.Api.Data.Entities
{
    public class Icecream
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Range(0.1, Double.MaxValue)]
        public double Price { get; set; }
        [Required, MaxLength(180)]
        public string Image { get; set; }

        public virtual ICollection<IcecreamOptions> Options { get; set; }
    }
}