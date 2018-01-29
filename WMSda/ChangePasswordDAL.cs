using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using WMSobjects;

namespace WMSda
{
    public class ChangePasswordDal
    {
        readonly SqlConnection _sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["WmsConnection"].ConnectionString);
        UserBo _objUserBo;
        private SqlDataReader _sqldr;
        private SqlCommand _sqlcmd;

        public string CheckOldPassword(string username)
        {
            string password = string.Empty;
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

        public bool ChangePassword(string newPassword)
        {
            _objUserBo = new UserBo();
            _objUserBo = (UserBo)(HttpContext.Current.Session["UserBO"]);
            _sqlcmd = new SqlCommand("CHANGE_PASSWORD", _sqlcon) {CommandType = CommandType.StoredProcedure};
            _sqlcmd.Parameters.AddWithValue("@Password", newPassword);
            _sqlcmd.Parameters.AddWithValue("@UserName", _objUserBo.UserName);
            _sqlcon.Open();
            int result = _sqlcmd.ExecuteNonQuery();
            _sqlcon.Close();
            return result.Equals(1);
        }

       }

      
    }
