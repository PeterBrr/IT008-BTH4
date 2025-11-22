using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTH4
{
    public partial class Bai04 : Form
    {
        // Biến lưu đường dẫn file hiện tại. Nếu là file mới thì biến này rỗng.
        private string currentFilePath = "";
        public Bai04()
        {
            InitializeComponent();
        }

        private void Bai04_Load(object sender, EventArgs e)
        {
            LoadFonts();
            LoadSizes();
            SetDefaultSettings();
        }

        private void LoadFonts()
        {
            // Lấy danh sách font hệ thống đưa vào ComboBox
            InstalledFontCollection installedFonts = new InstalledFontCollection();
            foreach (FontFamily font in installedFonts.Families)
            {
                cmbFonts.Items.Add(font.Name);
            }
        }

        private void LoadSizes()
        {
            // Tạo mảng các size theo yêu cầu
            int[] sizes = { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            foreach (int s in sizes)
            {
                cmbSizes.Items.Add(s);
            }
        }

        private void SetDefaultSettings()
        {
            // Thiết lập giá trị mặc định: Tahoma, 14
            cmbFonts.SelectedItem = "Tahoma";
            cmbSizes.SelectedItem = 14;

            // Áp dụng font mặc định cho RichTextBox
            ApplyFontToRichText();
        }

        // Áp dụng Font và Size từ ComboBox vào vùng chọn hoặc con trỏ hiện tại
        private void ApplyFontToRichText()
        {
            string fontName = cmbFonts.SelectedItem?.ToString() ?? "Tahoma";
            float fontSize = 14;
            if (cmbSizes.SelectedItem != null)
            {
                float.TryParse(cmbSizes.SelectedItem.ToString(), out fontSize);
            }

            FontStyle currentStyle = FontStyle.Regular;
            // Giữ nguyên định dạng (B/I/U) nếu đang có
            if (richTextBox1.SelectionFont != null)
            {
                currentStyle = richTextBox1.SelectionFont.Style;
            }

            richTextBox1.SelectionFont = new Font(fontName, fontSize, currentStyle);
        }

        private void tạoVănBảnMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewDocument();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            CreateNewDocument();
        }

        private void CreateNewDocument()
        {
            richTextBox1.Clear();
            currentFilePath = ""; // Reset đường dẫn
            SetDefaultSettings(); // Reset Font/Size về Tahoma 14
        }

        private void mởTậpTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void OpenFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Rich Text Format (*.rtf)|*.rtf|Text Files (*.txt)|*.txt";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = ofd.FileName;
                try
                {
                    if (currentFilePath.EndsWith(".txt"))
                    {
                        richTextBox1.LoadFile(currentFilePath, RichTextBoxStreamType.PlainText);
                    }
                    else
                    {
                        richTextBox1.LoadFile(currentFilePath, RichTextBoxStreamType.RichText);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi mở file: " + ex.Message);
                }
            }
        }

        private void lưuNộiDungVănBảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void SaveFile()
        {
            // TH1: Văn bản mới chưa lưu lần nào -> Hiện SaveFileDialog
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Rich Text Format (*.rtf)|*.rtf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = sfd.FileName;
                    richTextBox1.SaveFile(currentFilePath, RichTextBoxStreamType.RichText);
                    MessageBox.Show("Lưu văn bản thành công!", "Thông báo");
                }
            }
            else // TH2: Văn bản đã có đường dẫn -> Lưu đè
            {
                try
                {
                    richTextBox1.SaveFile(currentFilePath, RichTextBoxStreamType.RichText);
                    MessageBox.Show("Lưu văn bản thành công!", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu: " + ex.Message);
                }
            }
        }

        private void địnhDạngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.ShowColor = true; // Cho phép chọn màu nếu muốn

            // Lấy font hiện tại của vùng chọn làm mặc định cho Dialog
            if (richTextBox1.SelectionFont != null)
            {
                fd.Font = richTextBox1.SelectionFont;
            }

            if (fd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fd.Font;
                richTextBox1.SelectionColor = fd.Color;
            }
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Bold);
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Italic);
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Underline);
        }

        // Hàm chung để bật/tắt style (B, I, U)
        private void ToggleFontStyle(FontStyle style)
        {
            if (richTextBox1.SelectionFont == null) return;

            Font currentFont = richTextBox1.SelectionFont;
            FontStyle newStyle;

            // Kiểm tra xem style đó đã có chưa, nếu có rồi thì bỏ đi (XOR), chưa có thì thêm vào
            if (currentFont.Style.HasFlag(style))
            {
                newStyle = currentFont.Style & ~style; // Bỏ style
            }
            else
            {
                newStyle = currentFont.Style | style; // Thêm style
            }

            richTextBox1.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newStyle);
        }

        private void cmbFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Chỉ áp dụng khi người dùng chủ động thay đổi, tránh lỗi khi Load Form
            if (richTextBox1.Focused) ApplyFontToRichText();
        }

        private void cmbSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Focused) ApplyFontToRichText();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
