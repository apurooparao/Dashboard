using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMSda
{
   public class CommonDA
    {
        readonly SqlConnection _sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["WmsConnection"].ConnectionString);
        SqlDataAdapter sqlda;

        public DataSet getDropdown(string SelectQuery,string TableName,string Condition)
        {
            sqlda = new SqlDataAdapter("select " + SelectQuery + " from " + TableName + " where " + Condition, _sqlcon);
            DataSet ds = new DataSet();
            _sqlcon.Open();
            sqlda.Fill(ds);
            _sqlcon.Close();
            return ds;
        }


    }
}
