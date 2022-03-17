 using OnlineShop.DTOs;

namespace OnlineShop.Models;

public enum Gender
{
    Female = 1, 
    Male = 2, 
    
}

public record Customer
{
    public long CustomerId { get; set; }
    public string CustomerName { get; set; }

    public Gender Gender { get; set; }
    public string Address { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public long MobileNumber { get; set; }
    public long ProductId { get; set; }


    public long OrderId { get; set; }
    

    public CustomerDTO asDto => new CustomerDTO
    {
        CustomerId = CustomerId,
        CustomerName = CustomerName,
        Gender = Gender.ToString().ToLower(),
        Address = Address,
        MobileNumber = MobileNumber,
        ProductId = ProductId,
        
        
    };
}