using ElectricityApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectricityApplication.Controllers
{
    public class aaa
    {
        public List<JCSJ> jcsjlist { get; set; }
        public  string  pvs { get; set; }
        public string  Times { get; set; }
        public string pvtt { get; set; }
        public string timett { get; set; }
        
    }
    
    public class CollationMapController : Controller
    {
        
        BLL.BLLManger bll = new BLL.BLLManger();
        double[] pv;
        DateTime[] time;
        List<JCSJ> list = new List<JCSJ>();
        // GET: CollationMap
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Comparizon()
        {
            return View();
        }
        /// <summary>
        /// 第一个下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectQian()
        {
            List<JCSJ> list = new List<JCSJ>();
           list=  bll.GetListSelectQian();
            string[] s = new string[list.Count];
            for(var i=0;i<list.Count;i++)
            {
                s[i] = list[i].Tnumber;
            }
           // string ss= JsonConvert.SerializeObject(s).Replace("\\"," ");
            return Json(s, JsonRequestBehavior.AllowGet);
           
        }
        /// <summary>
        /// 第二个下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectHou()
        {
            List<JCSJ> list = new List<JCSJ>();
            list = bll.GetListSelectHou();
            string[] s = new string[list.Count];
            for (var i = 0; i < list.Count; i++)
            {
                s[i] = list[i].hou;
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 方法
        /// </summary>
        /// <returns></returns>
        public ActionResult MainAction()
        {  
            DateTime starttime = Convert.ToDateTime(Request.Form["starttime"]);
            DateTime endtime = Convert.ToDateTime(Request.Form["endtime"]);
            string  name=Request.Form["name"];
            int timeindex = Convert.ToInt32( Request.Form["timeindex"]);
            if (name==null||name=="")
            {
                name = "t101003\\iathd12";
            }
            if(starttime== null)
            { 
                starttime = DateTime.Now;
            }
            if(endtime==null)
            {
                endtime = DateTime.Now.AddDays(1);
            }
            if (timeindex==0)
            {
                list = bll.GetListFen(name, starttime, endtime);
                 pv = new double[list.Count];
                 time = new DateTime[list.Count];
              
                for (int i = 0; i < list.Count; i++)
                {
                    pv[i] = Math.Round(list[i].PV,2);
                   
                    time[i] = list[i].Time;
                }
            }
           else if(timeindex==1)
            {
                list = bll.GetListYear(name, starttime, endtime);
                pv = new double[list.Count];
                time = new DateTime[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    pv[i] = Math.Round(list[i].Avgpv, 2);
                    time[i] = Convert.ToDateTime( list[i].TimeYear);
                }
            }
           else if(timeindex==2)
            {
                
                list = bll.GetListMonth(name, starttime, endtime);
                pv = new double[list.Count];
                time = new DateTime[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    pv[i] = Math.Round(list[i].Avgpv, 2);
                    time[i] = Convert.ToDateTime(list[i].TimeYear+"-"+ list[i].TimeMonth);
                }
            }
            else if (timeindex == 3)
            {
                 list = bll.GetListDay(name, starttime, endtime);
                pv = new double[list.Count];
                time = new DateTime[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    pv[i] = Math.Round(list[i].Avgpv, 2);
                    time[i] = Convert.ToDateTime(list[i].TimeYear + "-" + list[i].TimeMonth+"-"+ list[i].TimeDay);
                }
            }
            else if (timeindex == 4)
            {            
                list= bll.GetListHour(name, starttime, endtime);
                pv = new double[list.Count];
                time = new DateTime[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    pv[i] = Math.Round(list[i].Avgpv, 2);
                    time[i] = Convert.ToDateTime(list[i].TimeYear + "-" + list[i].TimeMonth + "-" + list[i].TimeDay+"-"+ list[i].TimeHour);
                }
                
            }
            string pva = JsonConvert.SerializeObject(pv).Replace("\\"," ");
            string timea = JsonConvert.SerializeObject(time).Replace("T"," ").Replace("+08:00"," ").Replace("00:00:00"," ");
            List<aaa> aa = new List<Controllers.aaa>();
            aaa aaa = new aaa();
            aaa.pvs = pva;
            aaa.Times = timea;
            aa.Add(aaa);
            return Json(aa, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutoUpdateIndex()
        {
            return View();
        }
        public ActionResult AutoUpdateAction()
        {
            List<JCSJ> list = new List<JCSJ>();
            string name = Request.Form["name"];
            list = bll.GetListFen(name);
            pv = new double[list.Count];
            time = new DateTime[list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                pv[i] = Math.Round(list[i].PV, 2);

                time[i] = list[i].Time;
            }
            string pva = JsonConvert.SerializeObject(pv).Replace("\\", " ");
            string timea = JsonConvert.SerializeObject(time).Replace("T", " ").Replace("+08:00", " ").Replace("00:00:00", " ");
            List<aaa> aa = new List<Controllers.aaa>();
            aaa aaa = new aaa();
            aaa.pvtt = pva;
            aaa.timett = timea;
            aa.Add(aaa);
            return Json(aa, JsonRequestBehavior.AllowGet);
        }

    }
}