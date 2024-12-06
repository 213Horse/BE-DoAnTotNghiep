namespace CleanMinimalApi.Infrastructure.Databases.Tourism.Models;

public class BrandCategory : Entity
{
    public Guid BrandId { get; set; }
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public virtual Brand Brand { get; set; }
}
