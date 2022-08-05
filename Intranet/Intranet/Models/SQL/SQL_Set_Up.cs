using System.Data.SqlClient;

namespace Intranet.Models.SQL
{
    public class SQL_Set_Up
    {
       public SqlCommand com;
       public SqlDataReader dr;
       public SqlConnection conn;
        public SQL_Set_Up()
        {
            com = new SqlCommand();
            conn = new SqlConnection();
            conn.ConnectionString = "Data Source=PFM-SQL;Initial Catalog=PFMI_Signage;User Id=pfmarkey;Password=Pfmi2880;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database='PFMI_Signage'";
            conn.Open();
            com.Connection = conn;
        }

        public SQL_Set_Up(int pfmSV20)
        {
            com = new SqlCommand();
            conn = new SqlConnection();
            conn.ConnectionString = "Data Source=PFM-SRV20;Initial Catalog=DEMO;User Id=pfmarkey;Password=Pfmi2880;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Connection Timeout=120";
            conn.Open();
            com.Connection = conn;
        }

        public void Execute(string cmd)
        {
            com.CommandText = cmd;
            com.ExecuteNonQuery();
        }
    }
}
