using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayersDLL.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime? Date { get; set; }
        public int UserId { get; set; }
    }
}
