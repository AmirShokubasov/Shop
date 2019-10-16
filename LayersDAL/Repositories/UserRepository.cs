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
    public class UserRepository : IRepository<User>
    {
        private StoreContext db;
        public UserRepository(StoreContext context)
        {
            this.db = context;
        }
        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Delete(int id)
        {
            User item = db.Users.Find(id);
            if (item != null)
                db.Users.Remove(item);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<User> Search(string search)
        {
            return db.Users.Where(x => x.Name.StartsWith(search) || search == null).ToList();
        }
    }
}
