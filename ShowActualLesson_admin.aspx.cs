using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class ShowActualLesson_admin : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["studentDBConnectionString"].ConnectionString);
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds = new DataSet();
    string query;
    string day;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Page.IsPostBack)
            {

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["studentDBConnectionString"].ConnectionString))
                {
                    con.Open();
                    //Change with your select statement .
                    using (SqlCommand cmd = new SqlCommand("Select * from Lesson le inner join Profession pro on le.Les_Pro_id=pro.Pro_Id inner join Teacher tea on tea.Tea_id=le.Les_Tea_Id", con))
                    {
                        DataTable dt = new DataTable();
                        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                        adpt.Fill(dt);
                        Dictionary<int, string> lst = new Dictionary<int, string>();

             
                        foreach (DataRow row in dt.Rows)
                        {

                            switch (row[6].ToString())
                            {
                                case "1":
                                    day = "ראשון";
                                    break;
                                case "2":
                                    day = "שני";
                                    break;
                                case "3":
                                    day = "שלישי";
                                    break;
                                case "4":
                                    day = "רביעי";
                                    break;
                                case "5":
                                    day = "חמישי";
                                    break;

                            }
                            //Add values to Dictionary
                            string val = row[8].ToString() + " - " + row[10].ToString() + " " + row[11].ToString() + " - " + day + " - " + row[4].ToString() + " " + row[5].ToString();
                            lst.Add(Convert.ToInt32(row[0]), val);
                        }
                        TigburDDL.DataSource = lst;
                        TigburDDL.DataTextField = "Value";
                        TigburDDL.DataValueField = "key";
                        TigburDDL.DataBind();
                        TigburDDL.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        TigburDDL.SelectedIndex = 0;
                    }
                }

            }

        }

        bool AddActualLesson = Convert.ToBoolean(Session["AddActualLesson"]);
        if (AddActualLesson)
        {
            int lessonCounter = Convert.ToInt32(Session["LessonCounterSession"]);
            if (lessonCounter == 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('נוסף תגבור אחד למערכת')", true);
            }
            else if (lessonCounter > 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.success('נוספו " + lessonCounter + " תגבורים למערכת')", true);

            }
        else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr_message", "toastr.warning('בחר מספר תגבורים להוספה')", true);
            }

            Session["AddActualLesson"] = false;
        }

    }

    Lesson les = new Lesson();





    protected void generateDate_Click(object sender, EventArgs e)
    {
        Session["AddActualLesson"] = true;

        string dt = Request.Form["DatePickername"];
        int les_id = (int)(Session["LES_ID"]);
        int quantity = 0;
        DateTime start_date = Convert.ToDateTime(dt);
        int amount = Convert.ToInt32(counterTB.Text);
        ActualLesson al;
        for (int i = 0; i < amount; i++)
        {
            al = new ActualLesson(les_id, dt, quantity);
            al.insertActualLesson();
            start_date = start_date.AddDays(7);
            dt = start_date.ToString("yyyy-MM-dd");


        }
        string lessonCounter = counterTB.Text;
        Session["LessonCounterSession"] = lessonCounter;
        Response.Redirect("ShowActualLesson_admin.aspx");
    }

    protected void TigburDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["LES_ID"] = Convert.ToInt32(TigburDDL.SelectedValue);
    }
}