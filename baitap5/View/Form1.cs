using baitap5.BLL;
using baitap5.DTO;
using baitap5.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataView = System.Data.DataView;
using baitap5.View;

namespace baitap5
{
    public delegate void Loader();
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetCBBLopSH();
            SetCBBSort();
            LoadForm();
            
        }
        public void SetCBBLopSH()
        {
            QLSVBLL b = new QLSVBLL();
            cbbLopSH.Items.AddRange(b.GetCBB().ToArray());
            cbbLopSH.SelectedIndex = 0;
        }
        public void SetCBBSort()
        {
            cbbSort.Items.Add("MSSV");
            cbbSort.Items.Add("DTB");
            cbbSort.SelectedIndex = 0;
        }
        public void LoadForm()
        {
            QLSVBLL b = new QLSVBLL();
            dataGridView1.DataSource = b.GetDataViewByIdLop(0);
        }
        

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int idLop = ((CBBItem)(cbbLopSH.SelectedItem)).Value;
            QLSVBLL b = new QLSVBLL();
            dataGridView1.DataSource = b.GetDataViewsByTxtSearchAndIdLop(idLop, txtSearch.Text);
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QLSVBLL b = new QLSVBLL();
            dataGridView1.DataSource = b.GetDataViewsByTxtSearch(txtSearch.Text);
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            QLSVBLL b = new QLSVBLL();
            dataGridView1.DataSource = b.SortData(cbbSort.Text);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            QLSVBLL b = new QLSVBLL();
            b.DelSV(id);
            dataGridView1.DataSource = b.GetDataViewByIdLop(0);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm(LoadForm, null);
            addForm.ShowDialog();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = (dataGridView1.CurrentRow.Cells[0].Value).ToString();
            AddForm addForm = new AddForm(LoadForm, id);
            addForm.ShowDialog();
        }
    }
}
