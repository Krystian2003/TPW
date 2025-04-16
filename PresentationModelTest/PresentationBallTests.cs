using PresentationModel;

namespace PresentationModelTest
{
    public class PresentationBallTest
    {
        [Fact]
        private void ConstructorTest()
        {
            var ball = new PresentationBall(10.0f, 20.0f, 15.0f, "Red");

            Assert.Equal(10.0f, ball.X);
            Assert.Equal(20.0f, ball.Y);
            Assert.Equal(15.0f, ball.Radius);
            Assert.Equal("Red", ball.Color);
        }

        [Fact]
        private void PropertyXChangedTest()
        {
            var ball = new PresentationBall(10.0f, 20.0f, 15.0f, "Red");
            bool eventRaisedX = false;
            bool eventRaisedY = false;
            ball.PropertyChanged += (s, e) => eventRaisedX = true;
            ball.X = 30.0f;
            Assert.True(eventRaisedX);
            Assert.False(eventRaisedY);
        }

        [Fact]
        private void PropertyYChangedTest()
        {
            var ball = new PresentationBall(10.0f, 20.0f, 15.0f, "Red");
            bool eventRaisedX = false;
            bool eventRaisedY = false;
            ball.PropertyChanged += (s, e) => eventRaisedY = true;
            ball.Y = 30.0f;
            Assert.True(eventRaisedY);
            Assert.False(eventRaisedX);
        }
    }
}
