using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMSbl;

public partial class Reports : System.Web.UI.Page
{
    private readonly string _connectionString = ConfigurationManager.ConnectionStrings["WmsConnection"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserBO"] == null)
        {
            Response.Redirect("Login.aspx", false);
        }
        else
        {
            if (!IsPostBack)
            {
                PopulateDropDowns();
            }
        }
    }

    private void PopulateDropDowns()
    {
        try
        {
            FillListBoxValues(lstPriority, "PriorityID,PriorityName", " tblm_priority", " IsActive =1 order by PriorityName asc");
            FillListBoxValues(lstBranch, "BranchID,BranchName", " tblm_Branch", " IsActive =1 order by BranchName asc");
            FillListBoxValues(lstStatus, "StatusID,StatusName", " tblm_Status", " IsActive =1 order by StatusName asc");
        }
        catch (Exception)
        {
            lblReportMessage.Visible = true;
            lblReportMessage.Text = "Something went wrong please try again";
        }
       
    }

    private void FillListBoxValues(ListBox lstPriority, string selectQuery, string tblName, string condition)
    {

        try
        {
            var cmdbl = new CommonBL();
            var ds = cmdbl.GetDropDownValues(selectQuery, tblName, condition);
            lstPriority.DataSource = ds.Tables[0];
            lstPriority.DataValueField = ds.Tables[0].Columns[0].ToString();
            lstPriority.DataTextField = ds.Tables[0].Columns[1].ToString();
            lstPriority.DataBind();
        }
        catch (Exception)
        {
            lblReportMessage.Visible = true;
            lblReportMessage.Text = "Something went wrong please try again";
        }
    }

    protected void btnSubmitReport_Click(object sender, EventArgs e)
    {
        try
        {
            var priority = string.Join(",", lstPriority.Items.OfType<ListItem>()
                                       .Where(r => r.Selected)
                                       .Select(r => r.Value));
            if (string.IsNullOrEmpty(priority))
            {
                priority = "0";
            }

            var branch = string.Join(",", lstBranch.Items.OfType<ListItem>()
                                     .Where(r => r.Selected)
                                     .Select(r => r.Value));
            if (string.IsNullOrEmpty(branch))
            {
                branch = "0";
            }

            var status = string.Join(",", lstStatus.Items.OfType<ListItem>()
                                     .Where(r => r.Selected)
                                     .Select(r => r.Value));
            if (string.IsNullOrEmpty(status))
            {
                status = "0";
            }

            var rds = new ReportDataSource("RequestDataset", GetRequestReports(priority,branch,status));

            ReportViewer_RequestDetail.LocalReport.DataSources.Clear();
            ReportViewer_RequestDetail.LocalReport.DataSources.Add(rds);
            ReportViewer_RequestDetail.LocalReport.Refresh();

        }
        catch (Exception)
        {
            lblReportMessage.Visible = true;
            lblReportMessage.Text = "Something went wrong please try again"; 
        }

    }

    private object GetRequestReports(string priority,string branch,string status)
    {
        try
        {
            var ds = new RequestDataset();
            using (var sqlcon = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("sp_Reports_Request_Dynamic", sqlcon) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@PRIOIRITYID", SqlDbType.NVarChar).Value = priority;
                cmd.Parameters.Add("@BRANCHID", SqlDbType.NVarChar).Value = branch;
                cmd.Parameters.Add("@STATUSID", SqlDbType.NVarChar).Value = status;
                var sqlda = new SqlDataAdapter { SelectCommand = cmd };
                sqlda.Fill(ds, "RequestDataset");
                if (ds.Tables["RequestDataset"].Rows.Count == 0)
                {
                    divRequestNoResult.Visible = true;
                    lblRequestNoResult.Text = "No Requests found";
                    lblRequestNoResult.Visible = true;
                }
                else
                {

                    divRequestNoResult.Visible = false;
                    lblRequestNoResult.Text = "";
                    lblRequestNoResult.Visible = false;
                }
                return ds.Tables["RequestDataset"];
            }
        }
        catch (Exception)
        {
            lblReportMessage.Visible = true;
            lblReportMessage.Text = "Something went wrong please try again";
            return null;
        }
    }
}