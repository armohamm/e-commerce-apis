using MarketplaceWebService;
using MarketplaceWebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amazon.Interface
{
    interface ReportInterface
    {
        string RequestReport(string Type, DateTime From, DateTime To);

        string GetReportLatestReport(DateTime From, DateTime To, string ReportType);

        string GetReport(string ReportId, string Path);

        string ReadReport(string Path);

        void InvokeGetReport(Operations.Reports.MarketplaceWebService client, GetReportRequest requestReport);

        string InvokeRequestReport(Operations.Reports.MarketplaceWebService client, RequestReportRequest requestReport);

        string InvokeGetReportListByNextToken(amazon.Operations.Reports.MarketplaceWebService client, GetReportListByNextTokenRequest request);

        GetReportListResult InvokeGetReportList(amazon.Operations.Reports.MarketplaceWebService client, GetReportListRequest request);
    }
}
