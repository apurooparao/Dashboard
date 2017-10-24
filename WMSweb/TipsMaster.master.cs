using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMSbl;

public partial class TipsMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //protected void lnkRequest_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Session["WmsId"] = "";
    //        Session.Remove("WmsId");

    //        Response.Redirect("RequestDetails.aspx");
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}

    protected void lnkSignout_Click(object sender, EventArgs e)
    {
        try
        {
            loginBL _objloginBL = new loginBL();
            _objloginBL.UpdateLoginStatus();
            Session.Remove("UserBO");
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("~/SignOut.aspx");
        }
        catch (Exception ex)
        {
            


        }
    }
}
