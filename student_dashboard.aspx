<%@ Page Title="" Language="C#" MasterPageFile="~/MP/StudentMasterPage.master" AutoEventWireup="true" CodeFile="student_dashboard.aspx.cs" Inherits="student_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .rtl {
            direction: rtl;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container ">
        <div class="row col-lg-6">
            <section class="mainContent full-width clearfix featureSection">
                <div class="sectionTitle text-center">
                    <h2>
                        <span class="shape shape-left bg-color-4"></span>
                        <span>הודעות</span>
                        <span class="shape shape-right bg-color-4"></span>
                    </h2>
                </div>
            </section>
            <asp:SqlDataSource ID="upcomingLessonsForStudentDS" runat="server" ConnectionString="<%$ ConnectionStrings:studentDBConnectionString %>" SelectCommand="select top(10) actLes_date,Pro_Title,(Tea_FirstName + ' ' +  Tea_LastName) as 'full name',Les_StartHour,Les_EndHour,Les_MaxQuan,quantity from Lesson inner join ActualLesson on Les_Id = ActLes_LesId inner join Teacher on Les_Tea_Id= Tea_Id inner join Profession on Les_Pro_Id=Pro_Id inner join signedToLesson on ActLes_date=StLes_ActLesDate and StLes_ActLesId=ActLes_LesId where ActLes_date >= GETDATE()-1  AND actls_cancelled=0 AND StLes_stuId=@current_stu_id order by ActLes_date"></asp:SqlDataSource>
            <asp:GridView ID="upcomingLessonsGRDW" CssClass="grid" runat="server" AllowSorting="True" AutoGenerateColumns="False" ForeColor="#333333" CellPadding="4" Style="margin: 0 auto; margin-top: 20px; margin-bottom: 50px; text-align: center; width: 80%" DataSourceID="upcomingLessonsForStudentDS">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="actLes_date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="תאריך" SortExpression="actLes_date" />
                    <asp:BoundField DataField="Pro_Title" HeaderText="מקצוע" SortExpression="Pro_Title" />
                    <asp:BoundField DataField="full name" HeaderText="מתגבר" SortExpression="full name" />
                    <asp:BoundField DataField="Les_StartHour" HeaderText="התחלה" SortExpression="Les_StartHour" />
                    <asp:BoundField DataField="Les_EndHour" HeaderText="סיום" SortExpression="Les_EndHour" />

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
        <div class="row col-lg-6">
            <section class="mainContent full-width clearfix featureSection">
                <div class="sectionTitle text-center">
                    <h2>
                        <span class="shape shape-left bg-color-4"></span>
                        <span>תגבורים קרובים</span>
                        <span class="shape shape-right bg-color-4"></span>
                    </h2>
                </div>
            </section>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:studentDBConnectionString %>" SelectCommand="select top(10) actLes_date,Pro_Title,(Tea_FirstName + ' ' +  Tea_LastName) as 'full name',Les_StartHour,Les_EndHour,Les_MaxQuan,quantity from Lesson inner join ActualLesson on Les_Id = ActLes_LesId inner join Teacher on Les_Tea_Id= Tea_Id inner join Profession on Les_Pro_Id=Pro_Id inner join signedToLesson on ActLes_date=StLes_ActLesDate and StLes_ActLesId=ActLes_LesId where ActLes_date >= GETDATE()-1  AND actls_cancelled=0 AND StLes_stuId=@current_stu_id order by ActLes_date"></asp:SqlDataSource>
            <asp:GridView ID="GridView1" CssClass="grid" runat="server" AllowSorting="True" AutoGenerateColumns="False" ForeColor="#333333" CellPadding="4" Style="margin: 0 auto; margin-top: 20px; margin-bottom: 50px; text-align: center; width: 80%" DataSourceID="upcomingLessonsForStudentDS">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="actLes_date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="תאריך" SortExpression="actLes_date" />
                    <asp:BoundField DataField="Pro_Title" HeaderText="מקצוע" SortExpression="Pro_Title" />
                    <asp:BoundField DataField="full name" HeaderText="מתגבר" SortExpression="full name" />
                    <asp:BoundField DataField="Les_StartHour" HeaderText="התחלה" SortExpression="Les_StartHour" />
                    <asp:BoundField DataField="Les_EndHour" HeaderText="סיום" SortExpression="Les_EndHour" />

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

