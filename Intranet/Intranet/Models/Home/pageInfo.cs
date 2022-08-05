using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Intranet.Models.Home
{
    public class pageInfo
    {
        SqlCommand com;
        SqlDataReader dr;
        SqlConnection conn;
        public string quote;
        public pageInfo()
        {
            com = new SqlCommand();
            conn = new SqlConnection();
            conn.ConnectionString = "Data Source=PFM-SQL;Initial Catalog=PFMI_Signage;User Id=pfmarkey;Password=Pfmi2880;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database='PFMI_Signage'";
            conn.Open();
            com.Connection = conn;
            AddQuotes();
        }
        private void AddQuotes()
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
            quote = quotes[rNum];
            if (quote.Contains("\""))
            {
                try
                {

                    quote = quote.Substring(quote.IndexOf("\"") + 1, quote.LastIndexOf("\""));

                }catch(Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }
    }
   
}
