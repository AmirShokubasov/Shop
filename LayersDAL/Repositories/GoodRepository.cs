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
    public class GoodRepository : IRepository<Good>
    {
        private StoreContext db;
        public GoodRepository(StoreContext context)
        {
            this.db = context;
        }
        public void Create(Good item)
        {
            db.Goods.Add(item);
        }

        public void Delete(int id)
        {
            Good item = db.Goods.Find(id);
            if (item != null)
                db.Goods.Remove(item);
        }

        public IEnumerable<Good> Find(Func<Good, bool> predicate)
        {
            return db.Goods.Where(predicate).ToList();
        }

        public Good Get(int id)
        {
            return db.Goods.Find(id);
        }

        public IEnumerable<Good> GetAll()
        {
            return db.Goods;
        }

        public void Update(Good item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public IEnumerable<Good> Search(string search)
        {
            return db.Goods.Where(x=>x.Name.StartsWith(search) || search == null).ToList();
            
        }

      



    }
}
