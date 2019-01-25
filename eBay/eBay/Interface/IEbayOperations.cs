using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBay.Interface
{
    interface IEbayOperations
    {
        // will return a JSON with all the orders for that seller
        string GetOrders();

        // will return a JSON with all the inventory for that seller
        string GetInventory();

        // will update the orders given the tracking number
        string FulfillOrder(string OrderLineItemID, string ItemId, string TransactionId, string Carrier, string Tracking);
    }
}
