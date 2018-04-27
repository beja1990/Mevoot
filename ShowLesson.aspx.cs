﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShowLesson : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void addTemplateBTN_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddLesson.aspx");

    }

    protected void LessonTemplateGRDW_SelectedIndexChanged(object sender, EventArgs e)
    {
        int less_id = Convert.ToInt32(LessonTemplateGRDW.SelectedRow.Cells[1].Text);
        Session["selectedID"] = less_id;
        Response.Redirect("editLessonTemplate.aspx");
    }
}