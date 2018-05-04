<%@ Page Title="" Language="C#" MasterPageFile="~/MP/AdminMasterPage.master" AutoEventWireup="true" CodeFile="admin_dashboard.aspx.cs" Inherits="admin_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <script src="plugins/animateNumber/jquery.animateNumber.min.js"></script>
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/data.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script>

        var DateFilter = {
            startDate: null,
            endDate: null
        };

        function getDateforChart(value) {

            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!
            var yyyy = today.getFullYear();
            var startdateStr = yyyy + "-" + mm + "-" + dd;


            switch (value) {
                case 'day': alert(startdateStr);
                    $('#startDate').val(startdateStr);
                    $('#endDate').val(startdateStr);
                    loadGraphs();
                    break;
                case 'week': alert('filter by week');
                    $('#startDate').val(startdateStr);

                    $('#endDate').val(enddateStr);
                    break;
                case 'month': alert('filter by month');
                    $('#startDate').val(startdateStr);
                    break;
            }
        }


        function loadSettings() {
            var RequestsNumber = document.getElementById("<%=HiddenRequestsCounter.ClientID %>").innerHTML;
            var AttenderFormsNumber = document.getElementById("<%=HiddenAttendenceFormsCounter.ClientID %>").innerHTML;

            $('#requestsCounter').animateNumber({ number: RequestsNumber });
            $('#attendenceCounter').animateNumber({ number: AttenderFormsNumber });
            $('#counter1').animateNumber({ number: 162 });
            $('#counter2').animateNumber({ number: 43 });
            loadGraphs()
            return false;
        };

        $(function () {
            $("#startDate").datepicker(
                { dateFormat: 'yy-mm-dd' }
                );
        });
        $(function () {
            $("#endDate").datepicker(
                { dateFormat: 'yy-mm-dd' }
                );
        });



        function loadGraphs() {
            DateFilter.startDate = $('#startDate').val();
            DateFilter.endDate = $('#endDate').val();
            if (DateFilter.startDate == "" && DateFilter.endDate == "") {
                DateFilter.startDate = "1970-01-01";
                DateFilter.endDate = "3000-01-01";

            }
            //else if (DateFilter.startDate == "") {
            //    DateFilter.startDate = "1970-01-01";
            //}

            //else if (DateFilter.endDate == "") {
            //    DateFilter.endDate = "3000-01-01";
            //}
            //else {

            //}


            var dataString = JSON.stringify(DateFilter);

            $.ajax({
                type: 'POST',
                url: 'WebService.asmx/getProfessionCountForReport',
                data: dataString,
                contentType: "application/json; charset=utf-8",
                traditional: true,
                success: function (data) {
                    var ProfessionCountList = $.parseJSON(data.d);
                    //יצירת גרף עמודות
                    var tbody = $('#ProfessionCountChart').find('tbody');
                    $(tbody).find('tr').remove();
                    var str1 = "";
                    ProfessionCountList.forEach(function (profession) {
                        str1 += '<tr>' +

                            '<td>' + profession.Pro_title + '</td>' +
                            '<td>' + profession.Amount + '</td>' +
                            '</tr>';
                    });
                    tbody.append(str1);


                    $(function () {
                        $('#chart').highcharts({
                            data: {
                                table: 'ProfessionCountChart'
                            },
                            chart: {
                                type: 'column'
                            },
                            title: {
                                text: ' '
                            },
                            yAxis: {
                                allowDecimals: false,
                                title: {
                                    text: 'כמות'
                                }
                            },
                            series: [{

                                showInLegend: false,
                                color: "#6C6E87"

                            }],
                            plotOptions: {
                                series: {
                                    dataLabels: {
                                        //color: '#B0B0B3',
                                        enabled: true,
                                    },
                                }
                            },


                            tooltip: {

                                enabled: false
                            }
                        });
                    });
                }
            });

        }


    </script>

    <style>
        #filterLogo {
            width: 20px;
        }

        #filterRow {
            margin: 0 auto;
            margin-bottom: 30px;
        }

        input {
            margin-right: 10px;
            background-color: #245581;
            width: 70px;
        }

        #requestsKPI, #attendenceKPI, #KPI3, #KPI4 {
            width: 130px;
            height: 110px;
            display: inline-block;
            text-align: center;
            padding-top: 10px;
            padding-bottom: 10px;
            background-color: #245581;
            color: white;
            font-size: 30px;
        }


            #requestsKPI:hover, #attendenceKPI:hover, #KPI3:hover, #KPI4:hover {
                background-color: #cac7ff;
                color: #333;
            }


        .margin-right {
            margin-right: 50px;
        }

        p {
            font-weight: bold;
            font-size: 16px;
            color: white;
        }

        .counter-text {
            font-size: 26px;
        }

        .kpi1-row, .upper_row {
            margin-bottom: 40px;
        }

        .btn:hover {
            color: white;
        }

        #filter_dayBTN:focus, #filter_weekBTN:focus, #filter_monthBTN:focus {
            color: black;
            background-color: #fffb9c;
        }

        #chart {
            width: 80%;
            height: 200px;
            margin: 0 auto;
            float: left;
        }

        #endDate, #startDate {
            display: none;
        }

        .hiddenLBL {
            visibility: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="mainContent full-width clearfix featureSection">
        <div class="container">
            <div class="sectionTitle text-center">
                <h2>
                    <span class="shape shape-left bg-color-4"></span>
                    <span>דף הבית</span>
                    <span class="shape shape-right bg-color-4"></span>
                </h2>
            </div>

        </div>
    </section>
    <div class="container" style="margin: 0 auto; align-content: center; direction: rtl; padding-right: 50px;">
        <div class="row" id="filterRow">
            <img id="filterLogo" src="images/filterLogo.png" />
            <input type="button" id="filter_dayBTN" value="יום" class="btn btn-sm" onclick="getDateforChart('day')" />
            <input type="button" id="filter_weekBTN" value="שבוע" class="btn btn-sm" onclick="getDateforChart('week')" />
            <input type="button" id="filter_monthBTN" value="חודש" class="btn btn-sm" onclick="getDateforChart('month')" />

        </div>
        <div class="row kpi1-row">
            <div class="col-lg-8">
                <div id="chart"></div>
            </div>
            <div class="col-lg-4">
                <div class="upper_row">
                    <%--upper section--%>
                    <a href="ShowRequests.aspx">
                        <div id="requestsKPI" class="btn">
                            <p><span id="requestsCounter" class="counter-text">0</span></p>
                            <p>בקשות</p>
                            <p>ממתינות</p>
                        </div>
                    </a>

                    <div id="attendenceKPI" class="btn margin-right">
                        <p><span id="attendenceCounter" class="counter-text">0</span></p>
                        <p>טפסי משוב</p>
                    </div>
                </div>

                <div class="lower_row">
                    <%--lower section--%>
                    <div id="KPI3" class="btn">
                        <p><span id="counter1" class="counter-text">0</span></p>
                        <p>משהו</p>
                        <p>כלשהו</p>
                    </div>
                    <div id="KPI4" class="btn margin-right">
                        <p><span id="counter2" class="counter-text">0</span></p>
                        <p>עוד משהו</p>
                    </div>
                </div>
            </div>

        </div>
        <div class="row">
            <div class="col-lg-6">
                left div 2
            </div>
            <div class="col-lg-6">
            </div>

        </div>

    </div>
    <input type="text" id="endDate" placeholder="בחר תאריך סיום" name="endDatename" />

    <input type="text" id="startDate" placeholder="בחר תאריך התחלה" name="startDatename" />

    <table id="ProfessionCountChart" class="table table-bordered" style="display: none; margin: 0 auto; width: 200px; direction: rtl;">
        <thead>
            <tr>
                <th>שם</th>
                <th>כמות</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
    <asp:Label ID="HiddenRequestsCounter" runat="server" Text="" CssClass="hiddenLBL"></asp:Label>
    <asp:Label ID="HiddenAttendenceFormsCounter" runat="server" Text="" CssClass="hiddenLBL"></asp:Label>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="jsPlaceHolder" runat="Server">
</asp:Content>

