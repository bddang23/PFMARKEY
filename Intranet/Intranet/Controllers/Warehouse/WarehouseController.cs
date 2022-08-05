using Intranet.Models.SQL;
using Intranet.Models.Warehouse;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Intranet.Controllers.Warehouse
{
    public class WarehouseController : Controller
    {
        private SQL_Set_Up sql;
        public List<Shipments> shipments;
        public Shipments todayShipment;
        public Veriship verishipInfo;

        #region Shipments
        public IActionResult Shipments()
        {
            todayShipment = getTodayShipments();
            return View(todayShipment);
        }

        public JsonResult updateShipments(string Received, string Deliveries, string Shipped, string Mispacks)
        {
            sql = new SQL_Set_Up();
            int count;

            sql.com.CommandText = "SELECT COUNT(*) FROM [PFMI_Signage].[dbo].[Shipment_Tracking] WHERE(DateTracked)= CONVERT(DATE,GETDATE())";
            sql.dr = sql.com.ExecuteReader();
            sql.dr.Read();
            count = int.Parse(sql.dr[0].ToString());
            sql.dr.Close();
            if (count == 0)
            {
                string command = "INSERT INTO [PFMI_Signage].[dbo].[Shipment_Tracking] " +
                                 "VALUES ('" + Received + "','"
                                           + Shipped + "','"
                                            + Deliveries + "','"
                                             + Mispacks + "','"
                                           + DateTime.Today.ToString("d") + "')";
                sql.Execute(command);
            }
            else if (count == 1)
            {
                string command = "UPDATE [PFMI_Signage].[dbo].[Shipment_Tracking] " +
                                 "SET  [Packages_Received] ='" + Received + "'," +
                                     " [Packages_Shipped] ='" + Shipped + "'," +
                                     " [Deliveries_Made] ='" + Deliveries + "'," +
                                     " [Mispacks] ='" + Mispacks +
                                 "' WHERE [DateTracked] ='" + DateTime.Today.ToString("d") + "';";
                sql.Execute(command);
            }

            return Json("done");
        }

        private Shipments getTodayShipments()
        {
            sql = new SQL_Set_Up();
            Shipments today = new Shipments();

            sql.com.CommandText = "SELECT * FROM [PFMI_Signage].[dbo].[Shipment_Tracking] WHERE(DateTracked)= CONVERT(DATE,GETDATE())";
            sql.dr = sql.com.ExecuteReader();
            if (sql.dr != null)
            {
                sql.dr.Read();
                try
                {
                    today = new Shipments(sql.dr["Id"].ToString(), sql.dr["Packages_Received"].ToString(), sql.dr["Packages_Shipped"].ToString(), sql.dr["Deliveries_Made"].ToString(), sql.dr["Mispacks"].ToString(), sql.dr["DateTracked"].ToString());
                }
                catch
                {
                    today = new Shipments("0", "0", "0", "0", "0", DateTime.Today.ToString("d"));
                }
            }

            return today;
        }
        #endregion

        #region History
        [HttpGet]
        public IActionResult History(String date)
        {
            shipments = getHistory(date);
            return View(shipments);
        }

        private List<Shipments> getHistory(String date)
        {
            sql = new SQL_Set_Up();
            List<Shipments> shipments = new List<Shipments>();
            if (date == null)
            {
                date = DateTime.Today.ToString("MM-01-yyyy");
            }
            else
            {
                date = date + "-01";
            }
            sql.com.CommandText = "SELECT * FROM [PFMI_Signage].[dbo].[Shipment_Tracking] WHERE MONTH(DateTracked)=MONTH('" + date + "') AND YEAR(DateTracked) = YEAR('" + date + "') ORDER BY [DateTracked]";
            sql.dr = sql.com.ExecuteReader();
            if (sql.dr != null)
            {
                while (sql.dr.Read())
                {
                    shipments.Add(new Shipments(sql.dr["Id"].ToString(), sql.dr["Packages_Received"].ToString(), sql.dr["Packages_Shipped"].ToString(), sql.dr["Deliveries_Made"].ToString(), sql.dr["Mispacks"].ToString(), sql.dr["DateTracked"].ToString()));
                };

            }
            if (shipments.Count == 0)
            {
                shipments.Add(new Shipments(date));
            }
            return shipments;
        }

        public JsonResult updateHistory(string jsonString)
        {
            JArray array = JArray.Parse(jsonString);
            List<Shipments> shipments = new List<Shipments>();
            string command = "";
            try
            {

                foreach (JObject obj in array.Children<JObject>())
                {

                    string id = obj["id"].ToString();
                    string packages_Received = obj["Packages_Received"].ToString();
                    string packages_Shipped = obj["Packages_Shipped"].ToString();
                    string deliveries_Made = obj["Deliveries_Made"].ToString();
                    string mispacks = obj["Mispacks"].ToString();
                    string dateTracked = obj["DateTracked"].ToString();

                    command += "UPDATE [PFMI_Signage].[dbo].[Shipment_Tracking] " +
                                     "SET  [Packages_Received] ='" + packages_Received + "'," +
                                         " [Packages_Shipped] ='" + packages_Shipped + "'," +
                                         " [Deliveries_Made] ='" + deliveries_Made + "'," +
                                         " [Mispacks] ='" + mispacks +
                                       "', [DateTracked] ='" + dateTracked + "'" +
                                   " WHERE [Id] = " + id + ";\n";


                    sql = new SQL_Set_Up();
                    sql.Execute(command);
                }
                        return Json("done");
            }
            catch
            {
                return Json("fail");
            }
        }
        #endregion

        #region Veriship
        [HttpGet]
        public IActionResult Veriship(string date)
        {
            if (date == null)
                date = DateTime.Today.ToString("d");
            else
                date = date + "-01";
            verishipInfo = getVeriship(date);
            return View(verishipInfo);
        }

        private Veriship getVeriship(string date)
        {
            Veriship verishipInfo = new Veriship();
            sql = new SQL_Set_Up();

            sql.com.CommandText = "SELECT * FROM [PFMI_Signage].[dbo].[Veriship_Info] WHERE MONTH(Date_For)=MONTH('" + date + "') AND YEAR(Date_For) = YEAR('" + date + "');";
            sql.dr = sql.com.ExecuteReader();
            if (sql.dr != null)
            {
                sql.dr.Read();
                if (sql.dr.HasRows)
                {
                    verishipInfo = new Veriship(sql.dr["Id"].ToString(), sql.dr["Shipping_Charges"].ToString(), sql.dr["Avg_Cost_Per_Package"].ToString(), sql.dr["Cost_Per_Pound"].ToString(), sql.dr["Surchages"].ToString(), sql.dr["Audit_Savings"].ToString(), sql.dr["Date_For"].ToString());
                }
                else
                    verishipInfo = new Veriship("0", "0", "0", "0", "0", "0", date);
            }

            return verishipInfo;
        }

        public ActionResult UpdateVeriship(string dateFor, string shippingCharges, string avgCostPerPack, string costPerPound, string surcharges, string auditSaving)
        {
            sql = new SQL_Set_Up();
            dateFor = DateTime.Parse(dateFor).ToString("d");

            shippingCharges = trimInput(shippingCharges);
            avgCostPerPack = trimInput(avgCostPerPack);
            costPerPound = trimInput(costPerPound);
            surcharges = trimInput(surcharges);
            auditSaving = trimInput(auditSaving);

            int count;
            sql.com.CommandText = "SELECT COUNT(*) FROM [PFMI_Signage].[dbo].[Veriship_Info] WHERE  MONTH(Date_For)=MONTH('" + dateFor + "') AND YEAR(Date_For) = YEAR('" + dateFor + "');";
            sql.dr = sql.com.ExecuteReader();
            sql.dr.Read();
            count = int.Parse(sql.dr[0].ToString());
            sql.dr.Close();
            if (count == 0)
            {
                string command = "INSERT INTO [PFMI_Signage].[dbo].[Veriship_Info] " +
                                 "VALUES ('" + shippingCharges + "','"
                                           + avgCostPerPack + "','"
                                           + "0','"
                                           + costPerPound + "','"
                                           + surcharges + "','"
                                           + auditSaving + "','"
                                           + dateFor + "')";
                sql.Execute(command);
            }
            else if (count == 1)
            {
                string command = "UPDATE [PFMI_Signage].[dbo].[Veriship_Info]" +
                    "   SET [Shipping_Charges] = '" + shippingCharges +
                    "'       ,[Avg_Cost_Per_Package] = '" + avgCostPerPack +
                    "'       ,[Cost_Per_Pound] = '" + costPerPound +
                    "'      ,[Surchages] = '" + surcharges +
                    "'      ,[Audit_Savings] = '" + auditSaving +
                    "'  WHERE  MONTH(Date_For) = MONTH('" + dateFor + "') AND YEAR(Date_For) = YEAR('" + dateFor + "'); ";

                sql.Execute(command);
            }
            dateFor = DateTime.Parse(dateFor).ToString("yyyy-MM");
            return RedirectToAction("Veriship", "Warehouse", new { date = dateFor });
        }

        private string trimInput(string str)
        {
            return str.Replace(",", "").Trim();
        }


        #endregion
    }
}
