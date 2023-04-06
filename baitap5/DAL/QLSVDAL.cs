using baitap5.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baitap5.DAL
{
    internal class QLSVDAL
    {
        public List<SV> GetAllSVDAL()
        {
            List<SV> li = new List<SV>();
            DataTable dt = DBHelper.Instance.GetRecords("select * from SV");
            foreach(DataRow dr in dt.Rows)
            {
                SV sv = new SV();
                sv.MSSV = Convert.ToInt32(dr["MSSV"].ToString());
                sv.NameSV = dr["NameSV"].ToString();
                sv.Gender = Convert.ToBoolean(dr["Gender"].ToString());
                sv.DTB = Convert.ToDouble(dr["DTB"].ToString());
                sv.Id_Lop = Convert.ToInt32(dr["Id_Lop"].ToString());
                li.Add(sv);
            }
            return li;
        }
        public List<LopSH> GetAllLopSHDAL()
        {
            List<LopSH> li = new List<LopSH>();
            
            DataTable dt = DBHelper.Instance.GetRecords("select * from LopSH");

            foreach(DataRow dr in dt.Rows)
            {
                LopSH lopsh = new LopSH();
                lopsh.Id_Lop = Convert.ToInt32(dr["Id_Lop"].ToString());
                lopsh.Name = dr["Name"].ToString();
                li.Add(lopsh);
            }
            return li;
        }
        public void AddSVDAL(SV s)
        {
            string gender = "";
            if (s.Gender == true) gender = "1";
            else gender = "0";
            string query = "insert into SV values ("+s.MSSV+",N'"+s.NameSV+"',"+gender+","+s.DTB+","+s.Id_Lop+")";
            DBHelper.Instance.Execute(query);
        }
        public void DelSVDAL(int m)
        {
            string query = "delete from SV where MSSV = '"+m+"'";
            DBHelper.Instance.Execute(query);
        }
        public void Update(SV s)
        {
            string gender = "";
            if (s.Gender == true) gender = "1";
            else gender = "0";
            string query = "update SV set NameSV = N'"+s.NameSV+"',Gender = "+gender+",DTB="+s.DTB+",Id_Lop="+s.Id_Lop+"where MSSV = "+s.MSSV;
            DBHelper.Instance.Execute(query);
        }
    }
}
