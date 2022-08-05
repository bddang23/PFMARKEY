using Microsoft.Data.SqlClient;
using Signage.Controllers.Shared;
using Signage.Models.OSR;
using Signage2.Models.OSR;
using System.Diagnostics;

namespace Signage.Controllers.Signage
{
    public class OSR
    {
        SqlCommand com;
        SqlDataReader dr;
        SqlConnection conn;
        public TopInfo topInfo;
        public BottomFetchData botInfo;


        public OSR()
        {
            com = new SqlCommand();
            conn = new SqlConnection();
            topInfo = new TopInfo();
            botInfo = new BottomFetchData();
            conn.ConnectionString = "Data Source=PFM-SQL;Initial Catalog=PFMI_Signage;User Id=pfmarkey;Password=Pfmi2880;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database='PFMI_Signage'";
            FetchData();
        }

        private void FetchData()
        {
            if (topInfo.osrList.Count > 0)
                topInfo.osrList.Clear();

            try
            {
                conn.Open();
                com.Connection = conn;
                addTodayModel(com);
                addMTDModel(com);

                addMTD_OSR(com);
                addYTD_OSR(com);
         
                conn.Close();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }

        private void addYTD_OSR(SqlCommand com)
        {
            com.CommandText = "SELECT * FROM [PFMI_Signage].[dbo].[OSR_YTD_Table] ORDER BY [Name]";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                topInfo.ytdList.Add(new YTDModel(dr["Name"].ToString(), dr["thisYear"].ToString(), dr["lastYear"].ToString(), dr["New Customer"].ToString(), dr["New Cust Sales"].ToString(), dr["Rank"].ToString()));
            }
            dr.Close();


        }

        private void addMTD_OSR(SqlCommand com)
        {
            com.CommandText = "SELECT * FROM [PFMI_Signage].[dbo].[OSR_MTD_Table] ORDER BY [Name]";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                topInfo.osrList.Add(new MTDModel(dr["Name"].ToString(), dr["OrderSales"].ToString(), dr["Invoiced"].ToString(), dr["Rank"].ToString()));
            }
            dr.Close();
        }

        private void addTodayModel(SqlCommand com)
        {
            com.CommandText = "SELECT * FROM PFMI_Signage.dbo.BookingsDaily";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                topInfo.todayModel.bookings = dr["Expr1"].ToString();
            }
            dr.Close();

            com.CommandText = "SELECT * FROM PFMI_Signage.dbo.SalesDaily";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                topInfo.todayModel.sales = dr["Sales"].ToString();
            }
            dr.Close();

            com.CommandText = "SELECT * FROM PFMI_Signage.dbo.GoalDaily";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                topInfo.todayModel.goal = dr["Expr1"].ToString();
            }
            dr.Close();
        }
        private void addMTDModel(SqlCommand com)
        {
            com.CommandText = "SELECT * FROM PFMI_Signage.dbo.BookingsMTD";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                topInfo.mtdModel.bookings = dr["Expr1"].ToString();
            }
            dr.Close();

            com.CommandText = "SELECT * FROM PFMI_Signage.dbo.SalesMTD";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                topInfo.mtdModel.sales = dr["Sales"].ToString();
            }
            dr.Close();

            com.CommandText = "SELECT * FROM PFMI_Signage.dbo.GoalMTD";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                topInfo.mtdModel.goal = dr["Expr1"].ToString();
            }
            dr.Close();
        }

    }
}
