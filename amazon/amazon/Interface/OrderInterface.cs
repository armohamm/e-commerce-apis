using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amazon.Interface
{
    interface OrderInterface
    {
        string InvokeGetOrder(List<string> amazonOrderIdList);

        string InvokeGetServiceStatus();

        string InvokeListOrderItems(string OrderId);

        string InvokeListOrderItemsByNextToken(string nextToken);

        string InvokeListOrders(DateTime createdAfter, DateTime createdBefore);

        string InvokeListOrdersByNextToken(string nextToken);
    }
}
