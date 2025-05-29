using PresentationModel;

namespace PresentationModelTest
{
    public class PresentationBallTests
    {
        [Fact]
        public void ConstructorTest()
        {
            var ball = new PresentationBall(10.0f, 20.0f, 15.0f, "Red");

            Assert.Equal(10.0f, ball.X);
            Assert.Equal(20.0f, ball.Y);
            Assert.Equal(15.0f, ball.Radius);
            Assert.Equal("Red", ball.Color);
        }

        [Fact]
        public void PropertyXChangedTest()
        {
            var ball = new PresentationBall(10.0f, 20.0f, 15.0f, "Red");
            string? changedPropertyName = null;
            ball.PropertyChanged += (_, e) =>
            {
                changedPropertyName = e.PropertyName;
            };
            ball.X = 30.0f;
            Assert.Equal(30.0f, ball.X);
            Assert.NotNull(changedPropertyName);
            Assert.Equal("CanvasLeft", changedPropertyName);
        }

        [Fact]
        public void PropertyYChangedTest()
        {
            var ball = new PresentationBall(10.0f, 20.0f, 15.0f, "Red");
            string? changedPropertyName = null;
            ball.PropertyChanged += (_, e) =>
            {
                changedPropertyName = e.PropertyName;
            };
            ball.Y = 30.0f;
            Assert.Equal(30.0f, ball.Y);
            Assert.NotNull(changedPropertyName);
            Assert.Equal("CanvasTop", changedPropertyName);
        }
    }
}
