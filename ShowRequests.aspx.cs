using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShowRequests : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //if (Session["manUserSession"] == null)
        //{
        //    Response.Redirect("login.aspx");
        //}

    }

    protected void reqGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[12].Text == "1")
                e.Row.Cells[12].Text = "קבוע";
            else
                e.Row.Cells[12].Text = "חד-פעמי";

            if (e.Row.Cells[10].Text == "1")
                e.Row.Cells[10].Text = "א'";
            else if (e.Row.Cells[10].Text == "2")
                e.Row.Cells[10].Text = "ב'";
            else if (e.Row.Cells[10].Text == "3")
                e.Row.Cells[10].Text = "ג''";
            else if (e.Row.Cells[10].Text == "4")
                e.Row.Cells[10].Text = "ד'";
            else // יום חמישי 
                e.Row.Cells[11].Text = "ה'";


            if (e.Row.Cells[13].Text == "1")
                e.Row.Cells[13].Text = "אושרה";

            else if (e.Row.Cells[13].Text == "0")
                e.Row.Cells[13].Text = "נדחתה";

            else
                e.Row.Cells[13].Text = "ממתינה";


            if (e.Row.Cells[15].Text == "0")
                e.Row.Cells[15].Text = "הרשמה לתגבור";

            else if (e.Row.Cells[15].Text == "1")
                e.Row.Cells[15].Text = "ביטול השתתפות";
            else
                e.Row.Cells[15].Text = "";
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.Cells[7].Text.IndexOf(" ");
            e.Row.Cells[7].Text = e.Row.Cells[7].Text.Substring(0, index);
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[13].Text != "ממתינה")
            {
                Button statusAp = (Button)e.Row.FindControl("AproveButton");
                statusAp.Visible = false;
                Button statusDe = (Button)e.Row.FindControl("DeclineButton");
                statusDe.Visible = false;


            }
        }

    }



    protected void ApproveButton_Click(object sender, EventArgs e)
    {
        Request req = new Request();
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int index = gvRow.RowIndex;
        int req_num = Convert.ToInt32(gvRow.Cells[1].Text);
        int status = 1;
        int numEffected = req.updateSpecificRequest(req_num, status);

        int req_type = 1;
        string req_typeName = gvRow.Cells[15].Text;
        if (req_typeName != "ביטול השתתפות")
        {
            req_type = 0;
        }


        ActualLesson acl = new ActualLesson();
        SignedToLesson stl;
        double stuId = Convert.ToDouble(gvRow.Cells[3].Text);
        int lesId = Convert.ToInt32(gvRow.Cells[14].Text);
        DateTime tmpDate = Convert.ToDateTime(gvRow.Cells[7].Text);
        string lesDate = tmpDate.ToString("yyyy-MM-dd");


        if (req_type != 1) //אם זו בקשה להרשמה (זו לא בקשה לביטול השתתפות) 
        {
            try
            {
                stl = new SignedToLesson(lesId, lesDate, stuId);
            }
            catch (Exception ex)
            {
                Response.Write("illegal values to the SignedToLesson attributes - error message is " + ex.Message);
                return;
            }

            int actual_quan = Convert.ToInt32(gvRow.Cells[11].Text) + 1;



            try
            {
                int numEffected3 = acl.updateSpecificActualLesson(lesId, lesDate, actual_quan); //הגדלת כמות משתתפים ב-1
                int numEffected2 = stl.InsertSigendToLesson(); //הכנסת התלמיד לטבלת "רשום לתגבור"
                Server.TransferRequest(Request.Url.AbsolutePath, false);
            }
            catch (Exception ex)
            {
                Response.Write("There was an error " + ex.Message);
            }

        }
    }

    protected void DeclineButton_Click(object sender, EventArgs e)
    {
        Request req = new Request();
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int index = gvRow.RowIndex;
        int req_num = Convert.ToInt32(gvRow.Cells[1].Text);
        int status = 0;
        int numEffected = req.updateSpecificRequest(req_num, status);
        //Response.Redirect("ShowRequests.aspx");
        Server.TransferRequest(Request.Url.AbsolutePath, false);

    }
}




