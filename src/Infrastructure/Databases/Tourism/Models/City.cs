namespace CleanMinimalApi.Infrastructure.Databases.Tourism.Models;

public class City : Entity
{
    public string Name { get; set; }
    public virtual Guid ProvinceId { get; set; }
    public virtual Province Province { get; set; }
    public virtual ICollection<District> Districts { get; set; } = [];
}
