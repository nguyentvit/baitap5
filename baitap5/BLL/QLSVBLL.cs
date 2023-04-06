using baitap5.DAL;
using baitap5.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baitap5.BLL
{
    internal class QLSVBLL
    {
        public List<CBBItem> GetCBB()
        {
            List<CBBItem> li = new List<CBBItem>();
            li.Add(new CBBItem
            {
                Value = 0,
                Text = "All"
            });
            QLSVDAL q = new QLSVDAL();
            foreach (LopSH i in q.GetAllLopSHDAL())
            {
                li.Add(new CBBItem
                {
                    Value = i.Id_Lop,
                    Text = i.Name
                });
            }
            return li;
        }
        public List<CBBItem> SetCBBForAddForm()
        {
            List<CBBItem> li = new List<CBBItem>();
            QLSVDAL q = new QLSVDAL();
            foreach (LopSH i in q.GetAllLopSHDAL())
            {
                li.Add(new CBBItem
                {
                    Value = i.Id_Lop,
                    Text = i.Name
                });
            }
            return li;

        }
        public List<SV> GetSVByIdLop(int Id_Lop)
        {
            List<SV> li = new List<SV>();
            QLSVDAL q = new QLSVDAL();
            if (Id_Lop == 0)
            {
                li = q.GetAllSVDAL();
            }
            else
            {

                foreach (SV i in q.GetAllSVDAL())
                {
                    if (i.Id_Lop == Id_Lop)
                    {
                        li.Add(i);
                    }
                }
            }
            return li;
        }
        public List<DataView> GetDataViewByIdLop(int Id_Lop)
        {
            List<DataView> li = new List<DataView>();
            QLSVDAL q = new QLSVDAL();
            if (Id_Lop == 0)
            {
                foreach (SV i in q.GetAllSVDAL())
                {
                    string nameLop = "";
                    foreach (LopSH j in q.GetAllLopSHDAL())
                    {
                        if (i.Id_Lop == j.Id_Lop) nameLop = j.Name;
                    }
                    li.Add(new DataView
                    {
                        MSSV = i.MSSV,
                        NameSV = i.NameSV,
                        Gender = i.Gender,
                        DTB = i.DTB,
                        Name = nameLop
                    });

                }
            }
            else
            {
                foreach (SV i in q.GetAllSVDAL())
                {
                    if (i.Id_Lop == Id_Lop)
                    {
                        string nameLop = "";
                        foreach (LopSH j in q.GetAllLopSHDAL())
                        {
                            if (i.Id_Lop == j.Id_Lop) nameLop = j.Name;
                        }
                        li.Add(new DataView
                        {
                            MSSV = i.MSSV,
                            NameSV = i.NameSV,
                            Gender = i.Gender,
                            DTB = i.DTB,
                            Name = nameLop
                        });
                    }
                }
            }


            return li;
        }
        public List<DataView> GetDataViewsByTxtSearch(string m)
        {
            List<DataView> li = new List<DataView>();
            QLSVDAL q = new QLSVDAL();
            foreach (SV i in q.GetAllSVDAL())
            {
                if (i.NameSV.Contains(m))
                {
                    string nameLop = "";
                    foreach (LopSH j in q.GetAllLopSHDAL())
                    {
                        if (i.Id_Lop == j.Id_Lop) nameLop = j.Name;
                    }
                    li.Add(new DataView
                    {
                        MSSV = i.MSSV,
                        NameSV = i.NameSV,
                        Gender = i.Gender,
                        DTB = i.DTB,
                        Name = nameLop
                    });
                }
            }
            return li;
        }
        public List<DataView> GetDataViewsByTxtSearchAndIdLop(int Id_Lop, string txtSearch)
        {
            List<DataView> li = new List<DataView>();
            List<DataView> liIdLop = GetDataViewByIdLop(Id_Lop);
            List<DataView> liTxtSearch = GetDataViewsByTxtSearch(txtSearch);
            foreach (DataView dataLop in liIdLop)
            {
                foreach (DataView dataSearch in liTxtSearch)
                {
                    if (dataLop.MSSV == dataSearch.MSSV) li.Add(dataLop);
                }
            }
            return li;
        }
        public List<DataView> SortData(string cbbSort)
        {
            List<DataView> li = new List<DataView>();
            li = GetDataViewByIdLop(0);
            List<DataView> lisort = new List<DataView>();
            if (cbbSort == "DTB")
            {
                lisort = li.OrderBy(p => p.DTB).ToList();
            }
            else
            {
                lisort = li.OrderBy(p => p.MSSV).ToList();
            }

            return lisort;
        }
        public void DelSV(int m)
        {
            QLSVDAL q = new QLSVDAL();
            DialogResult result = MessageBox.Show("Bạn có chắc xóa sinh viên này không", "Thông báo", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                q.DelSVDAL(m);
            }
        }
        public bool checkId(int m)
        {
            bool status = false;
            QLSVDAL q = new QLSVDAL();
            foreach (SV i in q.GetAllSVDAL())
            {
                if (i.MSSV == m)
                {
                    status = true;
                    break;
                }
                
            }
            return status;

        }
        public void AddSV(SV s)
        {
            QLSVDAL q = new QLSVDAL();
            q.AddSVDAL(s);
        }
        public void UpdateSV(SV s)
        {
            QLSVDAL q = new QLSVDAL();
            q.Update(s);
        }
    }
}
