
    <%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/TipsMaster.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Styles/GridViewStyleSheet.css" rel="stylesheet" />
    <link href="Styles/CommonStyles.css" rel="stylesheet" />

    <script type="text/javascript">
        $(function () {
            var dashboardData;
            var pageLoad = true;
            //Get Stats tile data
            $.ajax({
                type: "POST",
                url: "/GetDataServices.asmx/GetRequestbyStatus",
                data: JSON.stringify({ Status: 1, Userid: 1003 }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            })
                .done(function (msg) {
                    var data = eval("(" + msg.d + ")");
                    var newData = [];
                    for (var i = 0, l = data.length; i < l; i++) {
                        var o = data[i];
                        newData[i] = [o.Criticality, o.IssueCount];
                    }
                    var chart = c3.generate({
                        bindto: '#charts',
                        data: {
                            columns: newData,
                            type: 'bar'
                        },
                        bar: {
                            width: {
                                ratio: 0.5 // this makes bar width 50% of length between ticks
                            }
                            // or
                            //width: 100 // this makes bar width 100px
                        }
                    });
                })
                .fail(function (msg) {
                    console.log("fail")
                })
                .always(function (msg) {
                });
            GetStatsData();

            $("div[role|='timeframe'] button").click(function (e) {
                GetStatsData();
            });

            function GetStatsData() {
                var timeframe = $("div[role|='timeframe'] button.active").attr("data-timeframe");

                if (timeframe == undefined) {
                    $("div[role|='timeframe'] button").removeClass("active");
                    $($("div[role|='timeframe'] button")[0]).addClass("active");

                    timeframe = $("div[role|='timeframe'] button.active").attr("data-timeframe");
                }

                $.ajax({
                    type: "POST",
                    url: "/GetDataServices.asmx/GetStatsData",
                    data: JSON.stringify({ Timeframe: timeframe }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                })
                    .done(function (msg) {
                        var data = eval("(" + msg.d + ")");
                        dashboardData = data;
                        var Critical = 0;
                        var Urgent = 0;
                        var Routine = 0;

                        if (pageLoad) {
                            pageLoad = false;
                            showDonut("Routine");
                        }

                        $.each(data,
                            function (index, row) {
                                switch (row.PriorityId) {
                                    case 1:
                                        Critical = Critical + row.StatusCount;
                                        break;
                                    case 2:
                                        Urgent = Urgent + row.StatusCount;
                                        break;
                                    case 3:
                                        Routine = Routine + row.StatusCount;
                                        break;

                                    default:
                                }
                            });
                        $($("div.tileStyle h1")[0]).text(Critical);
                        $($("div.tileStyle h1")[1]).text(Urgent);
                        $($("div.tileStyle h1")[2]).text(Routine);
                    })
                    .fail(function (msg) {
                        console.log("fail");
                    })
                    .always(function (msg) {
                    });
            }
            $(".tile").click(function (event) {
                if ($(this).find("h2.animate-text").length > 0) {
                    var selectedPriority = $(this).find("h2.animate-text").text();
                    showDonut(selectedPriority);
                }
            });

            function showDonut(selectedPriority) {
                if (selectedPriority == "") {
                    selectedPriority = 1;
                }
                var donutData = [];
                var i = 0;
                $.each(dashboardData,
                    function (index, row) {
                        if (row.PriorityName == selectedPriority) {
                            donutData[i] = [row.StatusName, row.StatusCount];
                            i++;
                        }
                    });
                var donut = c3.generate({
                    bindto: '#donut',
                    data: {
                        columns: donutData,
                        type: 'donut',
                        onclick: function (d, i) {
                            console.log("onclick", d, i);
                            location.href = "/SearchRequest.aspx?status=" + d.id;
                        },
                        onmouseover: function (d, i) { console.log("onmouseover", d, i); },
                        onmouseout: function (d, i) { console.log("onmouseout", d, i); }
                    },
                    donut: {
                        title: selectedPriority
                    }
                });
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_body" runat="Server">

    <div class="row-fluid" style="text-align: center;">
        <div class="btn-group" role="timeframe" aria-label="...">
            <button type="button" data-timeframe="Month" class="btn btn-default btn-frequency">Month</button>
            <button type="button" data-timeframe="Week" class="btn btn-default btn-frequency">Week</button>
            <button type="button" data-timeframe="Day" class="btn btn-default btn-frequency">Day</button>
        </div>
    </div>

    <div class="wrap">
        <div class="tile">
            <div class="tileStyle">
                <div class="text">
                    <h1>0</h1>
                    <h2 class="animate-text">Critical</h2>
                    <p class="animate-text">... </p>
                    <%--<div class="dots">
                        <span></span>
                        <span></span>
                        <span></span>
                    </div>--%>
                </div>
            </div>
        </div>


        <div class="tile">
            <div class="tileStyle">
                <div class="text">
                    <h1>0</h1>
                    <h2 class="animate-text">Urgent</h2>
                    <p class="animate-text">... </p>
                    <%--<div class="dots">
                        <span></span>
                        <span></span>
                        <span></span>
                    </div>--%>
                </div>
            </div>
        </div>

        <div class="tile">
            <div class="tileStyle">
                <div class="text">
                    <h1>0</h1>
                    <h2 class="animate-text">Routine</h2>
                    <p class="animate-text">... </p>
                    <%-- <div class="dots">
                        <span></span>
                        <span></span>
                        <span></span>
                    </div>--%>
                </div>
            </div>
        </div>
    </div>

    <div style="width: 70%; margin: 0 auto">
        <div class="tile" style="width: 557px; height: 360px; text-align: center; font-size: 2em;">
            <div id="donut"></div>
            <span style="font-weight: bold; font-style: italic;">Percentage of status under prioroty</span> </div>
        <div class="tile" style="padding-left: 10px; width: 557px; height: 360px; text-align: center; font-size: 2em;">
            <div id="charts"></div>
            <span style="font-weight: bold; font-style: italic;">Number of requests per status and Priorty</span></div>
    </div>
</asp:Content>
