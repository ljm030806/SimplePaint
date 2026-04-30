namespace SimplePaint
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            lblAppName = new Label();
            groupBox1 = new GroupBox();
            btnCircle = new Button();
            btnRectangle = new Button();
            btnLine = new Button();
            groupBox2 = new GroupBox();
            cmbColor = new ComboBox();
            groupBox3 = new GroupBox();
            trbLineWidth = new TrackBar();
            picCanvas = new PictureBox();
            btnOpenFile = new Button();
            btnSaveFile = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trbLineWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picCanvas).BeginInit();
            SuspendLayout();
            // 
            // lblAppName
            // 
            lblAppName.AutoSize = true;
            lblAppName.Font = new Font("맑은 고딕", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblAppName.ForeColor = Color.Blue;
            lblAppName.Location = new Point(12, 9);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(191, 40);
            lblAppName.TabIndex = 0;
            lblAppName.Text = "Simple Paint";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnCircle);
            groupBox1.Controls.Add(btnRectangle);
            groupBox1.Controls.Add(btnLine);
            groupBox1.Location = new Point(12, 68);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(194, 89);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "도형선택";
            // 
            // btnCircle
            // 
            btnCircle.ForeColor = SystemColors.ControlText;
            btnCircle.Image = (Image)resources.GetObject("btnCircle.Image");
            btnCircle.ImageAlign = ContentAlignment.TopCenter;
            btnCircle.Location = new Point(128, 19);
            btnCircle.Name = "btnCircle";
            btnCircle.Size = new Size(55, 64);
            btnCircle.TabIndex = 2;
            btnCircle.Text = "원";
            btnCircle.TextAlign = ContentAlignment.BottomCenter;
            btnCircle.UseVisualStyleBackColor = true;
            // 
            // btnRectangle
            // 
            btnRectangle.Image = (Image)resources.GetObject("btnRectangle.Image");
            btnRectangle.ImageAlign = ContentAlignment.TopCenter;
            btnRectangle.Location = new Point(67, 19);
            btnRectangle.Name = "btnRectangle";
            btnRectangle.Size = new Size(55, 64);
            btnRectangle.TabIndex = 1;
            btnRectangle.Text = "사각형";
            btnRectangle.TextAlign = ContentAlignment.BottomCenter;
            btnRectangle.UseVisualStyleBackColor = true;
            // 
            // btnLine
            // 
            btnLine.Image = (Image)resources.GetObject("btnLine.Image");
            btnLine.ImageAlign = ContentAlignment.TopCenter;
            btnLine.Location = new Point(6, 19);
            btnLine.Name = "btnLine";
            btnLine.Size = new Size(55, 64);
            btnLine.TabIndex = 0;
            btnLine.Text = "직선";
            btnLine.TextAlign = ContentAlignment.BottomCenter;
            btnLine.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(cmbColor);
            groupBox2.Location = new Point(212, 68);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(141, 90);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "색 선택";
            // 
            // cmbColor
            // 
            cmbColor.FormattingEnabled = true;
            cmbColor.Items.AddRange(new object[] { "Black", "Red", "Blue", "Green" });
            cmbColor.Location = new Point(6, 41);
            cmbColor.Name = "cmbColor";
            cmbColor.Size = new Size(121, 23);
            cmbColor.TabIndex = 0;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(trbLineWidth);
            groupBox3.Location = new Point(359, 68);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(164, 90);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "선 두께";
            groupBox3.Enter += groupBox3_Enter;
            // 
            // trbLineWidth
            // 
            trbLineWidth.Location = new Point(6, 22);
            trbLineWidth.Name = "trbLineWidth";
            trbLineWidth.Size = new Size(140, 45);
            trbLineWidth.TabIndex = 0;
            // 
            // picCanvas
            // 
            picCanvas.BackColor = Color.White;
            picCanvas.Location = new Point(15, 182);
            picCanvas.Name = "picCanvas";
            picCanvas.Size = new Size(694, 367);
            picCanvas.TabIndex = 4;
            picCanvas.TabStop = false;
            // 
            // btnOpenFile
            // 
            btnOpenFile.BackColor = Color.Yellow;
            btnOpenFile.Font = new Font("맑은 고딕", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            btnOpenFile.Location = new Point(556, 83);
            btnOpenFile.Name = "btnOpenFile";
            btnOpenFile.Size = new Size(69, 67);
            btnOpenFile.TabIndex = 5;
            btnOpenFile.Text = "열기";
            btnOpenFile.UseVisualStyleBackColor = false;
            // 
            // btnSaveFile
            // 
            btnSaveFile.BackColor = Color.Lime;
            btnSaveFile.Font = new Font("맑은 고딕", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            btnSaveFile.Location = new Point(640, 83);
            btnSaveFile.Name = "btnSaveFile";
            btnSaveFile.Size = new Size(69, 67);
            btnSaveFile.TabIndex = 6;
            btnSaveFile.Text = "저장";
            btnSaveFile.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(737, 570);
            Controls.Add(btnSaveFile);
            Controls.Add(btnOpenFile);
            Controls.Add(picCanvas);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(lblAppName);
            Name = "Form1";
            Text = "Simple Paint v1.0";
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trbLineWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)picCanvas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblAppName;
        private GroupBox groupBox1;
        private Button btnCircle;
        private Button btnRectangle;
        private Button btnLine;
        private GroupBox groupBox2;
        private ComboBox cmbColor;
        private GroupBox groupBox3;
        private TrackBar trbLineWidth;
        private PictureBox picCanvas;
        private Button btnOpenFile;
        private Button btnSaveFile;
    }
}
