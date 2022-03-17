using OnlineShop.DTOs;
using OnlineShop.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrdersRepository _orders;

    public OrdersController(ILogger<OrdersController> logger,
    IOrdersRepository orders)
    {
        _logger = logger;
        _orders = orders;
    
    }

    
    [HttpGet]
    public async Task<ActionResult<List<OrdersDTO>>> GetList()
    {
        var ordersList = await _orders.GetList();

        var dtoList = ordersList.Select(x => x.asDto);

        return Ok(dtoList);
    }


    [HttpPut("{order_id}")]
    public async Task<ActionResult> UpdateOrders([FromRoute] int order_id,
    [FromBody] OrdersCreateDTO Data)
    {
        var existing = await _orders.GetById(order_id);
        if (existing is null)
            return NotFound("No Order found with given id");

        var toUpdateItem = existing with
        {
            Status = Data.Status?.Trim(),
    
        };

        await _orders.Update(toUpdateItem);

        return NoContent();
    }

    [HttpDelete("{order_id}")]
    public async Task<ActionResult> DeleteOrders([FromRoute] int order_id)
    {
        var existing = await _orders.GetById(order_id);
        if (existing is null)
            return NotFound("No orders found with given id");

        await _orders.Delete(order_id);

        return NoContent();
    }
}
