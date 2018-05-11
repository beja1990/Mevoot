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
            $("#startDateTB").datepicker(
                { dateFormat: 'yy-mm-dd' }
                );
        });
        $(function () {
            $("#endDateTB").datepicker(
                { dateFormat: 'yy-mm-dd' }
                );
        });


        function loadGraphs() {
            DateFilter.startDate = $('#startDateTB').val();
            DateFilter.endDate = $('#endDateTB').val();
            if (DateFilter.startDate == "" && DateFilter.endDate == "") {
                DateFilter.startDate = "1970-01-01";
                DateFilter.endDate = "3000-01-01";

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
                                text: ''
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
        .filterLogo {
            width: 25px;
            background-color: transparent;
            position: relative;
            top: 6px;
        }

        #filterRow {
            margin: 0 auto;
            margin-bottom: 10px;
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
            color: white;
            font-size: 30px;
        }

        #requestsKPI {
            background-color: #b5d56a;
        }

        #attendenceKPI {
            background-color: #f0c24b;
        }

        #KPI3 {
            background-color: #245581;
        }

        #KPI4 {
            background-color: #ea7066;
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
            font-size: 18px;
            color: white;
        }

        .counter-text {
            font-size: 28px;
        }

        .kpi1-row, .upper_row {
            margin-bottom: 40px;
        }

        .btn:hover {
            color: white;
        }

        #filter_dayBTN:hover, #filter_weekBTN:hover, #filter_monthBTN:hover {
            color: white;
            background-color: #84bed6;
        }


        #chart {
            width: 90%;
            height: 300px;
            margin: 0 auto;
            float: left;
        }

        #endDateTB, #startDateTB {
            display: none;
        }


        .hiddenLBL {
            visibility: hidden;
        }

        .TDL {
            margin-right: 100px;
        }

        .filterBTN_leftSide {
            text-align: center;
        }

        .filterHeader {
            display: inline-block;
            margin-bottom: 0px;
            padding-right: 15px;
        }

        .KPI_box {
            border: solid 3px black;
            border-radius: 10px;
            height: 300px;
            padding: 10px;
            text-align: center;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="mainContent full-width clearfix featureSection">
        <div class="container" style="margin-bottom: 20px;">

            <div class="sectionTitle text-center">
                <h2>
                    <span class="shape shape-left bg-color-4"></span>
                    <span>מסך מנהל</span>
                    <span class="shape shape-right bg-color-4"></span>
                </h2>
            </div>

        </div>
    </section>
    <div class="container center-block" style="margin: 0 auto; align-content: center; direction: rtl; padding-right: 50px;">
        <div class="row" id="filterRow">
            <div class="col-lg-8 filterBTN_leftSide">
                <asp:Button ID="filter_dayBTN" runat="server" Text="יום" CssClass="btn btn-sm" OnClick="filter_dayBTN_Click" />
                <asp:Button ID="filter_weekBTN" runat="server" Text="שבוע" CssClass="btn btn-sm" OnClick="filter_weekBTN_Click" />
                <asp:Button ID="filter_monthBTN" runat="server" Text="חודש" CssClass="btn btn-sm" OnClick="filter_monthBTN_Click" />
                <asp:ImageButton ID="filterBTN" CssClass="filterLogo" src="images/filterLogo.png" runat="server" OnClick="filter_clear_Click" ToolTip="נקה בחירה" />
                <h4 id="chartTitle" class="filterHeader" runat="server"></h4>

            </div>
            <div class="col-lg-4">
                <h3 class="TDL">דברים שצריך לעשות</h3>
            </div>
        </div>

        <div class="row kpi1-row">
            <div class="col-lg-8 center-block">
                <div id="chart"></div>
            </div>
            <div class="col-lg-4" style="padding-right: 60px;">
                <div class="KPI_box form-control border-color-4 tb">
                    <div class="upper_row">
                        <%--upper section--%>

                        <a href="ShowRequests.aspx?id=1">
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
                        <div id="KPI4" class="btn">
                            <p><span id="counter2" class="counter-text">0</span></p>
                            <p>הודעות</p>
                            <p>ממתינות</p>
                        </div>
                        <div id="KPI3" class="btn margin-right">
                            <p><span id="counter1" class="counter-text">0</span></p>
                            <p>משהו</p>
                            <p>כלשהו</p>
                        </div>

                    </div>
                </div>
            </div>

        </div>
        <div class="row">
            <div class="col-lg-8" style="direction: rtl;">
                <h3 style="text-align: center;">תגבורים קרובים</h3>
                <asp:SqlDataSource ID="upcomingLessonsDS" runat="server" ConnectionString="<%$ ConnectionStrings:studentDBConnectionString %>" SelectCommand="select top(10) actLes_date,Pro_Title,(Tea_FirstName + ' ' +  Tea_LastName) as 'full name',Les_StartHour,Les_EndHour,Les_MaxQuan,quantity from Lesson inner join ActualLesson on Les_Id = ActLes_LesId inner join Teacher on Les_Tea_Id= Tea_Id inner join Profession on Les_Pro_Id=Pro_Id where ActLes_date >= GETDATE()-1  AND actls_cancelled=0 order by ActLes_date"></asp:SqlDataSource>
                <asp:GridView ID="upcomingLessonsGRDW" CssClass="grid" runat="server" AllowSorting="True" AutoGenerateColumns="False" ForeColor="#333333" CellPadding="4" Style="margin: 0 auto; margin-top: 20px; margin-bottom: 50px; text-align: center; width: 80%" DataSourceID="upcomingLessonsDS">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="actLes_date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="תאריך" SortExpression="actLes_date" />
                        <asp:BoundField DataField="Pro_Title" HeaderText="מקצוע" SortExpression="Pro_Title" />
                        <asp:BoundField DataField="full name" HeaderText="מתגבר" SortExpression="full name" />
                        <asp:BoundField DataField="Les_StartHour" HeaderText="התחלה" SortExpression="Les_StartHour" />
                        <asp:BoundField DataField="Les_EndHour" HeaderText="סיום" SortExpression="Les_EndHour" />
                        <asp:BoundField DataField="Les_MaxQuan" HeaderText="קיבולת" SortExpression="Les_MaxQuan" />
                        <asp:BoundField DataField="quantity" HeaderText="רשומים" SortExpression="quantity" />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#245581" Font-Bold="True" ForeColor="#F7F7F7" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>

            </div>
            <div class="col-lg-4" style="text-align: center;">
                <h3>מקום לטבלת הודעות אחרונות</h3>
            </div>
        </div>

    </div>
    <input type="text" id="endDateTB" placeholder="בחר תאריך סיום" name="endDatename" value="<%= this.inputEndValue %>" />
    <input type="text" id="startDateTB" placeholder="בחר תאריך התחלה" name="startDatename" value="<%= this.inputStartValue %>" />


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

