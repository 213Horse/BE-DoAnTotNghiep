namespace CleanMinimalApi.Infrastructure.Databases.Tourism.Models;

public class OrderDetail : Entity
{
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; }
    public Guid ServiceId { get; set; }
    public virtual Service Service { get; set; }
}
