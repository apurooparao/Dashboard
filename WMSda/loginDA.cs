using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using WMSobjects;

namespace WMSda
{
    public class LoginDa
    {
        readonly SqlConnection _sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["WmsConnection"].ConnectionString);
        private UserBo _objUserBo;
        private SqlDataReader _sqldr;

        public DataTable GetStatusvalues(string userName, string password)
        {
            try
            {
                var cmd = new SqlCommand("SELECT * FROM vw_UserRole WHERE UserName = @UserName AND UserPassword = @UserPassword", _sqlcon);
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@UserPassword", password);

                var adapt = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                adapt.Fill(dt);
                _sqlcon.Close();
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _sqlcon.Close();
            }
        }


        public UserBo CheckUser(string userName, string password)
        {
            try
            {

                var cmd = new SqlCommand("SELECT * FROM vw_UserRole WHERE UserName = @UserName AND UserPassword = @UserPassword", _sqlcon);
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@UserPassword", password);
                _sqlcon.Open();
                _sqldr = cmd.ExecuteReader();

                if (_sqldr.Read())
                {
                    _objUserBo = new UserBo
                    {
                        UserId = Convert.ToInt32(_sqldr["USERID"]),
                        UserName = _sqldr["UserName"].ToString(),
                        RoleId = Convert.ToInt32(_sqldr["ROLEID"]),
                        BranchId = _sqldr["BRANCHID"].ToString() != string.Empty
                            ? Convert.ToInt32(_sqldr["BRANCHID"])
                            : 1
                    };


                    _sqldr.Close();
                    _sqlcon.Close();

                    // INSERT A RECORD IN AUDIT TABLE IF USER IS LOGGED IN 
                    var sqlcmd =
                        new SqlCommand("sp_Audit_Ins", _sqlcon) {CommandType = CommandType.StoredProcedure};
                    sqlcmd.Parameters.AddWithValue("@UserID", _objUserBo.UserId);
                    sqlcmd.Parameters.Add("@sessionId", SqlDbType.VarChar).Value = HttpContext.Current.Session.SessionID;
                    _sqlcon.Open();
                    sqlcmd.ExecuteNonQuery();
                }
                _sqlcon.Close();
                return _objUserBo;
            }
            catch (Exception ex)
            {
                _objUserBo = new UserBo {UserName = ex.Message};
                return _objUserBo;

            }

        }

        public string UpdateLoginStatus()
        {
            try
            {
                _objUserBo = (UserBo)HttpContext.Current.Session["UserBO"];
                var sqlcmd =
                    new SqlCommand("sp_UpdateLogin_Signout", _sqlcon) {CommandType = CommandType.StoredProcedure};
                sqlcmd.Parameters.AddWithValue("@UserID", _objUserBo.UserId);
                _sqlcon.Open();
                var id = Convert.ToString(sqlcmd.ExecuteNonQuery());
                _sqlcon.Close();

                return id;
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
