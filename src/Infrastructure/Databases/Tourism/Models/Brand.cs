namespace CleanMinimalApi.Infrastructure.Databases.Tourism.Models;

public class Brand : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public Guid DistrictId { get; set; }
    public virtual District District { get; set; }
    public string Coordinate { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public decimal? CenterDistance { get; set; }
    public virtual ICollection<BrandCategory> BrandCategories { get; set; } = [];
    public virtual ICollection<Service> Services { get; set; } = [];
}
