namespace CleanMinimalApi.Infrastructure.Databases.Tourism.Models;

public class Sponsor : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<Service> Services { get; set; } = [];

}
