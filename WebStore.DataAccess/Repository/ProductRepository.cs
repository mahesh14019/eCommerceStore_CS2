using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using WebStore.DataAccess.Repository.IRepository;
using WebStore.Models;

namespace WebStore.DataAccess.Repository
{

    public class ProductRepository:IProductRepository
    {
        private IDbConnection db;

        // how to update nuget auto
        public ProductRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        
        public Product Insert(Product product)
        {
            var sql = "insert into [dbo].[Product] (ProductName,Author,ISBN,Price,AddedDate,IsActive) values(@ProductName,@Author,@ISBN,@Price,getdate(),1);"
                + "select cast(scope_identity() as int);";
            var id = db.Query<int>(sql, new
            {
                product.ProductName,
                product.Author,
                product.ISBN,
                product.Price,
                product.AddedDate
            }).Single();
            product.ProductId = id;
            return product;
        }
        public async Task<Product> Update(Product product)
        {
            var sql = "update Product set ProductName=@ProductName,Author=@Author,ISBN=@ISBN,Price=@Price,AddedDate=getdate() where ProductId=@ProductId";
            await db.ExecuteAsync(sql,product);
            return product;
        }
        public async void Delete(int productId)
        {
            //var sql = "delete from Product where ProductId=@ProductId";
            var sql = "update Product set IsActive=0 where ProductId=@ProductId";
            await db.ExecuteAsync(sql, new { productId });
        }
        
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var sql = "select * from Product where IsActive=1";
            return await db.QueryAsync<Product>(sql);
        }
        public List<Product> GetAll()
        {
            var sql = "select * from Product where IsActive=1";
            return db.Query<Product>(sql).ToList();
        }
        
        public Product FindAsync(int productId)
        {
            var sql = "select * from Product where IsActive=1 and ProductId=@ProductId";
            return db.Query<Product>(sql,new { @ProductId= productId }).Single();
        }

        public void DeleteById(int productId)
        {
            //var sql = "delete from Product where ProductId=@ProductId";
            var sql = "update Product set IsActive=0 where ProductId=@ProductId";
            db.ExecuteAsync(sql, new { productId });
        }

    }
}
