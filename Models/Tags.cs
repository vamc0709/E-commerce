using OnlineShop.DTOs;

namespace OnlineShop.Models;



public record Tags
{
    public long TagId { get; set; }
    public string TagName { get; set; }

    public string Description { get; set; }
    
    public decimal Price { get; set; }
    public string Status { get; set; }


    public long OrderId { get; set; }

    public long ProductId { get; set; }
    

    public TagsDTO asDto => new TagsDTO
    {
        TagId = TagId,
        TagName = TagName,
        Description = Description,
        Price = Price,
    };
}