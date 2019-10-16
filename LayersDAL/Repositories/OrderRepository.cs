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
    public class OrderRepository : IRepository<Order>
    {
        private StoreContext db;
        public OrderRepository(StoreContext context)
        {
            this.db = context;
        }
        public void Create(Order item)
        {
            db.Orders.Add(item);
        }

        public void Delete(int id)
        {
            Order item = db.Orders.Find(id);
            if (item != null)
                db.Orders.Remove(item);
        }

        public IEnumerable<Order> Find(Func<Order, bool> predicate)
        {
            return db.Orders.Where(predicate).ToList();
        }

        public Order Get(int id)
        {
            return db.Orders.Find(id);
        }

        public IEnumerable<Order> GetAll()
        {
            return db.Orders;
        }

        public void Update(Order item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public IEnumerable<Order> Search(string search)
        {
            return db.Orders.Where(x => x.Name.StartsWith(search) || search == null).ToList();
        }
    }
}
