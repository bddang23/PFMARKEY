using Intranet.Models.SQL;

namespace Intranet.Models.Business
{
    public class VCSPage
    {
        public List<VendorCostSave> vendors;
        public VendorCostSave selVendor;
        public List<int> year;
        public int selYear;
        private SQL_Set_Up sql = new SQL_Set_Up();
        public VCSPage()
        {
            sql = new SQL_Set_Up();
            vendors = GetVendorList();
            selVendor = vendors[0];
            year = getYear();
            selYear = year[year.Count-2];
        }

        private List<int> getYear()
        {
            List <int> yearList = new List<int>();

            sql.com.CommandText = "SELECT DISTINCT(YEAR([Date])) AS Year FROM [PFMI_Signage].[dbo].[Vendors_Cost_Saved]";
                sql.dr = sql.com.ExecuteReader();
            while (sql.dr.Read())
            {
                yearList.Add(int.Parse(sql.dr["Year"].ToString()));
            }
            sql.dr.Close();

                int latest = yearList[yearList.Count-1];
                yearList.Add(latest + 1);
            
            return yearList;    
        }

        private List<VendorCostSave> GetVendorList()
        {
            List<VendorCostSave> vendorsList = getVendorID();

            foreach (VendorCostSave v in vendorsList)
            {
                sql.com.CommandText = "SELECT  vcs.[Id],[Cost_Saved],[Date]  FROM [PFMI_Signage].[dbo].[Vendors_Cost_Saved] vcs " +
                                      "INNER JOIN [PFMI_Signage].[dbo].[Vendors] v ON vcs.Vendor = v.Id " +
                                      "WHERE vendor = " + v.VendorId  +
                                      " ORDER BY v.[Vendor_Name]";
                sql.dr = sql.com.ExecuteReader();
                while (sql.dr.Read())
                {
                    v.Id.Add(int.Parse(sql.dr["Id"].ToString()));
                    v.VendorCost.Add(DateTime.Parse( sql.dr["Date"].ToString()),Decimal.Parse(sql.dr["Cost_Saved"].ToString()));
                }
                sql.dr.Close();
            }

            return vendorsList;
        }

        private List<VendorCostSave> getVendorID()
        {
            List<VendorCostSave> vendorsList = new List<VendorCostSave>();
            sql.com.CommandText = "SELECT [Id],vendor_name FROM [PFMI_Signage].[dbo].[Vendors] v " +
                                  "ORDER BY v.[Vendor_Name];";
            sql.dr = sql.com.ExecuteReader();
            while (sql.dr.Read())
            {
                vendorsList.Add(new VendorCostSave(sql.dr["Id"].ToString(), sql.dr["Vendor_Name"].ToString()));
            }
            sql.dr.Close();
            return vendorsList;
        }
    }
}
