using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTH4
{
    public partial class frmThemSinhVien : Form
    {
        // Biến công khai để Form cha có thể lấy dữ liệu sau khi Form này đóng
        public SinhVien StudentData { get; private set; }
        public frmThemSinhVien()
        {
            InitializeComponent();
            cboKhoa.SelectedIndex = 0; // Chọn mặc định item đầu tiên
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra dữ liệu nhập
                if (string.IsNullOrWhiteSpace(txtMaSo.Text) || string.IsNullOrWhiteSpace(txtTen.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                    return;
                }

                if (!float.TryParse(txtDiem.Text, out float diem) || diem < 0 || diem > 10)
                {
                    MessageBox.Show("Điểm trung bình phải là số từ 0 đến 10!");
                    return;
                }

                // 2. Tạo đối tượng sinh viên mới
                StudentData = new SinhVien(
                    txtMaSo.Text,
                    txtTen.Text,
                    cboKhoa.Text,
                    diem
                );

                // 3. Đóng form và trả về kết quả OK
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
