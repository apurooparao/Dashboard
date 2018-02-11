<%@ Page Title="" Language="C#" MasterPageFile="~/TipsMaster.master" AutoEventWireup="true" CodeFile="SearchRequest.aspx.cs" Inherits="AddRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/jquery.bootgrid.css" rel="stylesheet" />
    <script src="Scripts/jquery.bootgrid.js"></script>
    <script src="Scripts/jquery.bootgrid.fa.js"></script>

    <script type="text/javascript">
        $(function () {
            var getStatuses = function () {
                $.ajax({
                    type: "POST",
                    url: "GetDataServices.asmx/GetStatuses",
                    contentType: "application/json; charset=utf-8"
                })
                    .done(function (msg) {
                        var data = eval("(" + msg.d + ")");
                        var newData = "";
                        var temp = "";
                        $.each(data,
                            function (index, value) {
                                $("#Statuslist").append($('<option>', {
                                    value: value.StatusID,
                                    text: value.StatusName
                                }));
                            });
                      
                        if (location.href.indexOf("?") > 0) {
                            // var statusText = location.href.substring(location.href.indexOf("?") + 1).split("=")[1].replace("%20", " ");
                            var statusText = getParameterByName('status');
                            var priorityText = getParameterByName('priority');
                        
                            $.each($("#Statuslist option"), function (index, element) {
                                if (element.text == statusText) {
                                    $("#Statuslist").val(element.value);
                                }
                            });                      
                        }
                        getPriorities();
                     //   loadRequests();
                    })
                    .fail(function (msg) {
                        console.log("fail");
                    })
                    .always(function (msg) {
                    });
            }();


            var getPriorities = function () {
                $.ajax({
                    type: "POST",
                    url: "GetDataServices.asmx/GetPriorities",
                    contentType: "application/json; charset=utf-8"
                })
                    .done(function (msg) {
                        var data = eval("(" + msg.d + ")");
                        var newData = "";
                        var temp = "";
                        $.each(data,
                            function (index, value) {
                                $("#Prioritylist").append($('<option>', {
                                    value: value.PriorityID,
                                    text: value.PriorityName
                                }));
                            });

                        if (location.href.indexOf("?") > 0) {
                            // var statusText = location.href.substring(location.href.indexOf("?") + 1).split("=")[1].replace("%20", " ");
                            var statusText = getParameterByName('status');
                            var priorityText = getParameterByName('priority');
                            //alert(statusText);
                            //alert(priorityText);
                            $.each($("#Prioritylist option"), function (index, element) {
                                if (element.text == priorityText) {
                                    $("#Prioritylist").val(element.value);
                                }
                            });
                        }
                        loadRequests();
                    })
                    .fail(function (msg) {
                        console.log("fail");
                    })
                    .always(function (msg) {
                    });
            }();


            //getStatuses();
            function getParameterByName(name, url) {
                if (!url) url = window.location.href;
                name = name.replace(/[\[\]]/g, "\\$&");
                var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                    results = regex.exec(url);
                if (!results) return null;
                if (!results[2]) return '';
                return decodeURIComponent(results[2].replace(/\+/g, " "));
            }
            function loadRequests() {
                //alert('here');
                var wmsid = $("#txtWMSid").val();
             
                if (wmsid != null && wmsid != "") {
                    
              
                    var numbers = /^[0-9]+$/;
                    var reg = /^\d+$/;
                    if (numbers.test(wmsid)) {
                        // alert(wmsid);
                        var tableHeader = "<table id='grid-requested-status' class='table table-condensed table-hover table-striped'><thead><tr><th data-column-id='WMSID' data-type='numeric'>WMSID</th><th data-column-id='PriorityName'>PriorityName</th><th data-column-id='BranchName' data-order='desc'>BranchName</th><th data-column-id='AffectOperation' data-order='desc'>AffectOperation</th><th data-column-id='Scope' data-order='desc'>Scope</th><th data-column-id='SectionName' data-order='desc'>SectionName</th><th data-column-id='Category' data-order='desc'>Category</th><th data-column-id='Requestor' data-order='desc'>Requestor</th><th data-type='date' data-column-id='StatusName' data-order='desc' >Status</th></tr></thead><tbody>$$$$</tbody></table>";
                        $.ajax({
                            type: "POST",
                            url: "GetDataServices.asmx/GetRequestDataByWMSid",
                            contentType: "application/json; charset=utf-8",
                            data: JSON.stringify({ wmsId: wmsid})
                        }).done(function (resp) {
                            var data = eval("(" + resp.d + ")");
                            var rows = "";
                            $.each(data,
                                function (index, row) {
                                    var newRow = "<tr>" +
                                        "<td>" +
                                        row.WMSID +
                                        "</td>" +
                                        "<td>" +
                                        row.Priority +
                                        "</td>" +
                                        "<td>" +
                                        row.BranchName +
                                        "</td>" +
                                        "<td>" +
                                        row.AffectOperation +
                                        "</td>" +
                                        "<td>" +
                                        row.Scope +
                                        "</td>" +
                                        "<td>" +
                                        row.SectionName +
                                        "</td>" +
                                        "<td>" +
                                        row.Category +
                                        "</td>" +
                                        "<td>" +
                                        row.Requestor +
                                        "</td>" +
                                        "<td >" +
                                        row.StatusName +
                                        "</td>" +
                                        "</tr>";
                                    rows = rows + newRow;
                                    //$("#grid-requested-status tr:last").after();
                                });
                           // alert(data.row.Priority);
                            //$.each($("#Prioritylist option"), function (index, element) {
                            //    if (element.text == priorityText) {
                            //        $("#Prioritylist").val(data.row.Priority)
                            //    }
                            //});

                            $('#Statuslist').prop('disabled', 'disabled');
                            $('#Prioritylist').prop('disabled', 'disabled');

                            $("#dRequestStatus").append(tableHeader.replace("$$$$", rows));
                            $("#dRequestStatus").find("table tbody tr").on("click", (function () {

                                var wmsid = parseInt($($(this)[0].cells[0]).text());
                                location.href = "RequestDetails.aspx?request=" + wmsid;
                            }));
                        }).fail().always();
                    }
                    else {
                        alert("Please enter a valid wmsid");
                        //alert(wmsid);
                    }
                }
                else {
                    $('#Statuslist').prop('disabled', false);
                    $('#Prioritylist').prop('disabled', false);
                    var statusId = $("#Statuslist").val();
                    var priorityId = $("#Prioritylist").val();


                    var tableHeader = "<table id='grid-requested-status' class='table table-condensed table-hover table-striped'><thead><tr><th data-column-id='WMSID' data-type='numeric'>WMSID</th><th data-column-id='PriorityName'>PriorityName</th><th data-column-id='BranchName' data-order='desc'>BranchName</th><th data-column-id='AffectOperation' data-order='desc'>AffectOperation</th><th data-column-id='Scope' data-order='desc'>Scope</th><th data-column-id='SectionName' data-order='desc'>SectionName</th><th data-column-id='Category' data-order='desc'>Category</th><th data-column-id='Requestor' data-order='desc'>Requestor</th><th data-type='date' data-column-id='StatusName' data-order='desc' >Status</th></tr></thead><tbody>$$$$</tbody></table>";
                    $.ajax({
                        type: "POST",
                        url: "GetDataServices.asmx/GetRequestDataByStatusAndPriority",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ Status: statusId, Priority: priorityId })
                    }).done(function (resp) {
                        var data = eval("(" + resp.d + ")");
                        var rows = "";
                        $.each(data,
                            function (index, row) {
                                var newRow = "<tr>" +
                                    "<td>" +
                                    row.WMSID +
                                    "</td>" +
                                    "<td>" +
                                    row.Priority +
                                    "</td>" +
                                    "<td>" +
                                    row.BranchName +
                                    "</td>" +
                                    "<td>" +
                                    row.AffectOperation +
                                    "</td>" +
                                    "<td>" +
                                    row.Scope +
                                    "</td>" +
                                    "<td>" +
                                    row.SectionName +
                                    "</td>" +
                                    "<td>" +
                                    row.Category +
                                    "</td>" +
                                    "<td>" +
                                    row.Requestor +
                                    "</td>" +
                                    "<td >" +
                                    row.StatusName +
                                    "</td>" +
                                    "</tr>";
                                rows = rows + newRow;
                                //$("#grid-requested-status tr:last").after();
                            });
                        $("#dRequestStatus").append(tableHeader.replace("$$$$", rows));
                        $("#dRequestStatus").find("table tbody tr").on("click", (function () {

                            var wmsid = parseInt($($(this)[0].cells[0]).text());
                            location.href = "RequestDetails.aspx?request=" + wmsid;
                        }));
                    }).fail().always();
                }
             
            }



            $("#Statuslist").on("change",function (e) {
                $("#dRequestStatus").empty();
                loadRequests();
            });
            $("#Prioritylist").on("change", function (e) {
                $("#dRequestStatus").empty();
                loadRequests();
            });
                $("#txtWMSid").on("keyup", function (e) {
                    $("#dRequestStatus").empty();
                    loadRequests();
                });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_body" runat="Server">

    <div id="dMasterStatus">
        <table>
            
            <tr>
                <td >
        <label class="form-horizontal">Status </label>
        <select id="Statuslist" class="form-control" style="width: 100%">
        </select>
                </td>
                <td style="padding-left: 3%;width:15%">
                    
              <label class="form-horizontal">Priority </label>
        <select id="Prioritylist" class="form-control" style="width: 100%">
        </select>
                </td>
                <td style="width:60%">

                </td>
                
                <td >  
                    <label class="form-horizontal">WMS Id </label>
                    <input type="text" id="txtWMSid" class="form-control" style="width:70%" />
                </td>
            </tr>
        </table>
        


     
        <div id="dRequestStatus"></div>
        <%--<table id="grid-data" class="table table-condensed table-hover table-striped">
            <thead>
            <tr>
                <th data-column-id="id" data-type="numeric">ID</th>
                <th data-column-id="sender">Sender</th>
                <th data-column-id="received" data-order="desc">Received</th>
                <th data-column-id="link" data-formatter="link" data-sortable="false">Link</th>
            </tr>
            </thead>
        </table>--%>

    </div>
</asp:Content>

