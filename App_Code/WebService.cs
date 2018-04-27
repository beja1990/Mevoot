using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
// REMEMBER TO ADD THIS NAMESPACE
using System.Web.Script.Serialization;
using System.Web.Script.Services;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    [WebMethod]

    public string getSpecificSchedule(string id)// הבאת תגבורים לתלמיד
    {
        Tigburim newTigbur = new Tigburim();
        List<Tigburim> listTigbur = newTigbur.getTigburimListForStudent("studentDBConnectionString","Lesson",id);

        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonStringgetSchedule = js.Serialize(listTigbur);
        return jsonStringgetSchedule;
    }


    [WebMethod]

    public string getSchedule()//הבאת תגבורים
    {
        Tigburim newTigbur = new Tigburim();
        List<Tigburim> listTigbur = newTigbur.getTigburimList("studentDBConnectionString", "Lesson");

        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonStringgetSchedule = js.Serialize(listTigbur);
        return jsonStringgetSchedule;
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public string getTigburById(string id)
    {
        Tigburim newTigbur = (new Tigburim()).getTigburById(id);


        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonStringGetTurtering = js.Serialize(newTigbur);
        return jsonStringGetTurtering;
    }

    //Reports Web methods
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string getProfessionCountForReport(string startDate, string endDate)//מחזיר כמות תגבורים לפי מקצוע בתאריכים הנתונים
    {

        Report r = new Report();
        List<Report> ProfessionCount = r.getProfessionCount(startDate, endDate);

        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonStringGetProfession = js.Serialize(ProfessionCount);
        return jsonStringGetProfession;
    }

    //Student Request by Profession Web methods
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string StudentRequestsByProfession(string startDate, string endDate,string userId)//מחזיר כמות בקשות לפי מקצוע בתאריכים הנתונים
    {

        Report r = new Report();
        List<Report> StudentRequestsByProfession = r.StudentRequestsByProfession(startDate, endDate, userId);

        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonStringGetProfession = js.Serialize(StudentRequestsByProfession);
        return jsonStringGetProfession;
    }

    //Student classes by Profession Web methods
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string StudentClassesByProfession(string startDate, string endDate, string userId)//מחזיר כמות בקשות לפי מקצוע בתאריכים הנתונים
    {

        Report r = new Report();
        List<Report> StudentClassesByProfession = r.StudentClassesByProfession(startDate, endDate, userId);

        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonStringGetProfession = js.Serialize(StudentClassesByProfession);
        return jsonStringGetProfession;
    }

}
