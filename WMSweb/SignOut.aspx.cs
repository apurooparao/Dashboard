using System;


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