using amazon.Model;
using amazon.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amazon.Operations.Orders
{
    public class AmazonOrders
    {
        Model.AmazonAPI api = new Model.AmazonAPI();
        private MarketplaceWebServiceOrdersConfig config = null;
        private MarketplaceWebServiceOrders client = null;
        public OrderService operations = null;

        public AmazonOrders()
        {
            config = new MarketplaceWebServiceOrdersConfig();
            config.ServiceURL = api.ServiceURL;
            client = new MarketplaceWebServiceOrdersClient(api.AccessKey, api.SecretKey, api.AppName, api.AppVersion, config);
            operations = new OrderService(client);
        }
    }
}
