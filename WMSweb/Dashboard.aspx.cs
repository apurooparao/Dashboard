using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using WMSbl;
using WMSobjects;

public partial class Dashboard : System.Web.UI.Page
{
    requestBL _rqstbl;
    UserBO _userBO;
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
                Session["WmsId"] = "";
                Session.Remove("WmsId");
                _userBO = new UserBO();
                _userBO = (UserBO)Session["UserBO"];
                Session["UserId"] = _userBO.UserID;
                InitializeControls();
            }

        }


    }
    private void InitializeControls()
    {
        try
        {
            lblMessage.Text = "";
            lblMessage.Visible = false;
            FillDropDown(ddlStatus, "StatusID,StatusName", "tblm_Status", " IsActive =1 order by StatusID");
            int status = Convert.ToInt16(ddlStatus.SelectedValue);

            int userid = _userBO.UserID;
            //chart_dashboard.DataBind();
            // int branchid = 1;
            //   FillGrid(status, branchid);
            //FillGrid(status, userid);

            grd_requests.DataBind();
            grdcnt();

        }
        catch (Exception ex)
        {

            throw;
        }
    }
    private void grdcnt()
    {
        lblMessage.Visible = false;
        var dv1 = (DataView)WmsDashboardDataSource.Select(DataSourceSelectArguments.Empty);
        //lblCnt.Text = dv1?.Count.ToString() ?? "0";
        if (dv1 != null)
        {
            lblCnt.Text = dv1.Count.ToString();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            int status = Convert.ToInt16(ddlStatus.SelectedValue);


            if (Session["UserBO"] == null)
            {
                Response.Redirect("Login.aspx", false);
            }
            else
            {
                _userBO = new UserBO();
                _userBO = (UserBO)Session["UserBO"];
                Session["UserId"] = _userBO.UserID;

                grd_requests.DataBind();
                grdcnt();
           }

         
        }
        catch (Exception)
        {

            throw;
        }
    }

    //private void FillGrid(int status, int userid)
    //{
    //    try
    //    {
    //        _rqstbl = new requestBL();
    //        DataSet drequest = _rqstbl.getRequestbyStatus(status, userid);


    //        if (drequest.Tables[0].Rows.Count != 0)
    //        {
    //            chart_dashboard.DataSource = drequest.Tables[0];
    //            chart_dashboard.Titles["OpenIssues"].Text = ddlStatus.SelectedItem.Text + " Requests";
    //            // Set series members names for the X and Y values

    //            //    Series["Count"].XValueMember = "Criticality";

    //            chart_dashboard.Series["Count"].YValueMembers = "IssueCount";

    //            chart_dashboard.Series["Count"].IsValueShownAsLabel = true;

    //            chart_dashboard.Series["Count"].ChartType = SeriesChartType.Column;

    //            // chart_dashboard.Titles[0].Text = "No of incidents vs Criticality";

    //            chart_dashboard.DataBind();

    //            for (int i = 0; i < drequest.Tables[0].Rows.Count; i++)
    //            {

    //                chart_dashboard.Series["Count"].Points[i].Color = Color.FromName(drequest.Tables[0].Rows[i]["ChartColor"].ToString());
    //            }

    //        }

    //    }
    //    catch (Exception ex)
    //    {

    //        throw;
    //    }
    //}

    private void FillDropDown(DropDownList ddl, string selectQuery, string table, string condition)
    {

        try
        {
            _rqstbl = new requestBL();

            DataSet ds = _rqstbl.getDropDownValues(selectQuery, table, condition);
            ddl.DataSource = ds.Tables[0];
            ddl.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddl.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddl.DataBind();
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int status = Convert.ToInt16(ddlStatus.SelectedValue);
            // int branchid = 1;
            // chart_dashboard.DataBind();
            //  FillGrid(status, branchid);

            if (Session["UserBO"] == null)
            {
                Response.Redirect("Login.aspx", false);
            }
            else
            {
                _userBO = new UserBO();
                _userBO = (UserBO)Session["UserBO"];
                int userid = _userBO.UserID;
                //FillGrid(status, userid);
                grd_requests.DataBind();
                grdcnt();

            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }






    protected void grd_requests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grd_requests, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void grd_requests_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int wmsid = Convert.ToInt32(grd_requests.SelectedDataKey.Value);

            Session["WmsId"] = wmsid;

            Response.Redirect("RequestDetails.aspx", false);
        }
        catch (Exception)
        {

            throw;
        }
    }
}