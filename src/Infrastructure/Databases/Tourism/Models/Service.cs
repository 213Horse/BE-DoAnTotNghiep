namespace CleanMinimalApi.Infrastructure.Databases.Tourism.Models;

public class Service : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal Commission { get; set; }
    public Guid BrandId { get; set; }
    public virtual Brand Brand { get; set; }
    public Guid SponsorId { get; set; }
    public virtual Sponsor Sponsor { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; } = [];

}
