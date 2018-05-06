<%@ Page Title="" Language="C#" MasterPageFile="~/MP/AdminMasterPage.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/data.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <style>
        .row {
            margin: 0 auto;
            text-align: center;
        }

        .btn {
            margin: 10px;
            background-color: #245581;
        }

            .btn:hover {
                color: white;
                background-color: #84bed6 ;
            }
            .btn:focus{
                background-color: #84bed6;
                color:white;
            }


        h3 {
            text-align: center;
            margin-top: 20px;
        }

        #endDate, #startDate {
            direction: rtl;
        }

        #imagPrint {
            vertical-align: middle;
        }
    </style>


    <script>
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
    </script>

    <script>
        var DateFilter = {
            startDate: null,
            endDate: null
        };

        function showReport() {
            DateFilter.startDate = $('#startDate').val();
            DateFilter.endDate = $('#endDate').val();
            if (DateFilter.startDate == "" && DateFilter.endDate == "") {
                DateFilter.startDate = "1970-01-01";
                DateFilter.endDate = "3000-01-01";

            }
            else if (DateFilter.startDate == "") {
                DateFilter.startDate = "1970-01-01";
            }

            else if (DateFilter.endDate == "") {
                DateFilter.endDate = "3000-01-01";
            }
            else {

            }


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

                    //יצירת הגרפים- עוגה
                    var tbody = $('#ProfessionCountPie').find('tbody');
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
                        $('#pie').highcharts({
                            data: {
                                table: 'ProfessionCountPie'
                            },
                            chart: {
                                type: 'pie'
                            },
                            title: {
                                text: ' '
                            },
                            series: [{

                                showInLegend: true


                            }],
                            legend: {
                                rtl: true
                            },
                            plotOptions: {
                                pie: {
                                    innerSize: 100,
                                    depth: 45,
                                    allowPointSelect: false,
                                    cursor: 'pointer',
                                    dataLabels: {
                                        enabled: false,
                                        depth: 35,
                                        format: '<b>{point.name}</b><br/>: {point.y}',
                                        style: {
                                            color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                                        },

                                    }
                                }
                            },


                            tooltip: {
                                enabled: true,
                                rtl: true,
                                formatter: function () {
                                    var name = this.point.name;
                                    var quan = this.y;
                                    var str = name + " - כמות  : " + quan + "";
                                    return str;
                                }
                            }
                        });
                    });
                }
            });


        }

        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;

            window.print();
            document.body.innerHTML = originalContents;
        }

        function showtable() {
            var x = document.getElementById("ProfessionCountChart");
            if (x.style.display === "none") {
                x.style.display = "block";
                document.getElementById("display").setActive = true;
            } else {
                x.style.display = "none";
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="mainContent full-width clearfix featureSection">
        <div class="container">
            <ol class="breadcrumb">
                <li><a href="admin_calendar.aspx">בית</a></li>
                <li class="active">דוחות</li>
            </ol>
            <div class="sectionTitle text-center">
                <h2>
                    <span class="shape shape-left bg-color-4"></span>
                    <span>דוחות</span>
                    <span class="shape shape-right bg-color-4"></span>
                </h2>
            </div>

        </div>
    </section>
    <div class="container">

        <div class="row">

            <input type="image" id="imagPrint" src="images/printer-service.png" alt="Print" width="30" height="30" onclick="printDiv('report_PH')" title="הדפסה" />

            <input type="button" id="display" value="הצג טבלת נתונים" class="btn btn-lg" onclick="showtable()" />

            <input type="button" id="profession_reportBTN" value="מקצועות" class="btn btn-lg" onclick="showReport()" />

            <input type="button" id="requests_reportBTN" value="בקשות / ביטולים" class="btn btn-lg" onclick="" />

            <input type="button" id="teacher_reportBTN" value="מתגברים" class="btn btn-lg" onclick="" />


            <input type="text" id="endDate" placeholder="בחר תאריך סיום" name="endDatename" title="במידה ולא נבחר תאריך, תאריך הסיום יהיה היום" />

            <input type="text" id="startDate" placeholder="בחר תאריך התחלה" name="startDatename" />




        </div>


        <div id="report_PH">
            <h3 id="title"></h3>
            <div id="chart" style="width: 70%; margin: 0 auto;"></div>
            <br />
            <div id="pie" style="width: 70%; margin: 0 auto;"></div>
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

            <table id="ProfessionCountPie" class="table table-bordered" style="display: none;">
                <thead>
                    <tr>

                        <th>שם</th>
                        <th>כמות</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>

    </div>
    <div style="margin-bottom: 100px;">&nbsp</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="jsPlaceHolder" runat="Server">
</asp:Content>

