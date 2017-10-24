<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignOut.aspx.cs" Inherits="SignOut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Signed Out</title>
</head>
<body>
    <form id="form1" runat="server">
  <asp:Label ID="lblMessage" runat="server" ForeColor="#CC3300" Text="You have signed out or Session is timed out. Please close this page for Security reasons."></asp:Label>
    </form>
</body>
</html>
