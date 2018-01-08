using ElectricityApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectricityApplication.BLL
{
    public class BLLManger
    {
        DAL.DAL dd = new DAL.DAL();
        public  List<JCSJ> GetListFen(string name, DateTime  starttime, DateTime  endtime)
        {
            return dd.GetListFen(name, starttime, endtime);
        }
        public List<JCSJ> GetListFen(string name)
        {
            return dd.GetListFen(name);
        }
        public List<JCSJ> GetListYear(string name, DateTime starttime, DateTime endtime)
        {
            return dd.GetListYear(name, starttime, endtime);
        }
        public List<JCSJ> GetListMonth(string name, DateTime starttime, DateTime endtime)
        {
            return dd.GetListMonth(name, starttime, endtime);
        }

        public List<JCSJ> GetListDay(string name, DateTime starttime, DateTime endtime)
        {
            return dd.GetListDay(name, starttime, endtime);
        }
        public List<JCSJ> GetListHour(string name, DateTime starttime, DateTime endtime)
        {
            return dd.GetListHour(name, starttime, endtime);
        }
        public List<JCSJ> GetListSelectQian()
        {
           return  dd.GetListSelectQian();
        }
        public List<JCSJ> GetListSelectHou()
        {
            return dd.GetListSelectHou();
        }
        }
}