using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amazon.Model
{
    class AmazonWebService
    {
        internal string APP_NAME { get; set; }
        internal string APP_VERSION { get; set; }
        internal string SERVICE_URL { get; set; }
        internal string USER_AGENT { get; set; }

        public AmazonWebService()
        {
            this.APP_NAME = ConfigurationManager.AppSettings["APP_NAME"];
            this.APP_VERSION = ConfigurationManager.AppSettings["APP_VERSION"];
            this.SERVICE_URL = ConfigurationManager.AppSettings["SERVICE_URL"];
            this.USER_AGENT = ConfigurationManager.AppSettings["USER_AGENT"];
        }
    }
}
