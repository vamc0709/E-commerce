using Dapper;
using OnlineShop.Models;
using OnlineShop.Utilities;

namespace OnlineShop.Repositories;

public interface IOrdersRepository
{
    Task Update(Orders Item);
    Task Delete(long OrderId);
    Task<List<Orders>> GetList();
    Task<Orders> GetById(long OrderId);

    Task<List<Orders>> GetListByCustomerId(long CustomerId);
}

public class OrdersRepository : BaseRepository, IOrdersRepository
{
    public OrdersRepository(IConfiguration config) : base(config)
    {

    }

    
    public async Task Delete(long OrderId)
    {
        var query = $@"DELETE FROM {TableNames.orders} WHERE order_id = @OrderId";

        using (var con = NewConnection)
            await con.ExecuteAsync(query, new { OrderId });
    }

    

    public async Task<List<Orders>> GetList()
    {
        var query = $@"SELECT * FROM {TableNames.orders}";

        using (var con = NewConnection)
            return (await con.QueryAsync<Orders>(query)).AsList();
    }

   
    public async Task<Orders> GetById(long OrderId)
    {
        var query = $@"SELECT * FROM {TableNames.orders} 
        WHERE order_id = @OrderId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Orders>(query, new { OrderId });
    }

    public async Task Update(Orders Item)
    {
        var query = $@"UPDATE {TableNames.orders} SET  status = @Status WHERE
        
        order_id = @OrderId";

        using (var con = NewConnection)
            await con.ExecuteAsync(query, Item);
    }

    public async Task<List<Orders>> GetListByCustomerId(long CustomerId)
    {
        var query = $@"SELECT o.* FROM {TableNames.customer} c 
        LEFT JOIN {TableNames.orders} o ON c.customer_id = o.customer_id 
        WHERE c.customer_id = @CustomerId";

        using (var con = NewConnection)
            return (await con.QueryAsync<Orders>(query, new { CustomerId })).AsList();
    }
}