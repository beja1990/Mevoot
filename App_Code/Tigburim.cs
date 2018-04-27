using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Tigburim
/// </summary>
public class Tigburim
{
    private int id;
    private double teacherId;
    private string teacherName;
    private int profId;
    private string profName;
    private int limit;
    private int actualLimit;
    private string startTime;
    private string endTime;
    private string tigburDate;

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

    public double TeacherId
    {
        get
        {
            return teacherId;
        }

        set
        {
            teacherId = value;
        }
    }

    public string TeacherName
    {
        get
        {
            return teacherName;
        }

        set
        {
            teacherName = value;
        }
    }

    public int ProfId
    {
        get
        {
            return profId;
        }

        set
        {
            profId = value;
        }
    }

    public string ProfName
    {
        get
        {
            return profName;
        }

        set
        {
            profName = value;
        }
    }

    public int Limit
    {
        get
        {
            return limit;
        }

        set
        {
            limit = value;
        }
    }

    public int ActualLimit
    {
        get
        {
            return actualLimit;
        }

        set
        {
            actualLimit = value;
        }
    }

    public string StartTime
    {
        get
        {
            return startTime;
        }

        set
        {
            startTime = value;
        }
    }

    public string EndTime
    {
        get
        {
            return endTime;
        }

        set
        {
            endTime = value;
        }
    }

    public string TigburDate
    {
        get
        {
            return tigburDate;
        }

        set
        {
            tigburDate = value;
        }
    }

    public Tigburim()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public List<Tigburim> getTigburimList(string conStr, string tableName)// פונקציה שמופעלת מהדף של השרת, יוצרים רשימה של שמירות
    {
        DBServices dbs = new DBServices();
        List<Tigburim> dblist = dbs.getTigburimList(conStr,tableName);
        return dblist;
    }


    public List<Tigburim> getTigburimListForStudent(string conString, string tableName, string id)// פונקציה שמופעלת מהדף של השרת, יוצרים רשימה של שמירות
    {
        DBServices dbs = new DBServices();
        List<Tigburim> dblist = dbs.getTigburimListForStudent(conString, tableName,id);
        return dblist;
    }
  

    public Tigburim getTigburById(string tigID)
    {
        int TigID = Convert.ToInt16(tigID);
        DBServices dbs = new DBServices();
        Tigburim DBTigbur = dbs.getTigburById(TigID, "studentDBConnectionString", "Lesson");
        if (DBTigbur == null) return null;
        else return DBTigbur;
    }

}