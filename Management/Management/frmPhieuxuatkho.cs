using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Management.Class;

using Microsoft.Office.Interop.Excel;
using app = Microsoft.Office.Interop.Excel.Application;


namespace Management
{
    public partial class frmPhieuxuatkho : Form
    {
        System.Data.DataTable Phieuxuatkho;
        public frmPhieuxuatkho()
        {
            InitializeComponent();
        }

        private void frmPhieuxuatkho_Load(object sender, EventArgs e)
        {
            
                
                btn_Luu.Enabled = false;
               
                btn_In.Enabled = false;
                txt_Maphieuxuat.Enabled = false;
                
               
                
                
               
                
                //Hiển thị thông tin của một hóa đơn được gọi từ form tìm kiếm
                
                LoadDataGridView();
            }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT maPhieuxuat,maID,maDonhang,giaca,soluong FROM  Phieuxuatkho ";
            Phieuxuatkho = Function.GetDataToTable(sql);
            dgvPhieuxuat.DataSource = Phieuxuatkho;
            dgvPhieuxuat.Columns[0].HeaderText = "Mã phiếu xuất";
            dgvPhieuxuat.Columns[1].HeaderText = "Mã nhân viên";
            dgvPhieuxuat.Columns[2].HeaderText = "Mã đơn hàng";
            dgvPhieuxuat.Columns[3].HeaderText = "Số lượng";
            dgvPhieuxuat.Columns[4].HeaderText = "Gía cả";
           
            dgvPhieuxuat.Columns[0].Width = 100;
            dgvPhieuxuat.Columns[1].Width = 100;
            dgvPhieuxuat.Columns[2].Width = 100;
            dgvPhieuxuat.Columns[3].Width = 100;
            dgvPhieuxuat.Columns[4].Width = 100;
            
            dgvPhieuxuat.AllowUserToAddRows = false;
            dgvPhieuxuat.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
      
        private void mnuIn_Click(object sender, EventArgs e)
        {
            frmHoaDon frm = new frmHoaDon();

            frm.ShowDialog();

        }

        private void btn_In_Click(object sender, EventArgs e)
        {
            
        }

   

        private void btn_Tao_Click(object sender, EventArgs e)
        {
            btn_Xoa.Enabled = false;
            btn_Luu.Enabled = true;
            btn_In.Enabled = false;
            btn_Tao.Enabled = false;
            txt_Maphieuxuat.Enabled = true;
            txt_Maphieuxuat.Focus();
            ResetValues();
            
           
        }
        private void ResetValues()
        {
            dt_Ngayxuat.Value = DateTime.Now;
            txt_Maphieuxuat.Text = "";
            txt_Manhanvien.Text = "";
            txt_Madonhang.Text = "";
            txt_soluong.Text = "";
            txt_Giaca.Text = "0";

        }
        private void btn_Luu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txt_Maphieuxuat.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã phiếu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_Maphieuxuat.Focus();
                return;
            }

            if (txt_Manhanvien.Text.Trim().Length == 0) //Nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn phải nhập mã kế toán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_Manhanvien.Focus();
                return;
            }
            if (txt_Madonhang.Text.Trim().Length == 0) //Nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn phải nhập mã đơn hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_Madonhang.Focus();
                return;
            }
            if (txt_soluong.Text.Trim().Length == 0) //Nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn phải nhập mã đơn hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_soluong.Focus();
                return;
            }
            if (txt_Giaca.Text.Trim().Length == 0) //Nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn phải nhập mã đơn hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_Giaca.Focus();
                return;
            }
            sql = "Select maPhieuxuat From Phieuxuatkho where maPhieuxuat=N'" + txt_Maphieuxuat.Text.Trim() + "'";
            if (Class.Function.CheckKey(sql))
            {
                MessageBox.Show("Mã phiếu này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_Maphieuxuat.Focus();
                return;
            }

            sql = "INSERT INTO Phieuxuatkho(maPhieuxuat,giaca,soluong,maDonhang,maID) VALUES(N'" + txt_Maphieuxuat.Text + "',N'" + float.Parse(txt_Giaca.Text) + "',N'" + int.Parse(txt_soluong.Text) + "',N'" + txt_Madonhang.Text + "',N'" + txt_Manhanvien.Text + "')";

            Function.RunSQL(sql);


            LoadDataGridView();
            ResetValues();
            btn_Xoa.Enabled = true;
            btn_Tao.Enabled = true;
            btn_Sua.Enabled = true;

            btn_Luu.Enabled = false;
            txt_Maphieuxuat.Enabled = false;
        }
        private void btn_Sua_Click(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            if (Phieuxuatkho.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txt_Maphieuxuat.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn mã phiếu xuất nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txt_Manhanvien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txt_Madonhang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập mã đơn hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txt_soluong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txt_Giaca.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập giá cả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE Phieuxuatkho SET maDonhang=N'" +
                txt_Madonhang.Text +
                "' WHERE maPhieuxuat=N'" + txt_Maphieuxuat.Text + "'";
            Function.RunSQL(sql);
            LoadDataGridView();
            ResetValues();

        }

        

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void dgvPhieuxuat_Click(object sender, EventArgs e)
        {
            if (btn_Tao.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_Maphieuxuat.Focus();
                return;
            }
            if (Phieuxuatkho.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txt_Maphieuxuat.Text = dgvPhieuxuat.CurrentRow.Cells["maPhieuxuat"].Value.ToString();

            txt_Manhanvien.Text = dgvPhieuxuat.CurrentRow.Cells["maID"].Value.ToString();
            txt_Madonhang.Text = dgvPhieuxuat.CurrentRow.Cells["maDonhang"].Value.ToString();
            txt_soluong.Text = dgvPhieuxuat.CurrentRow.Cells["soluong"].Value.ToString();
            txt_Giaca.Text = dgvPhieuxuat.CurrentRow.Cells["giaca"].Value.ToString();
            btn_Sua.Enabled = true;
            btn_Xoa.Enabled = true;

        }

        private void btn_Xoa_Click_1(object sender, EventArgs e)
        {

            string sql;
            if (Phieuxuatkho.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txt_Maphieuxuat.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE Phieuxuatkho WHERE maPhieuxuat=N'" + txt_Maphieuxuat.Text + "'";
                Class.Function.RunSQL(sql);
                LoadDataGridView();
                ResetValues();
            }
        }
        //private void export2Excel(DataGridView g,string duongDan,string TenTap)
        //{
        //    app obj = new app();
        //    obj.Application.Workbooks.Add(Type.Missing);
        //    obj.Columns.ColumnWidth = 25;

        //}
    }
}
