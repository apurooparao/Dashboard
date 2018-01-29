using System;
using WMSobjects;

public partial class Dashboard : System.Web.UI.Page
{
    
    private UserBo _userBO;
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