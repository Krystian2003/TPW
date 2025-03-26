using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using BusinessLogic;

namespace PresentationView
{
    public partial class MainWindow : Window
    {
        private readonly Logic logic = new();

        public MainWindow()
        {
            InitializeComponent();
            logic.InitializeBalls();
            DrawBalls();
        }

        private void DrawBalls()
        {
            canvas.Children.Clear();
            foreach (var ball in logic.GetBallsData())
            {
                Ellipse ellipse = new Ellipse
                {
                    Width = ball.Radius * 2,
                    Height = ball.Radius * 2,
                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ball.Color)),
                };
                Canvas.SetLeft(ellipse, ball.X - ball.Radius);
                Canvas.SetTop(ellipse, ball.Y - ball.Radius);
                canvas.Children.Add(ellipse);
            }
        }
    }
}