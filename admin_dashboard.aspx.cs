using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_dashboard : System.Web.UI.Page
{
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


}