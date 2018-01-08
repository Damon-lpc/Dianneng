using ElectricityApplication.Models;
using KeyProject;
using System;
using System.Collections.Generic;

namespace ElectricityApplication.DAL
{
    public class DAL
    {
        public List<JCSJ> GetListFen(string name, DateTime  starttime, DateTime  endtime)
        {
            List<JCSJ> list = new List<JCSJ>();
            string sql = "SELECT * FROM [JCSJ].[dbo].[JCSJ] where tagname='" + name + "' and time between '" + starttime + "' and '" + endtime + "'";
            return list = DBHelper.ExecuteToList<JCSJ>(sql);
           
        }
        public List<JCSJ> GetListFen(string name)
        {
            List<JCSJ> list = new List<JCSJ>();
            string sql = "SELECT top 100 * FROM [JCSJ].[dbo].[JCSJ] where tagname='" + name + "' order by  [Time] desc  ";
            return list = DBHelper.ExecuteToList<JCSJ>(sql);

        }
        public List<JCSJ> GetListYear(string name, DateTime starttime, DateTime endtime)
        {
            List<JCSJ> list = new List<JCSJ>();
            string sql = "select year(TIME) as 'TimeYear',AVG(PV) as 'Avgpv' from [JCSJ].[dbo].[JCSJ] WHERE TagName='" + name + "'and time between '" + starttime + "' and '" + endtime + "' group by year(TIME) ORDER BY year(TIME)";
           return  list = DBHelper.ExecuteToList<JCSJ>(sql);
           
        }
        public List<JCSJ> GetListMonth(string name, DateTime starttime, DateTime endtime)
        {
            List<JCSJ> list = new List<JCSJ>();
            string sql = "select year(TIME) as 'TimeYear',month(TIME) as 'TimeMonth',AVG(PV) as 'Avgpv' from [JCSJ].[dbo].[JCSJ] WHERE TagName='" + name + "' and time between '" + starttime + "' and '" + endtime + "' group by year(TIME),month(TIME) ORDER BY year(TIME),month(TIME)";
           return list = DBHelper.ExecuteToList<JCSJ>(sql);
           
        }
        public List<JCSJ> GetListDay(string name, DateTime starttime, DateTime endtime)
        {
            List<JCSJ> list = new List<JCSJ>();
            string sql = "select year(TIME) as 'TimeYear',month(TIME) as 'TimeMonth',day(TIME) as 'TimeDay',AVG(PV) as 'Avgpv' from [JCSJ].[dbo].[JCSJ] WHERE TagName='" + name + "' and time between '" + starttime + "' and '" + endtime + "' group by year(TIME),month(TIME),day(TIME) ORDER BY year(TIME),month(TIME),day(TIME)";
           return list = DBHelper.ExecuteToList<JCSJ>(sql);
           
        }
        public List<JCSJ> GetListHour(string name, DateTime starttime, DateTime endtime)
        {
            List<JCSJ> list = new List<JCSJ>();
            string sql = "select year(TIME) as 'TimeYear',month(TIME) as 'TimeMonth',day(TIME) as 'TimeDay',DATEPART(HOUR,TIME) as 'TimeHour',AVG(PV) as 'Avgpv' from [JCSJ].[dbo].[JCSJ] WHERE TagName='" + name + "' and time between '" + starttime + "' and '" + endtime + "'group by year(TIME),month(TIME),day(TIME),DATEPART(HOUR,TIME) ORDER BY year(TIME),month(TIME),day(TIME),DATEPART(HOUR,TIME)";
           return list = DBHelper.ExecuteToList<JCSJ>(sql);
            
        }
        public List<JCSJ> GetListSelectQian()
        {
            List<JCSJ> list = new List<JCSJ>();
            string sql = "select distinct substring([TagName],0,8) as Tnumber from [Dldb].[dbo].[DB] order by Tnumber";
            return  list = DBHelper.ExecuteToList<JCSJ>(sql);
           
        }
        public List<JCSJ> GetListSelectHou()
        {
            List<JCSJ> list = new List<JCSJ>();
            string sql = "select distinct substring([TagName],charindex('\\',[TagName])+1,len([TagName])-charindex('\\',[TagName])) as hou from [Dldb].[dbo].[DB] order by hou";
            return list = DBHelper.ExecuteToList<JCSJ>(sql);
          
        }
    }
}