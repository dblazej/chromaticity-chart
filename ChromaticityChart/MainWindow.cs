using System.Drawing.Drawing2D;
using System.Globalization;

namespace Chromaticity
{
    public partial class MainWindow : Form
    {
        private static readonly double[,] XyzToRgbMatrix = new double[3, 3]{
        { 3.241, -1.5374, -0.4986},
        { -0.9692,  1.876,  0.0416},
        { 0.0556, -0.204,  1.057} };

        private readonly static int margin = 45;
        private readonly static CIEXYZ[] waveLengths = new CIEXYZ[781];
        private readonly double k = 0;

        private int curvePointsCount = 5;
        private int movingPointIndex = -1;
        private List<Point> curvePoints;
        private readonly CIEXYZ bezierCurvePoint;

        private bool selected = false;
        private bool drawBackground = true;
        private bool drawBlackDots = true;

        public MainWindow()
        {
            InitializeComponent();

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            string[] lines = File.ReadAllLines(@"..\..\..\Resources\color_matching_functions.txt");
            foreach (var line in lines)
            {
                string[] subs = line.Split(' ', '\t');
                int wl = int.Parse(subs[0]);
                double x = double.Parse(subs[1]);
                double y = double.Parse(subs[2]);
                double z = double.Parse(subs[3]);
                waveLengths[wl] = new CIEXYZ(x, y, z);
                k += waveLengths[wl].Y;
            }
            k = 1 / k;

            curvePoints = new List<Point>();
            SetDefaultBezierCurvePoints();
            bezierCurvePoint = new CIEXYZ(0, 0, 0);
        }
        private void NumberOfPointsNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            curvePointsCount = (int)numberOfPointsNumericUpDown.Value;
            SetDefaultBezierCurvePoints();
            bezierCurvePictureBox.Invalidate();
        }
        private void BackgroundCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            drawBackground = backgroundCheckBox.Checked;
            horseshoePictureBox.Invalidate();
        }
        private void BlackDotsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            drawBlackDots = blackDotsCheckBox.Checked;
            horseshoePictureBox.Invalidate();
        }
        private void CreateImageButton_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new(horseshoePictureBox.ClientSize.Width, horseshoePictureBox.ClientSize.Height);
            horseshoePictureBox.DrawToBitmap(bmp, new Rectangle(0, 0, horseshoePictureBox.ClientSize.Width, horseshoePictureBox.ClientSize.Height));
            using SaveFileDialog saveFileDialog = new() { Filter = @"PNG|*.png" };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                bmp.Save(saveFileDialog.FileName);
            }
        }
        private void BezierCurvePictureBox_Paint(object sender, PaintEventArgs e)
        {
            Pen arrowPen = new(Brushes.Black, 1)
            {
                CustomEndCap = new AdjustableArrowCap(5, 5)
            };
            e.Graphics.DrawLine(arrowPen, new Point(0, bezierCurvePictureBox.Height - margin), new Point(bezierCurvePictureBox.Width, bezierCurvePictureBox.Height - margin));
            e.Graphics.DrawLine(arrowPen, new Point(margin, bezierCurvePictureBox.Height), new Point(margin, 0));

            Point pointA = curvePoints[0], pointB;
            for (double i = 0; i <= 1; i += 0.01)
            {
                pointB = new Point(0, 0);
                for (int j = 0; j <= curvePointsCount - 1; j++)
                {
                    int r = 1;
                    int n = curvePointsCount - 1;
                    if (j > n) r = 0;
                    else for (int l = 1; l <= j; ++l) r = (r * n--) / l;

                    pointB.X += (int)(curvePoints[j].X * r * Math.Pow(1 - i, curvePointsCount - 1 - j) * Math.Pow(i, j));
                    pointB.Y += (int)(curvePoints[j].Y * r * Math.Pow(1 - i, curvePointsCount - 1 - j) * Math.Pow(i, j));
                }
                e.Graphics.DrawLine(Pens.Black, pointA, pointB);
                pointA = pointB;
            }
            e.Graphics.DrawLine(Pens.Black, pointA, curvePoints[^1]);
            foreach (var curvePoint in curvePoints) e.Graphics.FillEllipse(Brushes.Black, new Rectangle(curvePoint.X - 5, curvePoint.Y - 5, 10, 10));

            SetBezierCurvePoint();
        }
        private void HorseshoePictureBox_Paint(object sender, PaintEventArgs e)
        {
            Pen arrowPen = new(Brushes.Black, 1)
            {
                CustomEndCap = new AdjustableArrowCap(5, 5)
            };
            e.Graphics.DrawLine(arrowPen, new Point(0, horseshoePictureBox.Height - margin), new Point(horseshoePictureBox.Width, horseshoePictureBox.Height - margin));
            e.Graphics.DrawLine(arrowPen, new Point(margin, horseshoePictureBox.Height), new Point(margin, 0));

            if (drawBackground)
            {
                Bitmap backgroundBitmap = new(new Bitmap(@"..\..\..\Resources\background.png"),
                    (int)(0.74 * (Math.Min(horseshoePictureBox.Width, horseshoePictureBox.Height) - margin)),
                    (int)(0.84 * (Math.Min(horseshoePictureBox.Width, horseshoePictureBox.Height) - margin)));
                e.Graphics.DrawImage(backgroundBitmap, new Point(margin, horseshoePictureBox.Height - backgroundBitmap.Height - margin));
            }

            if (drawBlackDots)
            {
                for (int i = 380; i <= 700; ++i)
                {
                    double sumC = waveLengths[i].X + waveLengths[i].Y + waveLengths[i].Z;
                    double xC = sumC == 0 ? 0 : waveLengths[i].X / sumC;
                    double yC = sumC == 0 ? 0 : waveLengths[i].Y / sumC;
                    int rColor = Math.Min((int)(255 * Math.Pow(Math.Max(XyzToRgbMatrix[0, 0] * waveLengths[i].X + XyzToRgbMatrix[0, 1] * waveLengths[i].Y +
                        XyzToRgbMatrix[0, 2] * waveLengths[i].Z, 0), 1 / 2.2)), 255);
                    int gColor = Math.Min((int)(255 * Math.Pow(Math.Max(XyzToRgbMatrix[1, 0] * waveLengths[i].X + XyzToRgbMatrix[1, 1] * waveLengths[i].Y +
                        XyzToRgbMatrix[1, 2] * waveLengths[i].Z, 0), 1 / 2.2)), 255);
                    int bColor = Math.Min((int)(255 * Math.Pow(Math.Max(XyzToRgbMatrix[2, 0] * waveLengths[i].X + XyzToRgbMatrix[2, 1] * waveLengths[i].Y +
                        XyzToRgbMatrix[2, 2] * waveLengths[i].Z, 0), 1 / 2.2)), 255);

                    e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(rColor, gColor, bColor)), new Rectangle(((int)(xC * (Math.Min(horseshoePictureBox.Width,
                        horseshoePictureBox.Height) - margin)) + margin) - 5,
                        (horseshoePictureBox.Height - (int)(yC * (Math.Min(horseshoePictureBox.Width, horseshoePictureBox.Height) - margin)) - margin) - 5, 10, 10));
                }

                if (bezierCurvePoint == null) return;
                double sum = bezierCurvePoint.X + bezierCurvePoint.Y + bezierCurvePoint.Z;
                double x = sum == 0 ? 0 : bezierCurvePoint.X / sum;
                double y = sum == 0 ? 0 : bezierCurvePoint.Y / sum;
                Point p = new((int)(x * (Math.Min(horseshoePictureBox.Width, horseshoePictureBox.Height) - margin)) + margin,
                    horseshoePictureBox.Height - (int)(y * (Math.Min(horseshoePictureBox.Width, horseshoePictureBox.Height) - margin)) - margin);

                e.Graphics.FillEllipse(Brushes.DarkRed, new Rectangle(p.X - 5, p.Y - 5, 10, 10));
                e.Graphics.DrawString(x.ToString("f") + ", " + y.ToString("f"), new Font("Arial", 10), Brushes.Black, p);
            }
        }
        private void BezierCurvePictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < curvePointsCount; ++i)
                {
                    if (Math.Sqrt((curvePoints[i].X - e.Location.X) * (curvePoints[i].X - e.Location.X) + (curvePoints[i].Y - e.Location.Y) * (curvePoints[i].Y - e.Location.Y)) < 5)
                    {
                        movingPointIndex = i;
                        selected = true;
                    }
                }
            }
        }
        private void BezierCurvePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            selected = false;
        }
        private void BezierCurvePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (selected)
            {
                double x = (double)(e.Location.X - margin) / (bezierCurvePictureBox.Width - margin) * 500 + 330;
                if (x >= 380 && x <= 780) curvePoints[movingPointIndex] = e.Location;
                bezierCurvePictureBox.Invalidate();
            }
        }
        private void SetDefaultBezierCurvePoints()
        {
            curvePoints = new List<Point>();
            int x = ((bezierCurvePictureBox.Width - margin) / (curvePointsCount + 1)) + margin;
            for (int i = 0; i < curvePointsCount; ++i)
            {
                curvePoints.Add(new Point(x, (int)((bezierCurvePictureBox.Height - margin) * 0.7)));
                x += (bezierCurvePictureBox.Width - margin) / (curvePointsCount + 1);
            }
        }
        private void SetBezierCurvePoint()
        {
            bezierCurvePoint.X = bezierCurvePoint.Y = bezierCurvePoint.Z = 0;
            Point pointA = curvePoints[0], pointB;
            for (double i = 0; i <= 1; i += 0.01)
            {
                pointB = new Point(0, 0);
                for (int j = 0; j <= curvePointsCount - 1; j++)
                {
                    int r = 1;
                    int n = curvePointsCount - 1;
                    if (j > n) r = 0;
                    else for (int l = 1; l <= j; ++l) r = (r * n--) / l;

                    pointB.X += (int)(curvePoints[j].X * r * Math.Pow(1 - i, curvePointsCount - 1 - j) * Math.Pow(i, j));
                    pointB.Y += (int)(curvePoints[j].Y * r * Math.Pow(1 - i, curvePointsCount - 1 - j) * Math.Pow(i, j));
                }

                double x1 = (double)(pointA.X - margin) / (bezierCurvePictureBox.Width - margin) * 499 + 330;
                double y1 = (double)(pointA.Y - margin) / (bezierCurvePictureBox.Height - margin) * 1.8;
                double x2 = (double)(pointB.X - margin) / (bezierCurvePictureBox.Width - margin) * 499 + 330;
                double y2 = (double)(pointB.Y - margin) / (bezierCurvePictureBox.Height - margin) * 1.8;
                CIEXYZ v1 = waveLengths[(int)x1];
                CIEXYZ v2 = waveLengths[(int)x2];
                int h = Math.Abs(pointB.X - pointA.X);
                bezierCurvePoint.X += (v1.X * y1 + v2.X * y2) * h / 2;
                bezierCurvePoint.Y += (v1.Y * y1 + v2.Y * y2) * h / 2;
                bezierCurvePoint.Z += (v1.Z * y1 + v2.Z * y2) * h / 2;

                pointA = pointB;
            }
            bezierCurvePoint.X *= k;
            bezierCurvePoint.Y *= k;
            bezierCurvePoint.Z *= k;
            horseshoePictureBox.Invalidate();
        }
    }
}