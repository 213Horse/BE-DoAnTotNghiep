namespace CleanMinimalApi.Infrastructure.Databases.Tourism.Models;

public class Category : Entity
{
    public string Name { get; set; }
    public ICollection<BrandCategory> BrandCategories { get; set; } = [];
}
