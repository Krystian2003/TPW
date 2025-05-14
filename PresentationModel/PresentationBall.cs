using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PresentationModel
{
    public class PresentationBall : INotifyPropertyChanged
    {
        private double _x;
        private double _y;
        private double _radius;

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

        public double Radius
        {
            get => _radius;
            set
            {
                if (_radius != value)
                {
                    _radius = value;
                    OnPropertyChanged(nameof(Radius));
                    OnPropertyChanged(nameof(Diameter));
                    OnPropertyChanged(nameof(CanvasLeft));
                    OnPropertyChanged(nameof(CanvasTop));
                }
            }
        }

        public string Color { get; }

        public double ReferenceX { get; set; }
        public double ReferenceY { get; set; }
        public double ReferenceRadius { get; set; }

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

        public PresentationBall(double x, double y, double radius, string color)
        {
            ReferenceX = x;
            ReferenceY = y;
            ReferenceRadius = radius;
            _x = x;
            _y = y;
            _radius = radius;
            Color = color;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}