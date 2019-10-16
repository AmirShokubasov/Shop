using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayersDLL.BLL
{
    public class Discount
    {
        public Discount(decimal val)
        {
            _value = val;
        }
        private decimal _value = 0;
        public decimal Value { get { return _value; } }
        public decimal GetDiscount(decimal price)
        {
            if (price > 20000)
                return price - price * _value;
            return price;
        }
    }
}
