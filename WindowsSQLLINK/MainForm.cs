using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace PixelArtGenerator
{
    public partial class MainForm : Form
    {
        private Bitmap originalImage;
        private BackgroundWorker worker;

        public MainForm()
        {
            InitializeComponent();
            // 初始化背景工作线程
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.picOriginal = new System.Windows.Forms.PictureBox();
            this.picPixelArt = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPixelSize = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboStyle = new System.Windows.Forms.ComboBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.picOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPixelArt)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(204)))), ((int)(((byte)(235)))));
            this.btnLoadImage.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLoadImage.Location = new System.Drawing.Point(54, 57);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(97, 60);
            this.btnLoadImage.TabIndex = 12;
            this.btnLoadImage.Text = "加载图片";
            this.btnLoadImage.UseVisualStyleBackColor = false;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // picOriginal
            // 
            this.picOriginal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picOriginal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picOriginal.Location = new System.Drawing.Point(54, 134);
            this.picOriginal.Name = "picOriginal";
            this.picOriginal.Size = new System.Drawing.Size(225, 165);
            this.picOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picOriginal.TabIndex = 1;
            this.picOriginal.TabStop = false;
            this.picOriginal.Click += new System.EventHandler(this.picOriginal_Click);
            // 
            // picPixelArt
            // 
            this.picPixelArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPixelArt.Location = new System.Drawing.Point(415, 134);
            this.picPixelArt.Name = "picPixelArt";
            this.picPixelArt.Size = new System.Drawing.Size(230, 165);
            this.picPixelArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPixelArt.TabIndex = 2;
            this.picPixelArt.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(102)))), ((int)(((byte)(164)))));
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("楷体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(114, 315);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "原始图片";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(102)))), ((int)(((byte)(164)))));
            this.label2.Font = new System.Drawing.Font("楷体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(489, 315);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "像素艺术";
            // 
            // txtPixelSize
            // 
            this.txtPixelSize.Location = new System.Drawing.Point(168, 343);
            this.txtPixelSize.Name = "txtPixelSize";
            this.txtPixelSize.Size = new System.Drawing.Size(79, 25);
            this.txtPixelSize.TabIndex = 10;
            this.txtPixelSize.Text = "10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(102)))), ((int)(((byte)(164)))));
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("楷体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(27, 345);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "像素大小：";
            // 
            // comboStyle
            // 
            this.comboStyle.FormattingEnabled = true;
            this.comboStyle.Items.AddRange(new object[] {
            "标准像素",
            "黑白风格",
            "复古红绿",
            "低饱和度"});
            this.comboStyle.Location = new System.Drawing.Point(456, 343);
            this.comboStyle.Name = "comboStyle";
            this.comboStyle.Size = new System.Drawing.Size(154, 23);
            this.comboStyle.TabIndex = 7;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGenerate.Location = new System.Drawing.Point(91, 376);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(137, 32);
            this.btnGenerate.TabIndex = 11;
            this.btnGenerate.Text = "生成像素艺术";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(482, 376);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 32);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "保存结果";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 443);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(711, 23);
            this.progressBar1.TabIndex = 8;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp|所有文件|*.*";
            this.openFileDialog1.Title = "选择图片";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "png";
            this.saveFileDialog1.Filter = "PNG图片|*.png|JPEG图片|*.jpg|所有图片|*.*";
            this.saveFileDialog1.Title = "保存像素艺术";
            // 
            // MainForm
            // 
            this.BackColor = System.Drawing.SystemColors.Info;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(711, 466);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.comboStyle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPixelSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picPixelArt);
            this.Controls.Add(this.picOriginal);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnLoadImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "像素艺术马赛克生成器";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPixelArt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.PictureBox picOriginal;
        private System.Windows.Forms.PictureBox picPixelArt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPixelSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboStyle;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    originalImage = new Bitmap(openFileDialog1.FileName);
                    picOriginal.Image = originalImage;
                    btnGenerate.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("加载图片失败：" + ex.Message);
                }
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("请先加载图片");
                return;
            }

            // 禁用按钮，防止重复点击
            btnGenerate.Enabled = false;
            btnLoadImage.Enabled = false;
            progressBar1.Value = 0;

            // 读取参数
            int pixelSize;
            if (!int.TryParse(txtPixelSize.Text, out pixelSize) || pixelSize < 2 || pixelSize > 50)
            {
                pixelSize = 10;
                txtPixelSize.Text = "10";
            }

            // 启动后台线程处理图片
            worker.RunWorkerAsync(new object[] { originalImage, pixelSize, comboStyle.SelectedIndex });
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] args = (object[])e.Argument;
            Bitmap inputImage = (Bitmap)args[0];
            int pixelSize = (int)args[1];
            int styleIndex = (int)args[2];

            int width = inputImage.Width;
            int height = inputImage.Height;
            Bitmap resultImage = new Bitmap(width, height);

            // 分块处理图片（减少内存占用）
            int blockSize = 50; // 每块处理50行
            int totalBlocks = height / blockSize + (height % blockSize > 0 ? 1 : 0);

            for (int blockY = 0; blockY < totalBlocks; blockY++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                int startY = blockY * blockSize;
                int endY = Math.Min(startY + blockSize, height);

                for (int y = startY; y < endY; y += pixelSize)
                {
                    for (int x = 0; x < width; x += pixelSize)
                    {
                        // 获取块内主色调
                        Color mainColor = GetDominantColor(inputImage, x, y, pixelSize);

                        // 根据风格调整颜色
                        switch (styleIndex)
                        {
                            case 1: // 黑白风格
                                mainColor = ToBlackAndWhite(mainColor);
                                break;
                            case 2: // 复古红绿
                                mainColor = ToRetroStyle(mainColor);
                                break;
                            case 3: // 低饱和度
                                mainColor = ToLowSaturation(mainColor);
                                break;
                        }

                        // 绘制像素块
                        for (int py = 0; py < pixelSize && y + py < height; py++)
                        {
                            for (int px = 0; px < pixelSize && x + px < width; px++)
                            {
                                resultImage.SetPixel(x + px, y + py, mainColor);
                            }
                        }
                    }
                }

                // 报告进度
                int progress = (int)((float)blockY / totalBlocks * 100);
                worker.ReportProgress(progress);
            }

            e.Result = resultImage;
        }

        private Color GetDominantColor(Bitmap image, int x, int y, int size)
        {
            int r = 0, g = 0, b = 0;
            int count = 0;

            for (int py = 0; py < size && y + py < image.Height; py++)
            {
                for (int px = 0; px < size && x + px < image.Width; px++)
                {
                    Color pixel = image.GetPixel(x + px, y + py);
                    r += pixel.R;
                    g += pixel.G;
                    b += pixel.B;
                    count++;
                }
            }

            if (count > 0)
            {
                return Color.FromArgb(r / count, g / count, b / count);
            }
            return Color.Black;
        }

        private Color ToBlackAndWhite(Color color)
        {
            int gray = (int)(0.299 * color.R + 0.587 * color.G + 0.114 * color.B);
            return Color.FromArgb(gray, gray, gray);
        }

        private Color ToRetroStyle(Color color)
        {
            // 增强红色和绿色，降低蓝色
            int r = Math.Min(255, color.R + 30);
            int g = Math.Min(255, color.G + 20);
            int b = Math.Max(0, color.B - 10);
            return Color.FromArgb(r, g, b);
        }

        private Color ToLowSaturation(Color color)
        {
            // 转换为HSV，降低饱和度
            float h = 0, s = 0.3f, v = 0;
            ColorToHSV(color, out h, out s, out v);
            return HSVToColor(h, s, v);
        }

        private void ColorToHSV(Color color, out float h, out float s, out float v)
        {
            int r = color.R;
            int g = color.G;
            int b = color.B;

            int max = Math.Max(r, Math.Max(g, b));
            int min = Math.Min(r, Math.Min(g, b));

            v = max / 255.0f;
            float delta = max - min;

            if (max == 0)
                s = 0;
            else
                s = delta / max;

            if (s == 0)
            {
                h = 0; // 灰色
            }
            else
            {
                float rNorm = (float)(r - min) / delta;
                float gNorm = (float)(g - min) / delta;
                float bNorm = (float)(b - min) / delta;

                if (r == max)
                    h = (gNorm - bNorm) / 6;
                else if (g == max)
                    h = 1.0f / 3.0f + (bNorm - rNorm) / 6;
                else
                    h = 2.0f / 3.0f + (rNorm - gNorm) / 6;

                if (h < 0)
                    h += 1;
                if (h > 1)
                    h -= 1;
            }
        }

        private Color HSVToColor(float h, float s, float v)
        {
            int i = (int)(h * 6);
            float f = h * 6 - i;
            float p = v * (1 - s);
            float q = v * (1 - f * s);
            float t = v * (1 - (1 - f) * s);

            switch (i % 6)
            {
                case 0: return Color.FromArgb((int)(v * 255), (int)(t * 255), (int)(p * 255));
                case 1: return Color.FromArgb((int)(q * 255), (int)(v * 255), (int)(p * 255));
                case 2: return Color.FromArgb((int)(p * 255), (int)(v * 255), (int)(t * 255));
                case 3: return Color.FromArgb((int)(p * 255), (int)(q * 255), (int)(v * 255));
                case 4: return Color.FromArgb((int)(t * 255), (int)(p * 255), (int)(v * 255));
                case 5: return Color.FromArgb((int)(v * 255), (int)(p * 255), (int)(q * 255));
                default: return Color.Black;
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // 恢复按钮状态
            btnGenerate.Enabled = true;
            btnLoadImage.Enabled = true;
            progressBar1.Value = 100;

            if (e.Cancelled)
            {
                MessageBox.Show("操作已取消");
                return;
            }

            if (e.Error != null)
            {
                MessageBox.Show("生成失败：" + e.Error.Message);
                return;
            }

            // 显示结果
            Bitmap resultImage = (Bitmap)e.Result;
            picPixelArt.Image = resultImage;
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (picPixelArt.Image == null) return;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ImageFormat format = saveFileDialog1.FileName.EndsWith(".jpg") ||
                                       saveFileDialog1.FileName.EndsWith(".jpeg") ?
                                       ImageFormat.Jpeg : ImageFormat.Png;
                    picPixelArt.Image.Save(saveFileDialog1.FileName, format);
                    MessageBox.Show("图片保存成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存失败：" + ex.Message);
                }
            }
        }

        private void picOriginal_Click(object sender, EventArgs e)
        {

        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}