namespace CleanMinimalApi.Infrastructure.Databases.Tourism.Models;

public class District : Entity
{
    public string Name { get; set; }
    public Guid CityId { get; set; }
    public virtual City City { get; set; }
    public virtual ICollection<Brand> Brands { get; set; } = [];
}
