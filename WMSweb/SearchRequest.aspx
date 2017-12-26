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
                    url: "/GetDataServices.asmx/GetStatuses",
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
                                    $("#Statuslist").val(element.value)
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
                    url: "/GetDataServices.asmx/GetPriorities",
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
                                    $("#Prioritylist").val(element.value)
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
                var statusId = $("#Statuslist").val();
                var priorityId = $("#Prioritylist").val();

                //var grid = $("#grid-data").bootgrid({
                //    ajax: true,
                //    type: "POST",                    
                //    url: "http://localhost:3232/services/Apis/DataServices/GetRequestDataByStatus/1/1"
                //});

                //$("#grid-data").bootgrid({
                //    ajax: true,
                //    post: function () {
                //        /* To accumulate custom parameter with the request object */
                //        return {
                //            id: "b0df282a-0d67-40e5-8558-c9e93b7befed"
                //        };
                //    },
                //    url: "http://localhost:3232/services/Apis/DataServices/GetRequestDataByStatus/1/1",
                //    formatters: {
                //        "link": function (column, row) {
                //            return "<a href=\"#\">" + column.id + ": " + row.id + "</a>";
                //        }
                //    }
                //});

                var tableHeader = "<table id='grid-requested-status' class='table table-condensed table-hover table-striped'><thead><tr><th data-column-id='WMSID' data-type='numeric'>WMSID</th><th data-column-id='PriorityName'>PriorityName</th><th data-column-id='BranchName' data-order='desc'>BranchName</th><th data-column-id='AffectOperation' data-order='desc'>AffectOperation</th><th data-column-id='Scope' data-order='desc'>Scope</th><th data-column-id='SectionName' data-order='desc'>SectionName</th><th data-column-id='Category' data-order='desc'>Category</th><th data-column-id='Requestor' data-order='desc'>Requestor</th><th data-type='date' data-column-id='CreatedDate' data-order='desc' >CreatedDate</th></tr></thead><tbody>$$$$</tbody></table>";
                $.ajax({
                    type: "POST",
                    url: "/GetDataServices.asmx/GetRequestDataByStatusAndPriority",
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
                                row.PriorityName +
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
                                row.CreatedDate +
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



            $("#Statuslist").on("change",function (e) {
                $("#dRequestStatus").empty();
                loadRequests();
            });
            $("#Prioritylist").on("change", function (e) {
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
                <td>
        <label class="form-horizontal">Status: </label>
        <select id="Statuslist" class="form-control" style="width: 120px">
        </select>
                </td>
                <td>
                    
              <label class="form-horizontal">Priority: </label>
        <select id="Prioritylist" class="form-control" style="width: 120px">
        </select>
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

