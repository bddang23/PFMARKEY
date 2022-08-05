using Microsoft.Data.SqlClient;
using Signage.Controllers.Shared;
using Signage.Models.Warehouse;
using System.Diagnostics;

namespace Signage.Controllers.Signage
{
    public class Warehouse
    {
        SqlCommand com;
        SqlDataReader dr;
        SqlConnection conn;
        public TopInfo topInfo;
        public BottomFetchData botInfo;

        public Warehouse()
        {
            com = new SqlCommand();
            conn = new SqlConnection();
            topInfo = new TopInfo();
            botInfo = new BottomFetchData();
            dr = null;

            FetchData();
        }

        private void FetchData()
        {
            try
            {

                AddPackageStats(conn);
                AddCostingInfo(conn);

                conn.Close();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }


        #region CostingInfo
        private void AddCostingInfo(SqlConnection conn)
        {
            conn.ConnectionString = "Data Source=PFM-SQL;Initial Catalog=PFMI_Signage;User Id=pfmarkey;Password=Pfmi2880;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database='PFMI_Signage'";
            conn.Open();
            com.Connection = conn;
            AddLastMonth(com); // last month this year and last year
            AddYTD(com);//this month last year and last year
            conn.Close();
        }

        private void AddYTD(SqlCommand com)
        {
            com.CommandText = "SELECT SUM(Shipping_Charges) sc, AVG(Avg_Cost_Per_Package) acpp,AVG(Cost_Per_Pound) cpp,SUM(Surchages) s,SUM(Audit_Savings) aSaving,YEAR(Date_For) y " +
                              "FROM PFMI_Signage.dbo.Veriship_Info " +
                              "WHERE MONTH(Date_For) <= MONTH(GETDATE())  AND YEAR(Date_For)> YEAR(GETDATE()) - 2 " +
                              "GROUP BY YEAR(Date_For)";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                topInfo.info.Add(new CostingInfo(dr["sc"].ToString(),
                                                 dr["acpp"].ToString(),
                                                 dr["cpp"].ToString(),
                                                 dr["s"].ToString(),
                                                 dr["aSaving"].ToString(),
                                                 dr["y"].ToString()));
            }
            dr.Close();
        }

        private void AddLastMonth(SqlCommand com)
        {

            com.CommandText = "SELECT Shipping_Charges, Avg_Cost_Per_Package,Cost_Per_Pound,Surchages,Audit_Savings,Date_For FROM PFMI_Signage.dbo.Veriship_Info WHERE MONTH(Date_For)=MONTH(GETDATE()) AND YEAR(Date_For)>YEAR(GETDATE())-2";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                topInfo.info.Add(new CostingInfo(dr["Shipping_Charges"].ToString(),
                                                 dr["Avg_Cost_Per_Package"].ToString(),
                                                 dr["Cost_Per_Pound"].ToString(),
                                                 dr["Surchages"].ToString(),
                                                 dr["Audit_Savings"].ToString(),
                                                 dr["Date_For"].ToString()));
            }
            dr.Close();
        }

        #endregion

        #region PackageStats

        private void AddPackageStats(SqlConnection conn)
        {
            conn.ConnectionString = "Data Source=PFM-SQL;Initial Catalog=PFMI_Signage;User Id=pfmarkey;Password=Pfmi2880;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database='PFMI_Signage'";
            conn.Open();
            com.Connection = conn;
            AddYTDModel(com);
            AddMTDModel(com);
            AddTodayModel(com);
            conn.Close();
        }

        private void AddYTDModel(SqlCommand com)
        {
            com.CommandText = "SELECT SUM(Packages_Received) received,SUM(Packages_Shipped) shipped,SUM(Deliveries_Made) delivered,SUM(Mispacks) mispacks FROM PFMI_Signage.dbo.Shipment_Tracking WHERE YEAR(DateTracked)=2022";
            dr = com.ExecuteReader();
            while (dr.Read())
            {

                topInfo.ytd.received = int.Parse(dr["received"].ToString());
                topInfo.ytd.shipped = int.Parse(dr["shipped"].ToString());
                topInfo.ytd.delivered = int.Parse(dr["delivered"].ToString());
                topInfo.ytd.mispacks = int.Parse(dr["mispacks"].ToString());
            }
            dr.Close();
        }
        private void AddTodayModel(SqlCommand com)
        {
            com.CommandText = "SELECT SUM(Packages_Received) received,SUM(Packages_Shipped) shipped,SUM(Deliveries_Made) delivered,SUM(Mispacks) mispacks FROM PFMI_Signage.dbo.Shipment_Tracking WHERE DateTracked = CONVERT(DATE,GETDATE())";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                try
                {

                    topInfo.today.received = int.Parse(dr["received"].ToString());
                    topInfo.today.shipped = int.Parse(dr["shipped"].ToString());
                    topInfo.today.delivered = int.Parse(dr["delivered"].ToString());
                    topInfo.today.mispacks = int.Parse(dr["mispacks"].ToString());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
            dr.Close();
        }
        private void AddMTDModel(SqlCommand com)
        {
            com.CommandText = "SELECT SUM(Packages_Received) received,SUM(Packages_Shipped) shipped,SUM(Deliveries_Made) delivered,SUM(Mispacks) mispacks FROM PFMI_Signage.dbo.Shipment_Tracking WHERE MONTH(DateTracked)=MONTH(GETDATE())";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                try
                {

                    topInfo.mtd.received = int.Parse(dr["received"].ToString());
                    topInfo.mtd.shipped = int.Parse(dr["shipped"].ToString());
                    topInfo.mtd.delivered = int.Parse(dr["delivered"].ToString());
                    topInfo.mtd.mispacks = int.Parse(dr["mispacks"].ToString());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
            dr.Close();
        }

        #endregion
    }

}

