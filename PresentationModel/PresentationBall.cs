using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PresentationModel
{
    public class PresentationBall : INotifyPropertyChanged
    {
        private double _x;
        private double _y;

        public double CanvasLeft => X - Radius;
        public double CanvasTop => Y - Radius;
        public double Diameter => Radius * 2;

        public double X
        {
            get => _x;
            set
            {
                if (_x != value)
                {
                    _x = value;
                    OnPropertyChanged(nameof(X));
                    OnPropertyChanged(nameof(CanvasLeft));
                }
            }
        }

        public double Y
        {
            get => _y;
            set
            {
                if (_y != value)
                {
                    _y = value;
                    OnPropertyChanged(nameof(Y));
                    OnPropertyChanged(nameof(CanvasTop));
                }
            }
        }

        public double Radius { get; }
        public string Color { get; }

        public PresentationBall(double x, double y, double radius, string color)
        {
            _x = x;
            _y = y;
            Radius = radius;
            Color = color;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}