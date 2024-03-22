using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IProdRepository ProductData { get; }
        void Save();
        Task SaveAsync();
    }
}
