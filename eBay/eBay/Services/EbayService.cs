using eBay.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using System.Configuration;

namespace eBay.Services
{
    public class EbayService : IEbay
    {
        private string APP_ID = ConfigurationManager.AppSettings["AppID"];
        private string DEV_ID = ConfigurationManager.AppSettings["DevID"];
        private string CERT_ID = ConfigurationManager.AppSettings["CertID"];
        private string AUTH_TOKEN = ConfigurationManager.AppSettings["EbayAuthToken"];
        private string END_POINT = ConfigurationManager.AppSettings["EndPoint"];
        private string VERSION = ConfigurationManager.AppSettings["Version"];

        public ApiContext GetContext()
        {
            ApiContext context = new ApiContext();

            try
            {
                // Credentials for the call
                context.ApiCredential.ApiAccount.Developer = DEV_ID;
                context.ApiCredential.ApiAccount.Application = APP_ID;
                context.ApiCredential.ApiAccount.Certificate = CERT_ID;
                context.ApiCredential.eBayToken = AUTH_TOKEN;
                // Set the URL
                context.SoapApiServerUrl = END_POINT;
                // Set the version
                context.Version = VERSION;
                // Set logging
                context.ApiLogManager = new ApiLogManager();
                context.ApiLogManager.ApiLoggerList.Add(new eBay.Service.Util.FileLogger("Messages.lo", true, true, true));
                context.ApiLogManager.EnableLogging = true;

                context.Site = eBay.Service.Core.Soap.SiteCodeType.US;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return context;
        }

        public CustomSecurityHeaderType GeteBayCredentials()
        {
            eBay.Service.Core.Soap.CustomSecurityHeaderType requesterCredentials = new eBay.Service.Core.Soap.CustomSecurityHeaderType();
            try
            {
                requesterCredentials.eBayAuthToken = AUTH_TOKEN;
                requesterCredentials.Credentials = new eBay.Service.Core.Soap.UserIdPasswordType();
                requesterCredentials.Credentials.AppId = APP_ID;
                requesterCredentials.Credentials.DevId = DEV_ID;
                requesterCredentials.Credentials.AuthCert = CERT_ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return requesterCredentials;
        }
    }
}
