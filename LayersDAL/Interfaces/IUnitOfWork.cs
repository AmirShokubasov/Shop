using LayersDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayersDAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<Role> Roles { get; }
        IRepository<User> Users { get; }
        IRepository<Good> Goods { get; }
        IRepository<Order> Orders { get; }
        void Save();
    }
}
