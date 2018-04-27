<%@ Page Title="" Language="C#" MasterPageFile="~/MP/StudentMasterPage.master" AutoEventWireup="true" CodeFile="student_calendar.aspx.cs" Inherits="student_calendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <meta charset="utf-8" />

    <script src='assets/js/jquery.min.js'></script>
    <script src='assets/js/jquery-ui.min.js'></script>

    <script src='assets/js/moment.min.js'></script>
    <link href='assets/css/fullcalendar.css' rel='stylesheet' />
    <link href='assets/css/fullcalendar.print.css' rel='stylesheet' media='print' />
    <script src='assets/js/fullcalendar.min.js'></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />

    <script type='text/javascript'>
        // script -> webservice -> tigbur -> dbservices and all the way back
        $(document).ready(function () {
            //error popup alert func
            function alertFunc(message_alert) {
                $('#alertlabel').empty();
                $('#alertlabel').append(message_alert);

                $("#alertModelDIV").dialog({
                    autoOpen: false,

                });
                $('#alertModelDIV').dialog('open');
                $("#alertModelDIV").dialog({
                    title: " הודעה",
                });
            }
            // page is now ready, initialize the calendar...
            var eventData = {
                id: null,
                title: null,
                start: null,
                end: null,
                startTime: null,
                endTime: null,
                teacherId: null,
                teacherName: null,
                profId: null,
                limit: null,
                actualLimit: null,
                color: null,
                type: null

            };

            var TigburById = {
                id: null

            }

        

            var myArray = []; // creating a new array object
            myArray['100'] = '#4d4dff'; // setting the attribute a to blue
            myArray['101'] = '#3333ff';
            myArray['102'] = '#1a1aff';
            myArray['103'] = '#009900';
            myArray['104'] = '#006600';
            myArray['105'] = '#003300';
            myArray['106'] = '#660b4a';

            $('#calendar').fullCalendar({
                utc: true,
                minTime: "12:00:00",
                maxTime: "22:00:00",
                nowIndicator: true,
                height: 600,
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },

                selectable: true,
                editable: false,

                monthNames: ["ינואר", "פברואר", "מרץ", "אפריל", "מאי", "יוני", "יולי", "אוגוסט", "ספטמבר", "אוקטובר", "נובמבר", "דצמבר"],
                monthNamesShort: ['ינואר', 'פברואר', 'מרץ', 'אפריל', 'מאי', 'יוני', 'יולי', 'אוגוסט', 'ספטמבר', 'אוקטובר', 'נובמבר', 'דצמבר'],
                dayNames: ['ראשון', 'שני', 'שלישי', 'רביעי', 'חמישי', 'שישי', 'שבת'],
                dayNamesShort: ['ראשון', 'שני', 'שלישי', 'רביעי', 'חמישי', 'שישי', 'שבת'],
                buttonText: {
                    today: 'היום',
                    month: 'חודש',
                    week: 'שבוע',
                    day: 'יום'
                },// datetabs names init
                defaultView: 'month',
                //  allDaySlot: false,
                slotMinutes: '00:30:00',
                eventLimit: true, // allow "more" link when too many events
                themeSystem: 'bootstrap4', // theme
                themeName: 'Minty',

                //disableDragging: disable_dragging,
                //disableResizing: disable_resizing,


                // getting all events from db and putting them into fullcalendar
                events: function (event) {

                    $('#calendar').fullCalendar('removeEvents', event._id)
                    $.ajax({
                        url: 'WebService.asmx/getSchedule',
                        type: 'POST', // Send post data
                        dataType: 'json',
                        contentType: 'application/json; charset = utf-8',
                        async: false,
                        success: function (obj) {
                            var obj = $.parseJSON(obj.d);
                            cache: true;
                            $(obj).each(function () {
                                var setcolor = myArray[$(this).attr('ProfId')];
                                eventData = {
                           
                                    id: $(this).attr('Id'),
                                    title: $(this).attr('ProfName') + "," + $(this).attr('TeacherName'),
                                    start: $(this).attr('TigburDate') + "T" + $(this).attr('StartTime'),
                                    end: $(this).attr('TigburDate') + "T" + $(this).attr('EndTime'),
                                    profId: $(this).attr('ProfId'),
                                    color: setcolor,
                                    type: "tutering"

                                };
                                $('#calendar').fullCalendar('renderEvent', eventData, true);
                            });

                        },
                    });
                },
               

                //===============================click==========================================
                //event data popup ajax func WS -> tigburim -> dbs and all the way back
                eventClick: function (event) {

                    TigburById.id = event.id;
                    var dataString = JSON.stringify(TigburById);
                    $.ajax
                        ({
                            url: 'WebService.asmx/getTigburById',
                            data: dataString,
                            type: 'POST',
                            dataType: 'json',
                            contentType: 'application/json; charset = utf-8', // sent to the server
                            async: false,
                            success: function (data) {
                                var tigubrObj = $.parseJSON(data.d);
                                document.getElementById("ContentPlaceHolder1_ProfessionName").innerHTML = tigubrObj.ProfName;
                                document.getElementById("ContentPlaceHolder1_timeStartLabel").innerHTML = tigubrObj.StartTime;
                                document.getElementById("ContentPlaceHolder1_timeEndLabel").innerHTML = tigubrObj.EndTime;
                                document.getElementById("ContentPlaceHolder1_TeacherLabel").innerHTML = tigubrObj.TeacherName;
                                document.getElementById("ContentPlaceHolder1_LimitLabel").innerHTML = tigubrObj.Limit;
                                document.getElementById("ContentPlaceHolder1_quantityleft").innerHTML = tigubrObj.ActualLimit;
                            },
                            error: function (result) { alertFunc(" התגבור לא זוהה"); }
                        });

                    $("#showTigburPopupPopupDiv").dialog({
                        autoOpen: false,
                    });

                    $('#showTigburPopupPopupDiv').dialog('open');
                    $("#showTigburPopupPopupDiv").dialog({
                        title: "פרטי תגבור",
                    });

                },
            });
        });



    </script>
    <style>
        #calendar {
            width: 80%;
            margin: 0 auto;
            margin-bottom: 80px;
        }

        .ui-dialog .ui-dialog-title{
            text-align:center;
        }

        .fc-button{

        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <section class="mainContent full-width clearfix featureSection">
        <div class="container">
            <ol class="breadcrumb">
                <li><a href="student_calendar.aspx">בית</a></li>
                <li class="active">מערכת שעות</li>

            </ol>
            <div class="sectionTitle text-center">
                <h2>
                    <span class="shape shape-left bg-color-4"></span>
                    <span>מערכת שעות</span>
                    <span class="shape shape-right bg-color-4"></span>
                </h2>
            </div>
        </div>
    </section>
    <div class="container">
  

            <div id="calendar"></div>

            <!--lesson details start -->
            <div data-role="popup" id="showTigburPopup" class="ui-content" style="min-width: 250px;">
                <div id="showTigburPopupPopupDiv" style="display: none; text-align:center;">

                    <table runat="server" dir="rtl">
                        <tr>
                            <td>
                               <asp:Label ID="Label1" runat="server" Text="מקצוע :"></asp:Label>
                            </td>

                            <td>
                                <asp:Label ID="ProfessionName" runat="server" Text=" "></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td>
                               <asp:Label ID="Label2" runat="server" Text="שעת התחלה :"></asp:Label>
                            </td>

                            <td>
                                <asp:Label ID="timeStartLabel" runat="server" Text=" "></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="שעת סיום :"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="timeEndLabel" runat="server" Text=" "></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="מתגבר :"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="TeacherLabel" runat="server" Text=" "></asp:Label>

                            </td>
                        </tr>

                        <tr>
                            <td>
                               <asp:Label ID="Label5" runat="server" Text="מקומות :"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LimitLabel" runat="server" Text=" "></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                 <asp:Label ID="Label6" runat="server" Text="מקומות פנויים :"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="quantityleft" runat="server" Text=" "></asp:Label>
                            </td>
                        </tr>

                    </table>
                </div>
            </div>
        </div>
    </div>
    <!--error alert start -->
    <div data-role="popup" id="alertModel" class="ui-content" style="min-width: 250px; width: auto">
        <div id="alertModelDIV" style="display: none;">
            <label id="alertlabel"></label>
            <button type="button" id="close_alertlabel" class="btn btn-default" style="float: left;">אישור</button>
        </div>

    </div>
</asp:Content>

