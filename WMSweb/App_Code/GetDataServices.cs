using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using WMSbl;

/// <summary>
/// Summary description for GetDataServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class GetDataServices : System.Web.Services.WebService
{

    public GetDataServices()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetRequestbyStatus()
    {
        int Status = 1; int Userid = 1;
        requestBL _rqstbl = new requestBL();
        List<RequestCriticalityData> lstRequestCriticalityData = new List<RequestCriticalityData>();

        DataTable dtRequest = _rqstbl.getRequestbyStatus(Status, Userid).Tables[0];
        foreach (DataRow row in dtRequest.Rows)
        {
            lstRequestCriticalityData.Add(new RequestCriticalityData
            {
                Criticality = Convert.ToString(row["Criticality"]),
                IssueCount = Convert.ToInt16(row["IssueCount"]),
                ChartColor = Convert.ToString(row["ChartColor"]),
            });
        }
        return new JavaScriptSerializer().Serialize(lstRequestCriticalityData);
    }

    public class RequestCriticalityData
    {
        public string Criticality { get; set; }
        public int IssueCount { get; set; }
        public string ChartColor { get; set; }
    }
}

