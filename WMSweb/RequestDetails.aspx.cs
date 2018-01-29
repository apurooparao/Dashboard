using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMSbl;
using WMSobjects;

public partial class RequestDetails : Page
{
    private requestBO _rqstbo;
    private RequestBl _rqstbl;
    private statusBO _statusbo;
    private UserBo _userBo;

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
                _userBo = new UserBo();
                _userBo = (UserBo)Session["UserBO"];
                if (Request["request"] != null)
                {
                    Session["WmsId"] = int.Parse(Request["request"]);
                }
                
                //Session["WmsId"] = 1;
                if (Session["WmsId"] != null && Convert.ToInt32(Session["WmsId"]) != 0)
                {
                    lblWmsIdValue.Text = Session["WmsId"].ToString();
                    var wmsId = Convert.ToInt32(Session["WmsId"]);
                    FillStatusDropDown(wmsId);
                    FillRequestDetails(wmsId);
                    Session["WmsId"] = "";
                    Session.Remove("WmsId");
                    if (_userBo.RoleId == 3)
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
                var ddlname = (DropDownList)Form_Request.FindControl("ddlBranchEdit");
                if (Session["UserBO"] == null)
                {
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    _userBo = new UserBo();
                    _userBo = (UserBo)Session["UserBO"];
                    ddlname.SelectedIndex = ddlname.Items.IndexOf(ddlname.Items.FindByValue(_userBo.BranchId.ToString()));
                  //  ddlname.SelectedIndex = _userBO.BranchID;
                    ddlname.Enabled = _userBo.RoleId==1;
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void BindControls()
    {
        try
        {
            FillDropDown("ddlBranchEdit", "BranchID,BranchName", "tblm_Branch", " IsActive =1 order by BranchID");
            FillDropDown("ddlSectionEdit", "SectionID,SectionName", "tblm_Section", " IsActive =1 order by SectionName");
            FillDropDown("ddlPriorityEdit", "PriorityID,PriorityName", "tblm_Priority", " IsActive =1 order by PriorityName");
            FillCheckBox("chkCategoryEdit", "CategoryID,CategoryName", "tblm_Category", " IsActive =1 order by CategoryName");


        }
        catch (Exception)
        {

            throw;
        }
    }
    private void FillCheckBox(string chk, string selectQuery, string table, string condition)
    {
        try
        {

            var chkname = (CheckBoxList)Form_Request.FindControl(chk);

            _rqstbl = new RequestBl();

            var ds = _rqstbl.GetDropDownValues(selectQuery, table, condition);
            chkname.DataSource = ds.Tables[0];
            chkname.DataValueField = ds.Tables[0].Columns[0].ToString();
            chkname.DataTextField = ds.Tables[0].Columns[1].ToString();
            chkname.DataBind();
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void FillDropDown(string ddl, string selectQuery, string table, string condition)
    {
        try
        {
            var ddlname = (DropDownList)Form_Request.FindControl(ddl);

            _rqstbl = new RequestBl();

            var ds = _rqstbl.GetDropDownValues(selectQuery, table, condition);
            ddlname.DataSource = ds.Tables[0];
            ddlname.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlname.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlname.DataBind();
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void FillRequestDetails(int wmsId)
    {

        try
        {
            _rqstbl = new RequestBl();
            var ds = _rqstbl.GetRequestDetail(wmsId);
            if (ds.Tables[0].Rows.Count > 0)
            {

                Form_Request.DataSource = ds.Tables[0];
                Form_Request.DataBind();
                lblCurrentStatusValue.Text = ds.Tables[0].Rows[0]["StatusName"].ToString();

                
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
                        if (Session["UserBO"] == null)
                        {
                            Response.Redirect("Login.aspx", false);
                        }
                        else
                        {
                            _userBo = new UserBo();
                            _userBo = (UserBo)Session["UserBO"];
                            btnEditRequest.Visible = _userBo.RoleId != 4;
                        }
                       
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
        catch (Exception)
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


            //   change after login page

            _rqstbl = new RequestBl();
            var tranid = _rqstbl.InsertUpdateRequest(_rqstbo);
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
        catch (Exception)
        {
            // ReSharper disable once RedundantCheckBeforeAssignment
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
            var ddlbranch = (DropDownList)Form_Request.FindControl("ddlBranchEdit");
            _rqstbo.branchId = Convert.ToInt16(ddlbranch.SelectedValue);

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
            var category = String.Join(", ", chkCategory.Items.Cast<ListItem>().Where(i => i.Selected));
            _rqstbo.categoryId = category;

            var txtRemarks = (TextBox)Form_Request.FindControl("txtRemarksEdit");
            _rqstbo.remarks = txtRemarks.Text;

            var txtRequestor = (TextBox)Form_Request.FindControl("txtRequestorEdit");
            _rqstbo.requestor = txtRequestor.Text;

            _rqstbo.createdDate = DateTime.Now;
            _rqstbo.statusId = 1;
            _rqstbo.assignedTo = 1;

        }
        catch (Exception)
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
      

            _rqstbo.wmsId = Convert.ToInt32(Form_Request.DataKey.Value);
            _rqstbl = new RequestBl();
            var tranid = _rqstbl.InsertUpdateRequest(_rqstbo);
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
            var ddlbranch = (DropDownList)Form_Request.FindControl("ddlBranchEdit");
            // var ddl = (DropDownList)Form_Request.FindControl("ddlPriorityEdit");
            var lblbrancheditvalue = (Label)Form_Request.FindControl("lblBranchEditValue");
            ddlbranch.SelectedIndex = ddlbranch.Items.IndexOf(ddlbranch.Items.FindByText(lblbrancheditvalue.Text));

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
            var categories = Category.Text.Split(new[] { ", " }, StringSplitOptions.None);
            foreach (ListItem item in chkCategoryEdit.Items)
            {
                item.Selected = categories.Contains(item.Text);
            }
        }
    }
    protected void btnSubmitStatus_Click(object sender, EventArgs e)
    {
        _statusbo = new statusBO
        {
            closureFlag = 0,
            wmsId = Convert.ToInt32(lblWmsIdValue.Text),
            status = Convert.ToInt32(ddlChangeStatus.SelectedValue),
            assignedTo = 0
        };

        if (!string.IsNullOrEmpty(ddlAssignTo.SelectedValue))
        {
            _statusbo.assignedTo = Convert.ToInt32(ddlAssignTo.SelectedValue);
        }
        _statusbo.comment = txtAdminComment.Text;
        if (ddlChangeStatus.SelectedItem.Text == "Close")
        {
            _statusbo.closureFlag = 1;
            _statusbo.materialsUsed = txtMaterialsUsed.Text;
            _statusbo.teamMembers = txtTeamMembers.Text;
              
            _statusbo.timeIn = Convert.ToDateTime(txtIntime.Text);
            _statusbo.timeOut = Convert.ToDateTime(txtOutTime.Text);
        }

        _rqstbl = new RequestBl();
        var tranid = _rqstbl.Changestatus(_statusbo);
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

    private void FillStatusDropDown(int wmsid)
    {
        try
        {
            _rqstbl = new RequestBl();
            var ds = _rqstbl.GetStatusValues(wmsid);
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
                    _userBo = new UserBo();
                    _userBo = (UserBo)Session["UserBO"];
                    statusdiv.Visible = _userBo.RoleId != 3;
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
        catch (Exception)
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

            _rqstbl = new RequestBl();

            var ds = _rqstbl.GetDropDownValues(selectQuery, table, condition);
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