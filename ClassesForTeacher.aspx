<%@ Page Title="" Language="C#" MasterPageFile="~/MP/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="ClassesForTeacher.aspx.cs" Inherits="ClassesForTeacher
    
    
    " %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="sectionTitle text-center">
        <h2>
            <span class="shape shape-left bg-color-4"></span>
            <span>התגבורים שלי</span>
            <span class="shape shape-right bg-color-4"></span>
        </h2>
    </div>
    <div class="container content">
        <asp:GridView ID="teacherClasses" runat="server" Style="margin-left: auto; margin-right: auto; margin-bottom: 100px; text-align: center; width: 80%" AllowSorting="True" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" EnableViewState="false" OnRowDataBound="teacherClasses_RowDataBound" >
            <AlternatingRowStyle BackColor="#F7F7F7" />
            <Columns>
                <%--                <asp:CommandField ShowSelectButton="True" SelectText="פרטים" />--%>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1"
                            Text="שלח טופס נוכחות"
                            CommandName="Attendance_Form"
                            OnCommand="LinkButton1_Command"
                            runat="server" CommandArgument='<%# Container.DataItem %>' />
                    </ItemTemplate>
                </asp:TemplateField>
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
<asp:Content ID="Content3" ContentPlaceHolderID="jsPlaceHolder" runat="Server">
</asp:Content>