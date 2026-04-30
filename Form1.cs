namespace SimplePaint
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        enum ToolType { Line, Rectangle, Circle }  // 사용할도형타입


        private Bitmap canvasBitmap;          // 실제그림이저장되는비트맵
        private Graphics canvasGraphics;      // 비트맵위에그리기위한객체


        private bool isDrawing = false;       // 현재드래그중인지여부
        private Point startPoint;             // 드래그 시작점
        private Point endPoint;               // 드래그끝점


        private ToolType currentTool = ToolType.Line;  // 현재선택된도형
        private Color currentColor = Color.Black;      // 현재색상
        private int currentLineWidth = 2;

        public Form1()
        {
            InitializeComponent();
            //캔버스 초기화
            canvasBitmap = new Bitmap(picCanvas.Width, picCanvas.Height);
            canvasGraphics = Graphics.FromImage(canvasBitmap);
            canvasGraphics.Clear(Color.White);      //캔버스를 흰색으로 초기화
            picCanvas.Image = canvasBitmap;   // 그린그림을화면(PictureBox)에표시

            // 마우스이벤트연결
            picCanvas.MouseDown += PicCanvas_MouseDown;
            picCanvas.MouseMove += PicCanvas_MouseMove;
            picCanvas.MouseUp += PicCanvas_MouseUp;

            picCanvas.Paint += PicCanvas_Paint;

            btnLine.Click += btnLine_Click;
            btnRectangle.Click += btnRectangle_Click;
            btnCircle.Click += btnCircle_Click;

            cmbColor.SelectedIndexChanged += cmbColor_SelectedIndexChanged;
            cmbColor.SelectedIndex = 0;

            trbLineWidth.Minimum = 1;
            trbLineWidth.Maximum = 10;
            trbLineWidth.Value = 5;
            trbLineWidth.ValueChanged += trbLineWidth_ValueChanged;

        }

        private void PicCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;             // 드래그시작
            startPoint = e.Location;      // 시작점저장}
        }
        private void PicCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;
            endPoint = e.Location;
            picCanvas.Invalidate();
        }
        private void PicCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;
            isDrawing = false;
            endPoint = e.Location;
            using (Pen pen = new Pen(currentColor, currentLineWidth))
            {
                DrawShape(canvasGraphics, pen, startPoint, endPoint);
            }
            picCanvas.Invalidate();
        }
        private void PicCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (!isDrawing) return;
            using (Pen previewPen = new Pen(currentColor, currentLineWidth))
            {
                previewPen.DashStyle = DashStyle.Dash;
                DrawShape(e.Graphics, previewPen, startPoint, endPoint);
            }
        }

        private void DrawShape(Graphics g, Pen pen, Point p1, Point p2)
        {
            Rectangle rect = GetRectangle(p1, p2);
            switch (currentTool)
            {
                case ToolType.Line:
                    g.DrawLine(pen, p1, p2);
                    break;
                case ToolType.Rectangle:
                    g.DrawRectangle(pen, rect);
                    break;
                case ToolType.Circle:
                    g.DrawEllipse(pen, rect);
                    break;
            }
        }
        private Rectangle GetRectangle(Point p1, Point p2)
        {
            return new Rectangle(
                Math.Min(p1.X, p2.X),
                Math.Min(p1.Y, p2.Y),
                Math.Abs(p1.X - p2.X),
                Math.Abs(p1.Y - p2.Y)
                );
        }
        private void btnLine_Click(object sender, EventArgs e)
        {
            currentTool = ToolType.Line;
        }
        private void btnRectangle_Click(object sender, EventArgs e)
        {
            currentTool = ToolType.Rectangle;
        }
        private void btnCircle_Click(object sender, EventArgs e)
        {
            currentTool = ToolType.Circle;
        }
        private void cmbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbColor.SelectedIndex)
            {
                case 0:
                    currentColor = Color.Black;
                    break;
                case 1:
                    currentColor = Color.Red;
                    break;
                case 2:
                    currentColor = Color.Blue;
                    break;
                case 3:
                    currentColor = Color.Green;
                    break;
                default:
                    currentColor = Color.Black;
                    break;
            }
        }
        private void trbLineWidth_ValueChanged(object sender, EventArgs e)
        {
            currentLineWidth = trbLineWidth.Value;
        }
    }
}
