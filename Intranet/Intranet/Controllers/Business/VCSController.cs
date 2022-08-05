using Intranet.Models.Business;
using Intranet.Models.SQL;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Intranet.Controllers.Business
{
    public class VCSController : Controller
    {
        public VCSPage vcs;
        private SQL_Set_Up sql;


        #region costSavingByYear
        [HttpGet]
        public IActionResult CostSaving_ByYear(string year)
        {
            vcs = new VCSPage();
            if (year != null)
            {
                vcs.selYear = int.Parse(year);
            }

            return View(vcs);
        }

        [HttpPost]
        public ActionResult UpdateYear(IFormCollection data)
        {
            int selYear = int.Parse(data["year"]);
            sql = new SQL_Set_Up();
            string command = "";

            foreach (var item in data)
            {
                int vendorID;
                decimal costSave;

                if (int.TryParse(item.Key, out vendorID))
                {
                    costSave = decimal.Parse(data[item.Key].ToString().Replace(",", "").Trim());
                    int count;
                    sql.com.CommandText = "SELECT COUNT(*) FROM [PFMI_Signage].[dbo].[Vendors_Cost_Saved] WHERE Vendor = " + vendorID + " AND YEAR([Date])=" + selYear;
                    sql.dr = sql.com.ExecuteReader();
                    sql.dr.Read();
                    count = int.Parse(sql.dr[0].ToString());
                    sql.dr.Close();
                    if (count == 0)
                    {
                        if (costSave > 0) { 
                        command = "INSERT INTO [PFMI_Signage].[dbo].[Vendors_Cost_Saved] " +
                                        "VALUES (" + vendorID + ","
                                                  + costSave + "," +
                                                  "'01-01-" + selYear + "');";
                        sql.Execute(command);
                    }

                }
                else if (count == 1)
                {
                        if (costSave == 0)
                        {
                            command = "DELETE FROM [PFMI_Signage].[dbo].[Vendors_Cost_Saved] " +
                                 " WHERE Vendor = " + vendorID + " AND YEAR([Date])= " + selYear;
                        }
                        else {

                            command = "UPDATE [PFMI_Signage].[dbo].[Vendors_Cost_Saved] " +
                                            "SET  [Cost_Saved] =" + costSave +
                                            " WHERE Vendor = " + vendorID + " AND YEAR([Date])=" + selYear;
                        }

                        sql.Execute(command);

                }

            }

        }

            return RedirectToAction("CostSaving_ByYear", "VCS", new { year = selYear });
        }

#endregion

#region costSavingByVendor
[HttpGet]
public IActionResult CostSaving_ByVendor(string vendor)
{
    vcs = new VCSPage();
    if (vendor != null)
    {
        foreach (var v in vcs.vendors)
        {
            if (v.VendorId == int.Parse(vendor))
            {
                vcs.selVendor = v;
                break;
            }
        }
    }

    return View(vcs);
}

[HttpPost]
public ActionResult UpdateCost(IFormCollection data)
{
    var id = data["vId"].ToString();
    sql = new SQL_Set_Up();
    string command = "";

    foreach (var item in data)
    {
        int year;
        if (int.TryParse(item.Key, out year))
        {
            decimal savingCost = decimal.Parse(item.Value.ToString().Replace(",", "").Trim());
            sql.com.CommandText = "SELECT COUNT(*) " +
                                  "FROM [dbo].[Vendors_Cost_Saved] " +
                                  "WHERE YEAR(Date)=" + year + " AND Vendor = " + id;
            sql.dr = sql.com.ExecuteReader();
            sql.dr.Read();
            if (sql.dr[0].ToString().Equals("1"))
            {
                command += " UPDATE [dbo].[Vendors_Cost_Saved] " +
                          " SET  [Cost_Saved] = " + savingCost +
                          " WHERE [Vendor] =  " + id + " AND YEAR(Date) = " + year + ";";
            }
            else
            {
                command += " INSERT INTO [dbo].[Vendors_Cost_Saved] " +
                          " VALUES ( " + id + "," + savingCost + ",'" + year + "-01-01');";

            }
            sql.dr.Close();
        }
    }

    if (!String.IsNullOrEmpty(command))
    {
        sql.Execute(command);
    }




    return RedirectToAction("CostSaving_ByVendor", "VCS", new { vendor = id });
}

[HttpPost]
public JsonResult DeleteYear(string Year, string vId)
{
    sql = new SQL_Set_Up();
    string command = "";
    sql.com.CommandText = "SELECT COUNT(*) " +
                                  "FROM [PFMI_Signage].[dbo].[Vendors_Cost_Saved] " +
                                  "WHERE YEAR(Date)=" + Year + " AND Vendor = " + vId;
    sql.dr = sql.com.ExecuteReader();
    sql.dr.Read();
    if (sql.dr[0].ToString().Equals("1"))
    {
        command = " DELETE FROM [PFMI_Signage].[dbo].[Vendors_Cost_Saved] " +
                      " WHERE [Vendor] =  " + vId + " AND YEAR(Date) = " + Year + ";";

    }

    sql.dr.Close();

    if (!String.IsNullOrEmpty(command))
    {
        sql.Execute(command);
    }


    return Json("success");
}
#endregion

#region vendorList
public ActionResult VendorList()
{
    vcs = new VCSPage();
    return View(vcs);
}

[HttpPost]
public IActionResult VendorList(string name)
{
    sql = new SQL_Set_Up();
    string command = "INSERT INTO [PFMI_Signage].[dbo].[Vendors] VALUES ('" + name + "')";
    sql.Execute(command);
    vcs = new VCSPage();
    return View(vcs);
}

[HttpGet]
public ActionResult UpdateVendor(string id)
{
    sql = new SQL_Set_Up();
    sql.com.CommandText = "SELECT * FROM [PFMI_Signage].[dbo].[Vendors] WHERE Id = " + id;
    sql.dr = sql.com.ExecuteReader();
    sql.dr.Read();
    VendorCostSave v = new VendorCostSave(sql.dr["Id"].ToString(), sql.dr["Vendor_Name"].ToString());
    sql.dr.Close();
    return View(v);
}


[HttpPost]
public ActionResult UpdateVendor(string id, string name)
{
    sql = new SQL_Set_Up();
    string command = "UPDATE [PFMI_Signage].[dbo].[Vendors] " +
                     "SET  [Vendor_Name] ='" + name +
                        "' WHERE [Id] ='" + id + "';";
    sql.Execute(command);

    return RedirectToAction("VendorList");
}

[HttpGet]
public ActionResult DeleteVendor(string id)
{
    sql = new SQL_Set_Up();
    string command = "DELETE FROM [PFMI_Signage].[dbo].[Vendors] " +
                     "WHERE [Id] ='" + id + "';";
    sql.Execute(command);

    return RedirectToAction("VendorList");
}
        #endregion
    }
}
