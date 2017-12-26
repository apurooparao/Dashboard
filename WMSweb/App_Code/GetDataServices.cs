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
    public string GetRequestbyStatus(int Status)
    {
        Status = 1;
        requestBL _rqstbl = new requestBL();
        List<RequestCriticalityData> lstRequestCriticalityData = new List<RequestCriticalityData>();
        int Userid = Convert.ToInt16(Session["UserId"]);

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

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetRequestbyStatusTypes()
    {
       // Status = 1;
        requestBL _rqstbl = new requestBL();
        List<RequestStatusData> lstRequestStatusData = new List<RequestStatusData>();
        int Userid = Convert.ToInt16(Session["UserId"]);

        DataTable dtRequest = _rqstbl.getRequestbyStatusTypes( Userid).Tables[0];
        foreach (DataRow row in dtRequest.Rows)
        {
            lstRequestStatusData.Add(new RequestStatusData
            {
                StatusName = Convert.ToString(row["StatusName"]),
                IssueCount = Convert.ToInt16(row["IssueCount"]),
               // ChartColor = Convert.ToString(row["ChartColor"]),
            });
        }
        return new JavaScriptSerializer().Serialize(lstRequestStatusData);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetStatsData(string Timeframe)
    {
        requestBL _rqstbl = new requestBL();
        int userid = Convert.ToInt16(Session["UserId"]);
        List<PriorityByStatus> lstPriorityByStatus = new List<PriorityByStatus>();

        DataTable dtRequest = _rqstbl.GetDashboardInfo(userid, Timeframe).Tables[1];
        foreach (DataRow row in dtRequest.Rows)
        {
            lstPriorityByStatus.Add(new PriorityByStatus
            {
                PriorityId = Convert.ToInt16(row["PriorityID"]),
                PriorityName = Convert.ToString(row["PriorityName"]),
                StatusId = Convert.ToInt16(row["StatusId"]),
                StatusName = Convert.ToString(row["StatusName"]),
                StatusCount = Convert.ToInt16(row["StatusCount"]),
            });
        }

        return new JavaScriptSerializer().Serialize(lstPriorityByStatus);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetRequestDataByStatus(int Status)
    {
        requestBL _rqstbl = new requestBL();
        int userid = Convert.ToInt16(Session["UserId"]);
        List<RequestData> lstRequestData = new List<RequestData>();

        DataTable dtRequest = _rqstbl.GetRequestDetailsByStatus(userid, Status);
        foreach (DataRow row in dtRequest.Rows)
        {
            lstRequestData.Add(new RequestData
            {
                WMSID = Convert.ToInt32(row["WMSID"]),
                PriorityName = Convert.ToString(row["PriorityName"]),
                BranchName = Convert.ToString(row["BranchName"]),
                AffectOperation = Convert.ToString(row["AffectOperation"]),
                Scope = Convert.ToString(row["Scope"]),
                SectionName = Convert.ToString(row["SectionName"]),
                Category = Convert.ToString(row["Category"]),
                Requestor = Convert.ToString(row["Requestor"]),
                CreatedDate = Convert.ToDateTime(row["CreatedDate"])
            });
        }

        return new JavaScriptSerializer().Serialize(lstRequestData);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetRequestDataByStatusAndPriority(int Status,int Priority)
    {
        requestBL _rqstbl = new requestBL();
        int userid = Convert.ToInt16(Session["UserId"]);
        List<RequestData> lstRequestData = new List<RequestData>();

        DataTable dtRequest = _rqstbl.GetRequestDetailsByStatusAndPriority(userid, Status,Priority);
        foreach (DataRow row in dtRequest.Rows)
        {
            lstRequestData.Add(new RequestData
            {
                WMSID = Convert.ToInt32(row["WMSID"]),
                PriorityName = Convert.ToString(row["PriorityName"]),
                BranchName = Convert.ToString(row["BranchName"]),
                AffectOperation = Convert.ToString(row["AffectOperation"]),
                Scope = Convert.ToString(row["Scope"]),
                SectionName = Convert.ToString(row["SectionName"]),
                Category = Convert.ToString(row["Category"]),
                Requestor = Convert.ToString(row["Requestor"]),
                CreatedDate = Convert.ToDateTime(row["CreatedDate"])
            });
        }

        return new JavaScriptSerializer().Serialize(lstRequestData);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetStatuses()
    {
        requestBL _rqstbl = new requestBL();
        List<Statuses> lstStatuses = new List<Statuses>();

        DataTable dtStatuses = _rqstbl.GetMasterStatus();
        foreach (DataRow row in dtStatuses.Rows)
        {
            lstStatuses.Add(new Statuses
            {
                StatusID = Convert.ToInt32(row["StatusID"]),
                StatusName = Convert.ToString(row["StatusName"])
            });
        }

        return new JavaScriptSerializer().Serialize(lstStatuses);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetPriorities()
    {
        requestBL _rqstbl = new requestBL();
        List<Priorities> lstpriorities = new List<Priorities>();

        DataTable dtpriorities = _rqstbl.GetMasterPriority();
        foreach (DataRow row in dtpriorities.Rows)
        {
            lstpriorities.Add(new Priorities
            {
                PriorityID = Convert.ToInt32(row["PriorityID"]),
                PriorityName = Convert.ToString(row["PriorityName"])
            });
        }

        return new JavaScriptSerializer().Serialize(lstpriorities);
    }

    


    public class RequestCriticalityData
    {
        public string Criticality { get; set; }
        public int IssueCount { get; set; }
        public string ChartColor { get; set; }
    }


    public class RequestStatusData
    {
        public string StatusName { get; set; }
        public int IssueCount { get; set; }
     //   public string ChartColor { get; set; }
    }

    public class PriorityByStatus
    {
        public int PriorityId { get; set; }
        public int StatusId { get; set; }
        public string PriorityName { get; set; }
        public string StatusName { get; set; }
        public int StatusCount { get; set; }
    }

    public class RequestData
    {
        public int WMSID { get; set; }
        public string PriorityName { get; set; }
        public string BranchName { get; set; }
        public string AffectOperation { get; set; }
        public string Scope { get; set; }
        public string SectionName { get; set; }
        public string Category { get; set; }
        public string Requestor { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class Statuses
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }

    public class Priorities
    {
        public int PriorityID { get; set; }
        public string PriorityName { get; set; }
    }

}

