using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace linebisectiontest
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        const int FIRST = 160;
        const int SECOND = 480;
        const int THIRD = 800;
        static int snc;
        Random rand = new Random();
        int first_center, second_center, third_center;

        double upx1, downx1, upy1, downy1;
        double first_result, second_result, third_result;
        DateTime start, end;
        public MainWindow()
        {
            InitializeComponent();
            first_center = Draw_line(rand.Next(0,920), FIRST);
            second_center = Draw_line(rand.Next(0, 920), SECOND);
            third_center = Draw_line(rand.Next(0, 920), THIRD);
            start = DateTime.Now;
        }
        private int Draw_line(int x1, int y)//1000픽셀 크기의 직선 그리기
        {
            Line line = new Line();
            line.Stroke = System.Windows.Media.Brushes.Black;
            line.X1 = x1;
            line.X2 = x1+1000;
            line.Y1 = y;
            line.Y2 = y;
            line.HorizontalAlignment = HorizontalAlignment.Left;
            line.VerticalAlignment = VerticalAlignment.Center;
            line.StrokeThickness = 5;
            inkCanvas.Children.Add(line);
            return x1 + 500;
        }
        private void InkCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePoint = Mouse.GetPosition(inkCanvas);
                txtTest.Text = snc.ToString();
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (mousePoint.Y < FIRST && snc == 0)
                {
                    upx1 = mousePoint.X;
                    upy1 = mousePoint.Y;
                }
                if (mousePoint.Y == FIRST && snc == 0)//잡힌 y좌표가 선과 같을때
                {
                    snc = 1;
                    first_result = mousePoint.X - first_center;
                }
                if (mousePoint.Y > FIRST && snc == 0)
                {
                    downx1 = mousePoint.X;
                    downy1 = mousePoint.Y;
                    snc = 1;
                    first_result=(upx1+(downx1 - upx1) * (FIRST - upy1) / (downy1 - upy1))-first_center;
                }
                if (mousePoint.Y < SECOND && snc == 1)
                {
                    upx1 = mousePoint.X;
                    upy1 = mousePoint.Y;
                }
                if (mousePoint.Y == SECOND && snc == 1)//잡힌 y좌표가 선과 같을때
                {
                    snc = 2;
                    second_result = mousePoint.X - second_center;
                }
                if (mousePoint.Y > SECOND && snc == 1)
                {
                    downx1 = mousePoint.X;
                    downy1 = mousePoint.Y;
                    snc = 2;
                    second_result = (upx1 + (downx1 - upx1) * (SECOND - upy1) / (downy1 - upy1)) - second_center;
                }
                if (mousePoint.Y < THIRD && snc == 2)
                {
                    upx1 = mousePoint.X;
                    upy1 = mousePoint.Y;
                }
                if (mousePoint.Y == THIRD && snc == 2)//잡힌 y좌표가 선과 같을때
                {
                    snc = 3;
                    third_result = mousePoint.X - third_center;
                }
                if (mousePoint.Y > THIRD && snc == 2)
                {
                    downx1 = mousePoint.X;
                    downy1 = mousePoint.Y;
                    snc = 3;
                    third_result = (upx1 + (downx1 - upx1) * (THIRD - upy1) / (downy1 - upy1)) - third_center;
                }

            }
            if (snc == 3 && e.LeftButton == MouseButtonState.Released)
            {
                snc = 0;
                end = DateTime.Now;
                MessageBox.Show("첫번째 중심 차이 = "+ ((first_result > 0) ? "우측으로"+first_result : "좌측으로"+ Math.Abs(first_result)) 
                    +"\n두번째 중심 차이 = "+ ((second_result > 0) ? "우측으로" + second_result : "좌측으로" + Math.Abs(second_result)) 
                    + "\n세번째 중심 차이 = "+ ((third_result > 0) ? "우측으로" + third_result : "좌측으로" + Math.Abs(third_result)) 
                    + "\n초과 시간 = "+ (end - start).ToString("ss")+"초");
                double result= (Math.Abs(first_result) + Math.Abs(second_result) + Math.Abs(third_result)) / 3.0f;//평균 편향값
                MessageBox.Show("6.33mm미만 = 정상  6.33mm이상 = 경미한 편측무시  12.5mm이상 = 심한 편측무시\n\t\t측정 값 = "+result*0.26458333+"mm");
            }
        }

    }
}