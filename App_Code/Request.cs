using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for Request
/// </summary>
public class Request
{
    private int req_id;
    private int req_actLes_id;
    private DateTime req_actLes_date;
    private double req_stu_id;
    private int req_status;
    private int req_is_permanent;
    private int req_type;

    public Request()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public int Req_actLes_id
    {
        get
        {
            return req_actLes_id;
        }

        set
        {
            req_actLes_id = value;
        }
    }

    public DateTime Req_actLes_date
    {
        get
        {
            return req_actLes_date;
        }

        set
        {
            req_actLes_date = value;
        }
    }

    public double Req_stu_id
    {
        get
        {
            return req_stu_id;
        }

        set
        {
            req_stu_id = value;
        }
    }

    public int Req_status
    {
        get
        {
            return req_status;
        }

        set
        {
            req_status = value;
        }
    }

    public int Req_is_permanent
    {
        get
        {
            return req_is_permanent;
        }

        set
        {
            req_is_permanent = value;
        }
    }

    public int Req_id
    {
        get
        {
            return req_id;
        }

        set
        {
            req_id = value;
        }
    }

    public int Req_type
    {
        get
        {
            return req_type;
        }

        set
        {
            req_type = value;
        }
    }

    public Request(int actLesId, DateTime actLesDate, double stuId, int reqStatus, int reqIsPermanent)
    {

        Req_actLes_id = actLesId; ;
        Req_actLes_date = actLesDate;
        Req_stu_id = stuId;
        Req_status = reqStatus;
        Req_is_permanent = reqIsPermanent;



    }
    public Request(int actLesId, DateTime actLesDate, double stuId, int reqStatus, int reqIsPermanent, int reqType)
    {

        Req_actLes_id = actLesId; ;
        Req_actLes_date = actLesDate;
        Req_stu_id = stuId;
        Req_status = reqStatus;
        Req_is_permanent = reqIsPermanent;
        Req_type = reqType;


    }

    public int updateSpecificRequest(int req_num, int status)
    {

        DBServices dbs = new DBServices();
        int numAffected = dbs.updateSpecificRequest(req_num, status);
        return numAffected;

    }

    public int InsertRequest()
    {

        DBServices dbs = new DBServices();
        int numAffected = dbs.InsertRequest(this);
        return numAffected;

    }


    public DataTable ReadSpecificRequest(int req_num)
    {

        DBServices dbs = new DBServices();
        dbs = dbs.readSpecificRequestDB("studentDBConnectionString", "[Requests]", req_num);
        return dbs.dt;
    }


    public int deleteRequest(double stu_id, int les_id, DateTime act_les_date)
    {
        DBServices dbs = new DBServices();
        int numAffected = dbs.deleteRequest( stu_id,  les_id,  act_les_date);
        return numAffected;

    }
}

