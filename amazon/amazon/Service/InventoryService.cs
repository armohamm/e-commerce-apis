using amazon.Interface;
using amazon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace amazon.Service
{
    class InventoryService : InventoryInterface
    {
        private const string MERCHANT_LISTINGS = "_GET_MERCHANT_LISTINGS_ALL_DATA_";
        private string PATH_LOCATION = @"YOUR_PATH_LOCATION";

        internal ReportInterface report = new ReportService();

        Model.AmazonAPI api = new Model.AmazonAPI();

        public string GetMerchantListingsReport()
        {
            try
            {
                Console.WriteLine("Starting to Update Amazon Inventory at {0}", DateTime.Now);

                var CurrentHour = DateTime.Now.Hour;
                int StartMinute = 0;
                int FinishMinute = 59;

                var AvailableFromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, CurrentHour, StartMinute, 0);
                var AvailableToDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, CurrentHour, FinishMinute, 0);

                // Requesting the Report
                Console.WriteLine("Requesting Report to Amazon");
                string ReportRequestId = report.RequestReport(MERCHANT_LISTINGS, AvailableFromDate, AvailableToDate);
                Console.WriteLine("Finishing to request the Report {0}...", MERCHANT_LISTINGS);
                Console.WriteLine(" ");

                // Has to wait for 4 minutes to wait for the report
                Thread.Sleep(120000);

                var StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, StartMinute, 0);
                var EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, FinishMinute, 0);

                Console.WriteLine("Getting the Requested Report from Amazon");
                string ReportId = report.GetReportLatestReport(StartDate, EndDate, MERCHANT_LISTINGS);

                Console.WriteLine("Getting Report for {0}", ReportId);
                Thread.Sleep(180000);
                string amazonFilePath = report.GetReport(ReportId, PATH_LOCATION);
                Console.WriteLine("Finishing to get the Report...");
                Console.WriteLine(" ");

                // Checking the Amazon Inventory against Fishbowl Inventory
                Console.WriteLine(string.Format("Reading {0}...", MERCHANT_LISTINGS));
                return report.ReadReport(amazonFilePath);
            }
            catch (Exception ex)
            {
                string error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                Console.WriteLine(error);
                Console.ReadLine();
                return error;
            }
        }
    }
}
