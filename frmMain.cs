using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class frmMain : Form
    {
        public frmMain(string LoaiTKhoan)
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            if(LoaiTKhoan=="0")
            {
                //Admin
                mnuDanhMuc.Enabled = true;
                mnuHoSo.Enabled = true;
                mnuHeThong.Enabled = true;
            }
            else
            {
                mnuDanhMuc.Enabled = false;
                mnuHoSo.Enabled = true;
                mnuHeThong.Enabled = false;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }        
        private void mnuChucVu_Click(object sender, EventArgs e)
        {
            frmChucVu _frmChucVu = new frmChucVu();
            _frmChucVu.MdiParent = this;
            _frmChucVu.Show();
        }
    }
}
