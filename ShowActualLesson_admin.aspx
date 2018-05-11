<%@ Page Title="" Language="C#" MasterPageFile="~/MP/AdminMasterPage.master" AutoEventWireup="true" CodeFile="ShowActualLesson_admin.aspx.cs" Inherits="ShowActualLesson_admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/plugins/toastr/toastr.css" rel="stylesheet" />
    <script src="assets/plugins/toastr/toastr.js"></script>

    <script>

        $(function () {
            $("#datepicker").datepicker(
                { dateFormat: 'yy-mm-dd' }
                );

        });

        $(function () {

            // Initialize and change language to hebrew
            $('#datepicker').datepicker($.datepicker.regional["he"]);

        });
    </script>

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
    </script>

    <script src="plugins/select2/select2.min.js"></script>
    <link href="css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript">

        $(document).ready(function () {

            $("#<%=TigburDDL.ClientID%>").select2({
                placeholder: "בחר תבנית תגבור",
                allowClear: true,
                dir: "rtl"
            });

        });

    </script>


    <style>
        .container.content {
            direction: rtl;
        }


        .boxes {
            position: relative;
            top: 2px;
            margin-left: 20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="mainContent full-width clearfix featureSection">
        <div class="container">
            <ol class="breadcrumb">
                <li><a href="admin_calendar.aspx">בית</a></li>
                <li class="active">תגבורים</li>

            </ol>
            <div class="sectionTitle text-center">
                <h2>
                    <span class="shape shape-left bg-color-4"></span>
                    <span>רשימת תגבורים</span>
                    <span class="shape shape-right bg-color-4"></span>
                </h2>
            </div>
        </div>
    </section>

    <div class="container content">
        <div>
            <asp:SqlDataSource ID="lessonsDS" runat="server"
                ConnectionString="<%$ ConnectionStrings:studentDBConnectionString %>"
                SelectCommand="	select Les_Id,Pro_Title, ActLes_date,Les_StartHour,Les_EndHour,(Tea_FirstName + ' ' +  Tea_LastName) as 'full name', Les_MaxQuan,quantity from Lesson inner join ActualLesson on Les_Id = ActLes_LesId inner join Teacher on Les_Tea_Id= Tea_Id inner join Profession on Les_Pro_Id=Pro_Id  where ActLes_date >= GETDATE()-1  AND actls_cancelled=0 order by ActLes_date"></asp:SqlDataSource>

        </div>
        <div>
            <asp:DropDownList ID="TigburDDL" runat="server" CssClass="form-control border-color-4" Style="width: 450px; margin-bottom: 10px; float: right;" OnSelectedIndexChanged="TigburDDL_SelectedIndexChanged">
            </asp:DropDownList>
            <input type="text" id="datepicker" class="boxes" style="margin-right:20px;" placeholder="בחר תאריך התחלה" name="DatePickername" />
            <asp:TextBox ID="counterTB" runat="server" CssClass=" boxes" placeholder="הזן כמות מופעים"></asp:TextBox>
            <asp:Button ID="generateDate" runat="server" Text="הוסף למערכת" OnClick="generateDate_Click" CssClass="btn btn-primary rightTB btn-sm slight_left" />
        </div>
        <div>
            <asp:GridView ID="lessonsGRDW" CssClass="grid" runat="server" Style="margin: 0 auto; margin-top: 20px; margin-bottom: 100px; text-align: center; width: 100%" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="lessonsDS" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="Les_Id" HeaderText="מזהה תבנית" InsertVisible="False" SortExpression="Les_Id" />
                    <asp:BoundField DataField="Pro_Title" HeaderText="מקצוע" SortExpression="Pro_Title" />
                    <asp:BoundField DataField="ActLes_date" HeaderText="תאריך" SortExpression="ActLes_date" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Les_StartHour" HeaderText="שעת התחלה" SortExpression="Les_StartHour" />
                    <asp:BoundField DataField="Les_EndHour" HeaderText="שעת סיום" SortExpression="Les_EndHour" />
                    <asp:BoundField DataField="full name" HeaderText="שם המתגבר" SortExpression="full name" />
                    <asp:BoundField DataField="Les_MaxQuan" HeaderText="קיבולת מקסימלית" SortExpression="Les_MaxQuan" />
                    <asp:BoundField DataField="quantity" HeaderText="כמות רשומים" SortExpression="quantity" />
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
    </div>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="jsPlaceHolder" runat="Server">
</asp:Content>

