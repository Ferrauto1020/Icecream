using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace IcecreamApp.Api.Data.Entities
{
    public class IcecreamOptions
    {
        public int IcecreamId { get; set; }
        [Required, MaxLength(50)]
        public string Flavor { get; set; }
        [Required, MaxLength(50)]
        public string Topping { get; set; }

        public virtual Icecream Icecream { get; set; }
    }
}