<%@ Page Title="" Language="C#" MasterPageFile="~/MP/AdminMasterPage.master" AutoEventWireup="true" CodeFile="ShowLesson.aspx.cs" Inherits="ShowLesson" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        #addTemplateBTN {
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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="mainContent full-width clearfix featureSection">
        <div class="container">
            <ol class="breadcrumb">
                <li><a href="admin_calendar.aspx">בית</a></li>
                <li class="active">רשימת תבניות תגבורים</li>
            </ol>
            <div class="sectionTitle text-center">
                <h2>
                    <span class="shape shape-left bg-color-4"></span>
                    <span>רשימת תבניות תגבורים</span>
                    <span class="shape shape-right bg-color-4"></span>
                </h2>
            </div>

        </div>
    </section>

    <asp:SqlDataSource ID="LessonTemplateDS" runat="server" ConnectionString="<%$ ConnectionStrings:studentDBConnectionString %>" SelectCommand="	select Les_Id,Pro_Title,Les_StartHour,Les_EndHour,(Tea_FirstName + ' ' +  Tea_LastName) as 'full name', Les_MaxQuan
	from Lesson inner join Teacher on Les_Tea_Id= Tea_Id inner join Profession on Les_Pro_Id=Pro_Id"></asp:SqlDataSource>

    <div class="container content">
        <div>
            <asp:Button ID="addTemplateBTN" runat="server" Text="הוסף תבנית תגבור" CssClass="btn btn-primary rightTB btn-sm" OnClick="addTemplateBTN_Click" />
        </div>
        <div>
            <asp:GridView ID="LessonTemplateGRDW" CssClass="grid" runat="server" Style="margin: 0 auto; margin-top: 20px; text-align: center; width: 100%" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="LessonTemplateDS" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="LessonTemplateGRDW_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField SelectText="פרטים" ShowSelectButton="True" />
                    <asp:BoundField DataField="Les_Id" HeaderText="מזהה תבנית" InsertVisible="False" ReadOnly="True" SortExpression="Les_Id" />
                    <asp:BoundField DataField="Pro_Title" HeaderText="מקצוע" SortExpression="Pro_Title" />
                    <asp:BoundField DataField="full name" HeaderText="מתגבר" SortExpression="full name" />
                    <asp:BoundField DataField="Les_StartHour" HeaderText="שעת התחלה" SortExpression="Les_StartHour" />
                    <asp:BoundField DataField="Les_EndHour" HeaderText="שעת סיום" SortExpression="Les_EndHour" />
                    <asp:BoundField DataField="Les_MaxQuan" HeaderText="כמות מקסימלית" SortExpression="Les_MaxQuan" />
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

