using System.ComponentModel;

namespace XstreaMonNET8
{
    public class Form_Status : Form
    {
        private IContainer components;
        private string Pri_Status_Text;

        private PictureBox pictureBox1;
        private Label lab1;
        private Label labStatus;
        private Label labVersion;
        private Label labCopyRight;

        public Form_Status()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            pictureBox1 = new PictureBox();
            lab1 = new Label();
            labStatus = new Label();
            labVersion = new Label();
            labCopyRight = new Label();

            SuspendLayout();

            pictureBox1.Dock = DockStyle.Top;
            pictureBox1.Image = Resources.Logo; // Asegúrate de que este recurso exista
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Size = new Size(439, 112);

            lab1.Dock = DockStyle.Top;
            lab1.Font = new Font("Segoe UI Semibold", 14f, FontStyle.Bold);
            lab1.TextAlign = ContentAlignment.MiddleCenter;
            lab1.Size = new Size(439, 40);
            lab1.Text = "XstreaMon";

            labVersion.Dock = DockStyle.Top;
            labVersion.Font = new Font("Segoe UI Semibold", 10f, FontStyle.Bold);
            labVersion.TextAlign = ContentAlignment.MiddleCenter;
            labVersion.Size = new Size(439, 20);

            labStatus.Dock = DockStyle.Top;
            labStatus.TextAlign = ContentAlignment.MiddleCenter;
            labStatus.Size = new Size(439, 26);

            labCopyRight.Dock = DockStyle.Bottom;
            labCopyRight.Font = new Font("Segoe UI", 6.5f);
            labCopyRight.ForeColor = Color.DimGray;
            labCopyRight.TextAlign = ContentAlignment.BottomCenter;
            labCopyRight.Size = new Size(439, 36);

            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(439, 304);
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Form_Status";

            Controls.Add(labCopyRight);
            Controls.Add(labStatus);
            Controls.Add(labVersion);
            Controls.Add(lab1);
            Controls.Add(pictureBox1);

            Load += Form_Status_Load;
            Paint += Form_Status_Paint;

            ResumeLayout(false);
        }

        public string Pro_Status_Text
        {
            set
            {
                try
                {
                    Pri_Status_Text = value;
                    Invalidate();
                    Update();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al actualizar el estado: {ex.Message}");
                }
            }
        }

        private void Form_Status_Load(object sender, EventArgs e)
        {
            labVersion.Text = Application.ProductVersion;
            labCopyRight.Text = $"© {DateTime.Today.Year} Dühring EDV-Service - Todos los derechos reservados.";
        }

        private void Form_Status_Paint(object sender, PaintEventArgs e)
        {
            if (string.IsNullOrEmpty(Pri_Status_Text)) return;

            using var brush = new SolidBrush(Color.White);
            var textWidth = TextRenderer.MeasureText(Pri_Status_Text, Font).Width;
            float x = (Width - textWidth) / 2f;
            float y = 180f;
            e.Graphics.DrawString(Pri_Status_Text, Font, brush, x, y);
        }
    }
}
