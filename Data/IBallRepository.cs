using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IBallRepository // Maybe change the name
    {
        IReadOnlyList<Ball> GetBalls();
        void AddBall(Ball ball);
        void Clear();
    }
}
