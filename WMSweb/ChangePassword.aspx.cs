using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMSobjects;

public partial class ChangePassword : System.Web.UI.Page
{
    private UserBO _userBO;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WmsConnection"].ConnectionString);

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
                lblUserNameValue.Text = _userBO.UserName;
            }

        }

    }

    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            // if (CheckDetails())
            if (!string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {

            }
         
        }
        catch (Exception)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "Something went wrong please try again"; throw;
        }
    }

    //private bool CheckDetails()
    //{
    //    try
    //    {
    //        bool result = true;
    //        _userBO = new UserBO();
    //        if (true)
    //        {

    //        }

    //    }
    //    catch (Exception)
    //    {

    //        lblMessage.Visible = true;
    //        lblMessage.Text = "Something went wrong please try again";
    //    }
    //}
}