<%@ Page Title="" Language="C#" MasterPageFile="~/MP/StudentMasterPage.master" AutoEventWireup="true" CodeFile="ClassesForStudent.aspx.cs" Inherits="ClassesForStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .container.content {
            direction: rtl;
        }
        .btn{
            width:120px;
            height:35px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="mainContent full-width clearfix featureSection">
        <div class="container">
            <ol class="breadcrumb">
                <li><a href="student_calendar.aspx">בית</a></li>
                <li class="active">תגבורים במרכז</li>

            </ol>
            <div class="sectionTitle text-center">
                <h2>
                    <span class="shape shape-left bg-color-4"></span>
                    <span>תגבורים</span>
                    <span class="shape shape-right bg-color-4"></span>
                </h2>
            </div>
        </div>
    </section>
    <div class="container content">

        <asp:SqlDataSource ID="classesDS" runat="server" ConnectionString="<%$ ConnectionStrings:studentDBConnectionString %>" SelectCommand=" select ActLes_LesId, Les_Id,Pro_Title, ActLes_date,Les_StartHour,Les_EndHour,(Tea_FirstName + ' ' +  Tea_LastName) as 'full_name', Les_MaxQuan,quantity
	from Lesson inner join ActualLesson on Les_Id = ActLes_LesId inner join Teacher on Les_Tea_Id= Tea_Id inner join Profession on Les_Pro_Id=Pro_Id"></asp:SqlDataSource>

        <asp:GridView ID="classesGV" runat="server" AutoGenerateColumns="False" DataSourceID="classesDS" AllowPaging="True" PageSize="20" Style="margin-left: auto; margin-right: auto; margin-bottom: 100px; text-align: center; width: 80%" AllowSorting="True" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" OnRowDataBound="classesGV_RowDataBound" EnableViewState="false">
            <AlternatingRowStyle BackColor="#F7F7F7" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="requestButton" Text="" runat="server" CssClass="btn btn-success btn-sm" CommandName="request"  />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--                <asp:BoundField DataField="req_status" HeaderText="סטטוס בקשה" InsertVisible="False" ReadOnly="True" SortExpression="req_status" />--%>
                <asp:BoundField DataField="ActLes_LesId" HeaderText="מספר תגבור" InsertVisible="False" ReadOnly="True" SortExpression="ActLes_LesId" />
                <asp:BoundField DataField="Pro_Title" HeaderText="מקצוע" SortExpression="Pro_Title" />
                <asp:BoundField DataField="ActLes_date" HeaderText="תאריך" SortExpression="ActLes_date" />
                <asp:BoundField DataField="Les_StartHour" HeaderText="שעת התחלה" SortExpression="Les_StartHour" />
                <asp:BoundField DataField="Les_EndHour" HeaderText="שעת סיום" SortExpression="Les_EndHour" />
                <asp:BoundField DataField="full_name" HeaderText="שם המתגבר" ReadOnly="True" SortExpression="full_name" />
                <asp:BoundField DataField="Les_MaxQuan" HeaderText="כמות מקסימלית בתגבור" SortExpression="Les_MaxQuan" />
                <asp:BoundField DataField="quantity" HeaderText="כמות נוכחית בתגבור" SortExpression="quantity" />
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