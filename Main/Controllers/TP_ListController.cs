using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.ViewModels;
using System.Text;
using SystemDAO;
using System.Data;
namespace Main.Controllers
{
    public class TP_ListController : Controller
    {
       
        // GET: TP_List
        public ActionResult Index()
        { 
            List<TP_ListViewModel> lstTP = new List<TP_ListViewModel>();
            lstTP = GetAllTP(lstTP);
            return View("TP_List");
        }

        private List<TP_ListViewModel> GetAllTP(List<TP_ListViewModel> lstTP)
        {
           
            StringBuilder sbSql = new StringBuilder();
            sbSql.Remove(0, sbSql.Length-1);
            sbSql.Append("select * from XM_TP");          
            DataSet dt=SqlHelper.ExecuteDataSetText(sbSql.ToString(), null);
            foreach (DataRow dr in dt.Tables[0].Rows)
            {
                TP_ListViewModel TP = new TP_ListViewModel();
                TP.TP_NO = dr["TP_NO"].ToString();
                TP.TP_Name = dr["TP_Name"].ToString();
                TP.TP_Type = int.Parse(dr["TP_Type"].ToString());
                lstTP.Add(TP);
            }
            return lstTP;
        }
        
    }
}