using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amazon.Model
{
    public class AmazonAPI
    {
        // Developer AWS access key
        internal string AccessKey { get; set; }

        // Developer AWS secret key
        internal string SecretKey { get; set; }

        // The client application name
        internal string AppName { get; set; }

        // The client application version
        internal string AppVersion { get; set; }

        // The endpoint for region service and version (see developer guide)
        // ex: https://mws.amazonservices.com
        internal string ServiceURL { get; set; }

        internal string SellerId { get; set; }

        internal string MWSAuthToken { get; set; }

        internal string MarketplaceId { get; set; }

        public AmazonAPI()
        {
            this.AccessKey = ConfigurationManager.AppSettings["AccessKey"];
            this.SecretKey = ConfigurationManager.AppSettings["SecretKey"];
            this.AppName = ConfigurationManager.AppSettings["AppName"];
            this.AppVersion = ConfigurationManager.AppSettings["AppVersion"];
            this.ServiceURL = ConfigurationManager.AppSettings["ServiceURL"];
            this.SellerId = ConfigurationManager.AppSettings["SellerId"];
            this.MWSAuthToken = ConfigurationManager.AppSettings["MWSAuthToken"];
            this.MarketplaceId = ConfigurationManager.AppSettings["MarketplaceId"];
        }
    }
}
