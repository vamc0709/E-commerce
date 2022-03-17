using System.Text.Json.Serialization;

namespace OnlineShop.DTOs;

public record ProductsDTO
{
    [JsonPropertyName("product_id")]
    public long ProductId { get; set; }

    [JsonPropertyName("product_name")]
    public string ProductName { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("in_stock")]
    public string InStock { get; set; }

}

public record ProductsCreateDTO
{
    [JsonPropertyName("product_id")]
    public long ProductId { get; set; }

    [JsonPropertyName("product_name")]
    public string ProductName { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("in_stock")]
    public string InStock { get; set; }

}

public record ProductsUpdateDTO
{
   
    [JsonPropertyName("product_name")]
    public string ProductName { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("in_stock")]
    public string InStock { get; set; }

}