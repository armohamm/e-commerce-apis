using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using wish.Interface;

namespace wish.Services
{
    class WishOperationsService : IWishOperations
    {
        private string ACCESS_TOKEN = ConfigurationSettings.AppSettings.Get("ACCESS_TOKEN");

        public string ExecuteAction(string Method)
        {
            string url = "";            

            switch (Method)
            {
                case "Order":
                    url = string.Format("https://merchant.wish.com/api/v2/order/get-fulfill?start=0&limit=100&access_token={0}", ACCESS_TOKEN);
                    break;
                case "Inventory":
                    url = string.Format("https://merchant.wish.com/api/v2/product/multi-get?limit=100&access_token={0}", ACCESS_TOKEN);
                    break;
                default:
                    throw new Exception("Method not implemented");
            }

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "GET";
            httpWebRequest.PreAuthenticate = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpWebResponse httpResponse;

            try
            {
                using (WebResponse response = httpWebRequest.GetResponse())
                {
                    httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
