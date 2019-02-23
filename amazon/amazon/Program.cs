using amazon.Interface;
using amazon.Model;
using amazon.Operations.Orders;
using amazon.Service;
using MarketplaceWebServiceOrders;
using MarketplaceWebServiceOrders.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amazon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AmazonOrders ord = new AmazonOrders();
            //ord.operations.InvokeGetOrder(new List<string>() { "order-id-1" });
            //ord.operations.InvokeGetServiceStatus();
            //ord.operations.InvokeListOrderItems("order-id");
            //ord.operations.InvokeListOrderItemsByNextToken("next-token-id");
            //ord.operations.InvokeListOrders(DateTime.Now.AddDays(-7), DateTime.Now);
            //ord.operations.InvokeListOrdersByNextToken("next-token-id");

            InventoryInterface inv = new InventoryService();
            inv.GetMerchantListingsReport();
        }        
    }
}
