using System;
using WMSbl;
using WMSobjects;

public partial class TipsMaster : System.Web.UI.MasterPage
{
    private UserBo _userBo;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserBO"] == null)
        {
            Response.Redirect("Login.aspx", false);
        }
        else
        {
            // ReSharper disable once InvertIf
            if (!IsPostBack)
            {
                _userBo = new UserBo();
                _userBo = (UserBo)Session["UserBO"];
                lblUsername.Text = _userBo.UserName;

                li_administration.Visible = _userBo.RoleId == 1;
                li_reports.Visible = _userBo.RoleId == 1;
                li_createrequest.Visible = _userBo.RoleId != 4;
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
            var objloginBl = new LoginBl();
            objloginBl.UpdateLoginStatus();
            Session.Remove("UserBO");
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("~/SignOut.aspx");
        }
        catch (Exception ex)
        {
            // ignored
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
            // ignored
        }
    }
}
