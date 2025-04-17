using PresentationViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Xunit;

namespace PresentationTests
{
    public class ViewModelTests
    {
        [Fact]
        public void SetTableSize_SetsCorrectDimensions()
        {
            // Arrange
            var viewModel = new ViewModel();
            float expectedWidth = 800;
            float expectedHeight = 600;

            // Act
            viewModel.SetTableSize(expectedWidth, expectedHeight);

            // Assert
            // Note: You may need to expose these properties or use a different approach based on your implementation
            Assert.Equal(expectedWidth, viewModel.TableWidth);
            Assert.Equal(expectedHeight, viewModel.TableHeight);
        }

        [Fact]
        public void GenerateBalls_CreatesCorrectNumberOfBalls()
        {
            // Arrange
            var viewModel = new ViewModel();
            viewModel.SetTableSize(800, 600);
            viewModel.BallCount = 5;

            // Act
            viewModel.GenerateBallsCommand.Execute(null);

            // Assert
            Assert.Equal(5, viewModel.Balls.Count);
        }

        [Fact]
        public void GenerateBalls_AllBallsHaveValidPosition()
        {
            // Arrange
            var viewModel = new ViewModel();
            viewModel.SetTableSize(800, 600);
            viewModel.BallCount = 10;

            // Act
            viewModel.GenerateBallsCommand.Execute(null);

            // Assert
            foreach (var ball in viewModel.Balls)
            {
                Assert.True(ball.CanvasLeft >= 0);
                Assert.True(ball.CanvasTop >= 0);
                Assert.True(ball.CanvasLeft + ball.Diameter <= viewModel.TableWidth);
                Assert.True(ball.CanvasTop + ball.Diameter <= viewModel.TableHeight);
            }
        }

        [Fact]
        public void GenerateBalls_BallsHaveDifferentColors()
        {
            // Arrange
            var viewModel = new ViewModel();
            viewModel.SetTableSize(800, 600);
            viewModel.BallCount = 10;

            // Act
            viewModel.GenerateBallsCommand.Execute(null);

            // Assert
            var distinctColorCount = viewModel.Balls.Select(b => b.Color).Distinct().Count();
            Assert.True(distinctColorCount > 1, "Expected balls to have different colors");
        }

        [Fact]
        public void BallCount_NegativeValue_ShouldDefaultToZero()
        {
            // Arrange
            var viewModel = new ViewModel();
            viewModel.SetTableSize(800, 600);

            // Act
            viewModel.BallCount = -5;
            viewModel.GenerateBallsCommand.Execute(null);

            // Assert
            Assert.Equal(0, viewModel.Balls.Count);
        }

        [Fact]
        public void GenerateBallsCommand_Execute_ClearsPreviousBalls()
        {
            // Arrange
            var viewModel = new ViewModel();
            viewModel.SetTableSize(800, 600);
            viewModel.BallCount = 5;
            viewModel.GenerateBallsCommand.Execute(null);

            // Act
            viewModel.BallCount = 3;
            viewModel.GenerateBallsCommand.Execute(null);

            // Assert
            Assert.Equal(3, viewModel.Balls.Count);
        }
    }

    public class BallModelTests
    {
        [Fact]
        public void Constructor_InitializesProperties()
        {
            // Arrange
            float x = 100;
            float y = 200;
            float diameter = 50;
            var color = new SolidColorBrush(Colors.Red);

            // Act
            var ball = new BallModel(x, y, diameter, color);

            // Assert
            Assert.Equal(x, ball.CanvasLeft);
            Assert.Equal(y, ball.CanvasTop);
            Assert.Equal(diameter, ball.Diameter);
            Assert.Equal(color, ball.Color);
        }

        [Fact]
        public void PropertyChanged_FiresWhenPropertiesChange()
        {
            // Arrange
            var ball = new BallModel(100, 200, 50, new SolidColorBrush(Colors.Red));
            var propertiesChanged = new List<string>();
            ball.PropertyChanged += (sender, e) => propertiesChanged.Add(e.PropertyName);

            // Act
            ball.CanvasLeft = 150;
            ball.CanvasTop = 250;
            ball.Diameter = 60;
            ball.Color = new SolidColorBrush(Colors.Blue);

            // Assert
            Assert.Equal(4, propertiesChanged.Count);
            Assert.Contains("CanvasLeft", propertiesChanged);
            Assert.Contains("CanvasTop", propertiesChanged);
            Assert.Contains("Diameter", propertiesChanged);
            Assert.Contains("Color", propertiesChanged);
        }
    }
}