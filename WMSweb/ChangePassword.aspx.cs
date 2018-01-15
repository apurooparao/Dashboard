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
using WMSobjects;



public partial class ChangePassword : System.Web.UI.Page
{
    UserBO _userBO;
    SqlConnection _sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["WmsConnection"].ConnectionString);    
    SqlDataReader _sqldr;
    SqlCommand _sqlcmd;


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
            if (CheckDetails())
            {
                bool result = ChangePasswordNew(txtConfirmPassword.Text.Trim());
                if (result)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Password succesfully changed";
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Password change failed";
                }
            }

        }
        catch (Exception)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "Something went wrong please try again"; throw;
        }
    }

   

    

    

    private bool CheckDetails()
    {
        try
        {
            bool result = true;
            _userBO = new UserBO();            
            string password = CheckOldPassword(lblUserNameValue.Text);
            string oldPassword = txtOldPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;


            if (!password.Equals(oldPassword))
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Password entered does not match with existing password";
                result = false;
            }
            else if (newPassword!=confirmPassword)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "New password does not match with confirm password";
                result = false;
            }
            else if (oldPassword==newPassword)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Current and New password is same";
                result = false;
            }
           
            return result;
        }
        catch (Exception)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "Something went wrong please try again";
            return false;
          
        }
    }

    public bool ChangePasswordNew(string NewPassword)
    {
        try
        {
            _userBO = new UserBO();
            _userBO = (UserBO)(HttpContext.Current.Session["UserBO"]);
            
            _sqlcmd = new SqlCommand("sp_ChangePassword", _sqlcon);
            _sqlcmd.CommandType = CommandType.StoredProcedure;
            _sqlcmd.Parameters.AddWithValue("@Password", NewPassword);
            _sqlcmd.Parameters.AddWithValue("@UserName", lblUserNameValue.Text);
            _sqlcon.Open();
            int result = _sqlcmd.ExecuteNonQuery();
            _sqlcon.Close();
            if (result.Equals(1))
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        catch (Exception ex)
        {

            lblMessage.Visible = true;
            lblMessage.Text = "Something went wrong please try again";
            return false;

        }
    }

    public string CheckOldPassword(string username)
    {
        try
        {
            string password = string.Empty;
            _sqlcmd = new SqlCommand();
            _sqlcmd.Connection = _sqlcon;
            _sqlcmd.Parameters.AddWithValue("@UserName", username);
            _sqlcmd.CommandText = "select UserPassword  from tblm_User where UserName = @UserName";
            _sqlcon.Open();
            _sqldr = _sqlcmd.ExecuteReader();
            if (_sqldr.Read())
            {
                password = _sqldr["UserPassword"].ToString();
            }
            _sqlcon.Close();
            return password;

        }
        catch (Exception ex) 
        {
            lblMessage.Visible = true;
            lblMessage.Text = "Something went wrong please try again";
            return null;          
          
        }
    }
}