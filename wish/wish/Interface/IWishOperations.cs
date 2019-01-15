using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wish.Interface
{
    interface IWishOperations
    {
        // will return a JSON with all the orders for that seller
        string GetOrders();
    }
}
