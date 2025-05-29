using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PresentationModel
{
    public sealed class PresentationBall(double x, double y, double radius, string color)
        : INotifyPropertyChanged
    {
        private const double Tolerance = double.Epsilon; 
        
        private double _x = x;
        private double _y = y;
        private double _radius = radius;

        public double CanvasLeft => X - Radius;
        public double CanvasTop => Y - Radius;
        public double Diameter => Radius * 2;

        public double X
        {
            get => _x;
            set
            {
                if (Math.Abs(_x - value) > Tolerance)
                {
                    _x = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CanvasLeft));
                }
            }
        }

        public double Y
        {
            get => _y;
            set
            {
                if (Math.Abs(_y - value) > Tolerance)
                {
                    _y = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CanvasTop));
                }
            }
        }

        public double Radius
        {
            get => _radius;
            set
            {
                if (Math.Abs(_radius - value) > Tolerance)
                {
                    _radius = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Diameter));
                    OnPropertyChanged(nameof(CanvasLeft));
                    OnPropertyChanged(nameof(CanvasTop));
                }
            }
        }

        public string Color { get; } = color;

        public double ReferenceX { get; set; } = x;
        public double ReferenceY { get; set; } = y;
        public double ReferenceRadius { get; set; } = radius;

        public void UpdateScaledValues(double scale)
        {
            X = ReferenceX * scale;
            Y = ReferenceY * scale;
            Radius = ReferenceRadius * scale;
            OnPropertyChanged(nameof(Radius));
            OnPropertyChanged(nameof(Diameter));
            OnPropertyChanged(nameof(CanvasLeft));
            OnPropertyChanged(nameof(CanvasTop));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}