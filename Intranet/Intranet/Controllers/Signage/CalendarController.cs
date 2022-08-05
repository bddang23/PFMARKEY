using Intranet.Models.Signage;
using Intranet.Models.SQL;
using Microsoft.AspNetCore.Mvc;

namespace Intranet.Controllers.Signage
{
    public class CalendarController : Controller
    {

        private SQL_Set_Up sql;
        public List<Event> events;

        public CalendarController()
        {
            sql= new SQL_Set_Up();
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents(DateTime start, DateTime end)
        {

            events = new List<Event>();

            //get employee birthday
            sql.com.CommandText = "SELECT * " +
                              "FROM[PFMI_Signage].[dbo].[CalendarEvents_NEW] " +
                              "WHERE Start BETWEEN '" +
                              start.ToString("yyyy-MM-dd HH:mm:ss") +
                              "' AND '" +
                              end.ToString("yyyy-MM-dd HH:mm:ss") + "';";
            sql.dr = sql.com.ExecuteReader();
            while (sql.dr.Read())
            {
                events.Add(new Event(int.Parse(sql.dr["Id"].ToString()),
                                               sql.dr["Table"].ToString(),
                                               sql.dr["Title"].ToString(),
                                               sql.dr["Start"].ToString(),
                                               sql.dr["EndColumn"].ToString(),
                                               sql.dr["AllDay"].ToString()));
            }
            sql.dr.Close();

            //get employee birthday
            sql.com.CommandText = "EXEC GetBirthdaysAndAnniversariesForCalendar @StartDate = '" + start.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', @EndDate = '" + end.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
            sql.dr = sql.com.ExecuteReader();
            while (sql.dr.Read())
            {
                events.Add(new Event(sql.dr["Title"].ToString(), sql.dr["Start"].ToString()));
            }
            sql.dr.Close();

            return Json(events);
        }

    }
}
