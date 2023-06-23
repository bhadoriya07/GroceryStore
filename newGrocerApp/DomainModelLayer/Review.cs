using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModelLayer
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Content { get; set; }

        [Required]
        public string reviewBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }

}
