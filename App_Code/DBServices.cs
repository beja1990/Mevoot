using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for DBServices
/// </summary>
public class DBServices
{

    public SqlDataAdapter da;
    public DataTable dt;

    public DBServices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

        return cmd;
    }

    //read users from student table
    public Student readSpecificUserStudentDB(double userId, string userPass, string conString, string tableName)
    {
        Student s = new Student();
        SqlConnection con = null;
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM " + tableName + " where stu_id='" + userId + "' and stu_password='" + userPass + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            int flg = 1;

            while (dr.Read())
            {   // Read till the end of the data into a row
                s.Stu_id = (double)dr["Stu_Id"];
                s.Stu_Instructor_id = (double)dr["ins_id"];
                s.FirstName = (string)dr["stu_FirstName"];
                s.LastName = (string)dr["stu_LastName"];
                s.BirthDate = (string)dr["stu_BirthDate"];
                s.PhoneNumber = (string)dr["stu_phoneNumber"];
                s.Email = (string)dr["Stu_Email"];
                s.Address = (string)dr["Stu_Address"];
                s.Status = (bool)dr["Stu_Status"];
                s.Password = (string)dr["Stu_Password"];
                s.Grade = (string)dr["Stu_Grade"];
                s.IsEntitled = (bool)dr["Stu_IsEntitled"];
                s.Notes = (string)dr["stu_note"];

                flg = 0; // only one row->only one student will return
            }

            if (flg == 0)
            {
                return s;
            }
            else return null;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    //read users from student table
    public Manager readSpecificUserManagerDB(double userId, string userPass, string conString, string tableName)
    {
        Manager m = new Manager();
        SqlConnection con = null;
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM " + tableName + " where Man_Id='" + userId + "' and Man_Password='" + userPass + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            int flg = 1;

            while (dr.Read())
            {   // Read till the end of the data into a row
                m.Man_id = (double)dr["Man_Id"];
                m.Man_firstName = (string)dr["Man_FirstName"];
                m.Man_lastName = (string)dr["Man_LastName"];
                m.Man_phoneNumber = (string)dr["Man_PhoneNumber"];
                m.Man_email = (string)dr["Man_Email"];
                m.Man_address = (string)dr["Man_Address"];
                m.Man_status = (bool)dr["Man_Status"];
                m.Man_password = (string)dr["Man_Password"];

                flg = 0; // only one row
            }

            if (flg == 0)
            {
                return m;
            }
            else return null;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }



    //Insert student to the db
    public int InsertStudent(Student student)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandStudent(student);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the Insert a student command String
    //--------------------------------------------------------------------
    private String BuildInsertCommandStudent(Student stud)
    {
        String command;


        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}' , '{4}' ,'{5}','{6}', '{7}', '{8}', '{9}','{10}','{11}', '{12}')", stud.Stu_id, stud.Stu_Instructor_id, stud.FirstName, stud.LastName, stud.BirthDate, stud.PhoneNumber, stud.Email, stud.Address, Convert.ToSByte(Convert.ToInt16(stud.Status)), stud.Password, stud.Grade, Convert.ToSByte(Convert.ToInt16(stud.IsEntitled)), stud.Notes);
        String prefix = "INSERT INTO Student  " + "( Stu_Id , Ins_Id, Stu_FirstName , Stu_LastName ,Stu_BirthDate, Stu_PhoneNumber , Stu_Email, stu_address, stu_status ,Stu_Password, Stu_grade ,Stu_IsEntitled  ,Stu_Note ) ";
        command = prefix + sb.ToString();

        return command;
    }

    //delete student from db
    public int deleteStudent(double id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = deleteStudentCommandStudent(id);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the delete a student command String
    //--------------------------------------------------------------------
    private String deleteStudentCommandStudent(double id)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string

        command = "delete from Student where stu_id=" + id;

        return command;
    }


    //update specific student 
    public int updateSpecificStudent(Student student)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildUpdateCommandStudent(student);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the update a student command String
    //--------------------------------------------------------------------
    private String BuildUpdateCommandStudent(Student stud)
    {
        // use a string builder to create the dynamic string
        String command = "UPDATE Student SET stu_id='" + stud.Stu_id + "', Ins_id='" + stud.Stu_Instructor_id + "', stu_firstName='" + stud.FirstName + "', stu_lastName='"
            + stud.LastName + "',stu_birthDate='" + stud.BirthDate + "', stu_phoneNumber='" + stud.PhoneNumber + "', stu_email='" + stud.Email + "', stu_address='" + stud.Address + "', stu_status='" + stud.Status + "', stu_password='" + stud.Password + "', stu_grade='"
            + stud.Grade + "',stu_isEntitled='" + stud.IsEntitled + "',stu_note='" + stud.Notes + "' WHERE stu_id='" + stud.Stu_id + "'";
        return command;
    }

    public DBServices ReadSpecipicStudentDB(string conString, string tableName, double _stud_id)
    {

        DBServices dbS = new DBServices(); // create a helper class
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString); // open the connection to the database

            String selectStr = "SELECT * FROM " + tableName + " WHERE Stu_Id = '" + _stud_id + "'";
            SqlDataAdapter da = new SqlDataAdapter(selectStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds);  // Fill the datatable (in the dataset), using the Select command

            DataTable dt = ds.Tables[0];

            // add the datatable and the data adapter to the dbS helper class in order to be able to save it to a Session Object
            dbS.dt = dt;
            dbS.da = da;

            return dbS;
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }


        }
    }


    public int InsertInstructor(Instructor Instructor)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandInstructor(Instructor);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the Insert a Instructor command String
    //--------------------------------------------------------------------
    private String BuildInsertCommandInstructor(Instructor inst)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}' , '{4}','{5}','{6}','{7}')", inst.Ins_id, inst.Ins_firstName, inst.Ins_lastName, inst.Ins_phoneNumber, inst.Ins_email, inst.Ins_addres, inst.Ins_status, inst.Ins_password);
        String prefix = "INSERT INTO Instructor  " + "(Ins_Id , Ins_FirstName , Ins_LastName , Ins_PhoneNumber , Ins_Email,Ins_address,Ins_status,Ins_password) ";
        command = prefix + sb.ToString();

        return command;
    }

    public int deleteInstructorFromStudent(double id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuilddeleteInstructorFromStudentCommand(id);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the delete a Instractur command String
    //--------------------------------------------------------------------
    private String BuilddeleteInstructorFromStudentCommand(double id)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string

        command = "delete from student where Stu_id= " + id;

        return command;
    }


    public int deleteInstructor(double id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuilddeleteInstructorCommand(id);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //--------------------------------------------------------------------
    // Build the delete a Instructor command tring
    //--------------------------------------------------------------------
    private String BuilddeleteInstructorCommand(double id)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string

        command = "delete from Instructor where Ins_id= " + id;

        return command;
    }

    public int updateSpecificInstructor(Instructor Instructor)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildUpdateCommandInstructor(Instructor);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the update Instructor command String
    //--------------------------------------------------------------------
    private String BuildUpdateCommandInstructor(Instructor Inst)
    {
        // use a string builder to create the dynamic string
        String command = "UPDATE Instructor SET Ins_id='" + Inst.Ins_id + "', Ins_firstName='" + Inst.Ins_firstName + "', Ins_lastName='" + Inst.Ins_lastName + "', Ins_phoneNumber='" + Inst.Ins_phoneNumber + "', Ins_email='" + Inst.Ins_email + "',Ins_address='" + Inst.Ins_addres + "',Ins_password='" + Inst.Ins_password + "',Ins_status='" + Inst.Ins_status + "' WHERE Ins_id='" + Inst.Ins_id + "'";
        return command;
    }


    public DBServices ReadSpecipicInstructorDB(string conString, string tableName, double _Ins_id)
    {

        DBServices dbS = new DBServices(); // create a helper class
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString); // open the connection to the database

            String selectStr = "SELECT * FROM " + tableName + " WHERE Ins_Id = '" + _Ins_id + "'";
            SqlDataAdapter da = new SqlDataAdapter(selectStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds);  // Fill the datatable (in the dataset), using the Select command

            DataTable dt = ds.Tables[0];

            // add the datatable and the data adapter to the dbS helper class in order to be able to save it to a Session Object
            dbS.dt = dt;
            dbS.da = da;

            return dbS;
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }


        }
    }

    public int InsertTeacher(Teacher t)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandTeacher(t);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    private String BuildInsertCommandTeacher(Teacher t)
    {
        String command;

        StringBuilder sb = new StringBuilder();

        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}' , '{4}' ,'{5}','{6}', '{7}')", t.Tea_id, t.Tea_firstName, t.Tea_lastName, t.Tea_phoneNumber, t.Tea_email, t.Tea_address, Convert.ToSByte(Convert.ToInt16(t.Tea_status)), t.Tea_password);
        String prefix = "InsERT INTO Teacher " + "( Tea_Id, Tea_FirstName, Tea_LastName, Tea_PhoneNumber, Tea_Email, Tea_Address, Tea_Status, Tea_Password )";
        command = prefix + sb.ToString();

        return command;
    }


    public DBServices ReadSpecipicTeacherDB(string conString, string tableName, double _tea_id)
    {

        DBServices dbS = new DBServices(); // create a helper class
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString); // open the connection to the database

            String selectStr = "SELECT * FROM " + tableName + " WHERE tea_Id = '" + _tea_id + "'";
            SqlDataAdapter da = new SqlDataAdapter(selectStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds);  // Fill the datatable (in the dataset), using the Select command

            DataTable dt = ds.Tables[0];

            // add the datatable and the data adapter to the dbS helper class in order to be able to save it to a Session Object
            dbS.dt = dt;
            dbS.da = da;

            return dbS;
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }


        }
    }



    //--------------------------------------------------------------------
    // Build the delete a teacher command String
    //--------------------------------------------------------------------
    private String deleteTeacherCommand(double id)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string

        command = "delete from Teacher where tea_id=" + id;

        return command;
    }

    //--------------------------------------------------------------------
    // Build the update a Teacher command String
    //--------------------------------------------------------------------
    private String BuildUpdateCommandTeacher(Teacher tea)
    {
        // use a string builder to create the dynamic string
        String command = "UPDATE Teacher SET tea_id='" + tea.Tea_id + "', tea_firstName='" + tea.Tea_firstName + "', tea_lastName='"
            + tea.Tea_lastName + "', tea_phoneNumber='" + tea.Tea_phoneNumber + "', tea_email='" + tea.Tea_email + "', tea_address='" + tea.Tea_address + "', tea_status='" + tea.Tea_status + "', tea_password='" + tea.Tea_password + "' WHERE tea_id='" + tea.Tea_id + "'";
        return command;
    }

    public int deleteTeacher(double id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = deleteTeacherCommand(id);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    public int updateSpecificTeacher(Teacher teacher)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildUpdateCommandTeacher(teacher);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //Insert TeachBy to the db
    public int InsertTeachBy(TeachBy tb)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandTeachBy(tb);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the Insert a student command String
    //--------------------------------------------------------------------
    private String BuildInsertCommandTeachBy(TeachBy tb)
    {
        String command;


        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}')", tb.Tea_Id, tb.Pro_Id);
        String prefix = "INSERT INTO TeachBy  " + "(tea_id , pro_id) ";
        command = prefix + sb.ToString();

        return command;
    }

    //פונקציה שמביאה רשימה של תגבורים
    public List<Tigburim> getTigburimList(string conString, string tableName)
    {

        List<Tigburim> TigburimList = new List<Tigburim>();
        SqlConnection con = null;
        try
        {

            con = connect(conString); // create a connection to the database using the connection String defined in the web config file
            String selectSTR;

            selectSTR = "SELECT al.number as 'SingID', t.[Tea_Id], (t.[Tea_FirstName] +' '+ t.[Tea_LastName]) as 'full_name', pro.Pro_Id, pro.[Pro_Title], les.[Les_MaxQuan], al.quantity, les.[Les_StartHour], les.[Les_EndHour], al.[ActLes_date]" +
                " FROM " + tableName + " les INNER JOIN [ActualLesson] al on les.Les_Id = al.ActLes_LesId INNER JOIN [Profession] pro on pro.Pro_Id = les.Les_Pro_Id INNER JOIN [Teacher] t ON t.Tea_Id = les.Les_Tea_Id";

            //SELECT *
            //FROM Lesson INNER JOIN[dbo].[Teacher]
            //ON Lesson.teacher_id = [dbo].[Teacher].[Tea_Id] INNER Join [dbo].[Profession] ON Lesson.[pro_id] = [dbo].[Profession].[pro_id]


            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end




            while (dr.Read())
            {   // Read till the end of the data into a row               
                Tigburim t = new Tigburim();
                string[] dateFormat1 = { };
                dateFormat1 = Convert.ToString(dr["ActLes_date"]).Split(' ');


                //object building
                t.Id = Convert.ToInt16(dr["SingID"]);
                t.StartTime = Convert.ToString(dr["Les_StartHour"]);
                t.EndTime = Convert.ToString(dr["Les_EndHour"]);
                t.ProfName = Convert.ToString(dr["Pro_Title"]);
                t.Limit = (int)(dr["Les_MaxQuan"]);
                t.ProfId = (int)(dr["Pro_Id"]);
                t.TeacherName = Convert.ToString(dr["full_name"]);
                t.TeacherId = Convert.ToDouble(dr["Tea_Id"]);
                t.ActualLimit = (int)(dr["Les_MaxQuan"]) - (int)(dr["quantity"]);

                //date formatting
                string[] dateFormat2 = dateFormat1[0].Split('/');

                t.TigburDate = dateFormat2[2] + "-" + dateFormat2[1] + "-" + dateFormat2[0];



                TigburimList.Add(t);

            }
            return TigburimList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    //פונקציה שמביאה רשימה של תגבורים
    public List<Tigburim> getTigburimListForStudent(string conString, string tableName, string id)
    {
        List<Tigburim> TigburimList = new List<Tigburim>();
        SqlConnection con = null;
        try
        {

            con = connect(conString); // create a connection to the database using the connection String defined in the web config file
            String selectSTR;

            selectSTR = "SELECT DISTINCT al.number as 'SingID', t.[Tea_Id], (t.[Tea_FirstName] +' '+ t.[Tea_LastName]) as 'full_name', pro.Pro_Id, pro.[Pro_Title], les.[Les_MaxQuan], al.quantity, les.[Les_StartHour], les.[Les_EndHour], al.[ActLes_date]" +
                " FROM " + tableName + " les INNER JOIN [ActualLesson] al on les.Les_Id = al.ActLes_LesId INNER JOIN [Profession] pro on pro.Pro_Id = les.Les_Pro_Id INNER JOIN [Teacher] t ON t.Tea_Id = les.Les_Tea_Id INNER JOIN [signedToLesson] on StLes_stuId='" + id + "'";

            //SELECT *
            //FROM Lesson INNER JOIN[dbo].[Teacher]
            //ON Lesson.teacher_id = [dbo].[Teacher].[Tea_Id] INNER Join [dbo].[Profession] ON Lesson.[pro_id] = [dbo].[Profession].[pro_id]


            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end




            while (dr.Read())
            {   // Read till the end of the data into a row               
                Tigburim t = new Tigburim();
                string[] dateFormat1 = { };
                dateFormat1 = Convert.ToString(dr["ActLes_date"]).Split(' ');


                //object building
                t.Id = Convert.ToInt16(dr["SingID"]);
                t.StartTime = Convert.ToString(dr["Les_StartHour"]);
                t.EndTime = Convert.ToString(dr["Les_EndHour"]);
                t.ProfName = Convert.ToString(dr["Pro_Title"]);
                t.Limit = (int)(dr["Les_MaxQuan"]);
                t.ProfId = (int)(dr["Pro_Id"]);
                t.TeacherName = Convert.ToString(dr["full_name"]);
                t.TeacherId = Convert.ToDouble(dr["Tea_Id"]);
                t.ActualLimit = (int)(dr["Les_MaxQuan"]) - (int)(dr["quantity"]);

                //date formatting
                string[] dateFormat2 = dateFormat1[0].Split('/');

                t.TigburDate = dateFormat2[2] + "-" + dateFormat2[1] + "-" + dateFormat2[0];



                TigburimList.Add(t);

            }
            return TigburimList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public Tigburim getTigburById(int tigID, string conString, string tableName)
    {

        Tigburim t = new Tigburim();
        SqlConnection con = null;
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT al.number, t.[Tea_Id], (t.[Tea_FirstName] +' '+ t.[Tea_LastName]) as 'full_name', pro.Pro_Id, pro.[Pro_Title], les.[Les_MaxQuan], al.quantity, les.[Les_StartHour], les.[Les_EndHour], al.[ActLes_date]" +
                " FROM " + tableName + " les INNER JOIN [ActualLesson] al on les.Les_Id = al.ActLes_LesId INNER JOIN [Profession] pro on pro.Pro_Id = les.Les_Pro_Id INNER JOIN [Teacher] t ON t.Tea_Id = les.Les_Tea_Id WHERE al.number='" + tigID + "'";

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            //bool status, string startTime, string endTime, string descEvent,int eventTypeID, double latitude, double longitude, int guardingID
            while (dr.Read())
            {   // Read till the end of the data into a row               


                string[] dateFormat1 = { };
                dateFormat1 = Convert.ToString(dr["ActLes_date"]).Split(' ');


                //object building
                t.Id = Convert.ToInt16(dr["number"]);
                t.StartTime = Convert.ToString(dr["Les_StartHour"]);
                t.EndTime = Convert.ToString(dr["Les_EndHour"]);
                t.ProfName = Convert.ToString(dr["Pro_Title"]);
                t.Limit = (int)(dr["Les_MaxQuan"]);
                t.ProfId = (int)(dr["Pro_Id"]);
                t.TeacherName = Convert.ToString(dr["full_name"]);
                t.TeacherId = Convert.ToDouble(dr["Tea_Id"]);
                t.ActualLimit = (int)(dr["Les_MaxQuan"]) - (int)(dr["quantity"]);

                //date formatting
                string[] dateFormat2 = dateFormat1[0].Split('/');

                t.TigburDate = dateFormat2[2] + "-" + dateFormat2[1] + "-" + dateFormat2[0];

            }
            return t;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }

    }

    public int updateIsEntittled(double stu_id, int updateIsEntitledTo)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildUpdateCommandStudentIsEntitled(stu_id, updateIsEntitledTo);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the update a student entitled status command String
    //--------------------------------------------------------------------
    private String BuildUpdateCommandStudentIsEntitled(double stu_id, int updateIsEntitledTo)
    {
        // use a string builder to create the dynamic string
        String command = "UPDATE Student SET stu_isEntitled= '" + updateIsEntitledTo + "' WHERE stu_id='" + stu_id + "'";
        return command;
    }

    public int InsertLesson(Lesson les)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandLesson(les);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the Insert a lesson command String
    //--------------------------------------------------------------------
    private String BuildInsertCommandLesson(Lesson les)
    {
        String command;


        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}' , '{4}' ,'{5}')", les.Tea_id, les.Pro_id, les.Les_maxQuan, les.Les_startHour, les.Les_endHour, les.Les_day);
        String prefix = "INSERT INTO Lesson  " + "( Les_Tea_Id, Les_Pro_Id, Les_MaxQuan, Les_StartHour, Les_EndHour, Les_day) ";
        command = prefix + sb.ToString();

        return command;
    }

    public int deleteLesson(int id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuilddeleteCommandLesson(id);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the delete a lesson command String
    //--------------------------------------------------------------------
    private String BuilddeleteCommandLesson(int id)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string

        command = "delete from ActualLesson where ActLes_LesId=" + id;

        return command;
    }

    public int deleteLessonFromRequest(int id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuilddeleteCommandLessonFromRequest(id);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the delete a LessonFromRequest command String
    //--------------------------------------------------------------------
    private String BuilddeleteCommandLessonFromRequest(int id)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string

        command = "delete from Requests where req_actLes_id=" + id;

        return command;
    }

    public DBServices ReadSpecipicLessonDB(string conString, string tableName, double _less_id)
    {

        DBServices dbS = new DBServices(); // create a helper class
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString); // open the connection to the database

            String selectStr = "SELECT * FROM " + tableName + " WHERE Les_Id = '" + _less_id + "'";
            SqlDataAdapter da = new SqlDataAdapter(selectStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds);  // Fill the datatable (in the dataset), using the Select command

            DataTable dt = ds.Tables[0];

            // add the datatable and the data adapter to the dbS helper class in order to be able to save it to a Session Object
            dbS.dt = dt;
            dbS.da = da;

            return dbS;
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }


        }
    }

    //update specific Lesson 
    public int updateSpecificLesson(Lesson les, int less_id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildUpdateCommandLesson(les, less_id);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the update a Lesson command String
    //--------------------------------------------------------------------
    private String BuildUpdateCommandLesson(Lesson les, int less_id)
    {
        // use a string builder to create the dynamic string
        String command = "UPDATE Lesson SET Les_Tea_id='" + les.Tea_id + "', Les_Pro_Id='" + les.Pro_id + "', Les_MaxQuan='" + les.Les_maxQuan + "',Les_StartHour='" + les.Les_startHour + "',Les_EndHour='" + les.Les_endHour + "', Les_Day='" + les.Les_day + "' WHERE Les_Id='" + less_id + "'";
        return command;
    }

    public int insertActualLesson(ActualLesson al)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandActualLesson(al);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the Insert a Actual lesson command String
    //--------------------------------------------------------------------
    private String BuildInsertCommandActualLesson(ActualLesson al)
    {
        String command;


        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}', '{2}')", al.Les_id, al.Act_les_date, al.Quantity);
        String prefix = "INSERT INTO ActualLesson  " + "( ActLes_LesId,ActLes_date, quantity) ";
        command = prefix + sb.ToString();

        return command;
    }

    //read Specific Reques from tDB
    public DBServices readSpecificRequestDB(string conString, string tableName, int req_num)
    {

        DBServices dbS = new DBServices(); // create a helper class
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString); // open the connection to the database

            String selectStr = "SELECT * FROM " + tableName + " WHERE req_number = '" + req_num + "'";
            SqlDataAdapter da = new SqlDataAdapter(selectStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds);  // Fill the datatable (in the dataset), using the Select command

            DataTable dt = ds.Tables[0];

            // add the datatable and the data adapter to the dbS helper class in order to be able to save it to a Session Object
            dbS.dt = dt;
            dbS.da = da;

            return dbS;
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }


        }
    }

    // insert request 
    public int InsertRequest(Request re)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandRequest(re);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the Insert a request command String
    //--------------------------------------------------------------------
    private String BuildInsertCommandRequest(Request re)
    {
        String command;


        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}' , '{4}', '{5}','{6}')", re.Req_actLes_id, (re.Req_actLes_date).ToString("yyyy-MM-dd"), re.Req_stu_id, re.Req_status, re.Req_is_permanent, re.Req_type, (re.Req_date).ToString("yyyy-MM-dd"));
        String prefix = "INSERT INTO Requests  " + "( req_actLes_id , req_actLes_date , req_stu_id ,req_status, req_is_permanent, req_type, request_date) ";
        command = prefix + sb.ToString();

        return command;
    }

    // update a request
    public int updateSpecificRequest(int req_num, int status)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildUpdateCommandRequest(req_num, status);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the update a request command String
    //--------------------------------------------------------------------
    private String BuildUpdateCommandRequest(int req_num, int status)
    {
        // use a string builder to create the dynamic string
        String command = "UPDATE Requests SET req_status='" + status + "' WHERE req_number='" + req_num + "'";
        return command;
    }


    //Insert signedToLesson to the db
    public int InsertSigendToLesson(SignedToLesson stl)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandSigendToLesson(stl);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //delete request from db
    public int deleteRequest(double stu_id, int les_id, DateTime act_les_date)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = deleteRequestommand(stu_id, les_id, act_les_date);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the delete a student command String
    //--------------------------------------------------------------------
    private String deleteRequestommand(double stu_id, int les_id, DateTime act_les_date)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string

        command = "delete from Requests where req_stu_id=" + stu_id + " and req_actLes_id=" + les_id + " and req_actLes_date=" + "'" + (act_les_date).ToString("yyyy-MM-dd") + "'";

        return command;
    }


    //--------------------------------------------------------------------
    // Build the Insert a sigendToLesson command String
    //--------------------------------------------------------------------
    private String BuildInsertCommandSigendToLesson(SignedToLesson stl)
    {
        String command;


        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}', '{2}')", stl.SigToLes_ActLesId, stl.SigToLes_ActLesDate, stl.SigToLess_stuId);
        String prefix = "INSERT INTO signedToLesson  " + "(StLes_ActLesId , StLes_ActLesDate, StLes_stuId) ";
        command = prefix + sb.ToString();

        return command;
    }

    public int updateSpecificActualLesson(int lesId, string lesDate, int actual_quan)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("studentDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildUpdateCommandActualLesson(lesId, lesDate, actual_quan);      // helper method to build the Insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the update a request command String
    //--------------------------------------------------------------------
    private String BuildUpdateCommandActualLesson(int les_numer, string lesDate, int actual_quan)
    {
        // use a string builder to create the dynamic string
        String command = "UPDATE ActualLesson SET quantity='" + actual_quan + "' WHERE ActLes_LesId='" + les_numer + "' AND ActLes_date='" + lesDate + "'";
        return command;
    }

    //read specific request
    public ActualLesson readSpecificActualLesson(int actl_num, string conString, string tableName)
    {
        ActualLesson a = new ActualLesson();
        SqlConnection con = null;
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM " + tableName + " where number='" + actl_num + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            int flg = 1;

            while (dr.Read())
            {   // Read till the end of the data into a row

                a.Les_id = (int)dr["ActLes_LesId"];
                a.Act_les_date = dr["ActLes_date"].ToString();
                a.Quantity = (int)dr["quantity"];

                flg = 0; // only one row
            }

            if (flg == 0)
            {
                return a;
            }
            else return null;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    //--------------------------------------------------------------------
    // Reports DBS functions
    //--------------------------------------------------------------------
    public List<Report> getProfessionCount(string startDate, string endDate, string conString)
    {

        List<Report> ProfessionCountReport = new List<Report>();
        SqlConnection con = null;
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = " select Pro_Id, Pro_Title, count(1) as 'amount' from Profession inner join Lesson on Les_Pro_Id = Pro_Id inner join ActualLesson on ActLes_LesId = Les_Id WHERE actLes_date BETWEEN '" + startDate + "' AND '" + endDate + "' group by Pro_Id, Pro_Title";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end


            while (dr.Read())
            {   // Read till the end of the data into a row               
                Report r = new Report();
                r.Id = Convert.ToInt32(dr["Pro_Id"]);
                r.Pro_title = (string)dr["Pro_title"];
                r.Amount = Convert.ToDouble(dr["amount"]);

                ProfessionCountReport.Add(r);

            }
            return ProfessionCountReport;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }


    //--------------------------------------------------------------------
    // User Request by Profession - User mini dashboard
    //--------------------------------------------------------------------
    public List<Report> StudentRequestsByProfession(string startDate, string endDate, string conString, string userId)
    {

        List<Report> StudentRequestsByProfessionCountList = new List<Report>();
        SqlConnection con = null;
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = "select Pro_Id, Pro_Title, count(1) as 'amount' from Requests inner join Lesson on req_actLes_id = Les_Id inner join Profession on Pro_Id = Les_Pro_Id WHERE req_stu_id = '" + userId + "' AND req_status = '2' and request_date BETWEEN '" + startDate + "' AND '" + endDate + "' group by Pro_Id, Pro_Title ";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end


            while (dr.Read())
            {   // Read till the end of the data into a row               
                Report r = new Report();
                r.Id = Convert.ToInt32(dr["Pro_Id"]);
                r.Pro_title = (string)dr["Pro_title"];
                r.Amount = Convert.ToDouble(dr["amount"]);

                StudentRequestsByProfessionCountList.Add(r);

            }
            return StudentRequestsByProfessionCountList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }



    //--------------------------------------------------------------------
    // User Request by Profession - User mini dashboard
    //--------------------------------------------------------------------
    public List<Report> StudentClassesByProfession(string startDate, string endDate, string conString, string userId)
    {

        List<Report> StudentClassesByProfessionCountList = new List<Report>();
        SqlConnection con = null;
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = "select Pro_Id, Pro_Title, count(1) as 'amount' from Requests inner join Lesson on req_actLes_id = Les_Id inner join Profession on Pro_Id = Les_Pro_Id WHERE req_stu_id = '" + userId + "' AND req_status = '1' and request_date BETWEEN '" + startDate + "' AND '" + endDate + "' group by Pro_Id, Pro_Title ";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end


            while (dr.Read())
            {   // Read till the end of the data into a row               
                Report r = new Report();
                r.Id = Convert.ToInt32(dr["Pro_Id"]);
                r.Pro_title = (string)dr["Pro_title"];
                r.Amount = Convert.ToDouble(dr["amount"]);

                StudentClassesByProfessionCountList.Add(r);

            }
            return StudentClassesByProfessionCountList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }



}