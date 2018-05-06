using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Teacher
/// </summary>
public class Teacher
{

    public Teacher()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    double tea_id;
    string tea_firstName;
    string tea_lastName;
    string tea_phoneNumber;
    string tea_email;
    string tea_address;
    bool tea_status;
    string tea_password;


    public double Tea_id
    {
        get
        {
            return tea_id;
        }

        set
        {
            tea_id = value;
        }
    }

    public string Tea_firstName
    {
        get
        {
            return tea_firstName;
        }

        set
        {
            tea_firstName = value;
        }
    }

    public string Tea_lastName
    {
        get
        {
            return tea_lastName;
        }

        set
        {
            tea_lastName = value;
        }
    }

    public string Tea_phoneNumber
    {
        get
        {
            return tea_phoneNumber;
        }

        set
        {
            tea_phoneNumber = value;
        }
    }

    public string Tea_email
    {
        get
        {
            return tea_email;
        }

        set
        {
            tea_email = value;
        }
    }

    public string Tea_address
    {
        get
        {
            return tea_address;
        }

        set
        {
            tea_address = value;
        }
    }

    public bool Tea_status
    {
        get
        {
            return tea_status;
        }

        set
        {
            tea_status = value;
        }
    }

    public string Tea_password
    {
        get
        {
            return tea_password;
        }

        set
        {
            tea_password = value;
        }
    }



    public Teacher(double _id, string _fname, string _lname, string _phone, string _mail, string _address, bool _status,string _password)
    {
        Tea_id = _id;
        Tea_firstName = _fname;
        Tea_lastName = _lname;
        Tea_phoneNumber = _phone;
        Tea_email = _mail;
        Tea_address = _address;
        Tea_status = _status;
        Tea_password = _password;
    }

    //--------------------------------------------------------------------------------
    // read the teacher database into a dataset
    //--------------------------------------------------------------------------------

    public DataTable readSpecipicTeacher(double _tea_id)
    {
        DBServices dbs = new DBServices();
        dbs = dbs.ReadSpecipicTeacherDB("studentDBConnectionString", "[Teacher]", _tea_id);
        return dbs.dt;
    }

    public int InsertTeacher()
    {
        DBServices dbs = new DBServices();
        int numAffected = dbs.InsertTeacher(this);
        return numAffected;
    }

    public int deleteTeacher(int id)
    {

        DBServices dbs = new DBServices();
        int numAffected = dbs.deleteTeacher(id);
        return numAffected;

    }

    public int updateSpecificTeacher()
    {

        DBServices dbs = new DBServices();
        int numAffected = dbs.updateSpecificTeacher(this);
        return numAffected;

    }

    public Teacher readSpecificUserTeacher(double username_id, string password)
    {
        DBServices dbs = new DBServices();
        Teacher DBuser = dbs.readSpecificUserTeacherDB(username_id, password, "studentDBConnectionString", "Teacher");

        if (DBuser == null)
            return null;
        else
            return DBuser;
    }
}