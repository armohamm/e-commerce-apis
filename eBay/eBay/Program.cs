using eBay.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBay
{
    class Program
    {
        static void Main(string[] args)
        {

            EbayOperationsService eBay = new EbayOperationsService();

            Console.Write("Executing eBay Get Orders " + DateTime.Now + " ...");
            Console.WriteLine("");
            Console.WriteLine(eBay.GetOrders());

        }
    }
}
