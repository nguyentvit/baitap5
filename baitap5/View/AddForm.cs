using baitap5.BLL;
using baitap5.DAL;
using baitap5.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataView = baitap5.DTO.DataView;

namespace baitap5.View
{
    public partial class AddForm : Form
    {
        public Loader loader;
        private string mssv;
        public AddForm()
        {
            InitializeComponent();
        }
        public AddForm(Loader loader1,string m)
        {
            InitializeComponent();
            SetCBBLopSH();
            mssv = m;
            if(mssv != null)
            {
                SetAddFormWhenUpdate();
            }

            this.loader = loader1;
        }
        private void SetAddFormWhenUpdate()
        {
            QLSVBLL q = new QLSVBLL();

            foreach (DataView i in q.GetDataViewByIdLop(0))
            {
                if (i.MSSV.ToString() == mssv)
                {
                    txtMSSV.Text = mssv;
                    txtMSSV.ReadOnly = true;
                    txtNameSV.Text = i.NameSV;
                    txtDTB.Text = i.DTB.ToString();
                    if (i.Gender) rdbtnMale.Checked = true;
                    else rdbtnFeMale.Checked = true;
                    cbbLopSH.Text = i.Name;


                }
            }
        }
        private void SetCBBLopSH()
        {
            QLSVBLL b = new QLSVBLL();
            cbbLopSH.Items.AddRange(b.SetCBBForAddForm().ToArray());
            
        }
        private void AddForm_Load(object sender, EventArgs e)
        {

        }
        private bool CheckNull()
        {
            bool status = true;
            errorProvider1.SetError(txtMSSV, "");
            errorProvider1.SetError(txtDTB, "");
            errorProvider1.SetError(txtNameSV, "");
            errorProvider1.SetError(cbbLopSH, "");
            

            
            
            if (txtMSSV.Text == "")
            {
                errorProvider1.SetError(txtMSSV, "Vui lòng nhập MSSV");
                status = false;
            }
            if (txtDTB.Text == "")
            {
                errorProvider1.SetError(txtDTB, "Vui lòng nhập DTB");
                status = false;
            }

            if (txtNameSV.Text == "")
            {
                errorProvider1.SetError(txtNameSV, "Vui lòng nhập Tên SV");
                status = false;
            }
            if (cbbLopSH.SelectedItem == null)
            {
                errorProvider1.SetError(cbbLopSH, "Vui lòng chọn lớp SH");
                status = false;
            }
            if(rdbtnMale.Checked == false && rdbtnFeMale.Checked == false)
            {
                MessageBox.Show("Vui Lòng chọn giới tính");
                status = false;
            }
            return status;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            
            if (CheckNull())
            {
                QLSVBLL b = new QLSVBLL();
                if(mssv == null)
                {
                    if(b.checkId(Convert.ToInt32(txtMSSV.Text)))
                    {
                        MessageBox.Show("MSSV đã trùng!!!");
                    }
                    else
                    {
                        SV s = new SV();
                        s.MSSV = Convert.ToInt32(txtMSSV.Text);
                        s.NameSV = txtNameSV.Text;
                        s.Gender = (rdbtnMale.Checked) ? true : false;
                        s.DTB = Convert.ToDouble(txtDTB.Text);
                        s.Id_Lop = ((CBBItem)(cbbLopSH.SelectedItem)).Value;
                        b.AddSV(s);
                        this.loader();
                        this.Close();

                    }
                }
                else
                {
                    SV s = new SV();
                    s.MSSV = Convert.ToInt32(txtMSSV.Text);
                    s.NameSV = txtNameSV.Text;
                    s.Gender = (rdbtnMale.Checked) ? true : false;
                    s.DTB = Convert.ToDouble(txtDTB.Text);
                    s.Id_Lop = ((CBBItem)(cbbLopSH.SelectedItem)).Value;
                    b.UpdateSV(s);
                    this.loader();
                    this.Close();

                }
            }

        }
    }
}
