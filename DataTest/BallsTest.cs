using Xunit;
using Data;

namespace DataTest
{
    public class BallsTest
    {
        [Fact]
        public void Test1()
        {
            Ball ball = new Ball(200, 100, 15.3, "red");

            Assert.Equal(200, ball.X);
            Assert.Equal(100, ball.Y);
            Assert.Equal(15.3, ball.Radius);
            Assert.Equal("red", ball.Color); 
        }
    }
}