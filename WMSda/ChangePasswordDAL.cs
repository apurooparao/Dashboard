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
    public class ChangePasswordDAL : IDisposable
    {
        private bool disposeflag = false;

        SqlConnection _sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["WmsConnection"].ConnectionString);
        UserBO _objUserBO;
        SqlDataReader _sqldr;
        SqlCommand _sqlcmd;

        public string CheckOldPassword(string username)
        {
            try
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
            catch (Exception)
            {

                throw;
            }
        }

        public bool ChangePassword(string NewPassword)
        {
            try
            {
                _objUserBO = new UserBO();
                _objUserBO = (UserBO)(HttpContext.Current.Session["UserBO"]);
                _sqlcmd = new SqlCommand("CHANGE_PASSWORD", _sqlcon);
                _sqlcmd.CommandType = CommandType.StoredProcedure;
                _sqlcmd.Parameters.AddWithValue("@Password", NewPassword);
                _sqlcmd.Parameters.AddWithValue("@UserName", _objUserBO.UserName);
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
            catch (Exception)
            {

                throw;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposeflag)
            {
                if (disposing)
                {
                    if (_sqlcmd != null)
                    {
                        _sqlcmd.Dispose();
                    }
                    if (_sqlcon != null)
                    {
                        _sqlcon.Dispose();
                    }
                }
                disposeflag = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

      
    }
}
