<%@ Page Title="" Language="C#" MasterPageFile="~/MP/AdminMasterPage.master" AutoEventWireup="true" CodeFile="ShowRequests.aspx.cs" Inherits="ShowRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="mainContent full-width clearfix featureSection">
        <div class="container">
            <ol class="breadcrumb">
                <li><a href="admin_calendar.aspx">בית</a></li>
                <li class="active">בקשות תלמידים</li>
            </ol>
            <div class="sectionTitle text-center">
                <h2>
                    <span class="shape shape-left bg-color-4"></span>
                    <span>בקשות תלמידים</span>
                    <span class="shape shape-right bg-color-4"></span>
                </h2>
            </div>

        </div>
    </section>
    <div class="container content">
        <asp:DropDownList ID="statusDDL" runat="server" AutoPostBack="true" Style="text-align: right" CssClass="filterTB">
            <asp:ListItem Text="כל הבקשות" Value="" />
            <asp:ListItem Value="2">בקשות ממתינות</asp:ListItem>
            <asp:ListItem Value="1">בקשות שאושרו</asp:ListItem>
            <asp:ListItem Value="0">בקשות שנדחו</asp:ListItem>
        </asp:DropDownList>

        <asp:SqlDataSource ID="waitingReqDSS" runat="server" ConnectionString="<%$ ConnectionStrings:studentDBConnectionString %>" SelectCommand=" select DISTINCT Requests.req_number, number ,req_type, pro_title, stu_id,(stu_firstName + ' ' + stu_lastName) as 'student_full_name',
            (tea_firstName + ' '+ tea_lastName) as'teacher_full_name', Les_maxQuan,actLes_date, Les_startHour, Les_EndHour, Les_Day , quantity,req_is_permanent, req_status, ActLes_LesId 
            from ((((requests inner join student on req_stu_id= stu_id) inner join lesson on req_actLes_id= les_id ) inner join Teacher on Les_tea_Id= tea_Id) inner join Profession on les_pro_id= pro_id) inner join ActualLesson on ActLes_LesId= req_actLes_id where actLes_date = req_actLes_date"
            FilterExpression="(req_status)= '{0}'">
            <FilterParameters>
                <asp:ControlParameter ControlID="statusDDL" PropertyName="SelectedValue" />
            </FilterParameters>
        </asp:SqlDataSource>

        <asp:GridView ID="reqGV" CssClass="grid" runat="server" DataSourceID="waitingReqDSS" DataKeyNames="number" Style="margin-left: auto; margin-top: 20px; margin-right: auto; margin-bottom: 100px; text-align: center; width: 100%" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" OnRowDataBound="reqGV_RowDataBound">
            <AlternatingRowStyle BackColor="#F7F7F7" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="AproveButton" Text="אשר" runat="server" CssClass="btn btn-success btn-sm" OnClick="ApproveButton_Click" />
                        <asp:Button ID="DeclineButton" Text="דחה" runat="server" CssClass="btn btn-danger btn-sm" OnClick="DeclineButton_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="req_number" HeaderText="מזהה" InsertVisible="False" ReadOnly="True" SortExpression="req_number" />
                <asp:BoundField DataField="pro_title" HeaderText="מקצוע" SortExpression="pro_title" />
                <asp:BoundField DataField="stu_id" HeaderText="ת.ז התלמיד" SortExpression="stu_id" />
                <asp:BoundField DataField="student_full_name" HeaderText="שם התלמיד" SortExpression="student_full_name" ReadOnly="True" />
                <asp:BoundField DataField="teacher_full_name" HeaderText="המתגבר" SortExpression="teacher_full_name" ReadOnly="True" />
                <asp:BoundField DataField="Les_maxQuan" HeaderText="קיבלות" SortExpression="Les_maxQuan" />
                <asp:BoundField DataField="actLes_date" HeaderText="תאריך התגבור" SortExpression="actLes_date" />
                <asp:BoundField DataField="Les_startHour" HeaderText="שעת התחלה" SortExpression="Les_startHour" />
                <asp:BoundField DataField="Les_EndHour" HeaderText="שעת סיום" SortExpression="Les_EndHour" />
                <asp:BoundField DataField="Les_Day" HeaderText="יום בשבוע" SortExpression="Les_Day" />
                <asp:BoundField DataField="quantity" HeaderText="כמות נוכחית" SortExpression="quantity" />
                <asp:BoundField DataField="req_is_permanent" HeaderText="האם קבוע" SortExpression="req_is_permanent" />
                <asp:BoundField DataField="req_status" HeaderText="סטטוס הבקשה" SortExpression="req_status" />
                <asp:BoundField DataField="ActLes_LesId" HeaderText="מספר התגבור" SortExpression="ActLes_LesId" />
                <asp:BoundField DataField="req_type" HeaderText="סוג הבקשה" ReadOnly="True" SortExpression="req_type" />
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

