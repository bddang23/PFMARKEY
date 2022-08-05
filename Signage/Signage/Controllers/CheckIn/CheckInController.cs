using Microsoft.AspNetCore.Mvc;
using Signage.Controllers.SQL;
using Signage.Models.Bottom;
using Signage.Models.CheckIn;

namespace Signage.Controllers.CheckIn
{
    public class CheckInController : Controller
    {
        public SQL_Set_Up sql = new SQL_Set_Up();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GuestCheckIn()
        {
            CheckInPage checkInPage = new CheckInPage();
            return View(checkInPage);
        }

        public IActionResult GuestCheckOut()
        {
            List<Visitor> visitors = getVisitor();

            return View(visitors);
        }

        public IActionResult checkOut(string visitor, string feedback)
        {
            if (feedback == null)
            {
                feedback = "NA";
            }
            if (visitor != null)
            {

                string time = DateTime.Now.ToString("G");
                string command = "UPDATE PFMI_Signage.dbo.Visitor SET [Feedback]='" + feedback + "',[Sign Out Time]='" + time + "' WHERE [visTUID] = " + visitor;
                sql.Execute(command);

            }
            return RedirectToAction("Index");
        }

        private List<Visitor> getVisitor()
        {
            List<Visitor> visitors = new List<Visitor>();
            sql.com.CommandText = "SELECT [visTUID], [Name] FROM PFMI_Signage.dbo.Visitor WHERE [Sign Out Time] ='NA' ";
            sql.dr = sql.com.ExecuteReader();
            while (sql.dr.Read())
            {
                visitors.Add(new Visitor(sql.dr["visTUID"].ToString(), sql.dr["Name"].ToString()));
            }
            sql.dr.Close();

            return visitors;
        }



        [HttpPost]
        public ActionResult FormSubmit(string Vname, string company, string employee)
        {
            string Ename;
            string EiD;
            if (employee != null)
            {
                Ename = employee.Substring(0, employee.IndexOf("_"));
                EiD = employee.Substring(employee.IndexOf("_") + 1);
            }
            else
            {
                Ename = "NA";
                EiD = "0";
            }

            DateTime dt = DateTime.Now;
            string command = "INSERT INTO [dbo].[Visitor] ([Name],[Company],[Sign In Time],[Sign Out Time],[EmployeeVisited],[EmployeeVisitedID]) VALUES('" + Vname.ToUpper() + "','" + company.ToUpper() + "','" + dt.ToString("G") + "','" + "NA" + "','" + Ename + "','" + EiD + "')";
            sql.Execute(command);

            sql.com.CommandText = "SELECT TOP 1 Email FROM [PFMI_Signage].[dbo].[Employees] e INNER JOIN [PFMI_Signage].[dbo].[Visitor] v ON e.Id = " + EiD;
            string recipient = "";
            sql.dr = sql.com.ExecuteReader();
            if (sql.dr != null)
            {
                while (sql.dr.Read())
                {
                    recipient = sql.dr["Email"].ToString();
                }
            }
            if (!String.IsNullOrEmpty(recipient))
            {
                EmailController email = new EmailController();
                email.sendMessage(recipient, Ename, Vname.ToUpper(), company.ToUpper());
            }
            return RedirectToAction("Index");
        }
    }
}
