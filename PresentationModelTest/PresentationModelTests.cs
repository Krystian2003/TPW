using PresentationModel;
using System.Numerics;

namespace PresentationModelTest
{
    public class PresentationModelTests
    {
        [Fact]
        public void ConstructorInitializeBallsTest()
        {
            var mockLogic = new MockLogic();
            mockLogic.BallsData.Add(
                (new Vector2(100, 200), new Vector2(5, -5), 10, "Red")
            );

            var model = new Model(mockLogic);

            Assert.Single(model.Balls);
            Assert.Equal(100, model.Balls[0].X);
            Assert.Equal(200, model.Balls[0].Y);
            Assert.Equal(10, model.Balls[0].Radius);
            Assert.Equal("Red", model.Balls[0].Color);
        }

        [Fact]
        public void ConstructorStartsLogic()
        {
            var mockLogic = new MockLogic();
            Assert.True(mockLogic.StartCalled);
        }

        [Fact]
        public void PositionsUpdatedUpdatesBalls()
        {
            var mockLogic = new MockLogic();
            mockLogic.BallsData.Add(
                (new Vector2(100, 200), new Vector2(5, -5), 10, "Red")
            );

            var model = new Model(mockLogic);
            var ball = model.Balls[0];

            mockLogic.BallsData[0] = (new Vector2(150, 250), new Vector2(5, -5), 10, "Red");

            mockLogic.RaisePositionsUpdated();

            Assert.Equal(150, ball.X);
            Assert.Equal(250, ball.Y);
        }

        [Fact]
        public void SetTableSizeUpdatesLogicTableSize()
        {
            var mockLogic = new MockLogic();
            var model = new Model(mockLogic);

            model.SetTableSize(1000, 500);

            Assert.Equal(1000, mockLogic.TableSize.X);
            Assert.Equal(500, mockLogic.TableSize.Y);
        }
    }
}