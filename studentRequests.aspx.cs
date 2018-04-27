using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class studentRequests : System.Web.UI.Page
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
            stuReqDS.SelectParameters.Add("current_stu_id", reqStuId);
        }

    }

    protected void stuReqGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[7].Text == "1")
                e.Row.Cells[7].Text = "קבוע";
            else
                e.Row.Cells[7].Text = "חד-פעמי";

            if (e.Row.Cells[6].Text == "1")
                e.Row.Cells[6].Text = "א'";
            else if (e.Row.Cells[6].Text == "2")
                e.Row.Cells[6].Text = "ב'";
            else if (e.Row.Cells[6].Text == "3")
                e.Row.Cells[6].Text = "ג''";
            else if (e.Row.Cells[6].Text == "4")
                e.Row.Cells[6].Text = "ד'";
            else // יום חמישי 
                e.Row.Cells[6].Text = "ה'";


            if (e.Row.Cells[0].Text == "1")
                e.Row.Cells[0].Text = "אושרה";

            else if (e.Row.Cells[0].Text == "0")
                e.Row.Cells[0].Text = "נדחתה";

            else
                e.Row.Cells[0].Text = "ממתינה";

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.Cells[3].Text.IndexOf(" ");
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Substring(0, index);
        }
    }
}