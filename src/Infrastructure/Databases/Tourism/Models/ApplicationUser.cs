namespace CleanMinimalApi.Infrastructure.Databases.Tourism.Models;

using Microsoft.AspNetCore.Identity;
public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public ICollection<Order> Orders { get; set; } = [];
}
