using Dapper;
using OnlineShop.Models;
using OnlineShop.Utilities;

namespace OnlineShop.Repositories;

public interface IProductsRepository
{
    Task<Products> Create(Products Item);
    Task<bool> Update(Products Item);
    Task<bool> Delete(long ProductId);
    Task<List<Products>> GetList();
    Task<Products> GetById(long ProductId);
}

public class ProductsRepository : BaseRepository, IProductsRepository
{
    public ProductsRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Products> Create(Products Item)
    {
        var query = $@"INSERT INTO {TableNames.products} (product_id, product_name, price,
        description, in_stock) VALUES (@ProductId, @ProductName, @Price, @Description, @InStock) 
        RETURNING *";

        using (var con = NewConnection)
            return await con.QuerySingleAsync<Products>(query, Item);
    }

    
    public async Task<bool> Delete(long ProductId)
    {
        var query = $@"DELETE FROM {TableNames.products} WHERE Product_id = @ProductId";

         using (var con = NewConnection){
            var res = await con.ExecuteAsync(query, new{ProductId});
        
           return res == 1;
        }
    }

    


    public async Task<List<Products>> GetList()
    {
        var query = $@"SELECT * FROM {TableNames.products}";

        using (var con = NewConnection)
            return (await con.QueryAsync<Products>(query)).AsList();
    }

    public async Task<Products> GetById(long ProductId)
    {
        var query = $@"SELECT * FROM {TableNames.products} 
        WHERE Product_id = @ProductId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Products>(query, new { ProductId });
    }

    public async Task<bool> Update(Products Item)
    {
        var query = $@"UPDATE {TableNames.products} SET  in_stock = @InStock WHERE product_id = @ProductId";

        using (var con = NewConnection){
            var res = await con.ExecuteAsync(query, Item);
        
           return res == 1;
        }
          


    }
}