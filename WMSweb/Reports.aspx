<%@ Page Title="Reports" Language="C#" MasterPageFile="~/TipsMaster.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Reports"  %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_body" runat="Server">
    <link href="Styles/CommonStyles.css" rel="stylesheet" />
    <script src="Scripts/bootstrap-multiselect.js"></script>
    <link href="Content/bootstrap-multiselect.css" rel="stylesheet" />

    <script type="text/javascript">
        $(function () {
            var lstPriority = document.getElementById("<%=lstPriority.ClientID%>");
            $(lstPriority).multiselect({
                includeSelectAllOption: true
            });
            var lstBranch = document.getElementById("<%=lstBranch.ClientID%>");
            $(lstBranch).multiselect({
                includeSelectAllOption: true
            });
            var lstStatus = document.getElementById("<%=lstStatus.ClientID%>");
            $(lstStatus).multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
    <asp:ScriptManager ID="scptmgr" runat="server"></asp:ScriptManager>
    <table style="width: 100%">
        <tr>
            <td class="tdlabelreport">Priority
            </td>
            <td class="tdTextboxreport">
                <asp:ListBox SelectionMode="Multiple" ID="lstPriority" runat="server"></asp:ListBox>
            </td>
      <%--  </tr>
        <tr>--%>
            <td class="tdlabelreport">Branch
            </td>
            <td class="tdTextboxreport">
                <asp:ListBox SelectionMode="Multiple" ID="lstBranch" runat="server"></asp:ListBox>
            </td>
       <%-- </tr>
        <tr>--%>
            <td class="tdlabelreport">Status
            </td>
            <td class="tdTextboxreport">
                <asp:ListBox SelectionMode="Multiple" ID="lstStatus" runat="server"></asp:ListBox>
            </td>
      <%--  </tr>
        <tr>--%>
            <td class="tdlabelreport">
                <asp:Button ID="btnSubmitReport" runat="server" OnClick="btnSubmitReport_Click" ValidationGroup="SubmitReport" Text="Search" CssClass="button" />
            </td>
        </tr>
    </table>

    <asp:Label ID="lblReportMessage" Visible="false" runat="server">
    </asp:Label>

    <rsweb:ReportViewer ID="ReportViewer_RequestDetail" runat="server" Font-Names="Verdana" Width="100%" SizeToReportContent="true">
        <LocalReport ReportPath="Report_requestDetail.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="objRequestDataSource" Name="RequestDataset" />


            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>

    <asp:ObjectDataSource ID="objRequestDataSource" runat="server" SelectMethod="GetData" TypeName="RequestDatasetTableAdapters.sp_Reports_Request_DynamicTableAdapter">
        <SelectParameters>
            <asp:Parameter Name="PRIOIRITYID" Type="String" />
            <asp:Parameter Name="BRANCHID" Type="String" />
            <asp:Parameter Name="STATUSID" Type="String" />
        </SelectParameters> 
    </asp:ObjectDataSource>
    <div style="padding-top: 5%; padding-bottom: 5%" id="divRequestNoResult" runat="server" visible="false">
        <asp:Label ID="lblRequestNoResult" runat="server" Visible="false"></asp:Label>
    </div>
    <asp:ValidationSummary ID="vldSearchRequest" ValidationGroup="SubmitReport" runat="server" ShowMessageBox="true" ShowSummary="false" HeaderText="Please enter all fields" />
</asp:Content>

