using System;
using System.Collections.Generic;
using System.Data;
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
        var rqstbl = new requestBL();
        var lstRequestCriticalityData = new List<RequestCriticalityData>();
        int userid = Convert.ToInt16(Session["UserId"]);

        var dtRequest = rqstbl.GetRequestbyStatus(Status, userid).Tables[0];
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
        var rqstbl = new requestBL();
        var lstRequestStatusData = new List<RequestStatusData>();
        int userid = Convert.ToInt16(Session["UserId"]);

        var dtRequest = rqstbl.GetRequestbyStatusTypes( userid).Tables[0];
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
        var rqstbl = new requestBL();
        int userid = Convert.ToInt16(Session["UserId"]);
        var lstPriorityByStatus = new List<PriorityByStatus>();

        var dtRequest = rqstbl.GetDashboardInfo(userid, Timeframe).Tables[1];
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
        var rqstbl = new requestBL();
        int userid = Convert.ToInt16(Session["UserId"]);
        var lstRequestData = new List<RequestData>();

        var dtRequest = rqstbl.GetRequestDetailsByStatus(userid, Status);
        foreach (DataRow row in dtRequest.Rows)
        {
            lstRequestData.Add(new RequestData
            {
                WMSID = Convert.ToInt32(row["WMSID"]),
                Priority = Convert.ToString(row["priority"]),
                BranchName = Convert.ToString(row["BranchName"]),
                AffectOperation = Convert.ToString(row["AffectOperation"]),
                Scope = Convert.ToString(row["Scope"]),
                SectionName = Convert.ToString(row["SectionName"]),
                Category = Convert.ToString(row["Category"]),
                Requestor = Convert.ToString(row["Requestor"]),
                StatusName = Convert.ToString(row["StatusName"])
            });
        }

        return new JavaScriptSerializer().Serialize(lstRequestData);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetRequestDataByStatusAndPriority(int Status,int Priority)
    {
        var rqstbl = new requestBL();
        int userid = Convert.ToInt16(Session["UserId"]);
        var lstRequestData = new List<RequestData>();

        var dtRequest = rqstbl.GetRequestDetailsByStatusAndPriority(userid, Status,Priority);
        foreach (DataRow row in dtRequest.Rows)
        {
            lstRequestData.Add(new RequestData
            {
                WMSID = Convert.ToInt32(row["WMSID"]),
                Priority = Convert.ToString(row["priority"]),
                BranchName = Convert.ToString(row["BranchName"]),
                AffectOperation = Convert.ToString(row["AffectOperation"]),
                Scope = Convert.ToString(row["Scope"]),
                SectionName = Convert.ToString(row["SectionName"]),
                Category = Convert.ToString(row["Category"]),
                Requestor = Convert.ToString(row["Requestor"]),
                StatusName = Convert.ToString(row["StatusName"])
            });
        }

        return new JavaScriptSerializer().Serialize(lstRequestData);
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetRequestDataByWMSid(int wmsId)
    {
        var rqstbl = new requestBL();
        int userid = Convert.ToInt16(Session["UserId"]);
        var lstRequestData = new List<RequestData>();

        var dtRequest = rqstbl.GetRequestDataByWmSid(userid, wmsId);
        foreach (DataRow row in dtRequest.Rows)
        {
            lstRequestData.Add(new RequestData
            {
                WMSID = Convert.ToInt32(row["WMSID"]),
                Priority = Convert.ToString(row["priority"]),
                BranchName = Convert.ToString(row["BranchName"]),
                AffectOperation = Convert.ToString(row["AffectOperation"]),
                Scope = Convert.ToString(row["Scope"]),
                SectionName = Convert.ToString(row["SectionName"]),
                Category = Convert.ToString(row["Category"]),
                Requestor = Convert.ToString(row["Requestor"]),
                StatusName = Convert.ToString(row["StatusName"])
            });
        }

        return new JavaScriptSerializer().Serialize(lstRequestData);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetStatuses()
    {
        var rqstbl = new requestBL();
        var lstStatuses = new List<Statuses>();

        var dtStatuses = rqstbl.GetMasterStatus();
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
        var rqstbl = new requestBL();
        var lstpriorities = new List<Priorities>();

        var dtpriorities = rqstbl.GetMasterPriority();
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
        public string Priority { get; set; }
        public string BranchName { get; set; }
        public string AffectOperation { get; set; }
        public string Scope { get; set; }
        public string SectionName { get; set; }
        public string Category { get; set; }
        public string Requestor { get; set; }
      //  public DateTime CreatedDate { get; set; }
      public string StatusName { get; set; }
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

