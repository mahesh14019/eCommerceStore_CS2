using WebStore.DataAccess.Data;
using WebStore.DataAccess.Repository;
using WebStore.DataAccess.Repository.IRepository;

namespace QrGeneratorDataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ProductData = new ProdRepository(_db);
        }

        public IProdRepository ProductData { get; private set; }
        public void Save()
        {
            _db.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
