using System;
using WMSbl;
using WMSobjects;

public partial class Login : System.Web.UI.Page
{
    UserBo _userbo;
    LoginBl _objloginBL;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {

            CheckUser();

                        
        }
        catch (Exception)
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
            _objloginBL = new LoginBl();
            _userbo = new UserBo();
            _userbo = _objloginBL.CheckUser(UserName, password);
            if (_userbo == null)
            {

                lblStatus.Visible = true;
                lblStatus.Text = "Invalid UserName or Password";
                txtPassword.Focus();
            }
            else
            {
                Session["UserBO"] = _userbo;
                Response.Redirect("Dashboard.aspx", false);
                //Response.Redirect("RequestDetails.aspx", false);
            }
        }
        catch (Exception ex)
        {
            lblStatus.Visible = true;
            lblStatus.Text = ex.ToString();
        }
    }
}