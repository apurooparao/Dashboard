<%@ Page Title="Request Details" Language="C#" MasterPageFile="~/TipsMaster.master" AutoEventWireup="true" CodeFile="RequestDetails.aspx.cs" Inherits="RequestDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Styles/CommonStyles.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.1.1.min.js"></script>
    <script src="Scripts/jquery.timepicker.min.js"></script>
    <link href="Content/jquery.timepicker.css" rel="stylesheet" />
    <script type="text/javascript">
        function datepickershow() {
            var textbox = '<%=txtIntime.ClientID%>';
                                                          $('#' + textbox).timepicker({ 'step': 15 });
                                                          var textbox2 = '<%=txtOutTime.ClientID%>';
                                                          $('#' + textbox2).timepicker({ 'step': 15 });
                                                      };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_body" runat="Server">
    <asp:ScriptManager runat="server" ID="scriptmgr"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updatepanelmain">
        <ContentTemplate>

            <table id="tblmaintable" runat="server" style="width: 100%">
                <tr>
                    <td style="width: 55%;">

                        <asp:FormView ID="Form_Request" runat="server" DefaultMode="ReadOnly" Width="100%" DataKeyNames="WMSID"
                            OnDataBound="Form_Request_DataBound" OnItemInserting="Form_Request_ItemInserting" OnItemUpdating="Form_Request_ItemUpdating"
                            OnModeChanging="Form_Request_ModeChanging">


                            <ItemTemplate>
                                <div class="tblnewstyle">
                                    <script>
                                        function ValidateCheckBox(source, args) {
                                            //alert('category');
                                            var chkListModules = document.getElementById('<%=Form_Request.FindControl("chkCategoryEdit").ClientID%>');

                                            var chkListinputs = chkListModules.getElementsByTagName("input");
                                            for (var i = 0; i < chkListinputs.length; i++) {
                                                if (chkListinputs[i].checked) {
                                                    args.IsValid = true;
                                                    return;
                                                }
                                            }
                                            args.IsValid = false;
                                        }

                                        function ValidateSection(source, args) {
                                            //alert('section');
                                            var ddllist = document.getElementById('<%=Form_Request.FindControl("ddlSectionEdit").ClientID%>');

                                            var othersection = document.getElementById('<%=Form_Request.FindControl("txtOtherSectionEdit").ClientID%>');
                                            if (ddllist.options[ddllist.selectedIndex].innerHTML == "Others" && othersection.value.trim() == "") {
                                                args.IsValid = false;
                                            }
                                            else {
                                                args.IsValid = true;
                                                return;
                                            }
                                        }

                                    </script>

                                    <table id="tblnewrequest" runat="server" style="min-height: 330px;">
                                        <tr class="form-top" style="height: 4%">
                                            <%--  <td colspan="4" style="text-align: left; background: #424242; color: #fff; padding-left: 5%; font-family: Calibri; font-size: 15px;">
                                                <strong>Request Details</strong>
                                            </td>--%>
                                            <td colspan="4" style="text-align: center">
                                                <div>
                                                    <h4>Request Details</h4>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr style="line-height: 200%">
                                            <td class="tdlabel">
                                                <asp:Label ID="lblPriority" runat="server" Text="Priority : "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <%--  <asp:DropDownList ID="ddlPriority" runat="server" CssClass="dropdownlistnew">
                            </asp:DropDownList>--%>
                                                <asp:Label ID="lblPriorityValue" runat="server" Text='<%# Bind("PriorityName") %>'></asp:Label>
                                            </td>
                                            <td class="tdlabel">
                                                <asp:Label ID="lblAffecting" runat="server" Text="Affecting Operation: "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:Label ID="lblAffectingValue" runat="server" Text='<%# Bind("AffectOperation") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="line-height: 200%">
                                            <td class="tdlabel">
                                                <asp:Label ID="lblScope" runat="server" Text="Scope Of Work : "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:Label ID="lblScopeValue" runat="server" Text='<%# Bind("Scope") %>'></asp:Label>
                                            </td>


                                            <td class="tdlabel">
                                                <asp:Label ID="lblFloor" runat="server" Text="Floor: "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:Label ID="lblFloorValue" runat="server" Text='<%# Bind("Floor") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="line-height: 200%">
                                            <td class="tdlabel">
                                                <asp:Label ID="lblSection" runat="server" Text="Section / Area : "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:Label ID="lblSectionValue" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                                            </td>
                                            <td class="tdlabel">
                                                <asp:Label ID="lblOtherSection" runat="server" Text="If Others : "></asp:Label>
                                                <asp:Label ID="ddlSectionEdit" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="txtOtherSectionEdit" runat="server" Visible="false"></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <%--<asp:TextBox ID="txtOtherSection" runat="server" class="textbox"></asp:TextBox></td>--%>
                                                <asp:Label ID="lblOtherSectionValue" runat="server" Text='<%# Bind("OtherSection") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="line-height: 200%">
                                            <td class="tdlabel">
                                                <asp:Label ID="lblCategory" runat="server" Text="Category : "></asp:Label>
                                                <asp:Label ID="chkCategoryEdit" runat="server" Visible="false"></asp:Label>

                                            </td>
                                            <td class="tdTextbox">
                                                <%--<asp:CheckBoxList ID="chkCategory" runat="server" CssClass="checkstyle">
                            </asp:CheckBoxList>--%>
                                                <asp:Label ID="lblCategoryValue" runat="server" Text='<%# Bind("Category") %>'></asp:Label>
                                            </td>

                                            <td class="tdlabel">
                                                <asp:Label ID="lblRequestor" runat="server" Text="Requestor Name : "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:Label ID="lblRequestorValue" runat="server" Text='<%# Bind("Requestor") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdlabel">
                                                <asp:Label ID="lblRemarks" runat="server" Text="Remarks : "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:Label ID="lblRemarksValue" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="line-height: 200%">
                                            <td></td>

                                            <td class="tdTextbox">
                                                <asp:Button ID="btnEditRequest" Text="Edit" runat="server" CssClass="button" CommandName="Edit" />
                                            </td>

                                            <%--   <td class="tdTextbox">
                                    <asp:Button ID="btnNewRequest" Text="New Request" runat="server" CssClass="button" CommandName="New" />
                                </td>--%>
                                        </tr>

                                    </table>
                                </div>
                            </ItemTemplate>
                            <InsertItemTemplate>
                                <div class="tblnewstyle">
                                    <script>
                                        function ValidateCheckBox(source, args) {
                                            //alert('category');
                                            var chkListModules = document.getElementById('<%=Form_Request.FindControl("chkCategoryEdit").ClientID%>');

                                            var chkListinputs = chkListModules.getElementsByTagName("input");
                                            for (var i = 0; i < chkListinputs.length; i++) {
                                                if (chkListinputs[i].checked) {
                                                    args.IsValid = true;
                                                    return;
                                                }
                                            }
                                            args.IsValid = false;
                                        }

                                        function ValidateSection(source, args) {
                                            //alert('section');
                                            var ddllist = document.getElementById('<%=Form_Request.FindControl("ddlSectionEdit").ClientID%>');

                                            var othersection = document.getElementById('<%=Form_Request.FindControl("txtOtherSectionEdit").ClientID%>');
                                            if (ddllist.options[ddllist.selectedIndex].innerHTML == "Others" && othersection.value.trim() == "") {
                                                args.IsValid = false;
                                            }
                                            else {
                                                args.IsValid = true;
                                                return;
                                            }
                                        }

                                    </script>
                                    <table id="tblInsertRequest" runat="server" style="min-height: 330px;">
                                        <tr class="form-top" style="height: 4%">
                                            <%-- <td colspan="4" style="text-align: left; background: #424242; color: #fff; padding-left: 5%; font-family: Calibri; font-size: 15px;">
                                                <strong>Request Details</strong>
                                            </td>--%>
                                            <td colspan="4" style="text-align: center">
                                                <div>
                                                    <h4>Request Details</h4>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdlabel">
                                                <asp:Label ID="lblPriorityEdit" runat="server" Text="Priority : "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:DropDownList ID="ddlPriorityEdit" runat="server" CssClass="dropdownlistnew">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="tdlabel">
                                                <asp:Label ID="lblAffectingEdit" runat="server" Text="Affecting Operation: "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:DropDownList ID="ddlAffectingEdit" runat="server" CssClass="dropdownlistnew">
                                                    <asp:ListItem Value="No" Text="No" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdlabel">
                                                <asp:Label ID="lblScopeEdit" runat="server" Text="Scope Of Work : "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:TextBox ID="txtScopeEdit" runat="server" class="textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="vld_scope" ValidationGroup="submit" runat="server"
                                                    ErrorMessage="Please enter Scope" ControlToValidate="txtScopeEdit" Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="tdlabel">
                                                <asp:Label ID="lblFloorEdit" runat="server" Text="Floor: "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:DropDownList ID="ddlFloorEdit" runat="server" CssClass="dropdownlistnew">
                                                    <asp:ListItem Value="0" Text="Ground" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="First"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Second"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Third"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Fourth"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="Fifth"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdlabel">
                                                <asp:Label ID="lblSectionEdit" runat="server" Text="Section / Area : "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:DropDownList ID="ddlSectionEdit" runat="server" CssClass="dropdownlistnew">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="tdlabel">
                                                <asp:Label ID="lblOtherSectionEdit" runat="server" Text="If Others : "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">

                                                <asp:TextBox ID="txtOtherSectionEdit" runat="server" class="textbox"></asp:TextBox>
                                                <asp:CustomValidator ID="vld_section" ValidationGroup="submit" ClientValidationFunction="ValidateSection"
                                                    ErrorMessage="Please enter Other section value" runat="server" Display="None"></asp:CustomValidator>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="tdlabel">
                                                <asp:Label ID="lblCategoryEdit" runat="server" Text="Category : "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:CheckBoxList ID="chkCategoryEdit" runat="server" CssClass="checkstyle">
                                                </asp:CheckBoxList>
                                                <asp:CustomValidator ID="vld_checkbox" ValidationGroup="submit" ClientValidationFunction="ValidateCheckBox"
                                                    ErrorMessage="Please select at least one Category" runat="server" Display="None"></asp:CustomValidator>

                                            </td>

                                            <td class="tdlabel">
                                                <asp:Label ID="lblRequestorEdit" runat="server" Text="Requestor Name : "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:TextBox ID="txtRequestorEdit" runat="server" class="textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="vld_requestor" ValidationGroup="submit" runat="server"
                                                    ErrorMessage="Please enter requestor name" ControlToValidate="txtRequestorEdit" Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdlabel">
                                                <asp:Label ID="lblRemarksEdit" runat="server" Text="Remarks : "></asp:Label>
                                            </td>
                                            <td class="tdTextbox" colspan="2">
                                                <asp:TextBox ID="txtRemarksEdit" runat="server" TextMode="MultiLine" class="textboxlong"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td></td>

                                            <td class="tdTextbox">
                                                <asp:Button ID="btnInsert" Text="Submit" runat="server" CssClass="button" ValidationGroup="submit" CommandName="Insert" />
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="button" CommandName="Cancel" />
                                            </td>
                                        </tr>

                                    </table>

                                </div>
                            </InsertItemTemplate>
                            <EditItemTemplate>
                                <div class="tblnewstyle">
                                    <script>
                                        function ValidateCheckBox(source, args) {

                                            var chkListModules = document.getElementById('<%=Form_Request.FindControl("chkCategoryEdit").ClientID%>');

                                            var chkListinputs = chkListModules.getElementsByTagName("input");
                                            for (var i = 0; i < chkListinputs.length; i++) {
                                                if (chkListinputs[i].checked) {
                                                    args.IsValid = true;
                                                    return;
                                                }
                                            }
                                            args.IsValid = false;
                                        }

                                        function ValidateSection(source, args) {

                                            var ddllist = document.getElementById('<%=Form_Request.FindControl("ddlSectionEdit").ClientID%>');

                                            var othersection = document.getElementById('<%=Form_Request.FindControl("txtOtherSectionEdit").ClientID%>');

                                            if (ddllist.options[ddllist.selectedIndex].innerHTML == "Others" && othersection.value.trim() == "") {
                                                args.IsValid = false;
                                            }
                                            else {
                                                args.IsValid = true;
                                                return;
                                            }
                                        }

                                    </script>
                                    <table id="tblInsertRequest" runat="server" style="min-height: 330px;">

                                        <tr class="form-top" style="height: 4%">

                                            <%--   <td colspan="4" style="text-align: left; background: #424242; color: #fff; padding-left: 5%; font-family: Calibri; font-size: 15px;">
                                                <strong>Request Details</strong>

                                            </td>--%>
                                            <td colspan="4" style="text-align: center">
                                                <div>
                                                    <h4>Request Details</h4>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdlabel">
                                                <asp:Label ID="lblPriorityEdit" runat="server" Text="Priority : "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:Label runat="server" ID="lblPriorityEditValue" Text='<%# Bind("PriorityName") %>' Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlPriorityEdit" runat="server" CssClass="dropdownlistnew">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="tdlabel">
                                                <asp:Label ID="lblAffectingEdit" runat="server" Text="Affecting Operation: "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:Label runat="server" ID="lblAffectingEditValue" Text='<%# Bind("AffectOperation") %>' Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlAffectingEdit" runat="server" CssClass="dropdownlistnew">
                                                    <asp:ListItem Value="No" Text="No" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdlabel">
                                                <asp:Label ID="lblScopeEdit" runat="server" Text="Scope Of Work : "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:TextBox ID="txtScopeEdit" runat="server" class="textbox" Text='<%# Bind("Scope") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="vld_scope" ValidationGroup="submit" runat="server"
                                                    ErrorMessage="Please enter Scope" ControlToValidate="txtScopeEdit" Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="tdlabel">
                                                <asp:Label ID="lblFloorEdit" runat="server" Text="Floor: "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:Label runat="server" ID="lblFloorEditValue" Text='<%# Bind("Floor") %>' Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlFloorEdit" runat="server" CssClass="dropdownlistnew">
                                                    <asp:ListItem Value="0" Text="Ground" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="First"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Second"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Third"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Fourth"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="Fifth"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdlabel">
                                                <asp:Label ID="lblSectionEdit" runat="server" Text="Section / Area : "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:Label runat="server" ID="lblSectionEditValue" Text='<%# Bind("SectionName") %>' Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlSectionEdit" runat="server" CssClass="dropdownlistnew">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="tdlabel">
                                                <asp:Label ID="lblOtherSectionEdit" runat="server" Text="If Others : "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:TextBox ID="txtOtherSectionEdit" runat="server" class="textbox" Text='<%# Bind("OtherSection") %>'> ></asp:TextBox>
                                                <asp:CustomValidator ID="vld_section" ValidationGroup="submit" ClientValidationFunction="ValidateSection"
                                                    ErrorMessage="Please enter Other section value" runat="server" Display="None"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdlabel">
                                                <asp:Label ID="lblCategoryEdit" runat="server" Text="Category : "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:Label runat="server" ID="lblCategoryEditValue" Text='<%# Bind("Category") %>' Visible="false"></asp:Label>
                                                <asp:CheckBoxList ID="chkCategoryEdit" runat="server" CssClass="checkstyle">
                                                </asp:CheckBoxList>
                                                <asp:CustomValidator ID="vld_checkbox" ValidationGroup="submit" ClientValidationFunction="ValidateCheckBox"
                                                    ErrorMessage="Please select at least one Category" runat="server" Display="None"></asp:CustomValidator>
                                            </td>

                                            <td class="tdlabel">
                                                <asp:Label ID="lblRequestorEdit" runat="server" Text="Requestor Name : "></asp:Label>
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:TextBox ID="txtRequestorEdit" runat="server" class="textbox" Text='<%# Bind("Requestor") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="vld_requestor" ValidationGroup="submit" runat="server"
                                                    ErrorMessage="Please enter requestor name" ControlToValidate="txtRequestorEdit" Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdlabel">
                                                <asp:Label ID="lblRemarksEdit" runat="server" Text="Remarks : "></asp:Label>
                                            </td>
                                            <td class="tdTextbox" colspan="2">
                                                <asp:TextBox ID="txtRemarksEdit" runat="server" TextMode="MultiLine" class="textboxlong" Text='<%# Bind("Remarks") %>'></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td></td>
                                            <td class="tdTextbox">
                                                <asp:Button ID="btnUpdate" Text="Submit" runat="server" CssClass="button" ValidationGroup="submit" CommandName="Update" />
                                            </td>
                                            <td class="tdTextbox">
                                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="button" CommandName="Cancel" />
                                            </td>
                                        </tr>

                                    </table>

                                </div>
                            </EditItemTemplate>
                        </asp:FormView>
                        <div id="validationcontrols_formview">
                            <asp:ValidationSummary ID="vldsummary" runat="server" ValidationGroup="submit" CssClass="validationsummary" />
                        </div>

                    </td>

                    <td style="width: 44%; padding-left: 1%;">
                        <div id="timelinediv" runat="server" visible="true">
                            <table id="tblTimeLine" runat="server" class="tblnewstyle" style="min-height: 330px;">
                                <tr class="form-top" style="height: 4%">
                                    <%--  <td colspan="6" style="text-align: left; background: #424242; color: #fff; padding-left: 5%; font-family: Calibri; font-size: 15px;">
                                        <strong>Request Timeline </strong>
                                    </td>--%>
                                    <td colspan="4" style="text-align: center">
                                        <div>
                                            <h4>Request Timeline</h4>
                                        </div>
                                    </td>
                                </tr>
                                <tr style="height: 96%">
                                    <td class="tdlabel">
                                        <asp:Label ID="lblWmsId" runat="server" Text="WMS ID : "></asp:Label>
                                    </td>
                                    <td class="tdTextbox">
                                        <asp:Label ID="lblWmsIdValue" runat="server"></asp:Label>
                                    </td>
                                    <td class="tdlabel">
                                        <asp:Label ID="lblCurrentStatus" runat="server" Text="Current Status : "></asp:Label>
                                    </td>

                                    <td class="tdTextbox">
                                        <asp:Label ID="lblCurrentStatusValue" runat="server" Text="Open"></asp:Label>
                                    </td>
                                </tr>


                            </table>

                        </div>

                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="margin-top: 0.5%">
                            <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">

                        <div id="statusdiv" runat="server" visible="false">
                            <table id="tblnewrequest" runat="server" class="tblnewstylestatus" style="margin-top: 0.5%;">
                                <tr class="form-top">
                                    <%-- <td colspan="4" style="text-align: left; background: #424242; color: #fff; padding-left: 5%; font-family: Calibri; font-size: 15px;">
                                        <strong>Status Details</strong>
                                    </td>--%>
                                    <td colspan="6" style="text-align: center">

                                        <div>
                                            <h4>Status Details</h4>
                                        </div>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdlabel">
                                        <asp:Label ID="lblChangeStatus" runat="server" Text="Change Status : "></asp:Label>
                                    </td>
                                    <td class="tdTextbox">
                                        <asp:DropDownList ID="ddlChangeStatus" AutoPostBack="true" runat="server"
                                            CssClass="dropdownlistnew" OnSelectedIndexChanged="ddlChangeStatus_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_changestatus" ValidationGroup="changestatus" runat="server" ErrorMessage="Please select new status"
                                            ControlToValidate="ddlChangeStatus" Display="None" InitialValue="Select Status"></asp:RequiredFieldValidator>

                                    </td>
                                    <td class="tdlabel">
                                        <asp:Label ID="lblAssignTo" runat="server" Text="Assign To : "></asp:Label>
                                    </td>

                                    <td class="tdTextbox">
                                        <asp:DropDownList ID="ddlAssignTo" runat="server" CssClass="dropdownlistnew"></asp:DropDownList>
                                    </td>
                                    <td class="tdlabel">
                                        <asp:Label ID="lblAdminComment" runat="server" Text="Comment : "></asp:Label>
                                    </td>
                                    <td class="tdTextbox" colspan="2">
                                        <asp:TextBox ID="txtAdminComment" runat="server" class="textboxlong" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_admincomment" ValidationGroup="changestatus" runat="server" ErrorMessage="Please enter a comment"
                                            ControlToValidate="txtAdminComment" Display="None"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                                <tr id="trclosure" runat="server">

                                    <td class="tdlabel">
                                        <asp:Label ID="lblMaterialsUsed" runat="server" Text="Materials Used : "></asp:Label>
                                    </td>
                                    <td class="tdTextbox">
                                        <asp:TextBox ID="txtMaterialsUsed" runat="server" class="textboxlong" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td class="tdlabel">
                                        <asp:Label ID="lblTeamMembers" runat="server" Text="Team Members : "></asp:Label>
                                    </td>
                                    <td class="tdTextbox">
                                        <asp:TextBox ID="txtTeamMembers" runat="server" class="textboxlong" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td class="tdTextboxTime">
                                        <asp:TextBox ID="txtIntime" CssClass="textboxtime" runat="server" placeholder="In Time"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtIntime" ValidationGroup="changestatus" runat="server" ErrorMessage="Please enter In time"
                                            ControlToValidate="txtIntime" Display="None" Enabled="false"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="tdTextboxTime">
                                        <asp:TextBox ID="txtOutTime" CssClass="textboxtime" runat="server" placeholder="Out Time"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtOutTime" ValidationGroup="changestatus" runat="server" ErrorMessage="Please enter Out time"
                                            ControlToValidate="txtOutTime" Display="None" Enabled="false"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rgvtxtOutTime" ControlToValidate="txtOutTime" ValidationGroup="changestatus" runat="server"
                                            ErrorMessage="Please enter valid Out time hh:mm tt" Display="None" ValidationExpression="[0-2]{0,1}[0-9]:?[0-5][0-9]([aA][mM]|[pP][mM])" Enabled="false">
                                        </asp:RegularExpressionValidator>
                                    </td>

                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td class="tdTextbox">
                                        <asp:Button ID="btnSubmitStatus" Text="Save" runat="server" CssClass="button" ValidationGroup="changestatus" OnClick="btnSubmitStatus_Click" />

                                    </td>

                                </tr>
                            </table>
                        </div>
                        <div id="validationcontrols_status">
                            <asp:ValidationSummary ID="vld_status" runat="server" ValidationGroup="changestatus" CssClass="validationsummary" />
                        </div>

                    </td>
                </tr>

            </table>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepanelmain">
        <ProgressTemplate>
            <center>
                <img id="Img1" src="Images/user-icon.png" runat="server" alt="" />
            </center>
        </ProgressTemplate>
    </asp:UpdateProgress>


</asp:Content>

