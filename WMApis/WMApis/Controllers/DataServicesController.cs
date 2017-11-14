using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using WMSbl;

namespace WMApis.Controllers
{
    public class DataServicesController : ApiController
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> GetValues()
        {
            return new string[] { "Anil", "Avinash" };
        }

        // GET api/values/5
        [HttpGet]
        public string GetValueById(int id)
        {
            return "Apuroopa";
        }

        //// POST api/values
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        public List<RequestData> GetRequestDataByStatus(int Userid,int Status)
        {
            requestBL _rqstbl = new requestBL();
            List<RequestData> lstRequestData = new List<RequestData>();

            DataTable dtRequest = _rqstbl.GetRequestDetailsByStatus(Userid, Status);
            foreach (DataRow row in dtRequest.Rows)
            {
                lstRequestData.Add(new RequestData
                {
                    WMSID = Convert.ToInt32(row["WMSID"]),
                    PriorityName = Convert.ToString(row["PriorityName"]),
                    BranchName = Convert.ToString(row["BranchName"]),
                    AffectOperation = Convert.ToString(row["AffectOperation"]),
                    Scope = Convert.ToString(row["Scope"]),
                    SectionName = Convert.ToString(row["SectionName"]),
                    Category = Convert.ToString(row["Category"]),
                    Requestor = Convert.ToString(row["Requestor"]),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                });
            }

            return lstRequestData;
        }
    }

    public class RequestData
    {
        public int WMSID { get; set; }
        public string PriorityName { get; set; }
        public string BranchName { get; set; }
        public string AffectOperation { get; set; }
        public string Scope { get; set; }
        public string SectionName { get; set; }
        public string Category { get; set; }
        public string Requestor { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class GridInput
    {
        public int current { get; set; }
        public int rowCount { get; set; }
        public int[] sort { get; set; }
        public string searchPhrase { get; set; }
        public string id { get; set; }

    }
}
