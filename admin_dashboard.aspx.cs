using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_dashboard : System.Web.UI.Page
{
    public string inputStartValue;
    public string inputEndValue;
    protected void Page_Load(object sender, EventArgs e)
    {
        Report r = new Report();
        try
        {
            int requestsCounter = r.getRequestsCount();
            HiddenRequestsCounter.Text = requestsCounter.ToString();
            int attendenceFormsCounter = r.getAttendenceFormsCount();
            HiddenAttendenceFormsCounter.Text = attendenceFormsCounter.ToString();
        }
        catch (Exception ex)
        {
            Response.Write("There was an error when trying to get requests count" + ex.Message);
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", "$(function () { loadSettings(); });", true);
        }

    }




    protected void filter_dayBTN_Click(object sender, EventArgs e)
    {
        string startDate = DateTime.Now.ToString("yyyy-MM-dd");
        inputStartValue = startDate;
        inputEndValue = startDate;
        chartTitle.InnerHtml = "סינון: יום";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", "$(function () { loadGraphs(); });", true);

    }

    protected void filter_weekBTN_Click(object sender, EventArgs e)
    {
        DateTime date = DateTime.Now;
        string startDate = DateTime.Now.ToString("yyyy-MM-dd");
        string endDate = date.AddDays(7).ToString("yyyy-MM-dd");
        inputStartValue = startDate;
        inputEndValue = endDate;
        chartTitle.InnerHtml = "סינון: שבוע";

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", "$(function () { loadGraphs(); });", true);
    }

    protected void filter_monthBTN_Click(object sender, EventArgs e)
    {
        DateTime date = DateTime.Now;
        string startDate = DateTime.Now.ToString("yyyy-MM-dd");
        string endDate = date.AddMonths(1).ToString("yyyy-MM-dd");
        inputStartValue = startDate;
        inputEndValue = endDate;
        chartTitle.InnerHtml = "סינון: חודש";

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", "$(function () { loadGraphs(); });", true);
    }

    protected void filter_clear_Click(object sender, EventArgs e)
    {    
        inputStartValue = "";
        inputEndValue = "";
        chartTitle.InnerHtml = "";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", "$(function () { loadGraphs(); });", true);
    }

}