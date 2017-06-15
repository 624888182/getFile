using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Models;
using Test.Utils;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private OracleHelper helper = new OracleHelper("conn");

        public ActionResult Index()
        {
            return View();
        }

        public string GetPersons(int yema,int yeshu) 
        {
            int startNum = (yema - 1) * yeshu;
            int endNum = yema * yeshu;
            string cmdText = string.Format(@"SELECT * FROM(
                SELECT ROWNUM RN,a.*,(SELECT COUNT(*) FROM C7106533.PERSON) TOTAL FROM C7106533.PERSON a) 
                WHERE RN>{0} AND RN <={1}", startNum, endNum);
            DataSet ds = helper.GetDataSet(cmdText);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }

        public string AddPerson(Person p)
        {
            string cmdText = string.Format(@"INSERT INTO C7106533.PERSON(ID,NAME,AGE)VALUES('{0}','{1}','{2}')",p.id,p.name,p.age);
            helper.ExecuteNonQuery(cmdText);
            return "ok";
        }

    }
}
