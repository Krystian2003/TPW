using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationModel
{
    public class PresentationBall
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }
        public string Color { get; set; }

        public PresentationBall(double x, double y, double radius, string color)
        {
            X = x;
            Y = y;
            Radius = radius;
            Color = color;
        }
    }
}
