using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModelLayer
{
    public class Order
    {
        public Order()
        {
            Products = new List<Product>(); // Initialize the Products collection
        }
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public ApplicationUser User { get; set; }
        public List<Product> Products { get; set; }
    }

}
