using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTH4
{
    public partial class Bai06 : Form
    {
        public Bai06()
        {
            InitializeComponent();
        }

        private void btnSelectSource_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Chọn thư mục nguồn chứa file";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtSource.Text = fbd.SelectedPath;
            }
        }

        private void btnSelectDest_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Chọn thư mục đích để lưu file";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtDest.Text = fbd.SelectedPath;
            }
        }

        private async void btnCopy_Click(object sender, EventArgs e)
        {
            string sourcePath = txtSource.Text;
            string destPath = txtDest.Text;

            // Kiểm tra đầu vào
            if (string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(destPath))
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thư mục nguồn và đích!");
                return;
            }

            if (!Directory.Exists(sourcePath))
            {
                MessageBox.Show("Thư mục nguồn không tồn tại!");
                return;
            }

            // Tạo thư mục đích nếu chưa có
            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }

            // Lấy danh sách tất cả các file trong thư mục nguồn
            string[] files = Directory.GetFiles(sourcePath);
            int totalFiles = files.Length;

            if (totalFiles == 0)
            {
                MessageBox.Show("Thư mục nguồn trống!");
                return;
            }

            // Cấu hình ProgressBar
            progressBar1.Minimum = 0;
            progressBar1.Maximum = totalFiles;
            progressBar1.Value = 0;

            // Khóa nút Copy để tránh bấm nhiều lần
            btnCopy.Enabled = false;

            await Task.Run(() =>
            {
                for (int i = 0; i < totalFiles; i++)
                {
                    string sourceFile = files[i];
                    string fileName = Path.GetFileName(sourceFile);
                    string destFile = Path.Combine(destPath, fileName);

                    // Thực hiện copy (overwrite = true: ghi đè nếu trùng)
                    File.Copy(sourceFile, destFile, true);

                    // Cập nhật giao diện (Phải dùng Invoke vì đang ở luồng phụ)
                    this.Invoke(new Action(() =>
                    {
                        // Cập nhật thanh tiến trình
                        progressBar1.Value = i + 1;

                        // Cập nhật Label trạng thái bên dưới
                        string statusMsg = $"Đang sao chép: {fileName}";
                        lblStatus.Text = statusMsg;

                        // Cập nhật ToolTip cho ProgressBar
                        // Khi chuột hover vào ProgressBar sẽ thấy tên file đang chạy
                        toolTip1.SetToolTip(progressBar1, statusMsg);
                    }));

                    // Giả lập độ trễ 1 chút để mắt thường kịp nhìn thấy tiến trình chạy                    
                    System.Threading.Thread.Sleep(100);
                }
            });

            // Mở khóa nút và thông báo sau khi xong
            btnCopy.Enabled = true;
            lblStatus.Text = "Sao chép hoàn tất!";
            MessageBox.Show("Đã sao chép thành công tất cả tập tin!");

        }
    }
}
