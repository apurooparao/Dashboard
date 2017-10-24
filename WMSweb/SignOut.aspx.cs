using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SignOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strscript = "<script language=javascript>window.top.close();</script>";
        if (!ClientScript.IsStartupScriptRegistered("clientscript"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "clientscript", strscript);
        }
    }
}