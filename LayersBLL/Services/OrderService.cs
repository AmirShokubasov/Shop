using LayersDLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LayersDLL.DTO;
using LayersDAL.Interfaces;
using LayersDAL.Repositories;
using LayersDLL.BLL;
using LayersDAL.Entities;
using AutoMapper;
using LayersDLL.Infrastructure;

namespace LayersDLL.Services
{
    public class OrderService : IOrderService
    {
        IUnitOfWork database { get; set; }
        public OrderService(IUnitOfWork uow)
        {
            database = uow;
        }

        

        public void AddUser(UserDTO userDto)
        {
            User user = new User
            {
                Login = userDto.Login,
                Password = userDto.Password,
                Address = userDto.Address,
                Email = userDto.Email,
                Name = userDto.Name,
                RoleId = 2
            };
            database.Users.Create(user);
            database.Save();
        }

        public IEnumerable<GoodDTO> GetGoods()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Good, GoodDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Good>, List<GoodDTO>>(database.Goods.GetAll());
        }

        public IEnumerable<OrderDTO> GetOrders()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Order>, List<OrderDTO>>(database.Orders.GetAll());
        }
        public IEnumerable<UserDTO> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(database.Users.GetAll());
        }

        public void MakeOrder(OrderDTO orderDto)
        {
            User user = database.Users.Get(orderDto.UserId);
            if (user == null)
                throw new ValidationException("Пользователь не найден","");
            decimal sum = new Discount(0.1m).GetDiscount(orderDto.Price);
            Order order = new Order
            {
                Date = DateTime.Now,
                UserId = user.Id,
                Sum = sum,
                Name = orderDto.Name,
                Price = orderDto.Price
            };
            database.Orders.Create(order);
            database.Save();

        }

        public UserDTO GetUser(int?id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id покупателя","");
            var user = database.Users.Get(id.Value);
            if (user == null)
                throw new ValidationException("Покупатель","");
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                RoleId = user.RoleId,
                Address = user.Address,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password
            };
        }

        public void DeleteGood(GoodDTO goodDto)
        {


            database.Goods.Delete(goodDto.Id);
            database.Save();

        }

        public void AddGood(GoodDTO goodDto)
        {
            Good good = new Good
            {
                Id = goodDto.Id,
                Name = goodDto.Name,
                Company = goodDto.Company,
                Price = goodDto.Price
            };

            database.Goods.Create(good);
            database.Save();

        }
        public void UpdateGood(GoodDTO goodDto)
        {
            Good good = new Good
            {
                Id = goodDto.Id,
                Name = goodDto.Name,
                Company = goodDto.Company,
                Price = goodDto.Price
            };
            database.Goods.Update(good);
            database.Save();
        }
        public IEnumerable<GoodDTO> Search(string search)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Good, GoodDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Good>, List<GoodDTO>>(database.Goods.Search(search));
        }

        public void Dispose()
        {
            database.Dispose();
        }
    }
}
