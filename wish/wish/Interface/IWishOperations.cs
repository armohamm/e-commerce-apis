using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wish.Interface
{
    interface IWishOperations
    {
        // will return a JSON with all the orders/products for that seller
        string ExecuteAction(string Method);
    }
}
