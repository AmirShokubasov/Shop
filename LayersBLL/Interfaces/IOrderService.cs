using LayersDLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayersDLL.Interfaces
{
    public interface IOrderService
    {
        void MakeOrder(OrderDTO orderDto);
        void DeleteGood(GoodDTO goodDto);

        void UpdateGood(GoodDTO goodDto);
        void AddGood(GoodDTO goodDto);
        IEnumerable<UserDTO> GetUsers();
        IEnumerable<OrderDTO> GetOrders();
        IEnumerable<GoodDTO> GetGoods();
        void AddUser(UserDTO userDto);
        UserDTO GetUser(int? id);
        void Dispose();
        IEnumerable<GoodDTO> Search(string search);

    }
}
