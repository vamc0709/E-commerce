using OnlineShop.Models;
using Dapper;
using OnlineShop.Utilities;


namespace OnlineShop.Repositories;

public interface ICustomerRepository
{
    Task<Customer> Create(Customer Item);
    Task<bool> Update(Customer Item);
    Task<Customer> GetById(long CustomerId);
    Task<List<Customer>> GetList();

}
public class CustomerRepository : BaseRepository, ICustomerRepository
{
    public CustomerRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Customer> Create(Customer Item)
    {
        var query = $@"INSERT INTO ""{TableNames.customer}"" 
        (customer_id, customer_name, gender, mobile_number, address) 
        VALUES (@CustomerId, @CustomerName, @Gender, @MobileNumber, @Address) 
        RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Customer>(query, Item);
            return res;
        }
    }

    public async Task<Customer> GetById(long CustomerId)
    {
        var query = $@"SELECT * FROM ""{TableNames.customer}"" 
        WHERE customer_id = @CustomerId";
        // SQL-Injection

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Customer>(query, new { CustomerId });
    }

    public async Task<List<Customer>> GetList()
    {
        
        var query = $@"SELECT * FROM ""{TableNames.customer}""";

        List<Customer> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Customer>(query)).AsList();
            
        return res;
    }

    public async Task<bool> Update(Customer Item)
    {
        var query = $@"UPDATE ""{TableNames.customer}"" SET  
        customer_name = @CustomerName, gender = @Gender, mobile_number = @MobileNumber, 
        email = @Email, gender = @Gender WHERE customer_id = @CustomerId address = @Address";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, Item);
            return rowCount == 1;
        }
    }
}