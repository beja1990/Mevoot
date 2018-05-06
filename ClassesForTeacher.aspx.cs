using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


public partial class ClassesForTeacher : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Teacher UserSTeacher = null;
            if (Session["teaUserSession"] != null)
            {
                UserSTeacher = (Teacher)(Session["teaUserSession"]);
                Session["teaUserSession"] = UserSTeacher;
            }
            //else Response.Redirect("login.aspx");
        }

        Teacher t = (Teacher)(Session["teaUserSession"]);
        double TeaId = t.Tea_id;
        Session["TeaId"] = t.Tea_id;

        string searchExpression = "Tea_id =" + TeaId;
        Session["searchExpression"] = searchExpression;

        DataTable dt1 = this.GetDetails();
        dt1.Columns.Remove("Les_Tea_id");

        teacherClasses.DataSource = dt1;
        teacherClasses.DataBind();
    }


    private DataTable GetDetails()
    {
        double tea_id = (double)(Session["TeaId"]);
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["studentDBConnectionString"].ConnectionString;
        string sql = "select Les_Tea_id ,ActLes_LesId as 'מספר תגבור',Pro_Title as 'מקצוע', ActLes_date as 'תאריך', Les_day as 'יום בשבוע', Les_StartHour as 'שעת התחלת', Les_EndHour as 'שעת סיום', Les_MaxQuan as 'כמות מקסימלית', quantity as 'כמות נוכחית', Attendance_Form as 'טופס נוכחות' from Lesson inner join ActualLesson on Les_Id = ActLes_LesId inner join Profession on Les_Pro_Id = Pro_Id where les_tea_id=" + tea_id;

        using (SqlConnection conn = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                cmd.Connection = conn;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }
        }
        return dt;
    }

    protected void teacherClasses_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.Cells[3].Text.IndexOf(" ");
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Substring(0, index);
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[9].Text != "1")
            {
                e.Row.Cells[9].Text = "טרם נשלח טופס";
                e.Row.Cells[9].ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                e.Row.Cells[9].Text = "נשלח";
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string iDate = e.Row.Cells[3].Text;
            DateTime oDate = Convert.ToDateTime(iDate);
            DateTime currentDate = DateTime.Now.Date;

            string endHour = e.Row.Cells[5].Text;
            DateTime eHour = Convert.ToDateTime(endHour);
            TimeSpan end_hour = eHour.TimeOfDay;
            TimeSpan currentHour = DateTime.Now.TimeOfDay;

            if ((e.Row.Cells[9].Text == "טרם נשלח טופס" && oDate < currentDate) || e.Row.Cells[9].Text == "טרם נשלח טופס" && oDate == currentDate && end_hour > currentHour)
            {
                LinkButton linkBTN = e.Row.FindControl("LinkButton1") as LinkButton;
                linkBTN.Text = "שלח טופס נוכחות";
                linkBTN.Command += LinkButton1_Command;
            }
            if (e.Row.Cells[9].Text != "טרם נשלח טופס" && oDate < currentDate)
            {
                LinkButton linkBTN = e.Row.FindControl("LinkButton1") as LinkButton;
                linkBTN.Text = "הצג טופס נוכחות";
                linkBTN.Command += LinkButton1_Command_ShowForm;
            }
            if (oDate > currentDate)
            {
                LinkButton linkBTN = e.Row.FindControl("LinkButton1") as LinkButton;
                linkBTN.Text = "הגש בקשה לביטול";
                linkBTN.Command += LinkButton1_Command_request;
            }
        }
    }


    public void LinkButton1_Command(Object sender, CommandEventArgs e) //טיפול במצב שבו התגבור כבר התקיים ועדיין לא נשלח טופס נוכחות
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int selectedLessonNum = Convert.ToInt32(gvRow.Cells[1].Text);
        string selectedLessonDate = (gvRow.Cells[3].Text);
        Session["selectedLessonNum"] = selectedLessonNum;
        Session["selectedLessonDate"] = selectedLessonDate;
        Session["selectedLessonProf"] = gvRow.Cells[2].Text;
        Session["selectedLessonDay"] = Convert.ToInt32(gvRow.Cells[4].Text);
        Session["selectedLessonHour"] = gvRow.Cells[5].Text;
        Teacher t = (Teacher)(Session["teaUserSession"]);
        string teaName = t.Tea_firstName + " " + t.Tea_lastName;
        Session["selectedLessonTeacher"] = teaName;
        Response.Redirect("TeacherAttendanceForm.aspx");


    }
    public void LinkButton1_Command_request(Object sender, CommandEventArgs e)//מטפל במצב שבו התגבור עוד לא התקיים והוגשה בקשה לביטול התגבור (מצד המתגבר)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int selectedLessonNum = Convert.ToInt32(gvRow.Cells[1].Text);
        string selectedLessonDate = (gvRow.Cells[3].Text);
        Session["selectedLessonNum"] = selectedLessonNum;
        Session["selectedLessonDate"] = selectedLessonDate;


    }
    public void LinkButton1_Command_ShowForm(Object sender, CommandEventArgs e)//מטפל במצב שבו התגבור התקיים והטופס נוכחות מלא ורוצים לראות את הטופס
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int selectedLessonNum = Convert.ToInt32(gvRow.Cells[1].Text);
        string selectedLessonDate = (gvRow.Cells[3].Text);
        Session["selectedLessonNum"] = selectedLessonNum;
        Session["selectedLessonDate"] = selectedLessonDate;
        Response.Redirect("TeacherAttendanceForm.aspx");

    }

}