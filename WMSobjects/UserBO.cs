using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMSobjects
{
  public  class UserBO
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int RoleID { get; set; }
        public int BranchID { get; set; }
    }
}
