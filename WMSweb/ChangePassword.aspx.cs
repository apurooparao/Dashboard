using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using WMSobjects;



public partial class ChangePassword : System.Web.UI.Page
{
    private UserBo _userBo;
    readonly SqlConnection _sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["WmsConnection"].ConnectionString);
    private SqlDataReader _sqldr;
    private SqlCommand _sqlcmd;


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
                lblUserNameValue.Text = _userBo.UserName;
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
                var result = ChangePasswordNew(txtConfirmPassword.Text.Trim());
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
            var result = true;
            _userBo = new UserBo();            
            var password = CheckOldPassword(lblUserNameValue.Text);
            var oldPassword = txtOldPassword.Text;
            var newPassword = txtNewPassword.Text;
            var confirmPassword = txtConfirmPassword.Text;


            if (!password.Equals(oldPassword))
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Password entered does not match with existing Password";
                result = false;
            }
            else if (newPassword!=confirmPassword)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "New Password does not match with Confirm Password";
                result = false;
            }
            else if (oldPassword==newPassword)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Current and New Password is same";
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

    public bool ChangePasswordNew(string newPassword)
    {
        try
        {
            _userBo = new UserBo();
            _userBo = (UserBo)(HttpContext.Current.Session["UserBO"]);

            _sqlcmd = new SqlCommand("sp_ChangePassword", _sqlcon) {CommandType = CommandType.StoredProcedure};
            _sqlcmd.Parameters.AddWithValue("@Password", newPassword);
            _sqlcmd.Parameters.AddWithValue("@UserName", lblUserNameValue.Text);
            _sqlcon.Open();
            var result = _sqlcmd.ExecuteNonQuery();
            _sqlcon.Close();
            return result > 1;


        }
        catch (Exception)
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
            var password = string.Empty;
            _sqlcmd = new SqlCommand {Connection = _sqlcon};
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
        catch (Exception) 
        {
            lblMessage.Visible = true;
            lblMessage.Text = "Something went wrong please try again";
            return null;          
          
        }
    }
}