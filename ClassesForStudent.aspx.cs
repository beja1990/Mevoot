using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


public partial class ClassesForStudent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Student UserStudent = null;
            if (Session["stuUserSession"] != null)
            {
                UserStudent = (Student)(Session["stuUserSession"]);
                Session["userStudent"] = UserStudent;
            }
            else Response.Redirect("login.aspx");
        }

        Student s = (Student)(Session["userStudent"]);
        double reqStuId = s.Stu_id;


        string searchExpression = "req_stu_id =" + reqStuId;
        Session["searchExpression"] = searchExpression;

        DataTable dt = this.GetData();
        DataRow[] foundRows = dt.Select(searchExpression); //תגבורים שאליהם הסטודנט הספציפי כבר שלח בקשה להירשם והבקשה ממתינה לאישור
        Session["dataRowsForUser"] = foundRows;

        DataTable dt2 = this.GetDetails();
        DataRow[] foundRows2 = dt2.Select(searchExpression); //תגבורים שאליהם הסטודנט הספציפי כבר רשום, כלומר הוא כבר שלח בקשה להרשמה ובקשתו אושרה
        Session["dataRowsForUser2"] = foundRows2;
    }

    protected void classesGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.Cells[3].Text.IndexOf(" ");
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Substring(0, index);
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToInt32(e.Row.Cells[8].Text) < (Convert.ToInt32(e.Row.Cells[7].Text))) //במידה והסטודנט לא רשום לתגבור ויש עדיין מקום בתגבור, כלומר אם הכמות המקסימלית קטנה מהכמות הפועל, ניתן להירשם לתגבור ולכן נציג את כפתור "הגש בקשה"
            {
                Button requestBTN = (Button)e.Row.FindControl("requestButton");
                requestBTN.Visible = true;
            }
            else
            {
                e.Row.Cells[0].Text = "לא ניתן להירשם"; //במידה והסטודנט לא רשום לתגבור וגם אין עוד מקום בתגבור, לא נאפשר הרשמה לתגבור זה
            }
        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string searchExp = (string)(Session["searchExpression"]);
            DataRow[] userRows = (DataRow[])(Session["dataRowsForUser"]);
            foreach (DataRow dr in userRows)
            {
                Student s = (Student)(Session["userStudent"]);
                double stuId = s.Stu_id;

                DateTime dat = (DateTime)(dr["ActLes_date"]);
                string lesNum = Convert.ToString(dr["ActLes_LesId"]);
                string lesDate = dat.ToString("dd/MM/yyyy");
                int req_status = Convert.ToInt32(dr["req_status"]);
                double ReqStuID = Convert.ToDouble(dr["req_stu_id"]);


                if ((e.Row.Cells[1].Text == lesNum) && (e.Row.Cells[3].Text == lesDate) && (req_status == 2) && (stuId == ReqStuID)) //במידה והסטודנט הספציפי שלח בקשה לתגבור ספציפי
                {

                    ////e.Row.Cells[0].Text = "ממתין לאישור";
                    Button requestBTN = (Button)e.Row.FindControl("requestButton");
                    requestBTN.Visible = false; //לא ניתן לשלוח בקשה להרשמה לתגבור שכבר שלח לו בקשה
                    Button cancelBTN = new Button();
                    cancelBTN.Text = "בטל בקשה";
                    cancelBTN.ID = "cancelButton";
                    cancelBTN.CssClass = "btn btn-success btn-sm";
                    cancelBTN.BackColor = System.Drawing.Color.FromArgb(255, 0, 0);
                    cancelBTN.Click += cancelBTN_click;
                    cancelBTN.OnClientClick = "javascript:return window.alert('בקשתך בוטלה')";

                    Button cancelButton = (Button)e.Row.FindControl("cancelButton");
                    if (cancelButton == null) //אם אין עדיין כפתור של ביטול בקשה, נוסיף כזה
                    {
                        e.Row.Cells[0].Controls.Add(cancelBTN);
                    }
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DataRow[] userRows = (DataRow[])(Session["dataRowsForUser2"]);
            foreach (DataRow dr in userRows)
            {
                Student s = (Student)(Session["userStudent"]);
                double stuId = s.Stu_id;

                DateTime dat = (DateTime)(dr["ActLes_date"]);
                string lesNum = Convert.ToString(dr["ActLes_LesId"]);
                string lesDate = dat.ToString("dd/MM/yyyy");
                int req_status = Convert.ToInt32(dr["req_status"]);
                double ReqStuID = Convert.ToDouble(dr["req_stu_id"]);


                if ((e.Row.Cells[1].Text == lesNum) && (e.Row.Cells[3].Text == lesDate) && req_status != 2 && (stuId == ReqStuID)) //תגבור ספציפי עבור תלמיד ספציפי, אם הסטטוס שונה מ2, המשמעות שהבקשה כבר אושרה או נדחתה
                {
                    if (req_status == 1) // request aproved
                    {

                        //e.Row.Cells[0].Text = "הינך רשום לתגבור זה";

                        Button cancelParticipationBTN = new Button();
                        cancelParticipationBTN.Text = "בטל השתתפות";
                        cancelParticipationBTN.ID = "cancelParticipationButton";
                        cancelParticipationBTN.CssClass = "btn btn-success btn-sm";
                        cancelParticipationBTN.BackColor = System.Drawing.Color.FromArgb(36, 85, 129);
                        cancelParticipationBTN.Click += cancelParticipationButton_Click;
                        cancelParticipationBTN.OnClientClick = "javascript:return window.alert('בקשתך לביטול הרשמה לתגבור נשלחה')";
                        e.Row.Cells[0].Controls.Add(cancelParticipationBTN);

                        //Button requestBTN = (Button)e.Row.FindControl("requestButton");
                        //requestBTN.Visible = false; //לא ניתן לשלוח בקשה להרשמה לתגבור שכבר רשום אליו

                    }
                    else // request denied
                    {
                        e.Row.Cells[0].Text = "בקשתך להירשם נדחתה";
                    }

                }
            }
        }

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (e.Row.Cells[0].Text == "בקשתך להירשם נדחתה")
        //    {
        //        Button cancelParticipationBTN = (Button)e.Row.FindControl("cancelParticipationButton");
        //        cancelParticipationBTN.Visible = false;
        //    }
        //}
    }


    private void cancelBTN_click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        Student s = (Student)(Session["userStudent"]);
        double reqStuId = s.Stu_id;

        int actLes_id = Convert.ToInt32(gvRow.Cells[1].Text);
        DateTime actlDate = Convert.ToDateTime(gvRow.Cells[3].Text);

        try
        {
            Request req = new Request();
            int numEffected = req.deleteRequest(reqStuId, actLes_id, actlDate);
        }
        catch (Exception ex)
        {
            Response.Write("There was an error " + ex.Message);
        }

        Button cancelBTN = (Button)(sender as Control);
        cancelBTN.Visible = false;

        Button requestButton = new Button();
        requestButton.ID = "requestButton";
        gvRow.Cells[0].Controls.Add(requestButton);
        Server.TransferRequest(Request.Url.AbsolutePath, false);

    }


    protected void requestButton_Click(object sender, EventArgs e)
    {

        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int index = gvRow.RowIndex;

        Student s = (Student)(Session["userStudent"]);
        double reqStuId = s.Stu_id;
        Request req;
        int actLes_id = Convert.ToInt32(gvRow.Cells[1].Text);
        DateTime actlDate = Convert.ToDateTime(gvRow.Cells[3].Text);
        int req_status = 2; //the request status is now pending
        int is_permanent = 0; //ther is only one-time request for now
        int req_type = 0; // הגשת בקשה להרשמה מקבלת סוג=0
        int flg = 0;
        DateTime nowDate =  DateTime.Now;

        DataTable dt = this.GetRequests();
        foreach (DataRow dr in dt.Rows)
        {

            DateTime dat = (DateTime)(dr["req_actLes_date"]);
            int lesNum = Convert.ToInt32(dr["req_actLes_id"]);
            double ReqStuID = Convert.ToDouble(dr["req_stu_id"]);
            int reqType = Convert.ToInt32(dr["req_type"]);
        

            if (dat == actlDate && lesNum == actLes_id && ReqStuID == reqStuId && reqType == req_type)
            {
                flg = 1;
            }
        }
        if (flg == 0)
        {
            try
            {
                req = new Request(actLes_id, actlDate, reqStuId, req_status, is_permanent, req_type, nowDate);
                int numEffected = req.InsertRequest();
            }
            catch (Exception ex)
            {
                Response.Write("There was an error " + ex.Message);
            }
        }
        Button requestBTN = (Button)(sender as Control);
        requestBTN.Visible = false;
        Server.TransferRequest(Request.Url.AbsolutePath, false);
    }


    private DataTable GetData()
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["studentDBConnectionString"].ConnectionString;
        string sql = " select req_status, req_stu_id,ActLes_LesId, Les_Id,Pro_Title, ActLes_date,Les_StartHour,Les_EndHour,(Tea_FirstName + ' ' +  Tea_LastName) as 'full_name', Les_MaxQuan,quantity from Lesson inner join ActualLesson on Les_Id = ActLes_LesId inner join Teacher on Les_Tea_Id = Tea_Id inner join Profession on Les_Pro_Id = Pro_Id inner join requests on req_actLes_id = Les_Id and req_actLes_date = ActLes_date";

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



    private DataTable GetDetails()
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["studentDBConnectionString"].ConnectionString;
        string sql = "select req_status, req_stu_id, ActLes_LesId, Les_Id, Pro_Title, ActLes_date, Les_StartHour, Les_EndHour,(Tea_FirstName + ' ' + Tea_LastName) as 'full_name', Les_MaxQuan,quantity from Lesson inner join ActualLesson on Les_Id = ActLes_LesId inner join Teacher on Les_Tea_Id = Tea_Id inner join Profession on Les_Pro_Id = Pro_Id inner join requests on req_actLes_id = Les_Id and req_actLes_date = ActLes_date";

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

    private DataTable GetRequests()
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["studentDBConnectionString"].ConnectionString;
        string sql = "select req_stu_id, req_actLes_id, req_actLes_date, req_type from requests";

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

    protected void cancelParticipationButton_Click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int index = gvRow.RowIndex;

        Student s = (Student)(Session["userStudent"]);
        double reqStuId = s.Stu_id;
        Request req;
        int actLes_id = Convert.ToInt32(gvRow.Cells[1].Text);
        DateTime actlDate = Convert.ToDateTime(gvRow.Cells[3].Text);
        int req_status = 2; //the request status is now pending
        int is_permanent = 0; //ther is only one-time request for now
        int req_type = 1; // הגשת בקשה לביטול השתתפות מקבלת סוג=1

        int flg = 0;

        DataTable dt = this.GetRequests();
        foreach (DataRow dr in dt.Rows)
        {

            DateTime dat = (DateTime)(dr["req_actLes_date"]);
            int lesNum = Convert.ToInt32(dr["req_actLes_id"]);
            double ReqStuID = Convert.ToDouble(dr["req_stu_id"]);
            int reqType = Convert.ToInt32(dr["req_type"]);

            if (dat == actlDate && lesNum == actLes_id && ReqStuID == reqStuId && reqType == req_type)
            {
                flg = 1;
            }
        }
        if (flg == 0)
        {
            try
            {
                req = new Request(actLes_id, actlDate, reqStuId, req_status, is_permanent, req_type);
                int numEffected = req.InsertRequest();

            }
            catch (Exception ex)
            {
                Response.Write("There was an error " + ex.Message);
            }

            //Button cancelButton = new Button();
            //cancelButton.Text = "בטל בקשה";
            //cancelButton.ID = "cancelButton";
            //requestButton.CssClass = "btn btn-success btn-sm";
            //requestButton.BackColor = System.Drawing.Color.FromArgb(255, 0, 0);
            //requestButton.Click += cancelBTN_click;
            //requestButton.OnClientClick = "javascript:return window.alert('בקשתך להירשם לתגבור נשלחה";
            //gvRow.Cells[0].Controls.Add(cancelButton);

            //Button cancelParticipationBTN = (Button)(sender as Control);
            //cancelParticipationBTN.Visible = false;


            //gvRow.Cells[0].Text = "נשלחה בקשה לביטול השתתפות";
            //Server.TransferRequest(Request.Url.AbsolutePath, false);

        }
        //Button cancelParticipationBTN = (Button)(sender as Control);
        //cancelParticipationBTN.Visible = false;

        gvRow.Cells[0].Text = "נשלחה בקשה לביטול השתתפות";

        //Server.TransferRequest(Request.Url.AbsolutePath, false);
    }
}