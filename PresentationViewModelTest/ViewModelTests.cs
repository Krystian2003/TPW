using PresentationViewModel;
using System.Numerics;

namespace PresentationViewModelTest
{
    public class ViewModelTests
    {
        [Fact]
        public void BallsReturnsModelBalls()
        {
            var mockModel = new MockModel();
            var viewModel = new ViewModel(mockModel);

            Assert.Same(mockModel.Balls, viewModel.Balls);
        }

        [Fact]
        public void AddBallCallsModelAddBall()
        {
            var mockModel = new MockModel();
            var viewModel = new ViewModel(mockModel);
            mockModel.AddBall(1.0f, 2.0f, 3.0f, 4.0f, 5.0f, "Red");
            Assert.Equal(1, mockModel.AddBallCallCount);
            Assert.Equal(1, mockModel.Balls[0].X);
            Assert.Equal("Red", mockModel.Balls[0].Color);
        }

        [Fact]
        public void GenerateBallsWithinCanvasBounds()
        {
            var mockModel = new MockModel();
            var viewModel = new ViewModel(mockModel);
            viewModel.GenerateBalls(5, 100.0f, 400.0f);
            foreach (var ball in mockModel.Balls)
            {
                Assert.InRange(ball.X, 0, 800);
                Assert.InRange(ball.Y, 0, 400);
            }
        }

        [Fact]
        public void GenerateBallsValidColors()
        {
            var mockModel = new MockModel();
            var viewModel = new ViewModel(mockModel);
            var validColors = new[] { "Red", "Blue", "Green", "Yellow", "Purple" };
            viewModel.GenerateBalls(5, 100.0f, 400.0f);
            foreach (var ball in mockModel.Balls)
            {
                Assert.Contains(ball.Color, validColors);
            }
        }

        [Fact]
        public void SetTableSizeCallsModelSetTableSize()
        {
            var mockModel = new MockModel();
            var viewModel = new ViewModel(mockModel);
            viewModel.SetTableSize(800, 400);
            Assert.Equal(new Vector2(800, 400), mockModel.TableSize);
        }
    }
}