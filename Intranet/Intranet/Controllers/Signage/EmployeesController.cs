using Intranet.Models.Signage.ManageEmployee;
using Intranet.Models.SQL;
using Microsoft.AspNetCore.Mvc;

namespace Intranet.Controllers.Signage
{
    public class Employees : Controller
    {
        EmployeePage ep;
        public IActionResult Index()
        {
            ep = new EmployeePage();
            ep.action = "AddEmployee";
            return View(ep);
        }

        [HttpPost]
        public IActionResult AddEmployee(string fname, string lname, string title, string devision, string role, string location, string startDate, string birthday, string phone, string email)
        {
            string taker_name = fname.Trim().ToUpper() + lname.Trim().ToUpper();

            string command = "INSERT INTO [dbo].[Employees] " +
                             "VALUES (" +
                             "'" + fname + "'," +
                             "'" + lname + "'," +
                             "'" + email + "'," +
                             "'" + birthday + "'," +
                             "'" + startDate + "'," +
                             "'" + devision + "'," +
                             "'" + title + "'," +
                             "'" + role + "'," +
                             "'" + location + "'," +
                             "'" + phone + "'," +
                             "'" + taker_name + "')";


            SQL_Set_Up sql = new SQL_Set_Up();
            sql.Execute(command);

            return RedirectToAction("Index");
        }

        public ActionResult DeleteEmployee(string id)
        {
            string command = "DELETE FROM [dbo].[Employees] " +
                             "WHERE Id = " + id;
            SQL_Set_Up sql = new SQL_Set_Up();
            sql.Execute(command);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateEmployee(string id, string fname, string lname, string title, string devision, string role, string location, string startDate, string birthday, string phone, string email)
        {
            string taker_name = fname.Trim().ToUpper() + lname.Trim().ToUpper();
            string command = "UPDATE [dbo].[Employees] " +
                            "SET [First_Name] ='" + fname + "'" +
                            "   ,[Last_Name] = '" + lname + "'" +
                            "   ,[Email] = '" + email + "'" +
                            "   ,[Birthday] = '" + birthday + "'" +
                            "   ,[Start_Date] ='" + startDate + "'" +
                            "   ,[Division] = '" + devision + "'" +
                            "   ,[Title] = '" + title + "'" +
                            "   ,[Role] = '" + role + "'" +
                            "   ,[Location] = '" + location + "'" +
                            "   ,[PhoneEmail] ='" + phone + "'" +
                            "   ,[Taker_Name] ='" + taker_name + "'" +
                            " WHERE Id = " + id;

            SQL_Set_Up sql = new SQL_Set_Up();
            sql.Execute(command);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateEmployee(string id)
        {
            EmployeePage ep = new EmployeePage(id);
            ep.action = "UpdateEmployee";
            return View(ep);
        }
    }
}
