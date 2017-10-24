<%@ Page Title="" Language="C#" MasterPageFile="~/TipsMaster.master" AutoEventWireup="true" CodeFile="TrackRequest.aspx.cs" Inherits="TrackRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Styles/CommonStyles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_body" runat="Server">
    <table>
        <tr>
            <td class="tdlabel">
                <asp:TextBox runat="server" class="textbox" ID="txtSearch" placeholder="WMS ID"> </asp:TextBox>
            </td>
            <td class="tdTextbox">
                <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="buttondash" TabIndex="1" />
            </td>
        </tr>
    </table>
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lbldetails" runat="server"> WMS ID 1201 reported on 01/01/2007 </asp:Label>
                    <asp:HyperLink runat="server" ID="lnk_details" Text="Details" ForeColor="blue"></asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblScope" runat="server"> Test Scope to be changed later </asp:Label>
                </td>
            </tr>
        </table>

    </div>

    <div class="lightblue">
        <h4>Request Timeline</h4>

        Request Created on 01/01/2017 by test requestor     <br />
        Remarks:
        <br />     <br />
        Assigned/Rejected/Closed on 02/01/2017 by admin
             <br />
        Comments :
        <br />      <br />
        Closed  on 03/01/2017 by test Supervisor
             <br />
        Comments :
     </div>


</asp:Content>

