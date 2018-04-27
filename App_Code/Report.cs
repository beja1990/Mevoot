using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Report
/// </summary>
public class Report
{
    int id;
    string pro_title;
    double amount;

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public double Amount
    {
        get
        {
            return amount;
        }

        set
        {
            amount = value;
        }
    }

    public string Pro_title
    {
        get
        {
            return pro_title;
        }

        set
        {
            pro_title = value;
        }
    }

    public Report()
    {
            
    }

    public List<Report> getProfessionCount(string startDate,string endDate)// מחזיר כמות תגבורים לפי מקצועות
    {
        DBServices dbsReport = new DBServices();
        List<Report> dbCountProfessionReport = dbsReport.getProfessionCount(startDate,endDate,"studentDBConnectionString");
        return dbCountProfessionReport;
    }


}