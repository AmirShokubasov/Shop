using LayersDAL.EF;
using LayersDAL.Entities;
using LayersDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayersDAL.Repositories
{
    public class RoleRepository : IRepository<Role>
    {
        private StoreContext db;
        public RoleRepository(StoreContext context)
        {
            this.db = context;
        }
        public void Create(Role item)
        {
            db.Roles.Add(item);
        }

        public void Delete(int id)
        {
           Role item = db.Roles.Find(id);
            if (item != null)
                db.Roles.Remove(item);
        }

        public IEnumerable<Role> Find(Func<Role, bool> predicate)
        {
            return db.Roles.Where(predicate).ToList();
        }

        public Role Get(int id)
        {
            return db.Roles.Find(id);
        }

        public IEnumerable<Role> GetAll()
        {
            return db.Roles;
        }

        public void Update(Role item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public IEnumerable<Role> Search(string search)
        {
            return db.Roles.Where(x => x.Name.StartsWith(search) || search == null).ToList();
        }
    }
}
