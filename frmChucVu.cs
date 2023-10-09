using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class frmChucVu : Form
    {
        public frmChucVu()
        {
            InitializeComponent();
            SetControl("Reset");
            GetData();
        }
        #region Variable
        public const string connectionString = ConnectionString.connectionString;
        public static string State = "-1";
        #endregion
        #region public function
        public void SetControl(string State)
        {
            switch (State)
            {
                case "Reset":
                    btnThem.Enabled = true;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    btnGhiDuLieu.Enabled = false;
                    btnHuyBo.Enabled = false;
                    lblStatus.Text = "";
                    lblError.Text = "";
                    break;
            }
        }

        public void GetData()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string query = @"SELECT * FROM tblChucVu ORDER BY ID_ChucVu desc";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dtgMain.DataSource = ds.Tables[0];
                    lblTongSo.Text = "Tổng số: " + (dtgMain.Rows.Count - 1) + " bản ghi";
                }
                else
                {
                    dtgMain.DataSource = null;
                    lblTongSo.Text = "Tổng số: 0 bản ghi";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        #endregion

        private void dtgMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dtgMain.Rows[index];
            txtID.Text = selectedRow.Cells["ID_ChucVu"].Value.ToString();
            txtMaChucVu.Text = selectedRow.Cells["Ma_ChucVu"].Value.ToString();
            txtTenChucVu.Text = selectedRow.Cells["Ten_ChucVu"].Value.ToString();
            txtGhiChu.Text = selectedRow.Cells["Ghi_Chu"].Value.ToString();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnGhiDuLieu.Enabled = true;
            btnHuyBo.Enabled = true;

            txtMaChucVu.Text = "";
            txtTenChucVu.Text = "";
            txtGhiChu.Text = "";
            txtID.Text = "";
            txtID.Enabled = false;
            txtMaChucVu.Focus();
            State = "Insert";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnGhiDuLieu.Enabled = true;
            btnHuyBo.Enabled = true;            

            txtMaChucVu.Focus();
            State = "Update";
        }

        private void btnGhiDuLieu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaChucVu.Text == "")
                {
                    MessageBox.Show("Chưa nhập thông tin mã chức vụ", "Thông báo");
                    txtMaChucVu.Focus();
                    return;
                }
                if (txtTenChucVu.Text == "")
                {
                    MessageBox.Show("Chưa nhập thông tin tên chức vụ", "Thông báo");
                    txtTenChucVu.Focus();
                    return;
                }
                if (State == "Insert")
                {
                    SqlConnection conn = new SqlConnection(connectionString);
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    string query = @"INSERT INTO tblChucVu(Ma_ChucVu,Ten_ChucVu,Ghi_Chu) VALUES(N'"+txtMaChucVu.Text.Trim()+"',N'"+txtTenChucVu.Text.Trim()+"',N'"+txtGhiChu.Text.Trim()+"')";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Thêm dữ liệu thành công!", "Thông báo");
                        GetData();
                    }
                    else
                        MessageBox.Show("Lỗi dữ liệu", "Thông báo");
                }
                if (State == "Update")
                {
                    SqlConnection conn = new SqlConnection(connectionString);
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    string query = @"UPDATE tblChucVu SET Ma_ChucVu=N'" + txtMaChucVu.Text.Trim() + "',Ten_ChucVu=N'" + txtTenChucVu.Text.Trim() + "',Ghi_Chu =N'" + txtGhiChu.Text.Trim() + "' WHERE ID_ChucVu='"+txtID.Text.Trim()+"'";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo");
                        GetData();
                    }
                    else
                        MessageBox.Show("Lỗi dữ liệu", "Thông báo");
                }
            }
            catch(Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            SetControl("Reset");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string query = @"DELETE FROM tblChucVu WHERE ID_ChucVu='"+txtID.Text.Trim()+"'";

                SqlCommand cmd = new SqlCommand(query, conn);
                var result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Xoá dữ liệu thành công!", "Thông báo");
                    GetData();
                }
                else
                    MessageBox.Show("Lỗi xoá dữ liệu", "Thông báo");
            }
            catch(Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string query = @"SELECT * FROM tblChucVu";

                if (filterMaChucVu.Text.Trim() != null && filterTenChucVu.Text.Trim() == null)
                    query += " WHERE Ma_ChucVu LIKE N'%" + filterMaChucVu.Text.Trim() + "%'";
                if (filterMaChucVu.Text.Trim() == null && filterTenChucVu.Text.Trim() != null)
                    query += " WHERE Ma_ChucVu LIKE N'%" + filterTenChucVu.Text.Trim() + "%'";
                if (filterMaChucVu.Text.Trim() != null && filterTenChucVu.Text.Trim() != null)
                    query += " WHERE Ma_ChucVu LIKE N'%" + filterMaChucVu.Text.Trim() + "%' AND Ten_ChucVu LIKE N'%" + filterTenChucVu.Text.Trim() + "%'";
                query += " ORDER BY ID_ChucVu DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dtgMain.DataSource = ds.Tables[0];
                    lblTongSo.Text = "Tổng số: " + (dtgMain.Rows.Count - 1) + " bản ghi";
                }
                else
                {
                    dtgMain.DataSource = null;
                    lblTongSo.Text = "Tổng số: 0 bản ghi";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}
