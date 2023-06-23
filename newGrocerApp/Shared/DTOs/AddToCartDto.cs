using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class AddToCartDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int ProductId { get; set; }
    }

}
