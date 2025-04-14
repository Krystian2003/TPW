using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PresentationModel
{
    public class PresentationBall : INotifyPropertyChanged
    {
        private double _x;
        private double _y;

        public double X
        {
            get => _x;
            set
            {
                if (_x != value)
                {
                    _x = value;
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }
        public double Radius { get; }
        public string Color { get; }
        // Dodaj velocity

        public PresentationBall(double x, double y, double radius, string color)
        {
            X = x;
            Y = y;
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
