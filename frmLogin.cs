using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class frmLogin : Form
    {
        #region Variable
        public const string connectionString = ConnectionString.connectionString;
        public static string LoaiTaiKhoan = "-1";
        #endregion
        public frmLogin()
        {
            InitializeComponent();
            lblError.Text = "";
            txtMatKhau.Text = "";
            txtMatKhau.PasswordChar = '*';
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //Kiểm tra xem đã nhập thông tin tên tài khoản hay chưa
                if (txtTenTKhoan.Text != null && txtTenTKhoan.Text.Trim() != "")
                {

                }
                else
                {
                    MessageBox.Show("Chưa nhập thông tin tên tài khoản", "Thông báo");
                    txtTenTKhoan.Focus();
                    return;
                }
                //Kiểm tra mật khẩu
                if (txtMatKhau.Text != null && txtMatKhau.Text.Trim() != "")
                {

                }
                else
                {
                    MessageBox.Show("Chưa nhập thông tin mật khẩu", "Thông báo");
                    txtMatKhau.Focus();
                    return;
                }

                SqlConnection conn = new SqlConnection(connectionString);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string TenTKhoan = txtTenTKhoan.Text.Trim();
                string MatKhau = txtMatKhau.Text.Trim();
                string query = @"SELECT * FROM tblTaiKhoan WHERE Ten_TKhoan= '" + TenTKhoan + "' AND Mat_Khau='" + MatKhau + "'";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    LoaiTaiKhoan = ds.Tables[0].Rows[0]["Loai_TKhoan"].ToString();
                    frmMain frmMain = new frmMain(LoaiTaiKhoan);
                    frmMain.Show();
                    this.Hide();
                }
                else
                    lblError.Text = "Thông tin tài khoản không chính xác";
            }
            catch(Exception ex)
            {
                lblError.Text = ex.Message;
            }            
            
        }
    }
}
