using eBay.Interface;
using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using eBay.Service.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBay.Services
{
    public class EbayOperationsService : IEbayOperations
    {
        IEbay eBayCall = new EbayService();

        public string GetOrders()
        {
            try
            {
                ApiContext context = eBayCall.GetContext();

                GetOrdersCall getOrders = new GetOrdersCall(context);

                PopulateGetOrders(getOrders);

                getOrders.Execute();

                if (getOrders.ApiResponse.Ack != AckCodeType.Failure)
                {
                    // Check if any orders are returned
                    if (getOrders.ApiResponse.OrderArray.Count != 0)
                    {
                        // Convert response to Json
                        return JsonConvert.SerializeObject(getOrders.ApiResponse.OrderArray);

                    }
                    else
                    {
                        return "No orders returned from the API.";
                    }
                }
                else
                {
                    return "AckCodeType Failed";
                }

            }
            catch (Exception ex)
            {
                return "There was an exception. " + ex.Message;
            }
        }

        public static void PopulateGetOrders(GetOrdersCall getOrders)
        {
            DateTime CreateTimeFromPrev, CreateTimeFrom, CreateTimeTo;

            getOrders.DetailLevelList = new DetailLevelCodeTypeCollection();
            getOrders.DetailLevelList.Add(DetailLevelCodeType.ReturnAll);

            // CreateTimeTo set to the current time
            CreateTimeTo = DateTime.Now.ToUniversalTime();
            // Assumption call is made every 15 sec. So CreateTimeFrom of last call was 15 mins
            // prior to now
            TimeSpan ts1 = new TimeSpan(9000000000);
            CreateTimeFromPrev = CreateTimeTo.Subtract(ts1);

            // Set the CreateTimeFrom the last time you made the call minus 2 minutes
            TimeSpan ts2 = new TimeSpan(1200000000);
            CreateTimeFrom = CreateTimeFromPrev.Subtract(ts2);
            getOrders.CreateTimeFrom = CreateTimeFrom.AddDays(-7);
            getOrders.CreateTimeTo = CreateTimeTo;
        }

        public string GetInventory()
        {
            ApiContext context = eBayCall.GetContext();

            try
            {

                GetSellerListCall oGetSellerListCall = new GetSellerListCall(context);

                // use GranularityLevel of Fine
                oGetSellerListCall.GranularityLevel = GranularityLevelCodeType.Fine;

                // get the first page, 200 items per page
                PaginationType oPagination = new PaginationType();
                oPagination.EntriesPerPage = 200;
                oPagination.EntriesPerPageSpecified = true;
                oPagination.PageNumber = 1;
                oPagination.PageNumberSpecified = true;
                oGetSellerListCall.Pagination = oPagination;

                // ask for all items that are ending in the future (active items)
                oGetSellerListCall.EndTimeFilter = new TimeFilter(DateTime.Now, DateTime.Now.AddMonths(1));

                // return items that end soonest first
                oGetSellerListCall.Sort = 2;

                ItemTypeCollection oItems = oGetSellerListCall.GetSellerList();

                if (oItems != null)
                {
                    if (oItems.Count > 0)
                    {
                        return JsonConvert.SerializeObject(oItems, Formatting.Indented);
                    }
                    else
                    {
                        return "No active inventory.";
                    }
                }
                else
                {
                    return "Inventory is null!";
                }

            }
            catch (ApiException oApiEx)
            {
                return oApiEx.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
