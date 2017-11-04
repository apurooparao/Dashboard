<%@ Page Title="" Language="C#" MasterPageFile="~/TipsMaster.master" AutoEventWireup="true" CodeFile="AdminRegion.aspx.cs" Inherits="AdminRegion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="Styles/GridViewStyleSheetSwetha.css" rel="stylesheet" />
    <link href="Styles/CommonStylesSwetha.css" rel="stylesheet" />
    <script type="text/javascript">

        function validate() {
            if (document.getElementById("<%= txtRegionName.ClientID%>").value.trim() == "") {
                alert("Region Name cannot be empty");
                return false;
            }
            else if (document.getElementById("<%= cbIsActive.ClientID %>").checked == false) {
                return confirm('Do you wish to continue');
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_body" Runat="Server">
         <div class="row-fluid">
             <div class="col-sm-5 well">
                 <div class="form-horizontal">
                     <div class="form-group">
                         <asp:Label ID="lblRegion"  class="col-sm-4 control-label" runat="server" Text="Region Name"></asp:Label>
                         <div class="col-sm-6">
                             <asp:TextBox ID="txtRegionName" runat="server" CssClass="form-control"></asp:TextBox>
                         </div>
                     </div>
                     <div class="form-group">
                         <asp:Label ID="lblActive" class="col-sm-4 control-label" runat="server" Text="Is Active"></asp:Label>
                         <div class="col-sm-6">
                             <asp:CheckBox ID="cbIsActive" runat="server"></asp:CheckBox>
                         </div>
                     </div>
                     <div class="form-group">
                         <div class="col-sm-offset-4 col-sm-6">
                             <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary" OnClientClick="javascript:return validate();" />
                             <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="btn btn-primary" OnClientClick="javascript:return validate();" 
                                         Visible="false" />
                             <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-default" />
                         </div>
                     </div>
                     <div>
                         <div class="alert alert-info alert-dismissible" runat="server" Visible="False" role="alert" id="dvRegionAlert">
                             <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                             <strong><asp:Label ID="lblMessage" runat="server" EnableViewState="false"></asp:Label></strong>
                         </div>
                     </div>
                 </div>
             </div>
             <div class="col-sm-7">
                 <asp:GridView ID="datagrid" runat="server" PagerStyle-CssClass="pager" CssClass="table table-striped table-hover"
                     GridLines="None"
                               AllowPaging="True" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
                               EmptyDataText="No Records Found" EmptyDataRowStyle-ForeColor="#ff8000">
                     <Columns>
                         <asp:TemplateField HeaderText="Action">
                             <ItemTemplate>
                                 <asp:LinkButton ID="lbEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                                 <asp:Label ID="lblRegionID" runat="server" Text='<%#Eval("RegionID") %>' Visible="false"></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Region Name">
                             <ItemTemplate>
                                 <asp:Label ID="lblRegionName" runat="server" Text='<%#Eval("RegionName") %>' BackColor="Transparent"></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Is Active">
                             <ItemTemplate>
                                 <asp:CheckBox ID="lblIsActive" runat="server" Checked='<%#Eval("IsActive") %>' Enabled="false" BackColor="Transparent"></asp:CheckBox>
                             </ItemTemplate>
                         </asp:TemplateField>
                     </Columns>
                 </asp:GridView>
             </div>
             <input type="hidden" runat="server" id="hidRegionID" />
         </div>
</asp:Content>

