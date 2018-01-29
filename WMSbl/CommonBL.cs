using System.Data;
using WMSda;

namespace WMSbl
{
    public class CommonBl
    {
        public DataSet GetDropDownValues(string selquery,string tblnm,string cond)
        {
            var cmndl = new CommonDa();
            return cmndl.getDropdown(selquery, tblnm, cond);
             }
    }
}
