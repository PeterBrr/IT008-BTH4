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
    public partial class Bai03 : Form
    {
        public Bai03()
        {
            InitializeComponent();
        }

        // Khởi động Timer để chạy giờ
        private void Bai03_Load(object sender, EventArgs e)
        {
            timer1.Start();
            UpdateStatusTime();
        }

        // Xử lý Menu Open: Mở hộp thoại chọn file
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Thiết lập bộ lọc file theo yêu cầu
            openFileDialog.Filter = "Media Files|*.avi;*.mpeg;*.wav;*.midi;*.mp4;*.mp3|All Files|*.*";
            openFileDialog.Title = "Select a Media File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn file đã chọn
                string filePath = openFileDialog.FileName;
                // Gán đường dẫn file cho điều khiển Windows Media Player
                axWindowsMediaPlayer1.URL = filePath;

                // Tự động phát (thường mặc định là true, nhưng set lại cho chắc)
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        // Xử lý Menu Exit: Thoát chương trình
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Xử lý sự kiện Timer Tick để cập nhật thời gian
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateStatusTime();
        }

        // Hàm cập nhật hiển thị ngày giờ  
        private void UpdateStatusTime()
        {
            // Định dạng: Hôm nay là ngày dd/MM/yyyy - Bây giờ là hh:mm:ss tt
            string statusText = "Hôm nay là ngày " + DateTime.Now.ToString("dd/MM/yyyy") +
                                " - Bây giờ là " + DateTime.Now.ToString("hh:mm:ss tt");

            lblStatus.Text = statusText;
        }
    }
}
