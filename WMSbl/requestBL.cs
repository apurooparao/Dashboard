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
  public   class requestBL
    {
        public int insertUpdateRequest(requestBO objActivityBO)
        {
            requestDA objRequestDA = new requestDA();
            return objRequestDA.insertUpdateRequest(objActivityBO);
        }

        public DataSet getDropDownValues(string selectQuery, string table, string condition)
        {
            requestDA objRequestDA = new requestDA();
            return objRequestDA.getDropDownValues(selectQuery, table, condition);
        }

        public DataSet getRequestbyStatus(int status, int userId)
        {
            requestDA objRequestDA = new requestDA();
            return objRequestDA.getRequestByStatus(status, userId);
        }

        public DataSet GetDashboardInfo(int UserId, string Timeframe)
        {
            requestDA objRequestDA = new requestDA();
            return objRequestDA.GetDashboardInfo(UserId, Timeframe);
        }

        public DataSet GetRequestDetail(int wmsId)
        {
            requestDA objRequestDA = new requestDA();
            return objRequestDA.getRequestDetail(wmsId);
        }

        public int changestatus(statusBO statusBO)
        {
            requestDA objRequestDA = new requestDA();
            return objRequestDA.changeStatus(statusBO);
        }

        public DataSet getStatusValues(int wmsId)
        {
            requestDA objRequestDA = new requestDA();
            return objRequestDA.getStatusvalues(wmsId);
        }

        public DataTable GetRequestDetailsByStatus(int userId, int status)
        {
            requestDA objRequestDA = new requestDA();
            return objRequestDA.GetRequestDetailsByStatus(userId, status);
        }
        public DataTable GetRequestDetailsByStatusAndPriority(int userId, int status, int priority)
        {
            requestDA objRequestDA = new requestDA();
            return objRequestDA.GetRequestDetailsByStatusAndPriority(userId, status, priority);
        }

        public DataTable GetMasterStatus()
        {
            requestDA objRequestDA = new requestDA();
            return objRequestDA.getMasterStatus();
        }


        public DataTable GetMasterPriority()
        {
            requestDA objRequestDA = new requestDA();
            return objRequestDA.getMasterPriority();
        }

        public DataSet getRequestbyStatusTypes(int userid)
        {
            requestDA objRequestDA = new requestDA();
            return objRequestDA.getRequestByStatusTypes(userid);
        }
    }
}
