using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using System.Web.Security;

namespace Main.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View("register");
        }

        public void UserCheck()
        {
            SQLHelper sqlhelp=new SQLHelper();
            string strUserName = Request.Params["UserName"];
            System.Text.StringBuilder sbSQL = new System.Text.StringBuilder();
            sbSQL.Append("select count(*) from xm_user");
            sbSQL.Append(" where user_name=@username");
            System.Data.SqlClient.SqlParameter[] paras = new System.Data.SqlClient.SqlParameter[]{
              new System.Data.SqlClient.SqlParameter("username",strUserName)
            };
            string strResult = sqlhelp.ExecuteScalar(sbSQL.ToString(), paras);
            if (strResult == "1")
            {
                Response.Write("1");
            }
            else
            {
                Response.Write("0");
            }
        }
        [HttpPost]
        public void AddUser()
        {
            SQLHelper sqlhelp = new SQLHelper();
            string strUserName = Request.Params["username"];
            string strPassword = SQLHelper.Md5Hash(Request.Params["password"]);
            string strEmail = Request.Params["email"];
            System.Text.StringBuilder sbSQL = new System.Text.StringBuilder();
            sbSQL.Append("insert into xm_user(user_id,user_name,password,email)");
            sbSQL.Append("values(@username,@username,@password,@email)");
            System.Data.SqlClient.SqlParameter[] paras = new System.Data.SqlClient.SqlParameter[]{
              new System.Data.SqlClient.SqlParameter("username",strUserName),
              new System.Data.SqlClient.SqlParameter("password",strPassword),
              new System.Data.SqlClient.SqlParameter("email",strEmail)
            };
            sqlhelp.ExecuteQuery(sbSQL.ToString(), paras, System.Data.CommandType.Text);          
        }
    }
}