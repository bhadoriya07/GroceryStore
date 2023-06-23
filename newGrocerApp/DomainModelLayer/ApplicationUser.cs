using Microsoft.AspNetCore.Identity;

namespace DomainModelLayer
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public Cart Cart { get; set; } = new Cart();
        public List<Order> Orders { get; set; }
        public bool IsAdmin { get; set; }  

    }
}