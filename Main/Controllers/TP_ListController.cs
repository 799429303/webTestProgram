using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.ViewModels;
using System.Text;
using System.Data;
using System.Reflection;
using DAL;
namespace Main.Controllers
{
    public class TP_ListController : Controller
    {
       
        // GET: TP_List
        public ActionResult Index()
        {            
            return View("TP_List");
        }

        public void GetAllTP()
        {
            List<TP_ListViewModel> lstTP = new List<TP_ListViewModel>();
            lstTP = GetAllTP(lstTP);
            if (lstTP.Count == 0)
                return;
            string strJason = ObjectToJson<TP_ListViewModel>("", lstTP);
            Response.Write(strJason);
            return;
        }

        private List<TP_ListViewModel> GetAllTP(List<TP_ListViewModel> lstTP)
        {
            SQLHelper sqlhellp = new SQLHelper();
            StringBuilder sbSql = new StringBuilder();
            sbSql.Remove(0, sbSql.Length);
            sbSql.Append("select * from XM_TP");
            sbSql.Append(" where apply_user=@apply_user");
            System.Data.SqlClient.SqlParameter[] paras=new System.Data.SqlClient.SqlParameter[]{
                new System.Data.SqlClient.SqlParameter("apply_user",HttpContext.Session["user"])
            };
            DataTable dt = sqlhellp.ExecuteQuery(sbSql.ToString(), paras, CommandType.Text);
            foreach (DataRow dr in dt.Rows)
            {
                TP_ListViewModel TP = new TP_ListViewModel();
                TP.TP_NO = dr["TP_NO"].ToString();
                TP.TP_Name = dr["TP_Name"].ToString();
                TP.State =(TP_ListViewModel.TP_State)(int.Parse(dr["State"].ToString()));
                TP.ApplyDate = DateTime.Parse(dr["ApplyDate"].ToString());
                TP.ApplyUser = dr["Apply_User"].ToString();
                TP.ApproveUser = "";
                if (TP.State ==TP_ListViewModel.TP_State.Create)
                {
                    TP.IsLocked = false;
                }
                else
                {
                    TP.IsLocked=true;
                }
                lstTP.Add(TP);
            }
            
            return lstTP;
        }

        #region ToJson泛型集合转json
        /// <summary>
        /// 泛型集合转json
        /// </summary>
        /// <typeparam name="T">< peparam>
        /// <param name="jsonName"></param>
        /// <param name="IL"></param>
        /// <returns></returns>
        public static string ObjectToJson<T>(string jsonName, IList<T> IL)
        {
            StringBuilder Json = new StringBuilder();
            if (jsonName.Equals(""))
            {
                Json.Append("[");
            }
            else
            {
                Json.Append("{\"" + jsonName + "\":[");
            }
            if (IL.Count > 0)
            {
                for (int i = 0; i < IL.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    Type type = obj.GetType();
                    PropertyInfo[] pis = type.GetProperties();
                    Json.Append("{");
                    for (int j = 0; j < pis.Length; j++)
                    {
                        Json.Append("\"" + pis[j].Name.ToString() + "\":\"" + pis[j].GetValue(IL[i], null).ToString() + "\"");
                        if (j < pis.Length - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < IL.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]");
            //Json.Append("]}");
            return Json.ToString();
        }

        #endregion
            }
    }
