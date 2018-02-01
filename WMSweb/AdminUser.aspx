<%@ Page Title="Admin User" Language="C#" MasterPageFile="~/TipsMaster.master" AutoEventWireup="true" CodeFile="AdminUser.aspx.cs" Inherits="AdminUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="Styles/GridViewStyleSheetSwetha.css" rel="stylesheet" />
    <link href="Styles/CommonStylesSwetha.css" rel="stylesheet" />
    <script type="text/javascript">
  function ValidateSection(source, args) {

            if (document.getElementById("<%= cbIsActive.ClientID %>").checked == false) {
                if (confirm('user will be Inactive. Do you wish to continue ?')) {
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
        <table align="center" style="position: relative; top: 5%; width: 100%;">
            <tr>
                <td style="width: 30%; border: 1px solid #530D43;">
                    <table style="position: relative; width: 100%;">
                        <tr>
                            <td class="tdlabeladminuser">
                                <asp:Label ID="lblUserName" runat="server" Text="User Name"></asp:Label>
                            </td>
                            <td class="tdTextboxadminuser">
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="textboxadmin"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtUserName" runat="server" ErrorMessage="User Name cannot be blank" ControlToValidate="txtUserName"
                                    ValidationGroup="Insert" Text="*" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" runat="server" id="regUserName" ErrorMessage="User name should not contain special characters or spaces" ControlToValidate="txtUserName"
                                    ValidationGroup="Insert" ValidationExpression="^[a-zA-Z0-9\\s]+$"></asp:RegularExpressionValidator>
                               
                            </td>
                        </tr>
                        <tr>
                            <td class="tdlabeladminuser">
                                <asp:Label ID="lblBranch" runat="server" Text="Branch"></asp:Label>
                            </td>
                            <td class="tdTextboxadminuser">
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="textboxadmin"></asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="rfvRegion" runat="server" ControlToValidate="ddlBranch" InitialValue="0" 
                                    ErrorMessage="Please select a Branch" Display="None" ValidationGroup="Insert" Text="*" ForeColor="Red" />
                            </td>
                        </tr>
                            <tr>
                            <td class="tdlabeladminuser">
                                <asp:Label ID="lblRole" runat="server" Text="Role"></asp:Label>
                            </td>
                            <td class="tdTextboxadminuser">
                                <asp:DropDownList ID="ddlRole" runat="server" CssClass="textboxadmindash"></asp:DropDownList>
                                   <asp:RequiredFieldValidator ID="rfvRole" runat="server" ControlToValidate="ddlRole" InitialValue="0" 
                                    ErrorMessage="Please select a Role" Display="None" ValidationGroup="Insert" Text="*" ForeColor="Red" />
                            </td>
                        </tr>
                         <tr>
                            <td class="tdlabeladminuser">
                                <asp:Label ID="lblEmail" runat="server" Text="Email Id"></asp:Label>
                            </td>
                            <td class="tdTextboxadminuser">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="textboxadmindash"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Email cannot be blank" ControlToValidate="txtEmail"
                                    ValidationGroup="Insert" Text="*" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" runat="server" id="regEmail" ErrorMessage="Please enter valid email" ControlToValidate="txtEmail"
                                    ValidationGroup="Insert" ValidationExpression="[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                         <tr>
                            <td class="tdlabeladminuser">
                                <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label>
                            </td>
                            <td class="tdTextboxadminuser">
                                <asp:TextBox ID="txtPhone" runat="server" CssClass="textboxadmin"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ErrorMessage="Phone cannot be blank" ControlToValidate="txtPhone"
                                    ValidationGroup="Insert" Text="*" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                                  <asp:RegularExpressionValidator Display="None" runat="server" id="regPhone" ErrorMessage="Please enter valid phone" ControlToValidate="txtPhone"
                                    ValidationGroup="Insert" ValidationExpression="^(?:\+971|00971|0)?(?:50|51|52|55|56|2|3|4|6|7|9)\d{7}$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdlabeladminuser">
                                <asp:Label ID="lblActive" runat="server" Text="Is Active" ></asp:Label>
                            </td>
                            <td style="width: 50%;">
                                <asp:CheckBox ID="cbIsActive" Checked="true" runat="server" CssClass="checkadminstyle" ></asp:CheckBox>
                                     <asp:CustomValidator ID="vld_section" ValidationGroup="Insert" ClientValidationFunction="ValidateSection"
                                                    ErrorMessage="Please Check Is Active" runat="server" Display="None" ></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>

                            <td class="tdlabeladmin">
                               

                            </td>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="buttonadmin" ValidationGroup="Insert"  />
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="buttonadmin" ValidationGroup="Insert"
                                            Visible="false" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="buttonadmin" />
                            </td>
                        </tr>

                    </table>

                </td>
                <td style="width: 70%; border: 1px solid #530D43;">
                    <table style="position: relative; width: 100%;">
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="datagrid" runat="server" CssClass="mydatagrid" PagerStyle-CssClass="pager"
                                    HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AllowPaging="True" OnPageIndexChanging="datagrid_PageIndexChanging"
                                     Width="100%" AllowSorting="True" AutoGenerateColumns="False"
                                    EmptyDataText="No Records Found" EmptyDataRowStyle-ForeColor="#ff8000">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbEdit" runat="server" Text="Edit" OnClick="lbEdit_Click" />
                                                <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserID") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName") %>' BackColor="Transparent"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branch">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("BranchName") %>' BackColor="Transparent"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Role">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRole" runat="server" Text='<%#Eval("RoleName") %>' BackColor="Transparent"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Phone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPhone" runat="server" Text='<%#Eval("PhoneNumber") %>' BackColor="Transparent"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("EmailID") %>' BackColor="Transparent"></asp:Label>
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
                    <asp:ValidationSummary ID="vldUser" ValidationGroup="Insert" ForeColor="Red" runat="server" />
                </td>

            </tr>
            </table>
            <input type="hidden" runat="server" id="hidUserId" />
         </div>
</asp:Content>

