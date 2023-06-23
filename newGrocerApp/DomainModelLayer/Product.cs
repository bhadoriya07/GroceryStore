using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModelLayer
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Discount { get; set; }

        [StringLength(100)]
        public string Specification { get; set; }

        public List<Cart> Carts { get; set; }
        public List<Order> Orders { get; set; }
        public List<Review> Reviews { get; set; }

        [NotMapped]
        public string ImageUrl { get; set; } // URL of the image, not mapped to database
    }


}
