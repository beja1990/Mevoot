using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SignedToLesson
/// </summary>
public class SignedToLesson
{

    private int sigToLes_ActLesId;
    private string sigToLes_ActLesDate;
    private double sigToLess_stuId;

    public SignedToLesson()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int SigToLes_ActLesId
    {
        get
        {
            return sigToLes_ActLesId;
        }

        set
        {
            sigToLes_ActLesId = value;
        }
    }

    public string SigToLes_ActLesDate
    {
        get
        {
            return sigToLes_ActLesDate;
        }

        set
        {
            sigToLes_ActLesDate = value;
        }
    }

    public double SigToLess_stuId
    {
        get
        {
            return sigToLess_stuId;
        }

        set
        {
            sigToLess_stuId = value;
        }
    }



    public SignedToLesson(int LesId, string LesDate, double stuId)
    {

        SigToLes_ActLesId = LesId; ;
        SigToLes_ActLesDate = LesDate;
        SigToLess_stuId = stuId;
    }

    public int InsertSigendToLesson()
    {
        DBServices dbs = new DBServices();
        int numAffected = dbs.InsertSigendToLesson(this);
        return numAffected;
    }
}
