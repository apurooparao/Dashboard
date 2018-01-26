<%@ Page Title="" Language="C#" MasterPageFile="~/TipsMaster.master" AutoEventWireup="true" CodeFile="AdminCategory.aspx.cs" Inherits="AdminCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <link href="Styles/GridViewStyleSheetSwetha.css" rel="stylesheet" />
    <link href="Styles/CommonStylesSwetha.css" rel="stylesheet" />
    <script type="text/javascript">

         function ValidateSection(source, args) {

            if (document.getElementById("<%= cbIsActive.ClientID %>").checked == false) {
                if (confirm('Category will be Inactive. Do you wish to continue ?')) {
                    args.IsValid = true;
                }

                else {
                    args.IsValid = false;
                    return;
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_body" Runat="Server">
      <div>
        <table align="center" style="position: relative; top: 20px; width: 100%;">
            <tr>
                <td style="width: 40%; border: 1px solid #530D43;">
                    <table style="position: relative; width: 100%;">
                        <tr>
                            <td class="tdlabeladmin">
                                <asp:Label ID="lblCategory" runat="server" Text="Category Name"></asp:Label>
                            </td>
                            <td class="tdTextboxadmin">
                                <asp:TextBox ID="txtCategoryName" runat="server" CssClass="textboxadmin"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvInsCategoryName" runat="server" ErrorMessage="Category Name cannot be blank" ControlToValidate="txtCategoryName"
                                    ValidationGroup="Insert" Text="*" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdlabeladmin">
                                <asp:Label ID="lblActive" runat="server" Text="Is Active"></asp:Label>
                            </td>
                            <td style="width: 50%;">
                                <asp:CheckBox ID="cbIsActive" runat="server" CssClass="checkadminstyle" Checked="true"></asp:CheckBox>
                                        <asp:CustomValidator ID="vld_section" ValidationGroup="Insert" ClientValidationFunction="ValidateSection"
                                                    ErrorMessage="Please Check Is Active" runat="server" Display="None" ></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>

                            <td class="tdlabeladmin">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="buttonadmin" ValidationGroup="Insert"  />
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="buttonadmin" ValidationGroup="Insert"  
                                    Visible="false" />

                            </td>
                            <td>
                                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="buttonadmin" />
                            </td>
                        </tr>

                    </table>

                </td>
                <td style="width: 60%; border: 1px solid #530D43;">
                    <table style="position: relative; width: 100%;">
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="datagrid" runat="server" CssClass="mydatagrid" PagerStyle-CssClass="pager"
                                    HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AllowPaging="True" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
                                    EmptyDataText="No Records Found" EmptyDataRowStyle-ForeColor="#ff8000">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                                                <%--<asp:LinkButton ID="lbDelete" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure? want to delete the department.');"
                                        OnClick="btnDelete_Click" />--%>
                                                <asp:Label ID="lblCategoryID" runat="server" Text='<%#Eval("CategoryID") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategoryName" runat="server" Text='<%#Eval("CategoryName") %>' BackColor="Transparent"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Is Active">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="lblIsActive" runat="server" Checked='<%#Eval("IsActive") %>' Enabled="false" BackColor="Transparent"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>

            </tr>
            <tr>
                <td align="center" colspan="2">
                    <br />
                    <asp:Label ID="lblMessage" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="Insert" ForeColor="Red" runat="server" />
                </td>
            </tr>
        </table>
        <input type="hidden" runat="server" id="hidCategoryID" />
    </div>
</asp:Content>

