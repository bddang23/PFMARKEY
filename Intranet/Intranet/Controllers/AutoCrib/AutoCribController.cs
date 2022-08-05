using Intranet.Controllers.AutoCrib;
using Intranet.Models.AutoCrib;
using Intranet.Models.SQL;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Intranet.Controllers.Autocrib
{
    public class AutocribController : Controller
    {
        public SQL_Set_Up sql =null;
        public List<Specialist> specialist;
        public List<Company> companyList;

        #region specialist
        public IActionResult Specialist()
        {
            specialist = getSpecialist();
            return View(specialist);
        }

        private List<Specialist> getSpecialist()
        {
            List<Specialist> specialists = new List<Specialist>();
            sql = new SQL_Set_Up(1);
            sql.com.CommandText = "SELECT  [Id],[Name],[# of Accounts],[Items],[Out of Stock],[Red],[Yellow],[In Stock %]" +
                "  FROM[DEMO].[dbo].[DaysOfSupply_By_Specialist] dbs" +
                "  JOIN[DEMO].[dbo].[DaysOfSupply_AutoCrib_Specialists] das ON das.Name = dbs.Specialist";

            sql.dr = sql.com.ExecuteReader();
            while (sql.dr.Read())
            {
                specialists.Add(new Specialist(sql.dr["Id"].ToString(), sql.dr["Name"].ToString(), sql.dr["# of Accounts"].ToString(), sql.dr["Items"].ToString(), sql.dr["Out of Stock"].ToString(), sql.dr["Red"].ToString(), sql.dr["Yellow"].ToString(), sql.dr["In Stock %"].ToString()));
            }
            return specialists;
        }
        #endregion

        #region companiesBySpecialist
        [HttpGet]
        public IActionResult CompaniesBySpecialist(string id)
        {

            List<Company> list = new List<Company>();
            list = getCompanybySpecialist(id);
            return View(list);
        }

        private List<Company> getCompanybySpecialist(string id)
        {
            sql = new SQL_Set_Up(1);
            List<Company> list = new List<Company>();
            sql.com.CommandText = "SELECT das.Id AS [SpecialistID], das.Name,db.Id As [CompanyID], [ActualName],[Items],[Out_of_Stock],[Red],[Yellow],[InStock] " +
                " FROM[DEMO].[dbo].[DaysOfSupply_DBName] db" +
                "  LEFT JOIN [DEMO].[dbo].[DaysOfSupply_Results] r ON db.Id = r.Customer" +
                "  LEFT JOIN [DEMO].[dbo].[DaysOfSupply_AutoCrib_Specialists] das ON db.Assigned_Specialist = das.Id" +
                "  WHERE db.[Assigned_Specialist] = " + id;

            sql.dr = sql.com.ExecuteReader();
            while (sql.dr.Read())
            {
                try
                {
                    Company c = new Company(sql.dr["CompanyID"].ToString(), sql.dr["ActualName"].ToString(), sql.dr["Items"].ToString(), sql.dr["Out_of_Stock"].ToString(), sql.dr["Red"].ToString(), sql.dr["Yellow"].ToString(), sql.dr["InStock"].ToString());
                    c.assignedSpecialist = new Specialist(sql.dr["specialistID"].ToString(), sql.dr["Name"].ToString());
                    list.Add(c);
                }
                catch
                {
                    Debug.WriteLine("No Company Associated with Specialist's ID" + id);
                }
            }
            sql.dr.Close();
            return list;
        }
        #endregion

        #region CRUD
        public IActionResult ManageSpecialist()
        {
            List<Specialist> specialists = getFullSpecialist();
            return View(specialists);
        }

        public List<Specialist> getFullSpecialist()
        {
            List<Specialist> specialists = new List<Specialist>();
            sql = new SQL_Set_Up(1);
            sql.com.CommandText = "SELECT * FROM [DEMO].[dbo].[DaysOfSupply_AutoCrib_Specialists]";
            sql.dr = sql.com.ExecuteReader();
            while (sql.dr.Read())
            {
                specialists.Add(new Specialist(sql.dr["Id"].ToString(), sql.dr["Name"].ToString()));
            }
            return specialists;
        }

        [HttpPost]
        public ActionResult AddUpdateSpecialist(string id, string fullName)
        {
            sql = new SQL_Set_Up(1);
            string command;
            if (id == null)
            {
                command = "INSERT INTO [DEMO].[dbo].[DaysOfSupply_AutoCrib_Specialists] VALUES ('" + fullName + "')";
            }
            else
            {
                command = "UPDATE [DEMO].[dbo].[DaysOfSupply_AutoCrib_Specialists] SET [Name] = '" + fullName + "' WHERE Id=" + id;
            }

            sql.Execute(command);
            return RedirectToAction("ManageSpecialist");
        }

        [HttpGet]
        public JsonResult deleteSpecialist(string id)
        {
            sql = new SQL_Set_Up(1);
            string command = "DELETE FROM [DEMO].[dbo].[DaysOfSupply_AutoCrib_Specialists] WHERE Id=" + id;
            sql.Execute(command);
            return Json("done");
        }
        #endregion

        #region Company
        public ActionResult Company()
        {
            List<Company> list = new List<Company>();
            list = getCompany();
            return View(list);
        }

        private List<Company> getCompany()
        {
            List<Company> companies = new List<Company>();
            sql = new SQL_Set_Up(1);
            List<Company> list = new List<Company>();
            sql.com.CommandText = "SELECT das.Id AS [SpecialistID], das.Name,db.Id As [CompanyID], [ActualName],ISNULL([Items],0) AS [Items],ISNULL([Out_of_Stock],0) AS [Out_Of_Stock],ISNULL([Red],0) AS [Red],ISNULL([Yellow],0)  AS [Yellow],ISNULL([InStock],0)  AS [InStock]  " +
                " FROM[DEMO].[dbo].[DaysOfSupply_DBName] db" +
                "  LEFT JOIN[DEMO].[dbo].[DaysOfSupply_Results] r ON db.Id = r.Customer" +
                "  LEFT JOIN[DEMO].[dbo].[DaysOfSupply_AutoCrib_Specialists] das ON db.Assigned_Specialist = das.Id";

            sql.dr = sql.com.ExecuteReader();
            while (sql.dr.Read())
            {
                try
                {

                    Company c = new Company(sql.dr["CompanyID"].ToString(), sql.dr["ActualName"].ToString(), sql.dr["Items"].ToString(), sql.dr["Out_of_Stock"].ToString(), sql.dr["Red"].ToString(), sql.dr["Yellow"].ToString(), sql.dr["InStock"].ToString());
                    c.assignedSpecialist = new Specialist(sql.dr["specialistID"].ToString(), sql.dr["Name"].ToString());
                    companies.Add(c);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    
                }
            }
            sql.dr.Close();
            return companies;
        }
        #endregion

        #region assignSpeciialist
        [HttpGet]
        public IActionResult AssignSpecialist(string id)
        {
            AssignSpecialistPage page = new AssignSpecialistPage();
            page.specialists = getFullSpecialist();

            if (id == null)
                id = page.specialists[0].id.ToString();

            page.assignedCompany = getCompanybySpecialist(id);

            foreach (Specialist s in page.specialists)
            {
                if (s.id.ToString() == id)
                {
                    page.selSpecialist = s;
                    break;
                }
            }

            page.companies = getCompanyFromAssigned(page.assignedCompany);


            return View(page);
        }

        [HttpGet]
        public JsonResult AssignCompanytoSpecialist(string specialist, string companyID)
        {
            sql = new SQL_Set_Up(1);
            string command = "UPDATE [DEMO].[dbo].[DaysOfSupply_DBName] SET Assigned_Specialist = " + specialist + " WHERE Id = " + companyID;
            sql.Execute(command);
            return Json("done");
        }
        private List<Company> getCompanyFromAssigned(List<Company> assignedCompany)
        {
            List<Company> all = getCompany();
            List<Company> companies = new List<Company>();
            foreach (Company c in all)
            {
                if (!Contains(c, assignedCompany))
                {
                    companies.Add(c);
                }
            }
            return companies;
        }

        private static bool Contains(Company c, List<Company> list)
        {
            foreach (Company s in list)
            {
                if (s.id.ToString() == c.id.ToString())
                {
                    return true;
                }
            }
            return false;
        }
    }
    #endregion

}
