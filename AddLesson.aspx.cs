using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class AddLesson : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["studentDBConnectionString"].ConnectionString);
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds = new DataSet();
    string query;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetTeachers();
            professionDDL.Items.Insert(0, "בחר מקצוע ");
        }
    }

    private void GetTeachers()
    {
        query = "select tea_id, (Tea_FirstName + ' '  + Tea_LastName + ' - ' + cast(Tea_Id as nvarchar (255))) as 'fullName'  from Teacher";
        da = new SqlDataAdapter(query, con);

        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            teacherDDL.DataSource = ds;
            teacherDDL.DataTextField = "fullName";
            teacherDDL.DataValueField = "tea_id";
            teacherDDL.DataBind();
            teacherDDL.Items.Insert(0, new ListItem(" בחר מתגבר ", "0"));
            teacherDDL.SelectedIndex = 0;
        }
    }

   

    protected void teacherDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        ds.Clear();
        string get_teacherName, teacherName;
        teacherName = teacherDDL.SelectedItem.Text;
        get_teacherName = teacherDDL.SelectedValue.ToString();

        if (get_teacherName != "0")
        {
            query = "Select pro.pro_id, pro.pro_title from Profession pro inner join TeachBy tb on pro.Pro_Id= tb.pro_id where tb.tea_id='" + get_teacherName.ToString() + "'";
            da = new SqlDataAdapter(query, con);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                professionDDL.DataSource = ds;
                professionDDL.DataTextField = "pro_title";
                professionDDL.DataValueField = "pro_id";
                professionDDL.DataBind();
                professionDDL.Items.Insert(0, new ListItem("בחר מקצוע", "0"));
                professionDDL.SelectedIndex = 0;
            }
        }
        else
        {
            professionDDL.Items.Insert(0, " מקצוע לא נבחר ");
            professionDDL.DataBind();
        }

    }

    protected void submitBTN_Click(object sender, EventArgs e)
    {
        Lesson les;
        double teacheId =Convert.ToDouble(teacherDDL.SelectedValue);
        int proId = Convert.ToInt32(professionDDL.SelectedValue);

        try
        {
            les = new Lesson(teacheId, proId,Convert.ToInt32(maxQuanTB.Text),(startTimeTB.Text),(endTimeTB.Text),Convert.ToInt32(dayDDL.SelectedValue));
        }
        catch (Exception ex)
        {
            Response.Write("illegal values to the Student attributes - error message is " + ex.Message);
            return;
        }

        try
        {
            int numEffected = les.InsertLesson();
            //lbl.Text = "תגבור " + numEffected.ToString() + " נוסף בהצלחה למערכת";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirmationModal", "$('#confirmationModal').modal();", true);

        }
        catch (Exception ex)
        {
            Response.Write("There was an error when trying to Insert the student into the database" + ex.Message);
        }

    }
}