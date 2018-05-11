using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class TeacherAttendanceForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string tea_name = (string)(Session["selectedLessonTeacher"]);
        //string proffesion = (string)(Session["selectedLessonProf"]);
        //string lessDate = (string)(Session["selectedLessonDate"]);
        //int lessDay = Convert.ToInt32(Session["selectedLessonDay"]);
        //string lessHour = (string)(Session["selectedLessonHour"]);
        //int lessID = Convert.ToInt32(Session["selectedLessonNum"]);

        //Session["teaNme"] = tea_name;
        //Session["proffesion"] = proffesion;
        //Session["lessDate"] = lessDate;
        //Session["lessDay"] = lessDay;
        //Session["lessHour"] = lessHour;
        //Session["lessID"] = lessID;


    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //string tea_name = (string)(Session["teaNme"]);
            //string proffesion = (string)(Session["proffesion"]);
            //string lessDate = (string)(Session["lessDate"]);
            //int lessDay = Convert.ToInt32(Session["lessDay"]);
            //string lessHour = (string)(Session["lessHour"]);

            string tea_name = (string)(Session["selectedLessonTeacher"]);
            string proffesion = (string)(Session["selectedLessonProf"]);
            string lessDate = (string)(Session["selectedLessonDate"]);
            int lessDay = Convert.ToInt32(Session["selectedLessonDay"]);
            string lessHour = (string)(Session["selectedLessonHour"]);
            int lessID = Convert.ToInt32(Session["selectedLessonNum"]);

            teacherLBL.Text = "שם המתגבר: " + tea_name;
            profssionLBL.Text = proffesion;
            dateLBL.Text = lessDate;
            DayLBL.Text = "יום " + Convert.ToString(lessDay);
            hourLBL.Text = "שעה: " + lessHour;

            DateTime date_less = Convert.ToDateTime(lessDate);

            SignedToLesson stl = new SignedToLesson();
            DataTable dt = stl.readStudentsList(lessID, date_less);

            //DataTable studentsDT = new DataTable();

            foreach (DataRow dr in dt.Rows)
            {
                Student s = new Student();
                double Stu_id = Convert.ToDouble(dr["StLes_stuId"]);
                DataTable dt2 = s.readSpecipicStudent(Stu_id);
                foreach (DataRow dr2 in dt2.Rows)
                {
                    string stuFirstlName = (string)(dr2["stu_firstName"]);
                    string stuLastlName = (string)(dr2["stu_lastName"]);
                    string stuFullName = stuFirstlName + " " + stuLastlName;
                    //studentsDT.Rows.Add(stuFullName);
                    Label lbl = new Label();
                    lbl.Text = stuFullName;
                    CheckBox cb = new CheckBox();
                    cb.Checked = true;
                    TextBox tb = new TextBox();
                    //tb.placeholder = "הערות...";
                    PlaceHolder1.Controls.Add(cb);
                    PlaceHolder1.Controls.Add(lbl);
                    PlaceHolder1.Controls.Add(new LiteralControl("&nbsp &nbsp"));
                    PlaceHolder1.Controls.Add(tb);
                    PlaceHolder1.Controls.Add(new LiteralControl("<br/>"));
                }


            }


        }
    }

    protected void sendForm_Click(object sender, EventArgs e)
    {

    }
}