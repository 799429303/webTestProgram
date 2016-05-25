using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
namespace Main.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View("Login");
        }
        [HttpPost]
        public void UserVer( )
        {
            SQLHelper sqlHelper = new SQLHelper();
            System.Text.StringBuilder sbSql = new System.Text.StringBuilder();
            sbSql.Append("select count(*) from XM_User");
            sbSql.Append(" where user_id=@UserName and password=@Password");
            string strUserName = Request.Params["UserName"];
            string strPassword =SQLHelper.Md5Hash(Request.Params["Password"]);
            System.Data.SqlClient.SqlParameter[] paras = new System.Data.SqlClient.SqlParameter[]
            {
             new System.Data.SqlClient.SqlParameter("UserName", strUserName),
             new System.Data.SqlClient.SqlParameter("Password", strPassword)
            };
            int intResult = int.Parse(sqlHelper.ExecuteScalar(sbSql.ToString(), paras));
            if (intResult == 1)
            {
                HttpContext.Session.Add("user", strUserName);
                Response.Write("1");
            }
            else
                Response.Write("0");
            
        }
    }

   
}