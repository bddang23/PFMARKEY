using Intranet.Models.Signage;
using Intranet.Models.SQL;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;


namespace Intranet.Controllers.Signage
{
    public class ManageEventsController : Controller
    {
        private SQL_Set_Up sql;
        public List<Event> events;

        public ManageEventsController()
        {
          sql= new SQL_Set_Up();
        }

        public IActionResult Index()
        {
            return View(ShowEvent(DateTime.Today.ToString("yyyy-MM")));
        }

        [HttpPost]
        public ActionResult Index(string filter)
        {
            return View(ShowEvent(filter));
        }

        [HttpPost]
        public ActionResult AddEvent(string title, string startDate, string endDate, string isAllDay, string startTime, string endTime)
        {
            if (!string.IsNullOrEmpty(isAllDay))
            {

                if (isAllDay.Equals("on"))
                {
                    if (startDate.Equals(endDate))
                    {
                        sql.com.CommandText = "INSERT INTO [dbo].[Events_and_Other_AllDay] ([Title],[Date]) " +
                                          "VALUES ('" + title + "','" + startDate + "')";
                        sql.com.ExecuteNonQuery();
                    }
                    else
                    {
                        DateTime start = DateTime.Parse(startDate);
                        DateTime end = DateTime.Parse(endDate);
                        TimeSpan dayApart = end - start;
                        int days = dayApart.Days + 1;

                        for (int i = 0; i < days; i++)
                        {

                            sql.com.CommandText = "INSERT INTO [dbo].[Events_and_Other_AllDay] ([Title],[Date]) " +
                                        "VALUES ('" + title + "','" + start.ToString("d") + "')";
                            sql.com.ExecuteNonQuery();
                            start = start.AddDays(1);
                        }

                    }
                }
            }
            else
            {
                sql.com.CommandText = "INSERT INTO [dbo].[Events_and_Other] ([Title],[Start_Time],[End_Time]) " +
                                  "VALUES ('" + title + "','" + startDate + " " + startTime + "','" + endDate + " " + endTime + "')";
                sql.com.ExecuteNonQuery();


            }

            return View();
        }

        public List<Event> ShowEvent(string filter)
        {
            if (string.IsNullOrEmpty(filter))
                filter = DateTime.Today.ToString("yyyy-MM");

            events = new List<Event>();
            events.Add(new Event(1, "placeholder", "placeholder", filter + "-01", filter + "-01", "1"));
            sql.com.CommandText = "SELECT * " +
                           "FROM[PFMI_Signage].[dbo].[CalendarEvents_NEW] " +
                           "WHERE (Month(Start)=Month('" + filter + "-01') OR Month(EndColumn)=Month('" + filter + "-01')) AND YEAR(Start)=Year('" + filter + "-01'); ";
            sql.dr = sql.com.ExecuteReader();
            if (sql.dr.HasRows)
            {
                events.Remove(events[0]);
                while (sql.dr.Read())
                {
                    events.Add(new Event(int.Parse(sql.dr["Id"].ToString()), sql.dr["Table"].ToString(), sql.dr["Title"].ToString(), sql.dr["Start"].ToString(), sql.dr["EndColumn"].ToString(), sql.dr["AllDay"].ToString()));
                }
                sql.dr.Close();
            }

            return events;
        }

        public ActionResult DeleteEvent(string id)
        {
            string ID = id.Substring(0, id.IndexOf("_"));
            string table = id.Substring(id.IndexOf("_") + 1);
            sql.com.CommandText = "DELETE FROM [dbo].[" + table + "] " +
                              "WHERE Id = " + ID;
            sql.com.ExecuteNonQuery();
            return View();
        }

        public ActionResult UpdateEvent(string id)
        {
            string ID = id.Substring(0, id.IndexOf("_"));
            string table = id.Substring(id.IndexOf("_") + 1);
            Event selectedEvent = null;
            sql.com.CommandText = "SELECT * " +
                              "FROM[PFMI_Signage].[dbo].[" + table + "] " +
                              "WHERE Id= " + ID;
            sql.dr = sql.com.ExecuteReader();
            if (table.Equals("Events_and_Other"))
            {
                sql.dr.Read();
                selectedEvent = new Event(int.Parse(sql.dr["Id"].ToString()), table, sql.dr["Title"].ToString(), sql.dr["Start_Time"].ToString(), sql.dr["End_Time"].ToString(), "0");
                sql.dr.Close();
            }
            else
            {
                sql.dr.Read();
                selectedEvent = new Event(int.Parse(sql.dr["Id"].ToString()), table, sql.dr["Title"].ToString(), sql.dr["Date"].ToString(), "", "1");
                sql.dr.Close();

            }

            return View(selectedEvent);
        }

        [HttpPost]
        public ActionResult UpdateEvent(string title, string id, string startDate, string endDate, string isAllDay, string startTime, string endTime)
        {


            if (!string.IsNullOrEmpty(isAllDay))
            {

                if (isAllDay.Equals("on"))
                {
                    if (startDate.Equals(endDate))
                    {
                        sql.com.CommandText = "UPDATE [dbo].[Events_and_Other_AllDay] " +
                                          "SET [Title] = '" + title + "', " +
                                          "[Date] = '" + startDate + "' " +
                                          "WHERE Id= " + id;
                        sql.com.ExecuteNonQuery();
                    }
                    else
                    {
                        DateTime start = DateTime.Parse(startDate);
                        DateTime end = DateTime.Parse(endDate);
                        TimeSpan dayApart = end - start;
                        int days = dayApart.Days + 1;

                        for (int i = 0; i < days; i++)
                        {

                            sql.com.CommandText = "UPDATE [dbo].[Events_and_Other_AllDay] " +
                                              "SET [Title] = '" + title + "', " +
                                              "[Date] = '" + startDate + "' " +
                                              "WHERE Id= " + (int.Parse(id) + i);

                            sql.com.ExecuteNonQuery();
                            start = start.AddDays(1);
                        }

                    }
                }
            }
            else
            {

                sql.com.CommandText = "UPDATE [dbo].[Events_and_Other] " +
                                  "SET [Title] = '" + title + "'," +
                                  "[Start_Time] ='" + startDate + "'," +
                                  ",[End_Time] ='" + endDate + "'," +
                                      "WHERE Id= " + id;
                sql.com.ExecuteNonQuery();

            }

            return RedirectToAction("Index");
        }

    }
}
