using Microsoft.Data.SqlClient;
using Signage.Controllers.Shared;
using Signage.Models.CSR;
using System.Diagnostics;

namespace Signage.Controllers.Signage
{
    public class CSR
    {
        SqlCommand com;
        SqlDataReader dr;
        SqlConnection conn;
        public TopInfo topInfo;
        public BottomFetchData botInfo;


        public CSR()
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

            if (topInfo.csrList.Count > 0)
                topInfo.csrList.Clear();

            try
            {
                conn.Open();
                com.Connection = conn;


                addTodayModel(com);
                addMTDModel(com);

                addCSR(com);
                conn.Close();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }

        private void addCSR(SqlCommand com)
        {
            com.CommandText = "SELECT * FROM PFMI_Signage.dbo.CSR_Table";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                topInfo.csrList.Add(new CSRModel(dr["Name"].ToString(), dr["Order"].ToString(), dr["Order Line"].ToString(), dr["Quotes"].ToString(), dr["Quotes Line"].ToString(), dr["Order Value"].ToString(), dr["Quote Value"].ToString()));
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
