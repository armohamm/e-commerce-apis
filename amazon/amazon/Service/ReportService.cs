using amazon.Interface;
using amazon.Model;
using amazon.Model.Amazon;
using MarketplaceWebService;
using MarketplaceWebService.Mock;
using MarketplaceWebService.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using amazon.Operations.Reports;
using Newtonsoft.Json;

namespace amazon.Service
{
    class ReportService : ReportInterface
    {
        Model.AmazonAPI api = new Model.AmazonAPI();
        AmazonWebService webService = new AmazonWebService();

        public amazon.Operations.Reports.MarketplaceWebService client = new MarketplaceWebServiceMock();

        public string GetReport(string ReportId, string Path)
        {
            string amazonFileLocation = "";

            GetReportRequest requestReport = new GetReportRequest();
#pragma warning disable CS0618 // Type or member is obsolete
            requestReport.Marketplace = api.MarketplaceId;
#pragma warning restore CS0618 // Type or member is obsolete
            requestReport.Merchant = api.Merchant;

            requestReport.ReportId = ReportId;

            string Extension = string.Format("{1}{0}", ".txt", DateTime.Now.ToString("hh.mm.ss.ffffff"));

            string FullPath = Path + Extension;

            if (!File.Exists(FullPath))
            {
                var stream = File.Create(FullPath);
                stream.Close();
            }

            // string fileLocation = FileAmazon.createFile(reportId, "txt", ReportModule, ""); -> METHOD TO CREATE THE PATH

            if (FullPath != "")
            {
                var stream = File.Open(FullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                requestReport.Report = stream;

                MarketplaceWebServiceConfig config = new MarketplaceWebServiceConfig();
                config.ServiceURL = webService.SERVICE_URL;
                config.WithUserAgent(webService.USER_AGENT);

                MarketplaceWebServiceClient client = new MarketplaceWebServiceClient(api.AccessKey, api.SecretKey, config);

                InvokeGetReport(client, requestReport);

                stream.Close();

                amazonFileLocation = FullPath;
            }

            return amazonFileLocation;
        }

        public string GetReportLatestReport(DateTime From, DateTime To, string ReportType)
        {
            List<string> ReportList = new List<string>();

            GetReportListRequest requestReport = new GetReportListRequest();
            requestReport.Acknowledged = false;
            requestReport.AvailableFromDate = From;
            requestReport.AvailableToDate = To;
#pragma warning disable CS0618 // Type or member is obsolete
            requestReport.Marketplace = api.MarketplaceId;
#pragma warning restore CS0618 // Type or member is obsolete
            requestReport.MaxCount = 99;
            requestReport.Merchant = api.Merchant;

            List<string> reportCode = new List<string>();
            reportCode.Add(ReportType);
            TypeList typeList = new TypeList();
            typeList.Type = reportCode;
            requestReport.ReportTypeList = typeList;

            MarketplaceWebServiceConfig config = new MarketplaceWebServiceConfig();
            config.ServiceURL = webService.SERVICE_URL;
            config.WithUserAgent(webService.USER_AGENT);

            MarketplaceWebServiceClient client = new MarketplaceWebServiceClient(api.AccessKey, api.SecretKey, config);

            GetReportListResult getReportListResult = null;

            getReportListResult = InvokeGetReportList(client, requestReport);
            if (getReportListResult != null)
            {
                if (getReportListResult.HasNext)
                {
                    while (getReportListResult.HasNext)
                    {
                        int i = 0;
                        if (i % 2 == 0)
                        {
                            Thread.Sleep(2000);
                        }
                        GetReportListByNextTokenRequest getReportListByNextTokenRequest = new GetReportListByNextTokenRequest();
#pragma warning disable CS0618 // Type or member is obsolete
                        getReportListByNextTokenRequest.Marketplace = api.MarketplaceId;
#pragma warning restore CS0618 // Type or member is obsolete
                        getReportListByNextTokenRequest.Merchant = api.Merchant;
                        getReportListByNextTokenRequest.NextToken = getReportListResult.NextToken;
                        string ReportId = InvokeGetReportListByNextToken(client, getReportListByNextTokenRequest);
                        ReportList.Add(ReportId);
                        i++;
                    }
                }
                List<ReportInfo> reportInfoList = getReportListResult.ReportInfo;
                if (reportInfoList.Count > 0)
                {
                    foreach (ReportInfo reportInfo in reportInfoList)
                    {
                        ReportList.Add(reportInfo.ReportId);
                    }
                }
                else
                {
                    Console.WriteLine("No reports returned from Amazon.");
                    throw new Exception("No reports returned from Amazon.");
                }
            }

            return ReportList[0];
        }

        public string ReadReport(string Path)
        {
            StreamReader sr = new StreamReader(Path);

            if (sr == null)
            {
                throw new Exception("The file path is invalid.");
            }

            try {

                List<AmazonInventory> listItems = new List<AmazonInventory>();

                AmazonInventory amazonItem = null;

                List<string> listLines = null;

                //Read the first line of text
                string line = sr.ReadLine();
                int i = 0;

                //Continue to read until you reach end of file
                while (line != null)
                {
                    if(listLines == null)
                    {
                        listLines = new List<string>();
                    }

                    listLines = line.Split('\t').ToList();

                    if (i > 0)
                    {
                        amazonItem = new AmazonInventory();
                        amazonItem.Item_Name = Convert.ToString(listLines[0]);
                        amazonItem.Item_Description = Convert.ToString(listLines[1]);
                        amazonItem.Listing_ID = Convert.ToString(listLines[2]);
                        amazonItem.Seller_SKU = Convert.ToString(listLines[3]);
                        amazonItem.Price = Convert.ToString(listLines[4]);
                        amazonItem.Quantity = Convert.ToString(listLines[5]);
                        amazonItem.Open_Date = Convert.ToString(listLines[6]);
                        amazonItem.Image_URL = Convert.ToString(listLines[7]);
                        amazonItem.Item_Is_Marketplace = Convert.ToString(listLines[8]);
                        amazonItem.Product_Id_Type = Convert.ToString(listLines[9]);
                        amazonItem.Zshop_Shipping_Fee = Convert.ToString(listLines[10]);
                        amazonItem.Item_Note = Convert.ToString(listLines[11]);
                        amazonItem.Item_Condition = Convert.ToString(listLines[12]);
                        amazonItem.Zshop_Category1 = Convert.ToString(listLines[13]);
                        amazonItem.Zshop_Browse_Path = Convert.ToString(listLines[14]);
                        amazonItem.Zshop_Storefront_Feature = Convert.ToString(listLines[15]);
                        amazonItem.asin1 = Convert.ToString(listLines[16]);
                        amazonItem.asin2 = Convert.ToString(listLines[17]);
                        amazonItem.asin3 = Convert.ToString(listLines[18]);
                        amazonItem.Will_Ship_Internationally = Convert.ToString(listLines[19]);
                        amazonItem.Expedited_Shipping = Convert.ToString(listLines[20]);
                        amazonItem.Zshop_Boldface = Convert.ToString(listLines[21]);
                        amazonItem.Product_ID = Convert.ToString(listLines[22]);
                        amazonItem.Bid_For_Featured_Placement = Convert.ToString(listLines[23]);
                        amazonItem.add_Delete = Convert.ToString(listLines[24]);
                        amazonItem.Pending_Quantity = Convert.ToString(listLines[25]);
                        amazonItem.Fulfillment_Channel = Convert.ToString(listLines[26]);
                        amazonItem.Merchant_Shipping_Group = Convert.ToString(listLines[27]);
                        amazonItem.Status = Convert.ToString(listLines[28]);
                        listItems.Add(amazonItem);
                        amazonItem = null;
                    }
                    i++;
                    line = sr.ReadLine();
                }

                // convert the list to json
                return JsonConvert.SerializeObject(listItems);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public string RequestReport(string Type, DateTime From, DateTime To)
        {
            try
            {
                RequestReportRequest requestReport = new RequestReportRequest();
#pragma warning disable CS0618 // Type or member is obsolete
                requestReport.Marketplace = api.MarketplaceId;
#pragma warning restore CS0618 // Type or member is obsolete
                requestReport.Merchant = api.Merchant;

                requestReport.StartDate = From;
                requestReport.EndDate = To;

                requestReport.ReportOptions = "true";

                IdList idList = new IdList();
                idList.Id = new List<string>() { api.MarketplaceId };
                requestReport.MarketplaceIdList = idList;

                requestReport.ReportType = Type;

                MarketplaceWebServiceConfig config = new MarketplaceWebServiceConfig();
                config.ServiceURL = webService.SERVICE_URL;
                config.WithUserAgent(webService.USER_AGENT);

                MarketplaceWebServiceClient client = new MarketplaceWebServiceClient(api.AccessKey, api.SecretKey, config);

                return InvokeRequestReport(client, requestReport);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void InvokeGetReport(Operations.Reports.MarketplaceWebService client, GetReportRequest requestReport)
        {
            try
            {
                GetReportResponse response = null;

                response = client.GetReport(requestReport);

                Console.WriteLine("Service Response");
                Console.WriteLine("=============================================================================");
                Console.WriteLine();

                Console.WriteLine("        GetReportResponse");
                if (response.IsSetGetReportResult())
                {
                    Console.WriteLine("            GetReportResult");
                    GetReportResult getReportResult = response.GetReportResult;
                    if (getReportResult.IsSetContentMD5())
                    {
                        Console.WriteLine("                ContentMD5");
                        Console.WriteLine("                    {0}", getReportResult.ContentMD5);
                    }
                }
                if (response.IsSetResponseMetadata())
                {
                    Console.WriteLine("            ResponseMetadata");
                    ResponseMetadata responseMetadata = response.ResponseMetadata;
                    if (responseMetadata.IsSetRequestId())
                    {
                        Console.WriteLine("                RequestId");
                        Console.WriteLine("                    {0}", responseMetadata.RequestId);
                    }
                }

                Console.WriteLine("            ResponseHeaderMetadata");
                Console.WriteLine("                RequestId");
                Console.WriteLine("                    " + response.ResponseHeaderMetadata.RequestId);
                Console.WriteLine("                ResponseContext");
                Console.WriteLine("                    " + response.ResponseHeaderMetadata.ResponseContext);
                Console.WriteLine("                Timestamp");
                Console.WriteLine("                    " + response.ResponseHeaderMetadata.Timestamp);

            }
            catch (MarketplaceWebServiceException ex)
            {
                Console.WriteLine("Caught Exception: " + ex.Message);
                Console.WriteLine("Response Status Code: " + ex.StatusCode);
                Console.WriteLine("Error Code: " + ex.ErrorCode);
                Console.WriteLine("Error Type: " + ex.ErrorType);
                Console.WriteLine("Request ID: " + ex.RequestId);
                Console.WriteLine("XML: " + ex.XML);
                Console.WriteLine("ResponseHeaderMetadata: " + ex.ResponseHeaderMetadata);
            }
        }

        public string InvokeRequestReport(Operations.Reports.MarketplaceWebService client, RequestReportRequest requestReport)
        {
            string ReportRequestId = "";
            try
            {
                RequestReportResponse response = client.RequestReport(requestReport);


                Console.WriteLine("Service Response");
                Console.WriteLine("=============================================================================");
                Console.WriteLine();

                Console.WriteLine("        RequestReportResponse");

                if (response.IsSetRequestReportResult())
                {
                    Console.WriteLine("            RequestReportResult");
                    RequestReportResult requestReportResult = response.RequestReportResult;
                    ReportRequestInfo reportRequestInfo = requestReportResult.ReportRequestInfo;
                    Console.WriteLine("                  ReportRequestInfo");

                    if (reportRequestInfo.IsSetReportProcessingStatus())
                    {
                        Console.WriteLine("               ReportProcessingStatus");
                        Console.WriteLine("                                  {0}", reportRequestInfo.ReportProcessingStatus);
                    }
                    if (reportRequestInfo.IsSetReportRequestId())
                    {
                        Console.WriteLine("                      ReportRequestId");
                        Console.WriteLine("                                  {0}", reportRequestInfo.ReportRequestId);
                        ReportRequestId = reportRequestInfo.ReportRequestId;
                    }
                    if (reportRequestInfo.IsSetReportType())
                    {
                        Console.WriteLine("                           ReportType");
                        Console.WriteLine("                                  {0}", reportRequestInfo.ReportType);
                    }
                    if (reportRequestInfo.IsSetStartDate())
                    {
                        Console.WriteLine("                            StartDate");
                        Console.WriteLine("                                  {0}", reportRequestInfo.StartDate);
                    }
                    if (reportRequestInfo.IsSetEndDate())
                    {
                        Console.WriteLine("                              EndDate");
                        Console.WriteLine("                                  {0}", reportRequestInfo.EndDate);
                    }
                    if (reportRequestInfo.IsSetSubmittedDate())
                    {
                        Console.WriteLine("                        SubmittedDate");
                        Console.WriteLine("                                  {0}", reportRequestInfo.SubmittedDate);
                    }
                }
                if (response.IsSetResponseMetadata())
                {
                    Console.WriteLine("            ResponseMetadata");
                    ResponseMetadata responseMetadata = response.ResponseMetadata;
                    if (responseMetadata.IsSetRequestId())
                    {
                        Console.WriteLine("                RequestId");
                        Console.WriteLine("                    {0}", responseMetadata.RequestId);
                    }
                }

                Console.WriteLine("            ResponseHeaderMetadata");
                Console.WriteLine("                RequestId");
                Console.WriteLine("                    " + response.ResponseHeaderMetadata.RequestId);
                Console.WriteLine("                ResponseContext");
                Console.WriteLine("                    " + response.ResponseHeaderMetadata.ResponseContext);
                Console.WriteLine("                Timestamp");
                Console.WriteLine("                    " + response.ResponseHeaderMetadata.Timestamp);

            }
            catch (MarketplaceWebServiceException ex)
            {
                ReportRequestId = "Error: " + ex.Message;
                Console.WriteLine("Caught Exception: " + ex.Message);
                Console.WriteLine("Response Status Code: " + ex.StatusCode);
                Console.WriteLine("Error Code: " + ex.ErrorCode);
                Console.WriteLine("Error Type: " + ex.ErrorType);
                Console.WriteLine("Request ID: " + ex.RequestId);
                Console.WriteLine("XML: " + ex.XML);
                Console.WriteLine("ResponseHeaderMetadata: " + ex.ResponseHeaderMetadata);
            }

            return ReportRequestId;
        }

        public string InvokeGetReportListByNextToken(Operations.Reports.MarketplaceWebService client, GetReportListByNextTokenRequest request)
        {
            string ReportId = "";
            try
            {
                GetReportListByNextTokenResponse response = client.GetReportListByNextToken(request);

                Console.WriteLine("Service Response");
                Console.WriteLine("=============================================================================");
                Console.WriteLine();

                Console.WriteLine("        GetReportListByNextTokenResponse");
                if (response.IsSetGetReportListByNextTokenResult())
                {
                    Console.WriteLine("            GetReportListByNextTokenResult");
                    GetReportListByNextTokenResult getReportListByNextTokenResult = response.GetReportListByNextTokenResult;
                    if (getReportListByNextTokenResult.IsSetNextToken())
                    {
                        Console.WriteLine("                NextToken");
                        Console.WriteLine("                    {0}", getReportListByNextTokenResult.NextToken);
                    }
                    if (getReportListByNextTokenResult.IsSetHasNext())
                    {
                        Console.WriteLine("                HasNext");
                        Console.WriteLine("                    {0}", getReportListByNextTokenResult.HasNext);
                    }
                    List<ReportInfo> reportInfoList = getReportListByNextTokenResult.ReportInfo;
                    foreach (ReportInfo reportInfo in reportInfoList)
                    {
                        Console.WriteLine("                ReportInfo");
                        if (reportInfo.IsSetReportId())
                        {
                            Console.WriteLine("                    ReportId");
                            Console.WriteLine("                        {0}", reportInfo.ReportId);
                            ReportId = reportInfo.ReportId;
                        }
                        if (reportInfo.IsSetReportType())
                        {
                            Console.WriteLine("                    ReportType");
                            Console.WriteLine("                        {0}", reportInfo.ReportType);
                        }
                        if (reportInfo.IsSetReportRequestId())
                        {
                            Console.WriteLine("                    ReportRequestId");
                            Console.WriteLine("                        {0}", reportInfo.ReportRequestId);
                        }
                        if (reportInfo.IsSetAvailableDate())
                        {
                            Console.WriteLine("                    AvailableDate");
                            Console.WriteLine("                        {0}", reportInfo.AvailableDate);
                        }
                        if (reportInfo.IsSetAcknowledged())
                        {
                            Console.WriteLine("                    Acknowledged");
                            Console.WriteLine("                        {0}", reportInfo.Acknowledged);
                        }
                        if (reportInfo.IsSetAcknowledgedDate())
                        {
                            Console.WriteLine("                    AcknowledgedDate");
                            Console.WriteLine("                        {0}", reportInfo.AcknowledgedDate);
                        }
                    }
                }
                if (response.IsSetResponseMetadata())
                {
                    Console.WriteLine("            ResponseMetadata");
                    ResponseMetadata responseMetadata = response.ResponseMetadata;
                    if (responseMetadata.IsSetRequestId())
                    {
                        Console.WriteLine("                RequestId");
                        Console.WriteLine("                    {0}", responseMetadata.RequestId);
                    }
                }

                Console.WriteLine("            ResponseHeaderMetadata");
                Console.WriteLine("                RequestId");
                Console.WriteLine("                    " + response.ResponseHeaderMetadata.RequestId);
                Console.WriteLine("                ResponseContext");
                Console.WriteLine("                    " + response.ResponseHeaderMetadata.ResponseContext);
                Console.WriteLine("                Timestamp");
                Console.WriteLine("                    " + response.ResponseHeaderMetadata.Timestamp);

            }
            catch (MarketplaceWebServiceException ex)
            {
                Console.WriteLine("Caught Exception: " + ex.Message);
                Console.WriteLine("Response Status Code: " + ex.StatusCode);
                Console.WriteLine("Error Code: " + ex.ErrorCode);
                Console.WriteLine("Error Type: " + ex.ErrorType);
                Console.WriteLine("Request ID: " + ex.RequestId);
                Console.WriteLine("XML: " + ex.XML);
                Console.WriteLine("ResponseHeaderMetadata: " + ex.ResponseHeaderMetadata);
            }

            return ReportId;
        }

        public GetReportListResult InvokeGetReportList(Operations.Reports.MarketplaceWebService client, GetReportListRequest request)
        {
            GetReportListResult getReportListResult = null;
            try
            {
                GetReportListResponse response = client.GetReportList(request);

                if (response != null)
                {
                    getReportListResult = response.GetReportListResult;
                }
            }
            catch (MarketplaceWebServiceException ex)
            {
                Console.WriteLine("Caught Exception: " + ex.Message);
                Console.WriteLine("Response Status Code: " + ex.StatusCode);
                Console.WriteLine("Error Code: " + ex.ErrorCode);
                Console.WriteLine("Error Type: " + ex.ErrorType);
                Console.WriteLine("Request ID: " + ex.RequestId);
                Console.WriteLine("XML: " + ex.XML);
                Console.WriteLine("ResponseHeaderMetadata: " + ex.ResponseHeaderMetadata);
            }

            return getReportListResult;
        }
    }
}
