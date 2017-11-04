using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMSbl;
using WMSobjects;

public partial class Login : System.Web.UI.Page
{
    UserBO _userbo;
    loginBL _objloginBL;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {

            CheckUser();


        }
        catch (Exception ex)
        {
            lblStatus.Visible = true;
            lblStatus.Text = "Please try again";
        }
    }

    private void CheckUser()
    {
        try
        {
            string UserName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            _objloginBL = new loginBL();
            _userbo = new UserBO();
            _userbo = _objloginBL.CheckUser(UserName, password);
            if (_userbo == null)
            {
                lblStatus.Visible = true;
                lblStatus.Text = "Invalid UserName or Password";
            }
            else
            {
                Session["UserBO"] = _userbo;
                var userId = _userbo.UserID;

                if (!this.Page.ClientScript.IsClientScriptBlockRegistered("myPostBackScript"))
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(Page),
                        "set", "SetUserId(" + userId + ");window.location='" + Request.ApplicationPath + "Dashboard.aspx';", true);
                }

                //Response.Redirect("Dashboard.aspx", false);




            }
        }
        catch (Exception ex)
        {
            lblStatus.Visible = true;
            lblStatus.Text = ex.ToString();
        }
    }
}