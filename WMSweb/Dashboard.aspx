
    <%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/TipsMaster.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Styles/GridViewStyleSheet.css" rel="stylesheet" />
    <link href="Styles/CommonStyles.css" rel="stylesheet" />

    <script type="text/javascript">
        $(function () {

            //
            //Load select controls with master data in dCriterion div


            // below code goes in change event of select controls
            var dashboardData;
            var pageLoad = true;
            //Get Stats tile data
            $.ajax({
                type: "POST",
                url: "/GetDataServices.asmx/GetRequestbyStatusTypes",
             //   data: JSON.stringify({ Status: 1, Userid: 1003 }),
                data: JSON.stringify({  Userid: 1003 }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            })
                .done(function (msg) {
                    var data = eval("(" + msg.d + ")");
                    var newData = [];
                    for (var i = 0, l = data.length; i < l; i++) {
                        var o = data[i];
                        newData[i] = [o.StatusName, o.IssueCount];
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
                        var Open = 0;
                        var Inprogress = 0;
                        var Close = 0;
                        var Rejected = 0;
                    
                        //if (pageLoad) {
                          //  pageLoad = false;
                            showDonut("Open");
                        //}

                        $.each(data,
                            function (index, row) {
                                switch (row.StatusId) {
                                    case 1:
                                        Open = Open + row.StatusCount;
                                        break;
                                    case 2:
                                        Inprogress = Inprogress + row.StatusCount;
                                        break;
                                    case 3:
                                        Close = Close + row.StatusCount;
                                        break;
                                    case 4:
                                        Rejected = Rejected + row.StatusCount;
                                        break;

                                    default:
                                }
                            });
                        $($("div.tileStyle h1")[0]).text(Open);
                        $($("div.tileStyle h1")[1]).text(Inprogress);
                        $($("div.tileStyle h1")[2]).text(Close);
                        $($("div.tileStyle h1")[3]).text(Rejected);
                    })
                    .fail(function (msg) {
                        console.log("fail");
                    })
                    .always(function (msg) {
                    });
            }
            $(".tile").click(function (event) {
                if ($(this).find("h2.animate-text").length > 0) {
                    var selectedStatus = $(this).find("h2.animate-text").text();
                    //  showDonut(selectedPriority);
                    showDonut(selectedStatus);
                }
            });

            function showDonut(selectedStatus) {
                if (selectedStatus == "") {
                    selectedStatus = "Open";
                }
                var donutData = [];
                var i = 0;
                $.each(dashboardData,
                    function (index, row) {
                       
                        if (row.StatusName == selectedStatus) {
                        //    alert(row.StatusName + " // " + selectedStatus);
                            donutData[i] = [row.PriorityName, row.StatusCount];
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
                            location.href = "/SearchRequest.aspx?status=" + selectedStatus + "&priority=" + d.id;
                        },
                        onmouseover: function (d, i) { console.log("onmouseover", d, i); },
                        onmouseout: function (d, i) { console.log("onmouseout", d, i); }
                        
                    },
                    donut: {
                        title:selectedStatus
                      //  title: {
                    //    title: function (d,i) { return d3.select('#donut .c3-chart-arcs-title').node().innerHTML = d.id; }
                      //  }
                    }
                    
                });
              
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_body" runat="Server">
    <div id="dCriterion">

    </div>
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
                    <h2 class="animate-text">Open</h2>
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
                    <h2 class="animate-text">In Progress</h2>
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
                    <h2 class="animate-text">Close</h2>
                    <p class="animate-text">... </p>
                    <%-- <div class="dots">
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
                    <h2 class="animate-text">Rejected</h2>
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
            <span style="font-weight: bold; font-style: italic;">Priority Distribution</span> </div>
        <div class="tile" style="padding-left: 10px; width: 557px; height: 360px; text-align: center; font-size: 2em;">
            <div id="charts"></div>
            <span style="font-weight: bold; font-style: italic;">Requests per status and Priority</span></div>
    </div>
</asp:Content>
