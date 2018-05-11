using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class student_dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Student UserStudent = null;
        if (Session["stuUserSession"] != null)
        {
            UserStudent = (Student)(Session["stuUserSession"]);
            Session["userStudent"] = UserStudent;
        }
        else Response.Redirect("login.aspx");
        if (!IsPostBack)
        {

            Student s = (Student)(Session["userStudent"]);
            string reqStuId = Convert.ToString(s.Stu_id);
            upcomingLessonsForStudentDS.SelectParameters.Add("current_stu_id", reqStuId);
        }

    }
}