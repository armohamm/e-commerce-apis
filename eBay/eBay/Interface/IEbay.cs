using eBay.Service.Core.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBay.Interface
{
    public interface IEbay
    {
        eBay.Service.Core.Soap.CustomSecurityHeaderType GeteBayCredentials();

        ApiContext GetContext();
    }
}
