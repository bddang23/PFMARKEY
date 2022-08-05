using Intranet.Models.Signage;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Intranet.Controllers.Signage
{
    public class QuotesController : Controller
    {
        private SqlCommand com;
        private SqlConnection conn;
        private SqlDataReader dr;
        public List<Quotes> quotes;

        public QuotesController()
        {
            com = new SqlCommand();
            conn = new SqlConnection();
            conn.ConnectionString = "Data Source=PFM-SQL;Initial Catalog=PFMI_Signage;User Id=pfmarkey;Password=Pfmi2880;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database='PFMI_Signage'";
            conn.Open();
            com.Connection = conn;

        }
        public IActionResult Index()
        {
            getQuotes();
            return View(quotes);
        }

        [HttpPost]
        public ActionResult Index(string quote, string name)
        {

            quote = quote.Replace("\n", "");
            quote = quote.Replace("\r", "");
            quote = quote.Replace("'", "''");
            quote = quote.Trim();


            try
            {
                com.CommandText = "INSERT INTO [dbo].[QuotesOfTheDay]  ([Quote],[Submitted_By]) " +
                                 "VALUES('" + quote + "','" + name + "')";
                com.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }


            getQuotes();
            return View(quotes);
        }

        [HttpGet]
        public ActionResult DeleteQuote(string id)
        {
            com.CommandText = "DELETE FROM [dbo].[QuotesOfTheDay] WHERE Id=" + id;
            dr = com.ExecuteReader();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult UpdateQuote(string id)
        {
            com.CommandText = "SELECT * FROM [dbo].[QuotesOfTheDay] WHERE Id=" + id;
            dr = com.ExecuteReader();
            dr.Read();
            Quotes quote = new Quotes(dr["Id"].ToString(), dr["Quote"].ToString(), dr["Submitted_By"].ToString());

            return View(quote);
        }

        [HttpPost]
        public ActionResult UpdateQuote(string id, string quote, string name)
        {

            quote = quote.Replace("\n", "");
            quote = quote.Replace("\r", "");
            quote = quote.Replace("'", "''");

            quote = quote.Trim();


            try
            {
                com.CommandText = "UPDATE [dbo].[QuotesOfTheDay] " +
                                  "SET [Quote] =  '" + quote +
                                   "', [Submitted_By] ='" + name +
                                 "' WHERE Id=" + id;
                com.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }


            getQuotes();
            return RedirectToAction("Index");
        }
        private void getQuotes()
        {
            quotes = new List<Quotes>();
            com.CommandText = "SELECT * FROM [dbo].[QuotesOfTheDay] ORDER BY Id DESC";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                quotes.Add(new Quotes(dr["Id"].ToString(), dr["Quote"].ToString(), dr["Submitted_By"].ToString()));
            }
        }
    }
}
