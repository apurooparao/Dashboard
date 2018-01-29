using System.Data;
using WMSda;
using WMSobjects;

namespace WMSbl
{
  public   class requestBL
    {
        public int InsertUpdateRequest(requestBO objActivityBo)
        {
            requestDA objRequestDa = new requestDA();
            return objRequestDa.insertUpdateRequest(objActivityBo);
        }

        public DataSet GetDropDownValues(string selectQuery, string table, string condition)
        {
            requestDA objRequestDa = new requestDA();
            return objRequestDa.getDropDownValues(selectQuery, table, condition);
        }

        public DataSet GetRequestbyStatus(int status, int userId)
        {
            requestDA objRequestDa = new requestDA();
            return objRequestDa.getRequestByStatus(status, userId);
        }

        public DataSet GetDashboardInfo(int userId, string timeframe)
        {
            requestDA objRequestDa = new requestDA();
            return objRequestDa.GetDashboardInfo(userId, timeframe);
        }

        public DataSet GetRequestDetail(int wmsId)
        {
            requestDA objRequestDa = new requestDA();
            return objRequestDa.getRequestDetail(wmsId);
        }

        public int Changestatus(statusBO statusBo)
        {
            requestDA objRequestDa = new requestDA();
            return objRequestDa.changeStatus(statusBo);
        }

        public DataSet GetStatusValues(int wmsId)
        {
            requestDA objRequestDa = new requestDA();
            return objRequestDa.getStatusvalues(wmsId);
        }

        public DataTable GetRequestDetailsByStatus(int userId, int status)
        {
            requestDA objRequestDa = new requestDA();
            return objRequestDa.GetRequestDetailsByStatus(userId, status);
        }
        public DataTable GetRequestDetailsByStatusAndPriority(int userId, int status, int priority)
        {
            requestDA objRequestDa = new requestDA();
            return objRequestDa.GetRequestDetailsByStatusAndPriority(userId, status, priority);
        }

        public DataTable GetRequestDataByWmSid(int userId, int wmsId)
        {
            requestDA objRequestDa = new requestDA();
            return objRequestDa.GetRequestDataByWMSid(userId, wmsId);
        }

        public DataTable GetMasterStatus()
        {
            requestDA objRequestDa = new requestDA();
            return objRequestDa.getMasterStatus();
        }


        public DataTable GetMasterPriority()
        {
            requestDA objRequestDa = new requestDA();
            return objRequestDa.getMasterPriority();
        }

        public DataSet GetRequestbyStatusTypes(int userid)
        {
            requestDA objRequestDa = new requestDA();
            return objRequestDa.getRequestByStatusTypes(userid);
        }
    }
}
