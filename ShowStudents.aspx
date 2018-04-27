<%@ Page Title="" Language="C#" MasterPageFile="~/MP/AdminMasterPage.master" AutoEventWireup="true" CodeFile="ShowStudents.aspx.cs" Inherits="ShowStudents" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //for serching gridview on keyup
        function Filter(Obj) {

            var grid = document.getElementById(("<%= studentsGRDW.ClientID %>"));
            var terms = Obj.value.toUpperCase();

            for (var r = 1; r < grid.rows.length; r++) {
                ele1 = grid.rows[r].cells[1].innerHTML.replace(/<[^>]+>/g, "");
                ele2 = grid.rows[r].cells[2].innerHTML.replace(/<[^>]+>/g, "");
                ele3 = grid.rows[r].cells[3].innerHTML.replace(/<[^>]+>/g, "");
                if (ele1.toUpperCase().indexOf(terms) >= 0)
                    grid.rows[r].style.display = '';
                else if (ele2.toUpperCase().indexOf(terms) >= 0)
                    grid.rows[r].style.display = '';
                else if (ele3.toUpperCase().indexOf(terms) >= 0)
                    grid.rows[r].style.display = '';

                else grid.rows[r].style.display = 'none';
            }
        }
    </script>

    <style>
        #addStudentBTN {
            text-align: right;
            float: right;
        }

        .container.content {
            direction: rtl;
        }

        #straightLeft {
            position: relative;
            right: 115px;
        }

        .filterTB {
            margin-right: 20px;
            width: 200px;
        }

        .excel {
            margin-right: 20px;
            float: left;
        }

        .Textbox_width {
            margin-right: 20px;
            width: 100px;
        }
    </style>
    <script src="https://code.jquery.com/jquery.js"></script>
    <link href="assets/plugins/toastr/toastr.css" rel="stylesheet" />
    <script src="assets/plugins/toastr/toastr.js"></script>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="mainContent full-width clearfix featureSection">
        <div class="container">
            <ol class="breadcrumb">
                <li><a href="admin_calendar.aspx">בית</a></li>
                <li class="active">תלמידים</li>

            </ol>
            <div class="sectionTitle text-center">
                <h2>
                    <span class="shape shape-left bg-color-4"></span>
                    <span>רשימת תלמידים</span>
                    <span class="shape shape-right bg-color-4"></span>
                </h2>
            </div>
        </div>
    </section>

    <div class="container content">
        <div>
            <asp:Button ID="addStudentBTN" runat="server" Text="הוסף תלמיד" OnClick="addStudentBTN_Click" CssClass="btn btn-primary rightTB btn-sm" />
            <asp:TextBox ID="searchTB" placeholder="חיפוש" runat="server" AutoPostBack="true" onkeyup="Filter(this)" CssClass="filterTB"></asp:TextBox>


            <asp:DropDownList ID="gradeDDL" runat="server" Style="text-align: right" CssClass="filterTB">
                <asp:ListItem Text="בחר כיתה" Value="" />
                <asp:ListItem>כיתה ז</asp:ListItem>
                <asp:ListItem>כיתה ח</asp:ListItem>
                <asp:ListItem>כיתה ט</asp:ListItem>
                <asp:ListItem>כיתה י</asp:ListItem>
                <asp:ListItem>כיתה יא</asp:ListItem>
                <asp:ListItem>כיתה יב</asp:ListItem>
            </asp:DropDownList>

            <asp:DropDownList ID="ddlInc" runat="server" AutoPostBack="true" DataSourceID="DropDownDataSource"
                DataTextField="full_name" DataValueField="full_name" AppendDataBoundItems="true" CssClass="Textbox_width">
                <asp:ListItem Text="בחר מדריך" Value="" />
            </asp:DropDownList>

            <asp:SqlDataSource ID="DropDownDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:studentDBConnectionString %>"
                SelectCommand="SELECT DISTINCT (Ins_FirstName + ' ' +  Ins_LastName) as 'full_name' FROM [Student] inner join Instructor on Student.Ins_Id= Instructor.Ins_Id"></asp:SqlDataSource>

            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="filterTB" placeholder="בחר מדריך">
                <asp:ListItem>כל המשתמשים</asp:ListItem>
                <asp:ListItem>משתמשים פעילים</asp:ListItem>
                <asp:ListItem>משתמשים לא פעילים</asp:ListItem>
            </asp:DropDownList>

            <%--For upload : download and install the following - https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734--%>
            <asp:FileUpload ID="excelFU" runat="server" ToolTip="בחר קובץ" CssClass="btn btn-success btn-sm excel" />

            <asp:Button ID="UploadBTN" runat="server" OnClick="UploadBTN_Click" Text="ייבא" CssClass="btn btn-success btn-sm excel" />
            <asp:Button ID="exportBTN" runat="server" Text="שמור רשימה" OnClick="exportBTN_Click" CssClass="btn btn-success btn-sm excel" data-toggle="tooltip" data-placement="top" title="" data-original-title="שמור כקובץ Excel" />

        </div>

        <div>

            <asp:SqlDataSource ID="studentsDS" runat="server" ConnectionString="<%$ ConnectionStrings:studentDBConnectionString %>" SelectCommand=" SELECT [stu_id], [stu_firstName], [stu_lastName], [stu_birthDate], [stu_phoneNumber], [stu_email],[stu_address],[stu_status],[stu_password],[stu_grade],[stu_IsEntitled],[stu_Note], (Ins_FirstName + ' ' +  Ins_LastName) as 'full_name'
                        FROM [Student] inner join Instructor on Student.Ins_Id= Instructor.Ins_Id"
                FilterExpression="full_name like '{0}'">
                <FilterParameters>
                    <asp:ControlParameter Name="full_name" ControlID="ddlInc" PropertyName="SelectedValue" />
                </FilterParameters>
            </asp:SqlDataSource>

        </div>

        <br />
        <div>
            <asp:GridView ID="studentsGRDW" CssClass="grid" runat="server" Style="margin: 0 auto; margin-top: 20px; text-align: center; width: 100%" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="stu_id" DataSourceID="studentsDS" GridLines="Horizontal" OnSelectedIndexChanged="studentsGRDW_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="פרטים" />
                    <asp:BoundField DataField="stu_id" HeaderText="ת.ז תלמיד" ReadOnly="True" SortExpression="stu_id" />
                    <asp:BoundField DataField="stu_firstName" HeaderText="שם פרטי" SortExpression="stu_firstName" />
                    <asp:BoundField DataField="stu_lastName" HeaderText="שם משפחה" SortExpression="stu_lastName" />
                    <asp:BoundField DataField="stu_birthDate" HeaderText="תאריך לידה" SortExpression="stu_birthDate" />
                    <asp:BoundField DataField="stu_grade" HeaderText="כיתה" SortExpression="stu_grade" />
                    <asp:BoundField DataField="full_name" HeaderText="שם המדריך" SortExpression="full_name" />
                    <asp:BoundField DataField="stu_phoneNumber" HeaderText="פלאפון" SortExpression="stu_phoneNumber" />
                    <asp:BoundField DataField="stu_email" HeaderText="מייל" SortExpression="stu_email" />
                    <asp:BoundField DataField="stu_address" HeaderText="כתובת" SortExpression="stu_address" />
                    <asp:CheckBoxField DataField="stu_IsEntitled" HeaderText="זכאי" SortExpression="stu_IsEntitled" />
                    <asp:CheckBoxField DataField="stu_status" HeaderText="סטטוס" SortExpression="stu_status" />
                    <asp:BoundField DataField="stu_Note" HeaderText="הערות" SortExpression="stu_Note" />


                </Columns>
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <HeaderStyle BackColor="#245581" Font-Bold="True" ForeColor="#F7F7F7" />
                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                <SortedDescendingHeaderStyle BackColor="#3E3277" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="jsPlaceHolder" runat="Server">
</asp:Content>

