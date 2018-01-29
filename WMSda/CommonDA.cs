using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace WMSda
{
   public class CommonDa
    {
        readonly SqlConnection _sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["WmsConnection"].ConnectionString);
        private SqlDataAdapter _sqlda;

        public DataSet getDropdown(string selectQuery,string tableName,string condition)
        {
            _sqlda = new SqlDataAdapter("select " + selectQuery + " from " + tableName + " where " + condition, _sqlcon);
            DataSet ds = new DataSet();
            _sqlcon.Open();
            _sqlda.Fill(ds);
            _sqlcon.Close();
            return ds;
        }


    }
}
