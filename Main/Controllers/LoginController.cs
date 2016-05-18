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

        public bool UserVer(string UserName, string Password)
        {
            SQLHelper sqlHelper = new SQLHelper();
            System.Text.StringBuilder sbSql = new System.Text.StringBuilder();
            sbSql.Append("select count(*) from XM_User");
            sbSql.Append(" where UserName=@UserName and Password=@Password");
            
            int intResult=int.Parse(sqlHelper.ExecuteScalar(sbSql.ToString()));
            if (intResult == 1)
                return true;
            else
                return false;
        }
    }

   
}