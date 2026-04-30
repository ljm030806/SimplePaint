namespace SimplePaint
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        enum ToolType { Line, Rectangle, Circle }  // 사용할도형타입


        private Bitmap canvasBitmap;          // 실제그림이저장되는비트맵
        private Graphics canvasGraphics;      // 비트맵위에그리기위한객체


        private bool isDrawing = false;       // 현재드래그중인지여부
        private Point startPoint;             // 드래그 시작점 (캔버스 기준 좌표)
        private Point endPoint;               // 드래그끝점 (캔버스 기준 좌표)


        private ToolType currentTool = ToolType.Line;  // 현재선택된도형
        private Color currentColor = Color.Black;      // 현재색상
        private int currentLineWidth = 2;

        // 🔥 확대/축소 배율 변수 추가
        private float zoom = 1.0f;

        public Form1()
        {
            InitializeComponent();
            //캔버스 초기화
            canvasBitmap = new Bitmap(picCanvas.Width, picCanvas.Height);
            canvasGraphics = Graphics.FromImage(canvasBitmap);
            canvasGraphics.Clear(Color.White);      //캔버스를 흰색으로 초기화
            picCanvas.Image = canvasBitmap;   // 그린그림을화면(PictureBox)에표시

            // PictureBox의 모드를 확대/축소가 반영되도록 변경
            picCanvas.SizeMode = PictureBoxSizeMode.StretchImage;

            // 마우스이벤트연결
            picCanvas.MouseDown += PicCanvas_MouseDown;
            picCanvas.MouseMove += PicCanvas_MouseMove;
            picCanvas.MouseUp += PicCanvas_MouseUp;
            picCanvas.Paint += PicCanvas_Paint;

            // 마우스 휠 작동을 위해 픽쳐박스에 진입 시 포커스 강제 부여
            picCanvas.MouseEnter += (s, e) => picCanvas.Focus();
            picCanvas.MouseWheel += PicCanvas_MouseWheel;

            // 도형 및 기타 버튼 이벤트
            btnLine.Click += btnLine_Click;
            btnRectangle.Click += btnRectangle_Click;
            btnCircle.Click += btnCircle_Click;
            btnSaveFile.Click += btnSaveFile_Click;
            
            // 🔥 불러오기 버튼 이벤트 연결
            btnOpenFile.Click += btnOpenFile_Click;

            cmbColor.SelectedIndexChanged += cmbColor_SelectedIndexChanged;
            cmbColor.SelectedIndex = 0;

            trbLineWidth.Minimum = 1;
            trbLineWidth.Maximum = 10;
            trbLineWidth.Value = 5;
            trbLineWidth.ValueChanged += trbLineWidth_ValueChanged;

            // 스크롤바 자동 생성을 위해 PictureBox 부모 컨테이너(Form 또는 Panel)의 AutoScroll 활성화
            if (picCanvas.Parent is ScrollableControl parent)
            {
                parent.AutoScroll = true;
            }
        }

        // 🔥 외부 이미지 로드 기능 추가
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "이미지 불러오기";
                openFileDialog.Filter = "이미지 파일 (*.png; *.jpg; *.jpeg; *.bmp)|*.png;*.jpg;*.jpeg;*.bmp|모든 파일 (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (Image img = Image.FromFile(openFileDialog.FileName))
                        {
                            // 원본 이미지를 락(Lock) 없이 사용하기 위해 새 비트맵으로 복사 후 캔버스 교체
                            canvasBitmap = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);
                            canvasGraphics?.Dispose();
                            canvasGraphics = Graphics.FromImage(canvasBitmap);
                            canvasGraphics.Clear(Color.White);
                            canvasGraphics.DrawImage(img, 0, 0, img.Width, img.Height);
                        }

                        picCanvas.Image = canvasBitmap;
                        zoom = 1.0f; // 배율 초기화
                        ApplyZoom(new Point(0, 0), 1.0f); // 캔버스(PictureBox) 크기 재조정
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("이미지를 불러오는 중 오류가 발생했습니다.\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // 🔥 마우스 휠 확대/축소 이벤트
        private void PicCanvas_MouseWheel(object sender, MouseEventArgs e)
        {
            if (canvasBitmap == null) return;

            float oldZoom = zoom;

            // 휠 방향에 따라 배율 25%씩 증가/감소
            if (e.Delta > 0)
                zoom *= 1.25f;
            else if (e.Delta < 0)
                zoom /= 1.25f;

            // 줌 범위 제한 (10% ~ 1000%)
            if (zoom < 0.1f) zoom = 0.1f;
            if (zoom > 10.0f) zoom = 10.0f;

            if (zoom != oldZoom)
            {
                ApplyZoom(e.Location, oldZoom);
            }
        }

        // 🔥 확대/축소 적용 및 스크롤, 캔버스 크기 재계산 (마우스 포인터 중심)
        private void ApplyZoom(Point mousePos, float oldZoom)
        {
            picCanvas.Width = (int)(canvasBitmap.Width * zoom);
            picCanvas.Height = (int)(canvasBitmap.Height * zoom);

            if (picCanvas.Parent is ScrollableControl parent)
            {
                // 확대 전 마우스가 있던 이미지 좌표가 확대 후에도 동일한 시야에 있도록 스크롤 위치 보정
                int newMouseX = (int)((mousePos.X / oldZoom) * zoom);
                int newMouseY = (int)((mousePos.Y / oldZoom) * zoom);

                int newScrollX = Math.Abs(parent.AutoScrollPosition.X) + (newMouseX - mousePos.X);
                int newScrollY = Math.Abs(parent.AutoScrollPosition.Y) + (newMouseY - mousePos.Y);

                parent.AutoScrollPosition = new Point(newScrollX, newScrollY);
            }
        }

        // 좌표 매핑 변경 : 현재 확대된 배율(zoom)로 나누어 실제 캔버스 그리기 좌표로 환산
        private void PicCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;             
            startPoint = new Point((int)(e.X / zoom), (int)(e.Y / zoom)); 
        }

        private void PicCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;
            endPoint = new Point((int)(e.X / zoom), (int)(e.Y / zoom));
            picCanvas.Invalidate();
        }

        private void PicCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;
            isDrawing = false;
            endPoint = new Point((int)(e.X / zoom), (int)(e.Y / zoom));
            using (Pen pen = new Pen(currentColor, currentLineWidth))
            {
                DrawShape(canvasGraphics, pen, startPoint, endPoint);
            }
            picCanvas.Invalidate();
        }

        private void PicCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (!isDrawing) return;
            
            // 프리뷰 역시 배율에 맞게 조정되도록 그래픽스 자체 배율 스케일링
            e.Graphics.ScaleTransform(zoom, zoom); 

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

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "그림 저장하기";
                saveFileDialog.Filter = "PNG 이미지 (*.png)|*.png|JPG 이미지 (*.jpg)|*.jpg|BMP 이미지 (*.bmp)|*.bmp";
                saveFileDialog.DefaultExt = "png";
                saveFileDialog.AddExtension = true;
                saveFileDialog.FilterIndex = 1; // 기본값을 PNG로 고정

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ImageFormat format = ImageFormat.Png;
                        string fileName = saveFileDialog.FileName;

                        // 선택한 필터 인덱스에 따라 저장 형식 결정
                        switch (saveFileDialog.FilterIndex)
                        {
                            case 1:
                                format = ImageFormat.Png;
                                break;
                            case 2:
                                format = ImageFormat.Jpeg; 
                                // 만약 확장자가 .jpeg로 알아서 붙었다면 .jpg로 강제 변경
                                if (fileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                                {
                                    fileName = System.IO.Path.ChangeExtension(fileName, ".jpg");
                                }
                                break;
                            case 3:
                                format = ImageFormat.Bmp;
                                break;
                        }

                        // 지정된 경로(강제된 확장자)와 포맷으로 캔버스 비트맵 저장
                        if (canvasBitmap != null)
                        {
                            canvasBitmap.Save(fileName, format);
                            MessageBox.Show("이미지가 성공적으로 저장되었습니다.", "저장 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("이미지를 저장하는 중 오류가 발생했습니다.\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
