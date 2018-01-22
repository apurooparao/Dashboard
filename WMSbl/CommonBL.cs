using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMSda;

namespace WMSbl
{
    public class CommonBL
    {
        public DataSet GetDropDownValues(string selquery,string tblnm,string cond)
        {
            CommonDA cmndl = new CommonDA();
            return cmndl.getDropdown(selquery, tblnm, cond);
             }
    }
}
