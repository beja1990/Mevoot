<%@ Page Title="" Language="C#" MasterPageFile="~/MP/StudentMasterPage.master" AutoEventWireup="true" CodeFile="studentRequests.aspx.cs" Inherits="studentRequests" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>


        .container.content {
            direction: rtl;
        }

        .straightLeft {
            position: relative;
            right: 130px;
        }

    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="mainContent full-width clearfix featureSection">
        <div class="container">
            <ol class="breadcrumb">
                <li><a href="student_calendar.aspx">בית</a></li>
                <li class="active">הבקשות שלי</li>

            </ol>
            <div class="sectionTitle text-center">
                <h2>
                    <span class="shape shape-left bg-color-4"></span>
                    <span>הבקשות שלי</span>
                    <span class="shape shape-right bg-color-4"></span>
                </h2>
            </div>
        </div>
    </section>
    <div class="container content">
        <asp:DropDownList ID="statusDDL" runat="server" AutoPostBack="true" Style="text-align: right" CssClass="filterTB straightLeft">
            <asp:ListItem Text="כל הבקשות" Value="" />
            <asp:ListItem Value="2">בקשות ממתינות</asp:ListItem>
            <asp:ListItem Value="1">בקשות שאושרו</asp:ListItem>
            <asp:ListItem Value="0">בקשות שנדחו</asp:ListItem>
        </asp:DropDownList>
        <asp:SqlDataSource ID="stuReqDS" runat="server" ConnectionString="<%$ ConnectionStrings:studentDBConnectionString %>" SelectCommand="select DISTINCT Requests.req_number, pro_title,
            (tea_firstName + ' '+ tea_lastName) as 'teacher_full_name',actLes_date, Les_startHour, Les_EndHour, Les_Day,req_is_permanent, req_status  
            from ((((requests inner join student on req_stu_id= stu_id) inner join lesson on req_actLes_id= les_id ) inner join Teacher on Les_tea_Id= tea_Id) inner join Profession on les_pro_id= pro_id) inner join ActualLesson on ActLes_LesId= req_actLes_id where actLes_date = req_actLes_date and stu_id=@current_stu_id"
            FilterExpression="(req_status)= '{0}'">
            <FilterParameters>
                <asp:ControlParameter ControlID="statusDDL" PropertyName="SelectedValue" />
            </FilterParameters>
        </asp:SqlDataSource>

        <asp:GridView ID="stuReqGV" runat="server" AutoGenerateColumns="False" Style="margin-left: auto; margin-right: auto; margin-top: 20px ; text-align: center; width: 80%" DataKeyNames="req_number" DataSourceID="stuReqDS" OnRowDataBound="stuReqGV_RowDataBound" CssClass="grid">
            <Columns>
                <asp:BoundField DataField="req_status" HeaderText="סטטוס הבקשה" SortExpression="req_status" />
                <asp:BoundField DataField="pro_title" HeaderText="מקצוע" SortExpression="pro_title" />
                <asp:BoundField DataField="teacher_full_name" HeaderText="שם המתגבר" SortExpression="teacher_full_name" ReadOnly="True" />
                <asp:BoundField DataField="actLes_date" HeaderText="תאריך" SortExpression="actLes_date" />
                <asp:BoundField DataField="Les_startHour" HeaderText="שעת התחלה" SortExpression="Les_startHour" />
                <asp:BoundField DataField="Les_EndHour" HeaderText="שעת סיום" SortExpression="Les_EndHour" />
                <asp:BoundField DataField="Les_Day" HeaderText="יום בשבוע" SortExpression="Les_Day" />
                <asp:BoundField DataField="req_is_permanent" HeaderText="האם קבוע" SortExpression="req_is_permanent" />
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

</asp:Content>
