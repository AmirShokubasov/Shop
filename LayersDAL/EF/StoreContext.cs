using LayersDAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayersDAL.EF
{
   public class StoreContext:DbContext
    {
        public DbSet<Good> Goods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        static StoreContext()
        {
            Database.SetInitializer<StoreContext>(new StoreDbInitializer());
        }

        public StoreContext(string connectionString) : base(connectionString)
        { }
    }
    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<StoreContext>
        {
        protected override void Seed(StoreContext db)
        {
            db.Goods.Add(new Good { Name="Samsung Galaxy S2",Company="Samsung", Price=30000});
            db.Goods.Add(new Good { Name = "Iphone 5s", Company = "Apple", Price = 50000});
            db.Goods.Add(new Good { Name = "Samsung Galaxy S3", Company = "Samsung", Price = 50000});
            db.Goods.Add(new Good { Name = "Iphone 6s", Company = "Apple", Price = 170000});
            db.Goods.Add(new Good { Name = "Samsung Galaxy S4", Company = "Samsung", Price = 70000});
            db.Goods.Add(new Good { Name = "Iphone 7", Company = "Apple", Price = 7200 });
            db.Goods.Add(new Good { Name = "Samsugn Galaxy S5", Company = "Samsung", Price = 90000});
            db.Goods.Add(new Good { Name = "Iphone 7 Plus", Company = "Apple", Price = 250000});
            db.Goods.Add(new Good { Name = "Samsung Galaxy S6", Company = "Samsung", Price = 150000});
            db.Goods.Add(new Good { Name = "Iphone 8", Company = "Apple", Price = 260000});
            db.Roles.Add(new Role { Name = "admin", Id = 1 });
            db.Roles.Add(new Role { Name = "user", Id = 2 });
            db.Users.Add(new User { Name = "admin", Login="admin", Password="1", RoleId=1,Id=1 });
            db.Users.Add(new User { Name = "USER1", Login = "USER1", Password = "1", RoleId = 2, Id = 2 });
            db.Orders.Add(new Order { Id=1,Name ="Iphone X",Sum=1,Price=350000,Date=DateTime.Now,UserId=2});
            db.SaveChanges();
        }
    }
    
}
