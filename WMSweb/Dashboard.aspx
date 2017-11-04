<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/TipsMaster.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="Scripts/jquery-3.1.1.js"></script>
    <%--<script src="Scripts/DataTables/jquery.dataTables.min.js"></script>
    <link href="Content/DataTables/css/jquery.dataTables.min.css" rel="stylesheet" />--%>
    <link href="Styles/GridViewStyleSheet.css" rel="stylesheet" />
    <%--<link href="Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="Content/bootstrap.css" rel="stylesheet" />--%>
    <link href="Styles/CommonStyles.css" rel="stylesheet" />
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_body" runat="Server">
    <asp:ScriptManager runat="server" ID="scrptmgr"></asp:ScriptManager>
    <div class="row-fluid" style="text-align: center;">
        Dashboard for :
        <div class="btn-group" role="group" aria-label="...">
            <button type="button" class="btn btn-default btn-frequency active">Month</button>
            <button type="button" class="btn btn-default btn-frequency">Week</button>
            <button type="button" class="btn btn-default btn-frequency">Day</button>
        </div>
    </div>
    
    <asp:UpdatePanel ID="upanel_full" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <table id="tblstatus" runat="server" style="width: 100%">
                    <tr>
                        <td class="tdlabel">
                            <asp:Label ID="Status" runat="server" Text="Status : "></asp:Label>
                        </td>
                        <td class="tdTextbox">
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdownlistdash" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>

            <table id="tblchart" runat="server" style="width: 100%">
                <tr>
                    <td>
                        <asp:Chart ID="chart_dashboard" CanResize="false" runat="server" Width="500px">
                            <Legends>
                                <asp:Legend Name="Legend2" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                                    LegendItemOrder="ReversedSeriesOrder" BorderColor="Blue" BorderWidth="2"
                                    IsTextAutoFit="False">
                                </asp:Legend>

                            </Legends>

                            <Titles>
                                <asp:Title BackColor="180, 165, 191, 228" BackGradientStyle="TopBottom"
                                    BackHatchStyle="None" Name="OpenIssues"
                                    ForeColor="Black">
                                </asp:Title>

                            </Titles>
                            <Series>
                                <asp:Series Name="Count" ChartArea="ChartArea1" Legend="Legend2" CustomProperties="DrawingStyle=Cylinder"
                                    BorderColor="64, 0, 0, 0" Color="0, 0, 0" MarkerSize="5" XValueMember="Criticality" YValueMembers="IssueCount">
                                    <EmptyPointStyle AxisLabel="0" />
                                </asp:Series>
                                <asp:Series Name="Critical  :  2-4 Hours" ChartArea="ChartArea1" Legend="Legend2" CustomProperties="DrawingStyle=Cylinder"
                                    BorderColor="64, 0, 0, 0" Color="224, 64, 10" MarkerSize="5">
                                    <EmptyPointStyle AxisLabel="0" />
                                </asp:Series>

                                <asp:Series Name="Urgent  : 1-2 Days" ChartArea="ChartArea1" Legend="Legend2" CustomProperties="DrawingStyle=Cylinder"
                                    BorderColor="64, 0, 0, 0" Color="51, 46, 193" MarkerSize="5">

                                    <EmptyPointStyle AxisLabel="0" />

                                </asp:Series>

                                <asp:Series Name="Routine : 1 Week" ChartArea="ChartArea1" Legend="Legend2" CustomProperties="DrawingStyle=Cylinder"
                                    BorderColor="64, 0, 0, 0" Color="44, 155, 32" MarkerSize="5">

                                    <EmptyPointStyle AxisLabel="0" />

                                </asp:Series>

                            </Series>
                            <ChartAreas>

                                <asp:ChartArea Name="ChartArea1" BackColor="64, 165, 191, 228" BackSecondaryColor="White"
                                    BorderColor="64, 64, 64, 64" ShadowColor="Transparent" BackGradientStyle="TopBottom">

                                    <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False" Title="No of Incidents" ArrowStyle="Triangle">
                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                    </AxisY>

                                    <AxisX IsLabelAutoFit="False" LineColor="64, 64, 64, 64" Title="Incident Criticality" ArrowStyle="Triangle"
                                        Interval="1">
                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                  
                </tr>


            </table>
        </ContentTemplate>

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlStatus" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="update_dashboard" runat="server">
        <ContentTemplate>
            <table id="tblSearch" runat="server" style="width: 100%">
                <tr>
                    <td class="tdlabellong">
                        <asp:TextBox runat="server" class="textboxdash" ID="txtSearch" placeholder="(Priority, Scope, Section, Category, Requestor)"> </asp:TextBox>
                    </td>
                    <td class="tdTextbox">
                        <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="buttondash" TabIndex="1" OnClick="btnSearch_Click" />
                    </td>
                    <td class="tdlabel">
                        <div style="text-align: left; width: 100%; color: #530D43; font-weight: bolder">
                            <span>No of Requests :
                    <asp:Label ID="lblCnt" runat="server"></asp:Label>
                            </span>
                        </div>

                    </td>

                </tr>
            </table>
            <div class="table-responsive">
                <asp:GridView ID="grd_requests" runat="server" CssClass="mydatagrid_grid" PagerStyle-CssClass="pager_grid" AllowSorting="True"
                    HeaderStyle-CssClass="header_grid" RowStyle-CssClass="rows_grid" AllowPaging="True" Width="100%" AutoGenerateColumns="False"
                    DataSourceID="WmsDashboardDataSource" EmptyDataText="No Results Found" DataKeyNames="WMSID"
                    OnRowDataBound="grd_requests_RowDataBound" OnSelectedIndexChanged="grd_requests_SelectedIndexChanged">

                    <Columns>

                        <%--                <asp:TemplateField HeaderText="WMS ID" SortExpression="WMSID" HeaderStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblWmsId_grid" runat="server" Text='<%# Bind("WMSID") %>' BackColor="Transparent"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                        <asp:BoundField DataField="WMSID" HeaderText="WMS ID" InsertVisible="False" ReadOnly="True" SortExpression="WMSID" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="PriorityName" HeaderText="Priority" SortExpression="PriorityName" ItemStyle-Width="8%" />
                         <asp:BoundField DataField="BranchName" HeaderText="Branch" SortExpression="BranchName" ItemStyle-Width="10%" />                       
                        <asp:BoundField DataField="AffectOperation" HeaderText="Affect Operation" SortExpression="AffectOperation" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="Scope" HeaderText="Scope" SortExpression="Scope" ItemStyle-Width="8%" />
                        <asp:BoundField DataField="SectionName" HeaderText="Section" SortExpression="SectionName" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" ItemStyle-Width="25%" />
                        <asp:BoundField DataField="Requestor" HeaderText="Requestor" SortExpression="Requestor" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" ReadOnly="True" SortExpression="CreatedDate" ItemStyle-Width="10%" />

                    </Columns>

                    <HeaderStyle CssClass="header_grid" />
                    <PagerStyle CssClass="pager_grid" />
                    <RowStyle CssClass="rows_grid" />

                </asp:GridView>
                <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
                <asp:SqlDataSource ID="WmsDashboardDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:WmsConnection %>" SelectCommand="sp_Dashboard_Grid_Sel"
                    SelectCommandType="StoredProcedure" FilterExpression="PriorityName like '%{0}%' or Scope like '%{0}%'  or SectionName like '%{0}%' 
                or Category like '%{0}%' or Requestor like '%{0}%' ">
                    <FilterParameters>
                        <%--  <asp:ControlParameter Name="WMSID" ControlID="txtSearch" PropertyName="Text" />--%>
                        <asp:ControlParameter Name="PriorityName" ControlID="txtSearch" PropertyName="Text" />
                        <asp:ControlParameter Name="Scope" ControlID="txtSearch" PropertyName="Text" />
                        <asp:ControlParameter Name="SectionName" ControlID="txtSearch" PropertyName="Text" />
                        <asp:ControlParameter Name="Category" ControlID="txtSearch" PropertyName="Text" />
                        <asp:ControlParameter Name="Requestor" ControlID="txtSearch" PropertyName="Text" />
                    </FilterParameters>
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="1" Name="UserId" SessionField="UserId" Type="Int32" />
                        <asp:ControlParameter ControlID="ddlStatus" DefaultValue="1" Name="StatusID" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>

            <div style="margin-top: 2%">
                <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
            </div>
            </div>
       
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

