<%@ Page Title="" Language="C#" MasterPageFile="~/MP/TeacherMasterPage.master" AutoEventWireup="true" CodeFile="TeacherAttendanceForm.aspx.cs" Inherits="TeacherAttendanceForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //for serching gridview on keyup
        function Filter(Obj) {

<%--            var grid = document.getElementById(("<%= studentsGRDW.ClientID %>"));
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
            }--%>
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>טופס נוכחות</h3>
    <asp:Label ID="teacherLBL" runat="server" Text=""></asp:Label>
    <table id="Attendance_Form ">
        <tr>
            <td>
                <asp:Label ID="profssionLBL" runat="server" Text=""></asp:Label>&nbsp</td>
            <td>
                <asp:Label ID="dateLBL" runat="server" Text=""></asp:Label>&nbsp</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="DayLBL" runat="server" Text=""></asp:Label>&nbsp</td>
            <td>
                <asp:Label ID="hourLBL" runat="server" Text=""></asp:Label>&nbsp</td>
        </tr>
    </table>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
    <br />
    <%--    <asp:CheckBox ID="CheckBox1" runat="server" /><asp:Label ID="studentLBL" runat="server" Text=""></asp:Label>

    <asp:TextBox ID="commentsTB" runat="server" placeholder="הוסף הערה..."></asp:TextBox>--%>

    <asp:Button ID="addStuTB" runat="server" Text="הוסף תלמיד" />

    <asp:Button ID="sendForm" runat="server" Text="שלח טופס" onclick="sendForm_Click"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="jsPlaceHolder" runat="Server">
</asp:Content>