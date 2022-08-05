using Microsoft.Data.SqlClient;
using Signage.Models.Bottom;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Signage.Controllers.Shared
{
    public class BottomFetchData
    {
        SqlCommand com;
        SqlDataReader dr;
        SqlConnection conn;
        public BottomInfo bottomInfo;

        public BottomFetchData()
        {
            com = new SqlCommand();
            conn = new SqlConnection();
            bottomInfo = new BottomInfo();
            dr = null;
            conn.ConnectionString = "Data Source=PFM-SQL;Initial Catalog=PFMI_Signage;User Id=pfmarkey;Password=Pfmi2880;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database='PFMI_Signage'";
            FetchBottomData();
        }
        private void FetchBottomData()
        {
            if (bottomInfo.employees.Count > 0)
                bottomInfo.employees.Clear();
            if (bottomInfo.events.Count > 0)
                bottomInfo.events.Clear();

            try
            {
                conn.Open();
                com.Connection = conn;

                addEmployees(com);
                addEvents(com);
                addQuote(com);

                conn.Close();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }

        }

        private void addQuote(SqlCommand com)
        {
            List<string> quotes = new List<string>();

            com.CommandText = "SELECT Quote FROM PFMI_Signage.dbo.QuotesOfTheDay";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                quotes.Add(dr["Quote"].ToString());
            }
            dr.Close();

            var rNum = RandomNumberGenerator.GetInt32(0, quotes.Count);
            bottomInfo.quote = quotes[rNum];
        }

        private void addEmployees(SqlCommand com)
        {
            com.CommandText = "SELECT Id, First_Name,Last_Name,Birthday,Start_Date FROM PFMI_Signage.dbo.Employees WHERE Month(Start_Date) = Month(GETDATE()) OR Month(Birthday)= Month(GETDATE()) ";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                bottomInfo.employees.Add(new EmployeeModel(dr["Id"].ToString(),
                                                dr["First_Name"].ToString(),
                                                dr["Last_Name"].ToString(),
                                                dr["Birthday"].ToString(),
                                                dr["Start_Date"].ToString()));
            }
            dr.Close();
        }

        private void addEvents(SqlCommand com)
        {
            //Get Data from Calendar Events and added to List
            com.CommandText = "SELECT Title, Start FROM PFMI_Signage.dbo.CalendarEvents WHERE Start>=Convert(date,GETDATE()) ORDER BY Start";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                bottomInfo.events.Add(new Event(dr["Title"].ToString(), Convert.ToDateTime(dr["Start"].ToString()).ToString("MMMM d")));
            }
            dr.Close();

            if (bottomInfo.events.Count == 0)
            {
                bottomInfo.events.Add(new Event("No Upcoming Events!", null));
            }
        }

    }
}
