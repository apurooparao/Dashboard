using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WMSobjects;

namespace WMSda
{
   
   public class requestDA
    {
        
        readonly SqlConnection _sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["WmsConnection"].ConnectionString);

        public int insertUpdateRequest(requestBO rqstbo)
        {
            try
            {
                var cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = _sqlcon,
                    CommandText = "sp_rqst_InsUpd"
                };
                cmd.Parameters.Add("@BranchID", SqlDbType.Int).Value = rqstbo.branchId;
                cmd.Parameters.Add("@PriorityID", SqlDbType.Int).Value = rqstbo.priorityId;
                cmd.Parameters.Add("@AffectOperation", SqlDbType.NVarChar).Value = rqstbo.affectOperation;
                cmd.Parameters.Add("@Scope", SqlDbType.VarChar).Value = rqstbo.scope;
                cmd.Parameters.Add("@Floor", SqlDbType.Int).Value = rqstbo.floor;
                cmd.Parameters.Add("@SectionID", SqlDbType.Int).Value = rqstbo.sectionId;
                cmd.Parameters.Add("@OtherSection", SqlDbType.VarChar).Value = rqstbo.otherSection;
                cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = rqstbo.categoryId;
                cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = rqstbo.remarks;
                cmd.Parameters.Add("@Requestor", SqlDbType.VarChar).Value = rqstbo.requestor;
                cmd.Parameters.Add("@StatusID", SqlDbType.Int).Value = rqstbo.statusId;
                cmd.Parameters.Add("@AssignedID", SqlDbType.Int).Value = rqstbo.assignedTo;
               // cmd.Parameters.Add("@Flag", SqlDbType.VarChar).Value = rqstbo.flag;

               cmd.Parameters.Add("@WMSID", SqlDbType.VarChar).Value = rqstbo.wmsId;
                cmd.Parameters.Add("@InsUpd_Flag", SqlDbType.Int).Value = rqstbo.InsUpdFlag;
                SqlParameter WMSID_Out = new SqlParameter("@WMSID_Out", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(WMSID_Out);
                _sqlcon.Open();
                cmd.ExecuteNonQuery();
                int result = Convert.ToInt16(WMSID_Out.Value);
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                _sqlcon.Close();
            }
        }

        public DataSet getStatusvalues(int wmsId)
        {
            try
            {
                var cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = _sqlcon,
                    CommandText = "sp_StatusAssign_Sel"
                };
                cmd.Parameters.Add("@wmsId", SqlDbType.Int).Value = wmsId;
                _sqlcon.Open();
                var sqlda = new SqlDataAdapter(cmd);
                var dset = new DataSet();
                sqlda.Fill(dset);
                return dset;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                _sqlcon.Close();
            }
        }

        public DataSet GetDashboardInfo(int UserId, string Timeframe)
        {
            try
            {
                var cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = _sqlcon,
                    CommandText = "USP_GetDashboardInfo"
                };
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                cmd.Parameters.Add("@Timeframe", SqlDbType.VarChar, 5).Value = Timeframe;
                _sqlcon.Open();
                var sqlda = new SqlDataAdapter(cmd);
                var dset = new DataSet();
                sqlda.Fill(dset);
                return dset;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                _sqlcon.Close();
            }
        }

        public int changeStatus(statusBO _statusBO)
        {
            try
            {
                var cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = _sqlcon,
                    CommandText = "sp_Status_Upd"
                };
                cmd.Parameters.Add("@WMSID", SqlDbType.VarChar).Value = _statusBO.wmsId;
                cmd.Parameters.Add("@StatusID", SqlDbType.Int).Value = _statusBO.status;
                cmd.Parameters.Add("@AssignedID", SqlDbType.Int).Value = _statusBO.assignedTo;
                cmd.Parameters.Add("@Comment", SqlDbType.VarChar).Value = _statusBO.comment;

                cmd.Parameters.Add("@CLOSUREFLAG", SqlDbType.Int).Value = _statusBO.closureFlag;
                cmd.Parameters.Add("@MATERIALSUSED", SqlDbType.VarChar).Value = _statusBO.materialsUsed;
                cmd.Parameters.Add("@TEAMMEMBERS", SqlDbType.VarChar).Value = _statusBO.teamMembers;
                cmd.Parameters.Add("@TIMEIN", SqlDbType.DateTime).Value = _statusBO.timeIn;
                cmd.Parameters.Add("@TIMEOUT", SqlDbType.DateTime).Value = _statusBO.timeOut;

                //change after login
                cmd.Parameters.Add("@ModifiedBy", SqlDbType.VarChar).Value = 2;
                //   cmd.Parameters.Add("@InsUpd_Flag", SqlDbType.Int).Value = rqstbo.InsUpdFlag;
                SqlParameter WMSID_Out = new SqlParameter("@WMSID_Out", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(WMSID_Out);
                _sqlcon.Open();
                cmd.ExecuteNonQuery();
                int result = Convert.ToInt16(WMSID_Out.Value);
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                _sqlcon.Close();
            }
        }

        public DataSet getRequestDetail(int wmsId)
        {
            try
            {
                var cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = _sqlcon,
                    CommandText = "sp_getRequestDetail_byId"
                };
                cmd.Parameters.Add("@wmsId", SqlDbType.Int).Value = wmsId;              
                _sqlcon.Open();
                var sqlda = new SqlDataAdapter(cmd);
                var dset = new DataSet();
                sqlda.Fill(dset);                    
                return dset;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                _sqlcon.Close();
            }
        }

        public DataSet getRequestByStatus(int status,int UserID)
        {
            try
            {
                var cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = _sqlcon,
                    CommandText = "sp_Dashboard_Chart_Sel"
                };
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                cmd.Parameters.Add("@StatusID", SqlDbType.Int).Value = status; 

                _sqlcon.Open();

                var sqlda = new SqlDataAdapter(cmd);
                var dset = new DataSet();
                sqlda.Fill(dset);
                // cmd.ExecuteNonQuery();               
                return dset;

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                _sqlcon.Close();
            }
        }

        public DataTable GetRequestDetailsByStatus(int userId, int status)
        {
            DataTable dset = new DataTable();
            try
            {
                var cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = _sqlcon,
                    CommandText = "sp_Dashboard_Grid_Sel"
                };
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;
                cmd.Parameters.Add("@StatusID", SqlDbType.Int).Value = status;

                _sqlcon.Open();

                var sqlda = new SqlDataAdapter(cmd);
                sqlda.Fill(dset);

            }
            catch (Exception ex)
            {
                dset = null;
            }
            finally
            {
                _sqlcon.Close();
            }
            return dset;
        }

        public DataTable getMasterStatus()
        {
            {
                DataTable dset = new DataTable();
                try
                {
                    var cmd = new SqlCommand
                    {
                        CommandType = CommandType.StoredProcedure,
                        Connection = _sqlcon,
                        CommandText = "sp_master_status"
                    };

                    var sqlda = new SqlDataAdapter(cmd);
                    sqlda.Fill(dset);

                }
                catch (Exception ex)
                {
                    dset = null;
                }
                finally
                {
                    _sqlcon.Close();
                }
                return dset;
            }
        }

        public DataSet getDropDownValues(string selectQuery, string table, string condition)
        {
            try
            {
                var cmd = new SqlCommand
                {
                    CommandType = CommandType.Text,
                    Connection = _sqlcon,
                    CommandText = "select " + selectQuery + " from " + table + " where " + condition
                };

                _sqlcon.Open();

                var sqlda = new SqlDataAdapter(cmd);
                var dset = new DataSet();
                sqlda.Fill(dset);
               // cmd.ExecuteNonQuery();               
                return dset;

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                _sqlcon.Close();
            }
        }
    }
}
