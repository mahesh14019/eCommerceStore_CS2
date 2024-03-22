using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Models;


namespace WebStore.DataAccess.Repository.IRepository
{
        public interface IProdRepository : IRepository<Product>
        {
            void Update(Product obj);
        }
}
