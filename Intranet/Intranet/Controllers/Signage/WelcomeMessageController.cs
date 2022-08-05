using Intranet.Models.Signage;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Intranet.Controllers.Signage
{
    public class WelcomeMessageController : Controller
    {
        private SqlCommand com;
        private SqlDataReader dr;
        private SqlConnection conn;
        public List<welcomeMessage> messages;
        public WelcomeMessageController()
        {
            com = new SqlCommand();
            conn = new SqlConnection();
            conn.ConnectionString = "Data Source=PFM-SQL;Initial Catalog=PFMI_Signage;User Id=pfmarkey;Password=Pfmi2880;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database='PFMI_Signage'";
            conn.Open();
            com.Connection = conn;

        }

        public IActionResult Index()
        {
            GetActiveMessage();

            return View(messages);
        }


        [HttpPost]
        public ActionResult Index(string message, string startDate, string endDate, string isAllDay, string startTime, string endTime)
        {
            if (isAllDay != null)
            {
                DateTime end = DateTime.Parse(endDate);
                end = end.AddHours(23);
                end = end.AddMinutes(59);

                com.CommandText = "INSERT INTO [dbo].[EntranceMessages] " +
                                  "VALUES  ('" + message + "' ,'" + startDate + "' ,'" + end.ToString("g") + "')";
                com.ExecuteNonQuery();

            }
            else
            {
                com.CommandText = "INSERT INTO [dbo].[EntranceMessages] " +
                                  "VALUES  ('" + message + "' ,'" + startDate + " " + startTime + "' ,'" + endDate + " " + endTime + "')";
                com.ExecuteNonQuery();


            }
            GetActiveMessage();

            return View(messages);
        }

        [HttpGet]
        public ActionResult DeleteMessage(string id)
        {
            com.CommandText = "DELETE FROM [dbo].[EntranceMessages] " +
                              "WHERE Id=" + id;
            com.ExecuteNonQuery();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateMessage(string id)
        {
            welcomeMessage selectedMsg;

            com.CommandText = "SELECT * " +
                              "FROM[PFMI_Signage].[dbo].[EntranceMessages] " +
                              "WHERE Id= " + id;
            dr = com.ExecuteReader();

            dr.Read();
            selectedMsg = new welcomeMessage(dr["Id"].ToString(), dr["Message"].ToString(), dr["Start_Time"].ToString(), dr["End_Time"].ToString());
            dr.Close();

            return View(selectedMsg);
        }

        [HttpPost]
        public ActionResult UpdateMessage(string message, string id, string startDate, string endDate, string isAllDay, string startTime, string endTime)
        {
            if (isAllDay != null)
            {
                DateTime end = DateTime.Parse(endDate);
                end = end.AddHours(23);
                end = end.AddMinutes(59);

                com.CommandText = "UPDATE [dbo].[EntranceMessages] " +
                                  "SET [Message] = '" + message + "' " +
                                     ",[Start_Time] = '" + startDate + " " + endTime + "' " +
                                     ",[End_Time] = '" + end.ToString("g") + "' " +
                                  "WHERE Id=" + id;
                com.ExecuteNonQuery();

            }
            else
            {
                com.CommandText = "UPDATE [dbo].[EntranceMessages] " +
                              "SET [Message] = '" + message + "' " +
                                 ",[Start_Time] = '" + startDate + "' " +
                                 ",[End_Time] = '" + endDate + " " + endTime + "' " +
                              "WHERE Id=" + id;
                com.ExecuteNonQuery();


            }

            return RedirectToAction("Index");
        }

        private void GetActiveMessage()
        {
            messages = new List<welcomeMessage>();
            com.CommandText = "SELECT *  FROM [PFMI_Signage].[dbo].[EntranceMessages]  WHERE Start_Time>= Convert(date,GETDATE())";
            dr = com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    messages.Add(new welcomeMessage(dr["Id"].ToString(), dr["Message"].ToString(), dr["Start_Time"].ToString(), dr["End_Time"].ToString()));
                }
            }
            else
            {
                messages.Add(new welcomeMessage("-1", "NO ACTIVE WELCOME MESSAGES", "01/01/2000", "01/01/2000"));
            }
        }
    }
}
