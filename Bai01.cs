using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BTH4
{
    public partial class Bai01 : Form
    {       
        public Bai01()
        {
            InitializeComponent();
            // Kích hoạt chế độ KeyPreview để Form nhận sự kiện bàn phím trước các điều khiển con
            this.KeyPreview = true;
        }

        // Xử lý sự kiện click chuột
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            // Nếu click vào Label (sender là lblInfo), cộng thêm vị trí của Label
            if (sender == lblInfo)
            {
                x += lblInfo.Location.X;
                y += lblInfo.Location.Y;
            }

            // Cập nhật text
            lblInfo.Text = $"CHUỘT CLICK\nNút: {e.Button}\nTọa độ trên Form: X={x}, Y={y}";
        }

        // Xử lý sự kiện nhấn phím (Dùng để lấy KeyCode, Modifiers như Ctrl, Shift)
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            string info = $"Phím (KeyDown): KeyCode={e.KeyCode}, KeyValue={e.KeyValue}, Modifiers={e.Modifiers}";
            lblInfo.Text = info;
        }

        // Xử lý sự kiện gõ phím (Dùng để lấy mã ASCII / ký tự thực tế)
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Lấy mã ASCII
            int asciiCode = (int)e.KeyChar;

            string info = $"Ký tự (KeyPress): '{e.KeyChar}' | Mã ASCII: {asciiCode}";

            // Nối tiếp thông tin vào Label (vì KeyDown chạy trước KeyPress)
            lblInfo.Text += $"\n{info}";
        }
    }
}
