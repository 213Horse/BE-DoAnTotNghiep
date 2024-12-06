namespace CleanMinimalApi.Infrastructure.Databases.Tourism.Models;
public class Province : Entity
{
    public string Name { get; set; }
    public virtual ICollection<City> Cities { get; set; } = [];
}
