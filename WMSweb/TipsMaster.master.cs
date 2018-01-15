using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMSbl;
using WMSobjects;

public partial class TipsMaster : System.Web.UI.MasterPage
{
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
                lblUsername.Text = _userBO.UserName;

                if (_userBO.RoleID == 1)
                {
                    li_administration.Visible = true;
                }
                else
                {
                    li_administration.Visible = false;
                }
                if (_userBO.RoleID == 4)
                {
                    li_createrequest.Visible = false;
                }
                else
                {
                    li_createrequest.Visible = true;
                }
            }
        }
    }

    //protected void lnkRequest_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Session["WmsId"] = "";
    //        Session.Remove("WmsId");

    //        Response.Redirect("RequestDetails.aspx");
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}

    protected void lnkSignout_Click(object sender, EventArgs e)
    {
        try
        {
            loginBL _objloginBL = new loginBL();
            _objloginBL.UpdateLoginStatus();
            Session.Remove("UserBO");
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("~/SignOut.aspx");
        }
        catch (Exception ex)
        {
            


        }
    }

    protected void knkChangePassword_Click(object sender, EventArgs e)
    {
        try
        {          
            Response.Redirect("~/ChangePassword.aspx");
        }
        catch (Exception ex)
        {
          
        }
    }
}
