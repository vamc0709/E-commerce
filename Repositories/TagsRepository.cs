using Dapper;
using OnlineShop.Models;
using OnlineShop.Utilities;

namespace OnlineShop.Repositories;

public interface ITagsRepository
{
    Task<Tags> Create(Tags Item);
    Task<bool> Update(Tags Item);
    Task<bool> Delete(long TagId);
    Task<List<Tags>> GetAllForOrders(long TagId);
    Task<Tags> GetById(long TagId);
}

public class TagsRepository : BaseRepository, ITagsRepository
{
    public TagsRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Tags> Create(Tags Item)
    {
        var query = $@"INSERT INTO {TableNames.tags} (tag_id, tag_name, description,
        price, status) VALUES (@TagId, @TagName, @Description, @Price, @Status) 
        RETURNING *";

        using (var con = NewConnection)
            return await con.QuerySingleAsync<Tags>(query, Item);
    }


    public async Task<bool> Delete(long TagId)
    {
        var query = $@"DELETE FROM {TableNames.tags} WHERE tag_id = @TagId";

        using (var con = NewConnection)
            {
            var res = await con.ExecuteAsync(query, new{TagId});
        
           return res == 1;
        }
    }

    

    public async Task<List<Tags>> GetAllForOrders(long TagId)
    {
        var query = $@"SELECT * FROM {TableNames.tags} 
        WHERE tag_id = @TagId";

        using (var con = NewConnection)
            return (await con.QueryAsync<Tags>(query, new { TagId })).AsList();
    }

   
    public async Task<Tags> GetById(long TagId)
    {
        var query = $@"SELECT * FROM {TableNames.tags} 
        WHERE tag_id = @TagId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Tags>(query, new { TagId });
    }

    public async Task<bool> Update(Tags Item)
    {
        var query = $@"UPDATE {TableNames.tags} SET  tag_name = @TagName, price = @Price, status = @Status
        WHERE tag_id = @TagId";

        using (var con = NewConnection)
            {
            var res = await con.ExecuteAsync(query, Item);
        
           return res == 1;
    }
  }
}