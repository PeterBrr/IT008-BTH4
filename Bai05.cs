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
    public partial class Bai05 : Form
    {
        // List lưu trữ dữ liệu sinh viên trong bộ nhớ
        private List<SinhVien> danhSachSinhVien = new List<SinhVien>();
        public Bai05()
        {
            InitializeComponent();
        }

        private void Bai05_Load(object sender, EventArgs e)
        {
            BindGrid(danhSachSinhVien);
        }

        // Hàm dùng chung để hiển thị danh sách lên Grid
        private void BindGrid(List<SinhVien> list)
        {
            dgvSinhVien.Rows.Clear(); // Xóa dữ liệu cũ trên lưới

            for (int i = 0; i < list.Count; i++)
            {
                // Thêm dòng mới: Số TT = i + 1
                dgvSinhVien.Rows.Add(
                    i + 1,
                    list[i].MaSo,
                    list[i].Ten,
                    list[i].Khoa,
                    list[i].DiemTB
                );
            }
        }

        private void menuThemMoi_Click(object sender, EventArgs e)
        {
            ShowFormThem();
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            ShowFormThem();
        }

        private void ShowFormThem()
        {
            frmThemSinhVien f = new frmThemSinhVien();

            // Hiện Form thêm dưới dạng Dialog (không thể thao tác form cha khi chưa đóng form con)
            if (f.ShowDialog() == DialogResult.OK)
            {
                // Nếu người dùng ấn "Thêm Mới" thành công
                // Lấy dữ liệu từ property StudentData của Form con
                danhSachSinhVien.Add(f.StudentData);

                // Hiển thị lại danh sách
                BindGrid(danhSachSinhVien);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                // Nếu ô tìm kiếm rỗng thì hiện tất cả
                BindGrid(danhSachSinhVien);
            }
            else
            {
                // Lọc danh sách theo tên (chứa từ khóa, không phân biệt hoa thường)
                var ketQua = danhSachSinhVien.Where(s => s.Ten.ToLower().Contains(keyword)).ToList();

                BindGrid(ketQua);
            }
        }

        private void menuThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
