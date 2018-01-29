using System.Data;
using WMSda;
using WMSobjects;

namespace WMSbl
{
    public class LoginBl
    {
        public DataTable LoginUser(string username, string password)
        {
            var objloginDa = new LoginDa();
            return objloginDa.GetStatusvalues(username, password);
        }
        public string UpdateLoginStatus()
        {
            var objloginDa = new LoginDa();
            return objloginDa.UpdateLoginStatus();
        }

        public UserBo CheckUser(string userName, string password)
        {
            var obLoginDa = new LoginDa();
            return obLoginDa.CheckUser(userName, password);
        }

        public string CheckOldPassword(string username)
        {

            var objchangepasswordDl = new ChangePasswordDal();
            return objchangepasswordDl.CheckOldPassword(username);

        }

        public bool ChangePassword(string newPassword)
        {
            var objChangeDal = new ChangePasswordDal();
            return objChangeDal.ChangePassword(newPassword);

        }

    }
}
