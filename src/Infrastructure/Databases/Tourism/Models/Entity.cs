namespace CleanMinimalApi.Infrastructure.Databases.Tourism.Models;
public abstract class Entity
{

    public Entity()
    {
        this.IsDeleted = false;
    }

    public Guid Id { get; init; }
    public DateTime DateCreated { get; init; }
    public DateTime DateModified { get; set; }
    public bool IsDeleted { get; set; }
}
