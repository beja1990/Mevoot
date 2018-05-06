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


        DataTable dt = this.GetRequests();
        Session["studentRequests"] = dt;

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
                Button BTN = e.Row.FindControl("requestButton") as Button;
                BTN.Text = "הגש בקשה";
                BTN.Click += requestButton_Click;
                BTN.OnClientClick = "javascript:return window.alert('בקשתך נשלחה')";
            }
            else
            {
                e.Row.Cells[0].Text = "לא ניתן להירשם"; //במידה והסטודנט לא רשום לתגבור וגם אין עוד מקום בתגבור, לא נאפשר הרשמה לתגבור זה
            }
        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DataTable userReqs = (DataTable)(Session["studentRequests"]);
            foreach (DataRow dr in userReqs.Rows)
            {
                Student s = (Student)(Session["userStudent"]);
                double stuId = s.Stu_id;

                DateTime dat = (DateTime)(dr["req_actLes_date"]);
                string lesNum = Convert.ToString(dr["req_actLes_id"]);
                string lesDate = dat.ToString("dd/MM/yyyy");
                int req_status = Convert.ToInt32(dr["req_status"]);
                int req_type = Convert.ToInt32(dr["req_type"]);
                double ReqStuID = Convert.ToDouble(dr["req_stu_id"]);


                if ((e.Row.Cells[1].Text == lesNum) && (e.Row.Cells[3].Text == lesDate)) //במידה והסטודנט הספציפי שלח בקשה לתגבור ספציפי
                {
                    if (req_type == 0 && req_status == 2)
                    {
                        Button BTN = e.Row.FindControl("requestButton") as Button;
                        BTN.Text = "בטל בקשה";
                        //BTN.BackColor = System.Drawing.Color.Red;
                        BTN.CssClass = "btn btn-danger btn-sm";
                        BTN.Click += cancelBTN_click;
                        BTN.OnClientClick = "javascript:return window.alert('בקשתך בוטלה')";
                    }
                    else if (req_type == 0 && req_status == 1)
                    {
                        Button BTN = e.Row.FindControl("requestButton") as Button;
                        BTN.Text = "בטל השתתפות";
                        //BTN.BackColor = System.Drawing.Color.DarkBlue;
                        BTN.CssClass = "btn btn-primary btn-sm";
                        BTN.Click += cancelParticipationButton_Click;
                        BTN.OnClientClick = "javascript:return window.alert('בקשתך לביטול הרשמה לתגבור נשלחה')";
                    }
                    else if (req_type == 0 && req_status == 0)
                    {
                        //e.Row.Cells[0].Text = "בקשתך להירשם נדחתה";
                        Button BTN = e.Row.FindControl("requestButton") as Button;
                        BTN.Text = "הגש בקשה";
                        //BTN.BackColor = System.Drawing.Color.DarkBlue;
                        BTN.CssClass = "btn btn-success btn-sm";
                        BTN.Enabled = false;
                        BTN.ToolTip = "בקשתך  להירשם לתגבור נדחתה";
                    }

                    if (req_type == 1 && req_status == 2)
                    {
                        e.Row.Cells[0].Text = "נשלחה בקשה לביטול השתתפות";
                    }
                    else if (req_type == 1 && req_status == 1)
                    {
                        Button BTN = e.Row.FindControl("requestButton") as Button;
                        BTN.Text = "הגש בקשה";
                        BTN.CssClass = "btn btn-success btn-sm";
                        BTN.Click += requestButton_Click;
                        BTN.OnClientClick = "javascript:return window.alert('בקשתך נשלחה')";
                    }
                    else if (req_type == 1 && req_status == 0)
                    {
                        //e.Row.Cells[0].Text = " בקשתך לביטול השתתפות נדחתה";
                        Button BTN = e.Row.FindControl("requestButton") as Button;
                        BTN.Text = "בטל השתתפות";
                        //BTN.BackColor = System.Drawing.Color.DarkBlue;
                        BTN.CssClass = "btn btn-primary btn-sm";
                        BTN.Enabled = false;
                        BTN.ToolTip = "בקשתך לביטול השתתפות בתגבור נדחתה";
                    }
                }
                //if ((e.Row.Cells[1].Text != lesNum) || (e.Row.Cells[3].Text != lesDate))
                // {
                //     Button BTN = e.Row.FindControl("requestButton") as Button;
                //     BTN.Text = "הגש בקשה";
                //     BTN.Click += requestButton_Click;
                //     BTN.OnClientClick = "javascript:return window.alert('בקשתך נשלחה')";
                // }
            }
        }


    }


    public void requestButton_Click(Object sender, EventArgs e)
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
        }
        Server.TransferRequest(Request.Url.AbsolutePath, false);
    }


    public void cancelBTN_click(Object sender, EventArgs e)
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
        Server.TransferRequest(Request.Url.AbsolutePath, false);

    }

    public void cancelParticipationButton_Click(Object sender, EventArgs e)
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
        }
        Server.TransferRequest(Request.Url.AbsolutePath, false);
    }



    private DataTable GetRequests()
    {
        Student s = (Student)(Session["userStudent"]);
        double reqStuId = s.Stu_id;

        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["studentDBConnectionString"].ConnectionString;
        string sql = "select * from requests where req_stu_id= '" + reqStuId + "'";

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


}