using System.Data;
using WMSda;

namespace WMSbl
{
    public class CommonBL
    {
        public DataSet GetDropDownValues(string selquery,string tblnm,string cond)
        {
            var cmndl = new CommonDA();
            return cmndl.getDropdown(selquery, tblnm, cond);
             }
    }
}
