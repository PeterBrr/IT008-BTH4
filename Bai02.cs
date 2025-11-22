using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace BTH4
{
    public partial class Bai02 : Form
    {
        public Bai02()
        {
            InitializeComponent();
        }

        // Sự kiện Load của Form (chạy khi form vừa mở lên)
        private void Bai02_Load(object sender, EventArgs e)
        {
            // Hiển thị giờ ngay lập tức, không cần đợi 1 giây đầu tiên
            ShowTime();
        }

        // Sự kiện Tick của Timer (chạy mỗi 1 giây)
        private void timer1_Tick(object sender, EventArgs e)
        {
            ShowTime();
        }

        // Hàm hiển thị giờ
        private void ShowTime()
        {
            // Tạo đối tượng văn hóa Tiếng Anh (Mỹ)
            CultureInfo usCulture = new CultureInfo("en-US");

            // dddd: Thứ (Tiếng Anh), MMMM: Tháng (Tiếng Anh đầy đủ), dd: Ngày, yyyy: Năm
            // hh:mm:ss tt: Giờ:Phút:Giây AM/PM
            lblTime.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy hh:mm:ss tt", usCulture);
        }
    }
}
