using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class entitled_list : System.Web.UI.Page
{
    ArrayList arraylist1 = new ArrayList();
    ArrayList arraylist2 = new ArrayList();
    int updateIsEntitledTo;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void transferOneBTN_Click(object sender, EventArgs e)
    {

        if (filterTB.Text != null)
            filterTB.Text = "";
        if (allLB.SelectedIndex >= 0)
        {
            for (int i = 0; i < allLB.Items.Count; i++)
            {
                if (allLB.Items[i].Selected)
                {


                    if (!arraylist1.Contains(allLB.Items[i]))
                    {
                        arraylist1.Add(allLB.Items[i]);
                        updateInDB(Convert.ToDouble(allLB.Items[i].Value), updateIsEntitledTo);
                    }
                }
            }
            for (int i = 0; i < arraylist1.Count; i++)
            {
                if (!blackLB.Items.Contains(((ListItem)arraylist1[i])))
                {
                    blackLB.Items.Add(((ListItem)arraylist1[i]));
                }
                allLB.Items.Remove(((ListItem)arraylist1[i]));
            }
            blackLB.SelectedIndex = -1;
        }
        else
        {
            lbltxt.Visible = true;
            lbltxt.Text = "Please select atleast one in Listbox1 to move";
        }
    }



    protected void transferOneBackBTN_Click(object sender, EventArgs e)
    {
        lbltxt.Visible = false;
        if (blackLB.SelectedIndex >= 0)
        {
            for (int i = 0; i < blackLB.Items.Count; i++)
            {
                if (blackLB.Items[i].Selected)
                {
                    if (!arraylist2.Contains(blackLB.Items[i]))
                    {
                        arraylist2.Add(blackLB.Items[i]);
                        updateIsEntitledTo = 1;
                        updateInDB(Convert.ToDouble(blackLB.Items[i].Value), updateIsEntitledTo);
                    }
                }
            }
            for (int i = 0; i < arraylist2.Count; i++)
            {
                if (!allLB.Items.Contains(((ListItem)arraylist2[i])))
                {
                    allLB.Items.Add(((ListItem)arraylist2[i]));
                }
                blackLB.Items.Remove(((ListItem)arraylist2[i]));
            }
            allLB.SelectedIndex = -1;
        }
        else
        {
            lbltxt.Visible = true;
            lbltxt.Text = "Please select atleast one in Listbox2 to move";
        }
    }

    protected void transferAllBackBTN_Click(object sender, EventArgs e)
    {
        updateIsEntitledTo = 1;
        lbltxt.Visible = false;
        while (blackLB.Items.Count != 0)
        {
            for (int i = 0; i < blackLB.Items.Count; i++)
            {
                updateInDB(Convert.ToDouble(blackLB.Items[i].Value), updateIsEntitledTo);
                allLB.Items.Add(blackLB.Items[i]);
                blackLB.Items.Remove(blackLB.Items[i]);
            }
        }
    }

    public void updateInDB(double id, int updateIsEntitledTo)
    {
        Student stud = new Student();
        try
        {
            int numEffected = stud.updateIsEntittled(id, updateIsEntitledTo);
        }
        catch (Exception ex)
        {
            Response.Write("There was an error when trying to insert the product into the database" + ex.Message);
        }
    }
}