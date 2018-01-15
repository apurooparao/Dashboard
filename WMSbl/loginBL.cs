using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMSda;
using WMSobjects;

namespace WMSbl
{
    public class loginBL
    {
        public DataTable loginUser(string username, string password)
        {
            loginDA objloginDA = new loginDA();
            return objloginDA.getStatusvalues(username, password);
        }
        public string UpdateLoginStatus()
        {
            loginDA _objloginDA = new loginDA();
            return _objloginDA.UpdateLoginStatus();
        }

        public UserBO CheckUser(string userName, string password)
        {
            loginDA _obLoginDA = new loginDA();
            return _obLoginDA.CheckUser(userName, password);
        }

        public string CheckOldPassword(string username)
        {

            ChangePasswordDAL objchangepasswordDL = new ChangePasswordDAL();
            return objchangepasswordDL.CheckOldPassword(username);

        }

        public bool ChangePassword(string newPassword)
        {
            ChangePasswordDAL objChangeDAL = new ChangePasswordDAL();
            return objChangeDAL.ChangePassword(newPassword);

        }

    }
}
