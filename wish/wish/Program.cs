using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wish.Interface;
using wish.Services;

namespace wish
{
    class Program
    {
        static void Main(string[] args)
        {
            IWishOperations wish = new WishOperationsService();

            Console.Write("Executing wish Get Orders " + DateTime.Now + " ...");
            Console.WriteLine("");
            Console.WriteLine(wish.GetOrders());
            Console.WriteLine("");
        }
    }
}
