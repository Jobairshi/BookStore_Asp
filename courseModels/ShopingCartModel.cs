using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseModels
{
    public class ShopingCartModel
    {
        public IEnumerable<ShopingCart>ListCart { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
