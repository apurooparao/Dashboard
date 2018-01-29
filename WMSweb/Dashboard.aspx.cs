using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using WMSbl;
using WMSobjects;

public partial class Dashboard : System.Web.UI.Page
{
    requestBL _rqstbl;
    UserBo _userBO;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserBO"] == null)
        {
            Response.Redirect("Login.aspx", false);
        }
        else
        {
            if (!IsPostBack)
            {
                Session["WmsId"] = "";
                Session.Remove("WmsId");
                _userBO = new UserBo();
                _userBO = (UserBo)Session["UserBO"];
                Session["UserId"] = _userBO.UserId;
            }

        }
    }
}