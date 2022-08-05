using Intranet.Models.Business;
using Intranet.Models.SQL;
using Microsoft.AspNetCore.Mvc;

namespace Intranet.Controllers.Business
{
    public class CSRController : Controller
    {
        SQL_Set_Up sql;


        #region CSR GOAL
        [HttpGet]
        public IActionResult CSRGoal(string month)
        {
            if (string.IsNullOrEmpty(month))
            {
                month = DateTime.Today.ToString("MM-01-yyyy");
            }
            else { month = DateTime.Parse(month).ToString("MM-01-yyyy"); }

            CSRGoal csr = getCSR(month);
            return View(csr);
        }

        [HttpPost]
        public ActionResult updateGoal(string monthYear, string daily, string mtd)
        {
            sql = new SQL_Set_Up();
            monthYear = DateTime.Parse(monthYear).ToString("MM-01-yyyy");
            int count;
            sql.com.CommandText = "SELECT COUNT(*) FROM [PFMI_Signage].[dbo].[Sales_Booking_Goals] WHERE Month_Applicable = '" + monthYear + "'";
            sql.dr = sql.com.ExecuteReader();
            sql.dr.Read();
            count = int.Parse(sql.dr[0].ToString());
            sql.dr.Close();
            if (count == 0)
            {
                string command = "INSERT INTO [PFMI_Signage].[dbo].[Sales_Booking_Goals] " +
                                 "VALUES ('" + daily + "','"
                                           + mtd + "','"
                                           + monthYear + "')";
                sql.Execute(command);
            }
            else if (count == 1)
            {
                string command = "UPDATE [PFMI_Signage].[dbo].[Sales_Booking_Goals] " +
                                 "SET  [Sales_Day] ='" + daily + "'," +
                                     " [Sales_Month] ='" + mtd +
                                 "' WHERE [Month_Applicable] ='" + monthYear + "';";
                sql.Execute(command);
            }
            monthYear = DateTime.Parse(monthYear).ToString("MM-yyyy");
            return RedirectToAction("CSRGoal", "CSR", new { month = monthYear });
        }

        private CSRGoal getCSR(string month)
        {
            sql = new SQL_Set_Up();
            CSRGoal csr;
            sql.com.CommandText = "SELECT * FROM [PFMI_Signage].[dbo].[Sales_Booking_Goals] WHERE Month_Applicable = '" + month + "'";
            sql.dr = sql.com.ExecuteReader();
            if (sql.dr.HasRows)
            {
                sql.dr.Read();
                csr = new CSRGoal(sql.dr["Sales_Day"].ToString(), sql.dr["Sales_Month"].ToString(), sql.dr["Month_Applicable"].ToString());
            }
            else
            {
                csr = new CSRGoal("0", "0", month);
            }
            sql.dr.Close();
            return csr;
        }
        #endregion

        

    }
}
