namespace CleanMinimalApi.Infrastructure.Databases.Tourism.Models;

public class Order : Entity
{
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Waiting;
    public DateTime PaymentDate { get; set; }
    public string PaymentDescription { get; set; }
    public string Signature { get; set; }
    public int OrderCode { get; set; }
    public DateTime ExpiredAt { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; } = [];
}
