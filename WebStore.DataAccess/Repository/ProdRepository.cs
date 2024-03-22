
using QrGeneratorDataAccess.Repository;
using WebStore.DataAccess.Data;
using WebStore.DataAccess.Repository.IRepository;
using WebStore.Models;

namespace WebStore.DataAccess.Repository
{
    public class ProdRepository : Repository<Product>, IProdRepository
    {
        private ApplicationDbContext _db;

        public ProdRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Product obj)
        {
            var objFromDb = _db.Product?.FirstOrDefault(u => u.ProductId == obj.ProductId);
            if (objFromDb != null)
            {
                objFromDb.ProductName = obj.ProductName;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Price = obj.Price;
                
            }
        }
    }
}
