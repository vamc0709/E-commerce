using OnlineShop.DTOs;

namespace OnlineShop.Models;


public record Orders
{
    
    public long OrderId { get; set; }
    public string Status { get; set; }

    public long CustomerId { get; set; }
    
    

    public OrdersDTO asDto => new OrdersDTO
    {
        // OrderId = OrderId,
        Status = Status,
        // CustomerId = CustomerId,
    };
}