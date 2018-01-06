using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WMSobjects;

namespace WMSda
{
    public class loginDA
    {
        readonly SqlConnection _sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["WmsConnection"].ConnectionString);
        UserBO _objUserBO;
        SqlDataReader _sqldr;

        public DataTable getStatusvalues(string UserName, string Password)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM vw_UserRole WHERE UserName = @UserName AND UserPassword = @UserPassword", _sqlcon);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@UserPassword", Password);

                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                _sqlcon.Close();
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                _sqlcon.Close();
            }
        }


        public UserBO CheckUser(string UserName, string Password)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("SELECT * FROM vw_UserRole WHERE UserName = @UserName AND UserPassword = @UserPassword", _sqlcon);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@UserPassword", Password);
                _sqlcon.Open();
                _sqldr = cmd.ExecuteReader();

                if (_sqldr.Read())
                {
                    _objUserBO = new UserBO();
                    _objUserBO.UserID = Convert.ToInt32(_sqldr["USERID"]);
                    _objUserBO.UserName = _sqldr["UserName"].ToString();
                    _objUserBO.RoleID = Convert.ToInt32(_sqldr["ROLEID"]);

                    if (_sqldr["BRANCHID"].ToString() != string.Empty)
                    {
                        _objUserBO.BranchID = Convert.ToInt32(_sqldr["BRANCHID"]);
                    }
                    else
                    {
                        _objUserBO.BranchID = 1;
                    }

                    _sqldr.Close();
                    _sqlcon.Close();

                    // INSERT A RECORD IN AUDIT TABLE IF USER IS LOGGED IN 
                    SqlCommand sqlcmd = new SqlCommand("sp_Audit_Ins", _sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@UserID", _objUserBO.UserID);
                    sqlcmd.Parameters.Add("@sessionId", SqlDbType.VarChar).Value = HttpContext.Current.Session.SessionID;
                    _sqlcon.Open();
                    sqlcmd.ExecuteNonQuery();
                }
                _sqlcon.Close();
                return _objUserBO;
            }
            catch (Exception ex)
            {
                _objUserBO = new UserBO();
                _objUserBO.UserName = ex.Message;
                return _objUserBO;

            }

        }

        public string UpdateLoginStatus()
        {
            try
            {
                _objUserBO = (UserBO)HttpContext.Current.Session["UserBO"];
                SqlCommand _sqlcmd = new SqlCommand("sp_UpdateLogin_Signout", _sqlcon);
                _sqlcmd.CommandType = CommandType.StoredProcedure;
                _sqlcmd.Parameters.AddWithValue("@UserID", _objUserBO.UserID);
                _sqlcon.Open();
                string ID = Convert.ToString(_sqlcmd.ExecuteNonQuery());
                _sqlcon.Close();

                return ID;
            }
            catch (Exception)
            {

                return string.Empty;
            }
            finally
            {
                _sqlcon.Close();
            }
        }
    }
}
