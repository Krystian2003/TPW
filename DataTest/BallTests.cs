using Xunit;
using Data;
using System.Numerics;

namespace DataTest
{
    public class BallTests
    {
        [Fact]
        public void ConstructorTest()
        {
            Ball ball = new Ball(10.0f, 20.0f, 5.0f, 6.0f, 15.0f, "Red");

            Assert.Equal(10.0f, ball.Position.X);
            Assert.Equal(20.0f, ball.Position.Y);
            Assert.Equal(5.0f, ball.Velocity.X);
            Assert.Equal(6.0f, ball.Velocity.Y);
            Assert.Equal(15.0f, ball.Radius);
            Assert.Equal("Red", ball.Color);
        }

        [Fact]
        public void SetVelocityTest()
        {
            Ball ball = new Ball(10.0f, 20.0f, 5.0f, 6.0f, 15.0f, "Red");
            ball.Velocity = new Vector2(7.0f, 8.0f);
            Assert.Equal(7.0f, ball.Velocity.X);
            Assert.Equal(8.0f, ball.Velocity.Y);
        }

        [Fact]
        public void SetPositionTest()
        {
            Ball ball = new Ball(10.0f, 20.0f, 5.0f, 6.0f, 15.0f, "Red");
            ball.Position = new Vector2(30.0f, 40.0f);
            Assert.Equal(30.0f, ball.Position.X);
            Assert.Equal(40.0f, ball.Position.Y);
        }
    }
}