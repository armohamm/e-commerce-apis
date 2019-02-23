using amazon.Interface;
using amazon.Model;
using MarketplaceWebServiceOrders.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amazon.Service
{
    public class OrderService : OrderInterface
    {
        Model.AmazonAPI api = new Model.AmazonAPI();
        IMWSResponse response = null;
        ResponseHeaderMetadata rhmd = null;

        private readonly amazon.Operations.Orders.MarketplaceWebServiceOrders client;

        public OrderService(amazon.Operations.Orders.MarketplaceWebServiceOrders client)
        {
            this.client = client;
        }


        public string InvokeGetOrder(List<string> amazonOrderIdList)
        {
            try
            {
                Console.WriteLine("Starting {0} at {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, DateTime.Now);
                // Create a request.
                GetOrderRequest request = new GetOrderRequest();
                request.SellerId = api.SellerId;
                request.MWSAuthToken = api.MWSAuthToken;
                request.AmazonOrderId = amazonOrderIdList;
                response = client.GetOrder(request);
                Console.WriteLine("Response:");
                rhmd = response.ResponseHeaderMetadata;
                // Is recommended logging the request id and timestamp of every call.
                Console.WriteLine("RequestId: " + rhmd.RequestId);
                Console.WriteLine("Timestamp: " + rhmd.Timestamp);
                string responseXml = response.ToXML();
                Console.WriteLine(responseXml);
                return responseXml;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public string InvokeGetServiceStatus()
        {
            try
            {
                Console.WriteLine("Starting {0} at {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, DateTime.Now);
                // Create a request.
                GetServiceStatusRequest request = new GetServiceStatusRequest();
                request.SellerId = api.SellerId;
                request.MWSAuthToken = api.MWSAuthToken;
                response = client.GetServiceStatus(request);
                Console.WriteLine("Response:");
                rhmd = response.ResponseHeaderMetadata;
                // Is recommended logging the request id and timestamp of every call.
                Console.WriteLine("RequestId: " + rhmd.RequestId);
                Console.WriteLine("Timestamp: " + rhmd.Timestamp);
                string responseXml = response.ToXML();
                Console.WriteLine(responseXml);
                return responseXml;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public string InvokeListOrderItems(string OrderId)
        {
            try
            {
                Console.WriteLine("Starting {0} at {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, DateTime.Now);
                // Create a request.
                ListOrderItemsRequest request = new ListOrderItemsRequest();
                request.SellerId = api.SellerId;
                request.MWSAuthToken = api.MWSAuthToken;
                request.AmazonOrderId = OrderId;
                response = client.ListOrderItems(request);
                Console.WriteLine("Response:");
                rhmd = response.ResponseHeaderMetadata;
                // Is recommended logging the request id and timestamp of every call.
                Console.WriteLine("RequestId: " + rhmd.RequestId);
                Console.WriteLine("Timestamp: " + rhmd.Timestamp);
                string responseXml = response.ToXML();
                Console.WriteLine(responseXml);
                return responseXml;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public string InvokeListOrderItemsByNextToken(string nextToken)
        {
            try
            {
                Console.WriteLine("Starting {0} at {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, DateTime.Now);
                // Create a request.
                ListOrderItemsByNextTokenRequest request = new ListOrderItemsByNextTokenRequest();
                request.SellerId = api.SellerId;
                request.MWSAuthToken = api.MWSAuthToken;
                request.NextToken = nextToken;
                response = client.ListOrderItemsByNextToken(request);
                Console.WriteLine("Response:");
                rhmd = response.ResponseHeaderMetadata;
                // Is recommended logging the request id and timestamp of every call.
                Console.WriteLine("RequestId: " + rhmd.RequestId);
                Console.WriteLine("Timestamp: " + rhmd.Timestamp);
                string responseXml = response.ToXML();
                Console.WriteLine(responseXml);
                return responseXml;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public string InvokeListOrders(DateTime createdAfter, DateTime createdBefore)
        {
            try
            {
                Console.WriteLine("Starting {0} at {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, DateTime.Now);
                // Create a request.
                ListOrdersRequest request = new ListOrdersRequest();
                request.SellerId = api.SellerId;
                request.MWSAuthToken = api.MWSAuthToken;
                request.CreatedAfter = createdAfter;
                request.CreatedBefore = createdBefore;
                request.OrderStatus = new List<string>() { "Unshipped", "PartiallyShipped" };
                request.MarketplaceId.Add(api.MarketplaceId);
                request.FulfillmentChannel = new List<string>() { "MFN" };
                request.MaxResultsPerPage = 100;
                request.TFMShipmentStatus = new List<string>();
                response = client.ListOrders(request);
                Console.WriteLine("Response:");
                rhmd = response.ResponseHeaderMetadata;
                // Is recommended logging the request id and timestamp of every call.
                Console.WriteLine("RequestId: " + rhmd.RequestId);
                Console.WriteLine("Timestamp: " + rhmd.Timestamp);
                string responseXml = response.ToXML();
                Console.WriteLine(responseXml);
                return responseXml;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public string InvokeListOrdersByNextToken(string nextToken)
        {
            try
            {
                Console.WriteLine("Starting {0} at {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, DateTime.Now);
                // Create a request.
                ListOrdersByNextTokenRequest request = new ListOrdersByNextTokenRequest();
                request.SellerId = api.SellerId;
                request.MWSAuthToken = api.MWSAuthToken;
                request.NextToken = nextToken;
                response = client.ListOrdersByNextToken(request);
                Console.WriteLine("Response:");
                rhmd = response.ResponseHeaderMetadata;
                // Is recommended logging the request id and timestamp of every call.
                Console.WriteLine("RequestId: " + rhmd.RequestId);
                Console.WriteLine("Timestamp: " + rhmd.Timestamp);
                string responseXml = response.ToXML();
                Console.WriteLine(responseXml);
                return responseXml;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
