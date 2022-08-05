using Signage.Controllers.SQL;
using Signage.Models.Bottom;

namespace Signage.Models.CheckIn
{
    public class CheckInPage
    {
        SQL_Set_Up sql;
        public List<EmployeeModel> emp;
        public List<Visitor> visitor;

        public CheckInPage()
        {
            sql = new SQL_Set_Up();
            emp = addEmployees();
            visitor = getVisitor();
        }

        private List<EmployeeModel> addEmployees()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();

            sql.com.CommandText = "SELECT Id, First_Name,Last_Name FROM PFMI_Signage.dbo.Employees";
            sql.dr = sql.com.ExecuteReader();
            while (sql.dr.Read())
            {
                employees.Add(new EmployeeModel(sql.dr["Id"].ToString(),
                                                sql.dr["First_Name"].ToString(),
                                                sql.dr["Last_Name"].ToString()));
            }
            sql.dr.Close();

            return employees;
        }


        private List<Visitor> getVisitor()
        {
            List<Visitor> visitors = new List<Visitor>();
            sql.com.CommandText = "SELECT DISTINCT [Name],[Company] FROM PFMI_Signage.dbo.Visitor";
            sql.dr = sql.com.ExecuteReader();
            while (sql.dr.Read())
            {
                visitors.Add(new Visitor(1, sql.dr["Name"].ToString(), sql.dr["Company"].ToString()));
            }
            sql.dr.Close();
            return visitors;
        }
    }

}
