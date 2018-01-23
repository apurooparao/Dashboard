<%@ Page Title="" Language="C#" MasterPageFile="~/TipsMaster.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Styles/CommonStyles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_body" runat="Server">
    <asp:ScriptManager runat="server" ID="scriptmgr"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdPanelChange" runat="server">
        <ContentTemplate>
            <div>
                <table id="tblmaintable" runat="server" style="width: 90%">
                    <tr >
                        <td  style="text-align: center">
                            <div>
                                <h4>Change Password</h4>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td class="tdlabelchangepassword">
                            <asp:Label ID="lblUserName" runat="server" Text="User Name "></asp:Label>
                        </td>
                        <td class="tdTextboxChangePassword">

                            <asp:Label ID="lblUserNameValue" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdlabelchangepassword">
                            <asp:Label ID="lblOldPassword" runat="server" Text="Old Password "></asp:Label>
                        </td>
                        <td class="tdTextboxChangePassword">
                            <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" class="textboxChangePassword"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtOldPassword" runat="server" ErrorMessage="Old Password cannot be blank" ControlToValidate="txtOldPassword"
                                ValidationGroup="Submit" Text="*" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdlabelchangepassword">
                            <asp:Label ID="lblNewPassword" runat="server" Text="New Password "></asp:Label>
                        </td>
                        <td class="tdTextboxChangePassword">
                            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" class="textboxChangePassword"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtNewPassword" runat="server" ErrorMessage="New Password cannot be blank" ControlToValidate="txtNewPassword"
                                ValidationGroup="Submit" Text="*" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdlabelchangepassword">
                            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password: "></asp:Label>
                        </td>
                        <td class="tdTextboxChangePassword">
                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" class="textboxChangePassword"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtConfirmPassword" runat="server" ErrorMessage="Confirm Password cannot be blank" ControlToValidate="txtConfirmPassword"
                                ValidationGroup="Submit" Text="*" ForeColor="Red">

                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cmptxtConfirmPassword" runat="server" ErrorMessage="Passwords do not match" ControlToValidate="txtConfirmPassword"
                                ControlToCompare="txtNewPassword" ValidationGroup="Submit" Text="*" ForeColor="Red"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdlabelchangepassword"></td>
                        <td class="tdTextboxChangePassword"><span>Password is case sensitive</span></td>
                    </tr>
                    <tr>
                         <td class="tdlabelchangepassword"></td>
                        <td class="tdTextboxChangePassword" colspan="1" style="text-align:left">
                            <asp:Button ID="btnChangePassword" runat="server" CssClass="buttonChangePassword" ValidationGroup="Submit" OnClick="btnChangePassword_Click" Text="Submit" />
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="tdlabelchangepassword"></td>
                        <td class="tdTextboxChangePassword">
                            <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="Submit" ForeColor="Red" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

