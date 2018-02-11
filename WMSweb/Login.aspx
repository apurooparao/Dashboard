<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="Styles/loginstyle.css" rel="stylesheet" />
</head>
<body style="background-image: url(Images/TipsNToes.jpg)">
    <form id="loginform" class="login-form" runat="server" name="login-form">

         <div class="header">
        <h2 style="color:#efe3af">Work Order Management System</h2>
    </div>
    <div class="content">
    
        <asp:TextBox ID="txtUserName" CssClass="input username" placeholder="Username" runat="server"></asp:TextBox>
        <div class="user-icon" style="background-image: url(Images/user-icon.png)"></div>
        <asp:TextBox ID="txtPassword" CssClass="input password" placeholder="Password" TextMode="Password" runat="server"></asp:TextBox>
        <div class="pass-icon" style="background-image: url(Images/pass-icon.png)"></div>
        <div class="footer">
        <asp:Button ID="btnLogin" runat="server"  OnClick="btnLogin_Click" Text="Login" CssClass="button" />
        <asp:Label ID="lblStatus" runat="server" Text="Label" CssClass="label" Visible="false"></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
