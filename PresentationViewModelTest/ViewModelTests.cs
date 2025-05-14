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
            viewModel.BallCount = 1;
            viewModel.GenerateBallsCommand.Execute(null);

            Assert.Equal(1, mockModel.AddBallCallCount);
            Assert.NotEmpty(mockModel.Balls);
        }

        [Fact]
        public void GenerateBallsWithinCanvasBounds()
        {
            var mockModel = new MockModel();
            mockModel.SetTableSize(800, 400);
            var viewModel = new ViewModel(mockModel);
            viewModel.BallCount = 5;
            viewModel.GenerateBallsCommand.Execute(null);

            foreach (var ball in mockModel.Balls)
            {
                Assert.InRange(ball.X, 0, mockModel.GetCanvasWidth());
                Assert.InRange(ball.Y, 0, mockModel.GetCanvasHeight());
            }
        }

        [Fact]
        public void GenerateBallsValidColors()
        {
            var mockModel = new MockModel();
            var viewModel = new ViewModel(mockModel);
            var validColors = new[] { "Red", "Blue", "Green", "Yellow", "Purple", "DefaultColor" };
            viewModel.BallCount = 5;
            viewModel.GenerateBallsCommand.Execute(null);

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