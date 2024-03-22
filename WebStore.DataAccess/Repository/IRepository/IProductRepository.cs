using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.DataAccess.Repository.IRepository
{
    public interface IProductRepository
    {
        Product Insert(Product product);
        Task<Product> Update(Product product);
        void Delete(int productId);
        //Task<IEnumerable<Product>> FindAsync(int productId);
        public Product FindAsync(int productId);
        Task<IEnumerable<Product>> GetAllAsync();
        List<Product> GetAll();

        void DeleteById(int productId);
    }
}
