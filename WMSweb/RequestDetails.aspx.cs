using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMSbl;
using WMSobjects;

public partial class RequestDetails : System.Web.UI.Page
{
    requestBO _rqstbo;
    requestBL _rqstbl;
    statusBO _statusbo;
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
                _userBO = new UserBO();
                _userBO = (UserBO)Session["UserBO"];
                if (Request["request"] != null)
                {
                    Session["WmsId"] = int.Parse(Request["request"]);
                }
                
                //Session["WmsId"] = 1;
                if (Session["WmsId"] != null && Convert.ToInt32(Session["WmsId"]) != 0)
                {
                    lblWmsIdValue.Text = Session["WmsId"].ToString();
                    var WmsId = Convert.ToInt32(Session["WmsId"]);
                    FillStatusDropDown(WmsId);
                    FillRequestDetails(WmsId);
                    Session["WmsId"] = "";
                    Session.Remove("WmsId");
                    if (_userBO.RoleID == 3)
                    {
                        statusdiv.Visible = false;
                    }
                }
                else
                {
                    Form_Request.ChangeMode(FormViewMode.Insert);
                }
            }

        }
    }

    protected void Form_Request_DataBound(object sender, EventArgs e)
    {
        try
        {
            if (Form_Request.CurrentMode != FormViewMode.ReadOnly)
            {
                BindControls();
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    private void BindControls()
    {
        try
        {    
            FillDropDown("ddlSectionEdit", "SectionID,SectionName", "tblm_Section", " IsActive =1 order by SectionName");
            FillDropDown("ddlPriorityEdit", "PriorityID,PriorityName", "tblm_Priority", " IsActive =1 order by PriorityName");
            FillCheckBox("chkCategoryEdit", "CategoryID,CategoryName", "tblm_Category", " IsActive =1 order by CategoryName");


        }
        catch (Exception ex)
        {

            throw;
        }
    }
    private void FillCheckBox(string chk, string selectQuery, string table, string condition)
    {
        try
        {

            var chkname = (CheckBoxList)Form_Request.FindControl(chk);

            _rqstbl = new requestBL();

            DataSet ds = _rqstbl.getDropDownValues(selectQuery, table, condition);
            chkname.DataSource = ds.Tables[0];
            chkname.DataValueField = ds.Tables[0].Columns[0].ToString();
            chkname.DataTextField = ds.Tables[0].Columns[1].ToString();
            chkname.DataBind();
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private void FillDropDown(string ddl, string selectQuery, string table, string condition)
    {
        try
        {
            var ddlname = (DropDownList)Form_Request.FindControl(ddl);

            _rqstbl = new requestBL();

            DataSet ds = _rqstbl.getDropDownValues(selectQuery, table, condition);
            ddlname.DataSource = ds.Tables[0];
            ddlname.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlname.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlname.DataBind();
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    private void FillRequestDetails(int wmsId)
    {

        try
        {
            _rqstbl = new requestBL();
            var ds = _rqstbl.GetRequestDetail(wmsId);
            if (ds.Tables[0].Rows.Count > 0)
            {

                Form_Request.DataSource = ds.Tables[0];
                Form_Request.DataBind();
                lblCurrentStatusValue.Text = ds.Tables[0].Rows[0]["StatusName"].ToString();

                

                //StringBuilder sbstring = new StringBuilder();
                //foreach (DataRow row in ds.Tables[1].Rows)
                //{
                //    sbstring.Append("\n");
                //    sbstring.Append(row[0].ToString() + "   ");
                //    sbstring.Append(row[1].ToString());
                //    sbstring.Append(row[2].ToString());
                //    sbstring.Append(row[3].ToString());
                //    sbstring.Append(row[4].ToString());
                //    sbstring.Append("\n");
                //}
                //lblTimeline.Text = sbstring.ToString();

                if (Convert.ToInt16(ds.Tables[0].Rows[0]["StatusId"]) == 3 || Convert.ToInt16(ds.Tables[0].Rows[0]["StatusId"]) == 4)
                {
                    var btnEditRequest = (Button)Form_Request.FindControl("btnEditRequest");
                    btnEditRequest.Visible = false;
                }
                else
                {
                    var btnEditRequest = (Button)Form_Request.FindControl("btnEditRequest");
                    if (btnEditRequest != null)
                    {
                        btnEditRequest.Visible = true;
                    }
                }
             //   Session["wmsId"] = wmsId;
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                grdTimeline.DataSource = ds.Tables[1];
                grdTimeline.DataBind();
            }


            }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void Form_Request_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        try
        {
            _rqstbo = CreateRequest();
            _rqstbo.InsUpdFlag = 0;
            _rqstbo.statusId = 1;
            _rqstbo.assignedTo = 1;


            if (Session["UserBO"] == null)
            {
                Response.Redirect("Login.aspx", false);
            }
            else
            {
                _userBO = new UserBO();
                _userBO = (UserBO)Session["UserBO"];
                // change after adding branch drop down for admin
                if (_userBO.RoleID == 1 )
                {
                    _rqstbo.branchId = 1;
                }
                else
                {
                    _rqstbo.branchId = _userBO.BranchID;
                }
              
            }
            //   change after login page

            _rqstbl = new requestBL();
            var tranid = _rqstbl.insertUpdateRequest(_rqstbo);
            if (!(tranid.Equals(0)))
            {
                lblMessage.Text = "Request placed succesfully";
                lblMessage.Visible = true;
                Form_Request.ChangeMode(FormViewMode.ReadOnly);
                FillRequestDetails(tranid);
                FillStatusDropDown(tranid);
                lblWmsIdValue.Text = tranid.ToString();
            }
            else
            {
                lblMessage.Text = "Request creation failed. Please try again";
                lblMessage.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Session["wmsId"]!=null)
            {
                Session["wmsId"] = null;
            }            
            throw;
        }
        finally
        {
            _rqstbo = null;
            _rqstbl = null;
        }
    }
    private requestBO CreateRequest()
    {
        _rqstbo = new requestBO();
        try
        {
            var ddlpriority = (DropDownList)Form_Request.FindControl("ddlPriorityEdit");
            _rqstbo.priorityId = Convert.ToInt16(ddlpriority.SelectedValue);

            var ddlAffecting = (DropDownList)Form_Request.FindControl("ddlAffectingEdit");
            _rqstbo.affectOperation = ddlAffecting.SelectedValue;

            var txtScope = (TextBox)Form_Request.FindControl("txtScopeEdit");
            _rqstbo.scope = txtScope.Text;

            var ddlFloor = (DropDownList)Form_Request.FindControl("ddlFloorEdit");
            _rqstbo.floor = Convert.ToInt16(ddlFloor.SelectedValue);

            var ddlSection = (DropDownList)Form_Request.FindControl("ddlSectionEdit");
            _rqstbo.sectionId = Convert.ToInt16(ddlSection.SelectedValue);

            var txtOtherSection = (TextBox)Form_Request.FindControl("txtOtherSectionEdit");
            _rqstbo.otherSection = txtOtherSection.Text;

            var chkCategory = (CheckBoxList)Form_Request.FindControl("chkCategoryEdit");
            string category = String.Join(", ", chkCategory.Items.Cast<ListItem>().Where(i => i.Selected));
            _rqstbo.categoryId = category;

            var txtRemarks = (TextBox)Form_Request.FindControl("txtRemarksEdit");
            _rqstbo.remarks = txtRemarks.Text;

            var txtRequestor = (TextBox)Form_Request.FindControl("txtRequestorEdit");
            _rqstbo.requestor = txtRequestor.Text;

            _rqstbo.createdDate = DateTime.Now;
            _rqstbo.statusId = 1;
            _rqstbo.assignedTo = 1;

        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "Something went wrong please try again";

        }
        return _rqstbo;

    }
    protected void Form_Request_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        try
        {
            _rqstbo = CreateRequest();
            _rqstbo.InsUpdFlag = 1;
            _rqstbo.statusId = 1;
            _rqstbo.assignedTo = 1;
            if (Session["UserBO"] == null)
            {
                Response.Redirect("Login.aspx", false);
            }
            else
            {
                _userBO = new UserBO();
                _userBO = (UserBO)Session["UserBO"];
                // change after adding branch drop down for admin
                if (_userBO.RoleID == 1)
                {
                    _rqstbo.branchId = 1;
                }
                else
                {
                    _rqstbo.branchId = _userBO.BranchID;
                }

            }

            _rqstbo.wmsId = Convert.ToInt32(Form_Request.DataKey.Value);
            _rqstbl = new requestBL();
            var tranid = _rqstbl.insertUpdateRequest(_rqstbo);
            if (!(tranid.Equals(0)))
            {
                lblMessage.Text = "Request updated succesfully";
                lblMessage.Visible = true;
                Form_Request.ChangeMode(FormViewMode.ReadOnly);
                FillRequestDetails(tranid);
                FillStatusDropDown(tranid);
            }
            else
            {
                lblMessage.Text = "Request updation failed. Please try again";
                lblMessage.Visible = true;
            }
        }
        catch (Exception ex)
        {

            throw;
        }
        finally
        {
            _rqstbo = null;
            _rqstbl = null;
        }
    }
    protected void Form_Request_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        try
        {
            Form_Request.ChangeMode(e.NewMode);

            // if (Session["wmsId"] != null)
            if (!string.IsNullOrWhiteSpace(lblWmsIdValue.Text))
            {
                //var wmsid = Convert.ToInt32(Session["wmsId"]);
                var wmsid = Convert.ToInt32(lblWmsIdValue.Text);
                FillRequestDetails(wmsid);
            }
            else
            {
              Form_Request.ChangeMode(FormViewMode.Insert);
            }

            if (e.NewMode == FormViewMode.Insert)
            {
                //  Session["wmsId"] = null;
                lblWmsIdValue.Text = string.Empty;
            }

            if (e.NewMode == FormViewMode.Edit)
            {
                var ddlpriority = (DropDownList)Form_Request.FindControl("ddlPriorityEdit");
                // var ddl = (DropDownList)Form_Request.FindControl("ddlPriorityEdit");
                var lbl = (Label)Form_Request.FindControl("lblPriorityEditValue");
                ddlpriority.SelectedIndex = ddlpriority.Items.IndexOf(ddlpriority.Items.FindByText(lbl.Text));

                var ddlAffectingEdit = (DropDownList)Form_Request.FindControl("ddlAffectingEdit");
                var lblAffectingEditValue = (Label)Form_Request.FindControl("lblAffectingEditValue");
                ddlAffectingEdit.SelectedIndex = ddlAffectingEdit.Items.IndexOf(ddlAffectingEdit.Items.FindByText(lblAffectingEditValue.Text));

                var ddlFloorEdit = (DropDownList)Form_Request.FindControl("ddlFloorEdit");
                var lblFloorEditValue = (Label)Form_Request.FindControl("lblFloorEditValue");
                // ddlFloorEdit.SelectedIndex = ddlFloorEdit.Items.IndexOf(ddlFloorEdit.Items.FindByText(lblFloorEditValue.Text));
                ddlFloorEdit.SelectedValue = lblFloorEditValue.Text;

                var ddlSectionEdit = (DropDownList)Form_Request.FindControl("ddlSectionEdit");
                var lblSectionEditValue = (Label)Form_Request.FindControl("lblSectionEditValue");
                ddlSectionEdit.SelectedIndex = ddlSectionEdit.Items.IndexOf(ddlSectionEdit.Items.FindByText(lblSectionEditValue.Text));

                var chkCategoryEdit = (CheckBoxList)Form_Request.FindControl("chkCategoryEdit");
                var Category = (Label)Form_Request.FindControl("lblCategoryEditValue");
                String[] categories = Category.Text.Split(new[] { ", " }, StringSplitOptions.None);
                foreach (ListItem item in chkCategoryEdit.Items)
                {
                    item.Selected = categories.Contains(item.Text);
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void btnSubmitStatus_Click(object sender, EventArgs e)
    {
        try
        {
            _statusbo = new statusBO();

            _statusbo.closureFlag = 0;
            _statusbo.wmsId = Convert.ToInt32(lblWmsIdValue.Text);
            _statusbo.status = Convert.ToInt32(ddlChangeStatus.SelectedValue);
            _statusbo.assignedTo = 0;
            if (ddlAssignTo.SelectedValue != "" && ddlAssignTo.SelectedValue != null)
            {
                _statusbo.assignedTo = Convert.ToInt32(ddlAssignTo.SelectedValue);
            }
            _statusbo.comment = txtAdminComment.Text;
            if (ddlChangeStatus.SelectedItem.Text == "Close")
            {
                _statusbo.closureFlag = 1;
                _statusbo.materialsUsed = txtMaterialsUsed.Text;
                _statusbo.teamMembers = txtTeamMembers.Text;
              
                _statusbo.timeIn = Convert.ToDateTime(txtIntime.Text.ToString());
                _statusbo.timeOut = Convert.ToDateTime(txtOutTime.Text.ToString());
            }

            _rqstbl = new requestBL();
            var tranid = _rqstbl.changestatus(_statusbo);
            if (!(tranid.Equals(0)))
            {
                lblMessage.Text = "Status updated succesfully";
                lblMessage.Visible = true;
                FillStatusDropDown(tranid);
                FillRequestDetails(tranid);
                txtAdminComment.Text = "";
            }
            else
            {
                lblMessage.Text = "Status updation failed. Please try again";
                lblMessage.Visible = true;
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private void FillStatusDropDown(int wmsid)
    {
        try
        {
            _rqstbl = new requestBL();
            DataSet ds = _rqstbl.getStatusValues(wmsid);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlChangeStatus.DataSource = ds.Tables[0];
                ddlChangeStatus.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlChangeStatus.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlChangeStatus.DataBind();
                ddlChangeStatus.Items.Insert(0, "Select Status");
                ddlChangeStatusEvent();
                if (Session["UserBO"] == null)
                {
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    _userBO = new UserBO();
                    _userBO = (UserBO)Session["UserBO"];
                    if (_userBO.RoleID == 3)
                    {
                        statusdiv.Visible = false;
                    }
                    else
                    {
                        statusdiv.Visible = true;
                    }
                }

            }
            else
            {
                statusdiv.Visible = false;
            }
            trclosure.Attributes.CssStyle.Add("Display", "none");
            rfvtxtIntime.Enabled = false;
            rfvtxtOutTime.Enabled = false;
            rgvtxtOutTime.Enabled = false;
        }
        catch (Exception ex)
        {
            statusdiv.Visible = false;
            throw;
        }
    }
    protected void ddlChangeStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlChangeStatusEvent();
    }

    private void ddlChangeStatusEvent()
    {
        try
        {
            trclosure.Attributes.CssStyle.Add("Display", "none");
            rfvtxtIntime.Enabled = false;
            rfvtxtOutTime.Enabled = false;
            rgvtxtOutTime.Enabled = false;

            if (ddlChangeStatus.SelectedItem.Text != lblWmsIdValue.Text)
            {
                if (ddlChangeStatus.SelectedItem.Text == "In Progress")
                {
                    FillDropDown(ddlAssignTo, "UserID,UserName", "tblm_user", " RoleID in (1,4) and IsActive = 1 order by UserName");
                }
                else if (ddlChangeStatus.SelectedItem.Text == "Close")
                {
                    trclosure.Attributes.CssStyle.Add("Display", "visible");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Callscript", "datepickershow();", true);
                    rfvtxtIntime.Enabled = true;
                    rfvtxtOutTime.Enabled = true;
                    rgvtxtOutTime.Enabled = true;
                }
                else
                {
                    ddlAssignTo.Items.Clear();

                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private void FillDropDown(DropDownList ddlAssignTo, string selectQuery, string table, string condition)
    {
        try
        {

            _rqstbl = new requestBL();

            DataSet ds = _rqstbl.getDropDownValues(selectQuery, table, condition);
            ddlAssignTo.DataSource = ds.Tables[0];
            ddlAssignTo.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlAssignTo.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlAssignTo.DataBind();
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}