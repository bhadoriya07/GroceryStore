using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModelLayer
{
    public class Cart
    {
        public Cart()
        {
            Products = new List<Product>();
        }
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public List<Product> Products { get; set; }
    }

}
