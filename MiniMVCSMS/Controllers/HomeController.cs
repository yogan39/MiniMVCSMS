using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniMVCSMS.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connectionstr"].ConnectionString);
        OleDbConnection Econ;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {

            string strmessage = Request.Form["txtbox"];
            string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string filepath = "/excelfolder/" + filename;
            file.SaveAs(Path.Combine(Server.MapPath("/excelfolder"), filename));
            int inttest = InsertExceldata(filepath, filename, strmessage);
            if (inttest == 1)
            {
                ViewBag.result = "Succesfully Submitted" + " date: " + DateTime.Now.ToString("o");
            }
            else
            {
                ViewBag.result = "File not Uploded";
            }

            return View();

        }

        private void ExcelConn(string filepath)
        {
            string constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", filepath);
            Econ = new OleDbConnection(constr);

        }

        private int InsertExceldata(string fileepath, string filename, string strmessage)
        {
            string fullpath = Server.MapPath("/excelfolder/") + filename;
            ExcelConn(fullpath);
            string query = string.Format("Select * from [{0}]", "Sheet1$");
            OleDbCommand Ecom = new OleDbCommand(query, Econ);
            Econ.Open();

            DataSet ds = new DataSet();
            OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);
            Econ.Close();
            oda.Fill(ds);
            DataTable dtExcelData = ds.Tables[0];
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
            {
                if (dtExcelData.Rows.Count > 0)
                {
                    DataColumn newColumn = new DataColumn("SentDate", typeof(System.DateTime));
                    newColumn.DefaultValue = DateTime.Now;
                    dtExcelData.Columns.Add(newColumn);

                    DataColumn strcolumn = new DataColumn("MessageSent", typeof(string));
                    strcolumn.DefaultValue = strmessage;
                    dtExcelData.Columns.Add(strcolumn);
                }

                //Set the database table name
                sqlBulkCopy.DestinationTableName = "dbo.MinTable";

                sqlBulkCopy.ColumnMappings.Add("MobileNos", "MobileNos");
                sqlBulkCopy.ColumnMappings.Add("SentDate", "SentDate");
                sqlBulkCopy.ColumnMappings.Add("MessageSent", "MessageSent");
                sqlBulkCopy.ColumnMappings.Add("GroupName", "GroupName");

                con.Open();
                sqlBulkCopy.WriteToServer(dtExcelData);
                con.Close();
            }
            return 1;
        }
    }
}