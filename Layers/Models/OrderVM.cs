using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Layers.Models
{
    public class OrderVM
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime? Date { get; set; }
        public int UserId { get; set; }
    }
}
