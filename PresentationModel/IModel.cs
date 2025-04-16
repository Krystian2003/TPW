using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationModel
{
    public interface IModel
    {
        public ObservableCollection<PresentationBall> Balls { get; }
        void SetTableSize(float width, float height);
        void AddBall(float x, float y, float vx, float vy, float radius, string color);
    }
}
