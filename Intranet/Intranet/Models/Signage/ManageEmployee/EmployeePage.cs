
using Intranet.Models.SQL;

namespace Intranet.Models.Signage.ManageEmployee
{
    public class EmployeePage
    {
        public List<string> roles;
        public List<string> locations;
        public List<Employee> employees;
        private SQL_Set_Up sql;
        public string action;
        public EmployeePage()
        {
            sql = new SQL_Set_Up();
            getLocations();
            getRoles();
            getEmployees();
        }

        public EmployeePage(string id)
        {
            sql = new SQL_Set_Up();
            getLocations();
            getRoles();
            getEmployees(id);
        }

        private void getEmployees(string id)
        {
            employees = new List<Employee>();
            sql.com.CommandText = "SELECT e.Id,[First_Name],[Last_Name],[Email],[Birthday],[Start_Date],[Division],[Title], [Role],[Location],[PhoneEmail],[Taker_Name] " +
                               "FROM[PFMI_Signage].[dbo].[Employees] e " +
                               "WHERE e.Id = " + id;
            sql.dr = sql.com.ExecuteReader();

            while (sql.dr.Read())
            {

                employees.Add(new Employee(sql.dr["Id"].ToString(),
                                           sql.dr["First_Name"].ToString(),
                                           sql.dr["Last_Name"].ToString(),
                                           sql.dr["Email"].ToString(),
                                           sql.dr["Birthday"].ToString(),
                                           sql.dr["Start_Date"].ToString(),
                                           sql.dr["Division"].ToString(),
                                           sql.dr["Title"].ToString(),
                                           sql.dr["Role"].ToString(),
                                           sql.dr["Location"].ToString(),
                                           sql.dr["PhoneEmail"].ToString()));
            }
            sql.dr.Close();
        }

        public void executeCmd(string command)
        {
            sql.Execute(command);
        }
        private void getEmployees()
        {
           employees = new List<Employee>();
           sql.com.CommandText = "SELECT e.Id,[First_Name],[Last_Name],[Email],[Birthday],[Start_Date],[Division],[Title],r.RoleName [Role],[Location],[PhoneEmail],[Taker_Name] " +
                              "FROM[PFMI_Signage].[dbo].[Employees] e INNER JOIN " +
                              "[PFMI_Signage].[dbo].[Roles] r ON e.Role = r.Id " +
                              "ORDER BY Last_Name,First_Name,Location";
            sql.dr = sql.com.ExecuteReader();
            
            while (sql.dr.Read())
            {

                employees.Add(new Employee(sql.dr["Id"].ToString(),
                                           sql.dr["First_Name"].ToString(),
                                           sql.dr["Last_Name"].ToString(),
                                           sql.dr["Email"].ToString(),
                                           sql.dr["Birthday"].ToString(),
                                           sql.dr["Start_Date"].ToString(),
                                           sql.dr["Division"].ToString(),
                                           sql.dr["Title"].ToString(),
                                           sql.dr["Role"].ToString(),
                                           sql.dr["Location"].ToString(),
                                           sql.dr["PhoneEmail"].ToString()));
            }
            sql.dr.Close();
        }

        private void getLocations()
        {
            locations = new List<string>();
            sql.com.CommandText = "SELECT Location FROM [PFMI_Signage].[dbo].[Employees] GROUP BY Location";
            sql.dr = sql.com.ExecuteReader();
            while (sql.dr.Read())
            {
                if (!sql.dr["Location"].ToString().Equals("NULL"))
                {
                    locations.Add(sql.dr["Location"].ToString());
                }
            }
            sql.dr.Close();
        }

        private void getRoles()
        {
            roles = new List<string>();
            sql.com.CommandText = "SELECT * FROM [dbo].Roles";
            sql.dr = sql.com.ExecuteReader();
            while (sql.dr.Read())
            {
                roles.Add(sql.dr["RoleName"].ToString());
            }
            sql.dr.Close();
        }
    }
}
