using Microsoft.Data.SqlClient;
using Signage.Controllers.Shared;
using Signage.Models.AutoCrib;
using Signage.Models.AutoCribs;
using System.Diagnostics;

namespace Signage.Controllers.Signage
{
    public class AutoCrib
    {
        SqlCommand com;
        SqlDataReader dr;
        SqlConnection conn;
        public TopInfo topInfo;
        public BottomFetchData botInfo;

        public AutoCrib()
        {
            com = new SqlCommand();
            conn = new SqlConnection();
            topInfo = new TopInfo();
            botInfo = new BottomFetchData();

            FetchData();
        }

        private void FetchData()
        {
            try
            {
                AddInventoryStats(conn);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message.ToString());
            }
        }


        #region InventoryStats
        private void AddInventoryStats(SqlConnection conn)
        {
            try
            {
                conn.ConnectionString = "Data Source=PFM-SRV20;Initial Catalog=DEMO;User Id=pfmarkey;Password=Pfmi2880;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Connection Timeout=120";
                conn.Open();
                com.Connection = conn;


                AddDayofSupply(com);
                AddAutoCrib(com);
                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private void AddDayofSupply(SqlCommand com)
        {
 
            com.CommandText = "SELECT * FROM [DEMO].[dbo].[DaysOfSupply_With_Names]";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                topInfo.daysOfSupply.Add(new InventoryStats(dr["Account"].ToString(),
                                                                  dr["Items"].ToString(),
                                                                  dr["Out of Stock"].ToString(),
                                                                  dr["Red"].ToString(),
                                                                  dr["Yellow"].ToString(),
                                                                  dr["In Stock %"].ToString()));
            }
            dr.Close();
        }

        private void AddAutoCrib(SqlCommand com)
        {

            com.CommandText = "SELECT * FROM [DEMO].[dbo].[InvValue_By_Specialist]";
            dr = com.ExecuteReader();
            Debug.WriteLine("here");
            while (dr.Read())
            {

                topInfo.autocribSpecialist.Add(new Specialist(dr["Specialist"].ToString(),
                                                                  dr["# of Accounts"].ToString(),
                                                                  dr["ItemsCount"].ToString(),
                                                                  dr["inventoryValue"].ToString(),
                                                                  dr["SlowValue"].ToString(),
                                                                  dr["DeadValue"].ToString(),
                                                                  dr["Dead %"].ToString()));
            }
            dr.Close();
        }


        #endregion
    }
}
