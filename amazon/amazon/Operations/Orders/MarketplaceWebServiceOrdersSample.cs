/*******************************************************************************
 * Copyright 2009-2018 Amazon Services. All Rights Reserved.
 * Licensed under the Apache License, Version 2.0 (the "License"); 
 *
 * You may not use this file except in compliance with the License. 
 * You may obtain a copy of the License at: http://aws.amazon.com/apache2.0
 * This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
 * CONDITIONS OF ANY KIND, either express or implied. See the License for the 
 * specific language governing permissions and limitations under the License.
 *******************************************************************************
 * Marketplace Web Service Orders
 * API Version: 2013-09-01
 * Library Version: 2018-08-01
 * Generated: Wed Aug 29 10:45:09 PDT 2018
 */

using MarketplaceWebServiceOrders.Model;
using System;
using System.Collections.Generic;

namespace amazon.Operations.Orders
{

    /// <summary>
    /// Runnable sample code to demonstrate usage of the C# client.
    ///
    /// To use, import the client source as a console application,
    /// and mark this class as the startup object. Then, replace
    /// parameters below with sensible values and run.
    /// </summary>
    public class MarketplaceWebServiceOrdersSample {

        public static void GetOrders()
        {
            // TODO: Set the below configuration variables before attempting to run

            // Developer AWS access key
            string accessKey = "AKIAJHVFTQFZLBA42KEA";

            // Developer AWS secret key
            string secretKey = "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";

            // The client application name
            string appName = "CSharpSampleCode";

            // The client application version
            string appVersion = "1.0";

            // The endpoint for region service and version (see developer guide)
            // ex: https://mws.amazonservices.com
            string serviceURL = "https://mws.amazonservices.com";

            // Create a configuration object
            MarketplaceWebServiceOrdersConfig config = new MarketplaceWebServiceOrdersConfig();
            config.ServiceURL = serviceURL;
            // Set other client connection configurations here if needed
            // Create the client itself
            Operations.Orders.MarketplaceWebServiceOrders client = new MarketplaceWebServiceOrdersClient(accessKey, secretKey, appName, appVersion, config);

            MarketplaceWebServiceOrdersSample sample = new MarketplaceWebServiceOrdersSample(client);

            // Uncomment the operation you'd like to test here
            // TODO: Modify the request created in the Invoke method to be valid

            try
            {
                IMWSResponse response = null;
                // response = sample.InvokeGetOrder();
                // response = sample.InvokeGetServiceStatus();
                // response = sample.InvokeListOrderItems();
                // response = sample.InvokeListOrderItemsByNextToken();
                response = sample.InvokeListOrders();
                // response = sample.InvokeListOrdersByNextToken();
                Console.WriteLine("Response:");
                ResponseHeaderMetadata rhmd = response.ResponseHeaderMetadata;
                // We recommend logging the request id and timestamp of every call.
                Console.WriteLine("RequestId: " + rhmd.RequestId);
                Console.WriteLine("Timestamp: " + rhmd.Timestamp);
                string responseXml = response.ToXML();
                Console.WriteLine(responseXml);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        private readonly Operations.Orders.MarketplaceWebServiceOrders client;

        public MarketplaceWebServiceOrdersSample(Operations.Orders.MarketplaceWebServiceOrders client)
        {
            this.client = client;
        }

        public GetOrderResponse InvokeGetOrder()
        {
            // Create a request.
            GetOrderRequest request = new GetOrderRequest();
            string sellerId = "AHBAQH5P0EYIZ";
            request.SellerId = sellerId;
            string mwsAuthToken = "amzn.mws.8aa598d3-a72b-8732-61ef-0be3b97cf2ed";
            request.MWSAuthToken = mwsAuthToken;
            List<string> amazonOrderId = new List<string>() { "114-764691-5105047" };
            request.AmazonOrderId = amazonOrderId;
            return this.client.GetOrder(request);
        }

        public GetServiceStatusResponse InvokeGetServiceStatus()
        {
            // Create a request.
            GetServiceStatusRequest request = new GetServiceStatusRequest();
            string sellerId = "example";
            request.SellerId = sellerId;
            string mwsAuthToken = "example";
            request.MWSAuthToken = mwsAuthToken;
            return this.client.GetServiceStatus(request);
        }

        public ListOrderItemsResponse InvokeListOrderItems()
        {
            // Create a request.
            ListOrderItemsRequest request = new ListOrderItemsRequest();
            string sellerId = "example";
            request.SellerId = sellerId;
            string mwsAuthToken = "example";
            request.MWSAuthToken = mwsAuthToken;
            string amazonOrderId = "example";
            request.AmazonOrderId = amazonOrderId;
            return this.client.ListOrderItems(request);
        }

        public ListOrderItemsByNextTokenResponse InvokeListOrderItemsByNextToken()
        {
            // Create a request.
            ListOrderItemsByNextTokenRequest request = new ListOrderItemsByNextTokenRequest();
            string sellerId = "example";
            request.SellerId = sellerId;
            string mwsAuthToken = "example";
            request.MWSAuthToken = mwsAuthToken;
            string nextToken = "example";
            request.NextToken = nextToken;
            return this.client.ListOrderItemsByNextToken(request);
        }

        public ListOrdersResponse InvokeListOrders()
        {
            // Create a request.
            ListOrdersRequest request = new ListOrdersRequest();
            request.SellerId = "AHBAQH5P0EYIZ";
            request.MWSAuthToken = "amzn.mws.8aa598d3-a72b-8732-61ef-0be3b97cf2ed";
            DateTime createdAfter = DateTime.Now.AddDays(-7).AddMinutes(-5);
            request.CreatedAfter = createdAfter;
            DateTime createdBefore = DateTime.Now.AddMinutes(-3);
            request.CreatedBefore = createdBefore;
            List<string> orderStatus = new List<string>();
            orderStatus.Add("Unshipped");
            orderStatus.Add("PartiallyShipped");
            request.OrderStatus = orderStatus;
            request.MarketplaceId.Add("ATVPDKIKX0DER");
            List <string> fulfillmentChannel = new List<string>();
            request.FulfillmentChannel.Add("MFN");
            decimal maxResultsPerPage = 100;
            request.MaxResultsPerPage = maxResultsPerPage;
            List<string> tfmShipmentStatus = new List<string>();
            request.TFMShipmentStatus = tfmShipmentStatus;

            return this.client.ListOrders(request);
        }

        public ListOrdersByNextTokenResponse InvokeListOrdersByNextToken()
        {
            // Create a request.
            ListOrdersByNextTokenRequest request = new ListOrdersByNextTokenRequest();
            string sellerId = "example";
            request.SellerId = sellerId;
            string mwsAuthToken = "example";
            request.MWSAuthToken = mwsAuthToken;
            string nextToken = "example";
            request.NextToken = nextToken;
            return this.client.ListOrdersByNextToken(request);
        }
        
    }
}
