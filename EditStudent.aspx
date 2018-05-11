<%@ Page Title="" Language="C#" MasterPageFile="~/MP/AdminMasterPage.master" AutoEventWireup="true" CodeFile="EditStudent.aspx.cs" Inherits="EditStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://code.jquery.com/jquery.js"></script>
    <script src="plugins/jquery-ui/jquery-ui.js"></script>
    <link href="assets/plugins/toastr/toastr.css" rel="stylesheet" />
    <script src="assets/plugins/toastr/toastr.js"></script>
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/data.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>

    <script>
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-bottom-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut",

        }

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

        var DateFilter = {
            startDate: null,
            endDate: null,
            userId: null
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


            DateFilter.userId = document.getElementById('<%=IdTB.ClientID%>').value;
            var dataString = JSON.stringify(DateFilter);

            $.ajax({
                type: 'POST',
                url: 'WebService.asmx/StudentRequestsByProfession',
                data: dataString,
                contentType: "application/json; charset=utf-8",
                traditional: true,
                success: function (data) {
                    var StudentRequestsByProfessionCountList = $.parseJSON(data.d);
                    //יצירת גרף עמודות
                    var tbody = $('#requestsChartbyProfession').find('tbody');
                    $(tbody).find('tr').remove();
                    var str1 = "";
                    StudentRequestsByProfessionCountList.forEach(function (profession) {
                        str1 += '<tr>' +

                            '<td>' + profession.Pro_title + '</td>' +
                            '<td>' + profession.Amount + '</td>' +
                            '</tr>';
                    });
                    tbody.append(str1);


                    $(function () {
                        $('#chart').highcharts({
                            data: {
                                table: 'requestsChartbyProfession'
                            },
                            chart: {
                                type: 'column'
                            },
                            title: {
                                text: ' בקשות לפי מקצועות'
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
            }); //End ajax call for student requests

            $.ajax({
                type: 'POST',
                url: 'WebService.asmx/StudentClassesByProfession',
                data: dataString,
                contentType: "application/json; charset=utf-8",
                traditional: true,
                success: function (data) {
                    var StudentClassesByProfessionCountList = $.parseJSON(data.d);


                    //יצירת הגרפים- עוגה
                    var tbody = $('#ClassesbyProfessionCountPie').find('tbody');
                    $(tbody).find('tr').remove();
                    var str2 = "";
                    StudentClassesByProfessionCountList.forEach(function (profession) {
                        str2 += '<tr>' +
                            '<td>' + profession.Pro_title + '</td>' +
                            '<td>' + profession.Amount + '</td>' +
                            '</tr>';
                    });
                    tbody.append(str2);

                    $(function () {
                        $('#pie').highcharts({
                            data: {
                                table: 'ClassesbyProfessionCountPie'
                            },
                            chart: {
                                type: 'pie'
                            },
                            title: {
                                text: 'תגבורים לפי מקצועות '
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
            });  //End ajax call for student classes

        }

        window.onload = showReport;

    </script>
    <style>
        ::-webkit-input-placeholder {
            text-align: right;
        }

        .tb {
            text-align: right;
            direction: rtl;
            width: 400px;
            margin-left: auto;
        }

        .rightTB {
            position: relative;
            left: 50px;
        }

        .modaltb {
            text-align: right;
            direction: rtl;
            width: 500px;
            margin: 0 auto;
            position: absolute;
            top: 200px;
        }

        .cbRight {
            text-align: right;
            direction: rtl;
            position: relative;
            right: 20px;
            bottom: 20px;
        }

        #endDate, #startDate {
            direction: rtl;
            margin: 0 auto;
            float: right;
            margin-bottom: 100px;
            margin-left: 40px;
        }

        .filterBTN {
            margin: 10px;
            background-color: #9fccdf;
            width: 100px;
        }

        #report_PH {
            margin-bottom: 100px;
        }

        .filterDiv {
            height: 150px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="mainContent full-width clearfix featureSection">
        <div class="container">
            <ol class="breadcrumb">
                <li><a href="admin_calendar.aspx">בית</a></li>
                <li><a href="ShowStudents.aspx">תלמידים</a></li>
                <li class="active">עריכת פרטי תלמיד</li>
            </ol>

            <div class="sectionTitle text-center">
                <h2>
                    <span class="shape shape-left bg-color-4"></span>
                    <span>עריכת פרטי תלמיד</span>
                    <span class="shape shape-right bg-color-4"></span>
                </h2>
            </div>

        </div>
    </section>

    <!-- Starts contact form -->
    <div class="container">
        <div class="row cbRight">
            <asp:CheckBox ID="isEntitledCB" runat="server" />
            זכאות
            &nbsp&nbsp
                <asp:CheckBox ID="statusCB" runat="server" />
            משתמש פעיל
        </div>

        <div class="row">
            <div class="col-sm-6 col-xs-12">
                <div class="homeContactContent">

                    <div class="form-group">

                        <asp:DropDownList ID="instructorDDL" runat="server" CssClass="form-control border-color-4 tb" placeholder="בחר מדריך">
                        </asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-user"></i>
                        <asp:TextBox ID="LastNameTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="שם משפחה"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-phone"></i>
                        <asp:TextBox ID="PhoneTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="טלפון"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-home"></i>
                        <asp:TextBox ID="addressTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="כתובת"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-comments" aria-hidden="true"></i>
                        <asp:TextBox ID="NoteTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="הערות"></asp:TextBox>
                    </div>

                    <div class="form-group" style="text-align: center; padding-left: 170px;">
                        <button id="deletePopUp" class="btn btn-danger" style="display: inline-block; margin-right: 20px;" type="button" data-toggle="modal" data-target="#deleteConfirmationModal"><span class="glyphicon glyphicon-trash"></span></button>

                        <asp:Button ID="saveBTN" runat="server" Text="עדכון תלמיד" OnClick="saveBTN_Click" CssClass="btn btn-primary " data-target="#confirmationModal" />
                    </div>

                </div>
            </div>
            <div class="col-sm-6 col-xs-12">
                <div class="homeContactContent">

                    <div class="form-group">
                        <i class="fa fa-id-card"></i>
                        <asp:TextBox ID="IdTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="ת.ז" ReadOnly="true"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-user"></i>
                        <asp:TextBox ID="FirstNameTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="שם פרטי"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-birthday-cake"></i>
                        <asp:TextBox ID="BirhtDateTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="תאריך לידה dd/mm/yyyy"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-envelope"></i>
                        <asp:TextBox ID="MailTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="אימייל"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-unlock-alt"></i>
                        <asp:TextBox ID="PasswordTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="סיסמא"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:DropDownList ID="gradeDDL" runat="server" Style="text-align: right" CssClass="form-control border-color-4 tb">
                            <asp:ListItem>בחר כיתה</asp:ListItem>
                            <asp:ListItem>כיתה ז</asp:ListItem>
                            <asp:ListItem>כיתה ח</asp:ListItem>
                            <asp:ListItem>כיתה ט</asp:ListItem>
                            <asp:ListItem>כיתה י</asp:ListItem>
                            <asp:ListItem>כיתה יא</asp:ListItem>
                            <asp:ListItem>כיתה יב</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                </div>
            </div>

        </div>
        <!-- Starts user dashboard -->
        <div class="row">
            <div class="sectionTitle text-center">
                <h2>
                    <span class="shape shape-left bg-color-4"></span>
                    <span>נתוני תלמיד</span>
                    <span class="shape shape-right bg-color-4"></span>
                </h2>
            </div>
            <div id="report_PH">
                <div class="row filterDiv">
                    <input type="text" id="endDate" placeholder="בחר תאריך סיום" name="endDatename" title="במידה ולא נבחר תאריך, תאריך הסיום יהיה היום" />
                    <input type="text" id="startDate" placeholder="בחר תאריך התחלה" name="startDatename" />
                    <input type="button" id="profession_reportBTN" value="סנן" class="btn btn-sm filterBTN" onclick="showReport()" style="float: right; margin: -5px;" />
                </div>
                <h3 id="title"></h3>
                <div class="row">
                    <div class="col-lg-6" id="chart" style="width: 50%; margin-top: 20px;"></div>
                    <br />
                    <div class="col-lg-6" id="pie" style="width: 50%;"></div>
                </div>
                <%--  כמות בקשות לפי מקצוע לתלמיד --%>

                <table id="requestsChartbyProfession" class="table table-bordered" style="display: none; margin: 0 auto; width: 200px; direction: rtl;">
                    <thead>
                        <tr>
                            <th>מקצוע</th>
                            <th>כמות</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>

                <%--   כמות תגבורים שהתלמיד נרשם אליהם ואושרו לפי מקצוע--%>
                <table id="ClassesbyProfessionCountPie" class="table table-bordered" style="display: none; margin: 0 auto; width: 200px; direction: rtl;">
                    <thead>
                        <tr>

                            <th>מקצוע</th>
                            <th>כמות</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>

        </div>
        <!-- Ends user dashboard -->

        <!-- Ends contact form -->
    </div>



    <div id="deleteConfirmationModal" class="modal fade modal-md modaltb" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content btn">
                <div class="modal-header">
                    <h5>האם ברצונך למחוק את התלמיד ?</h5>
                    &nbsp
                    <asp:Button ID="deleteBTN" runat="server" Text="מחק" OnClick="deleteBTN_Click" CssClass="btn btn-sm btn-danger" />
                    &nbsp&nbsp
                    <button type="button" class="btn btn-sm btn-success" data-dismiss="modal">ביטול</button>
                </div>
            </div>

        </div>
    </div>

</asp:Content>

