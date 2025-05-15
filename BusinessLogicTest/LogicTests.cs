using Logic;
using System.Numerics;

namespace LogicTest
{
    public class LogicTests
    {
        [Fact]
        public async Task AddBallTest()
        {
            var logic = new Logic();
            await logic.AddBallAsync(10.0f, 20.0f, 5.0f, 6.0f, 15.0f, "Red");

            var balls = logic.GetBallsData().ToList();
            Assert.Single(balls);
            Assert.Equal(new Vector2(10, 20), balls[0].Position);
            Assert.Equal(new Vector2(5, 6), balls[0].Velocity);
            Assert.Equal(15.0f, balls[0].Radius);
            Assert.Equal("Red", balls[0].Color);
        }

        [Fact]
        public async Task UpdateBallPositionTest()
        {
            var logic = new Logic();
            await logic.AddBallAsync(10.0f, 20.0f, 5.0f, 6.0f, 15.0f, "Red");

            logic.UpdateBallPositions(1.0f); 

            var balls = logic.GetBallsData().ToList();
            Assert.Equal(new Vector2(15.0f, 26.0f), balls[0].Position);
        }

        [Fact]
        public async Task RightWallCollisionTest()
        {
            var logic = new Logic();
            logic.SetTableSize(100.0f, 100.0f);
            await logic.AddBallAsync(95.0f, 50.0f, 10.0f, 0.0f, 5.0f, "Red");

            logic.UpdateBallPositions(1.0f);

            var balls = logic.GetBallsData().ToList();
            Assert.Equal(95.0f, balls[0].Position.X, 1);
            Assert.Equal(50.0f, balls[0].Position.Y, 1);
            Assert.Equal(-10.0f, balls[0].Velocity.X, 1);
            Assert.Equal(0.0f, balls[0].Velocity.Y, 1);
        }

        [Fact]
        public async Task TwoBallsCollisionTest()
        {
            var logic = new Logic();
            logic.SetTableSize(200.0f, 200.0f);

            await logic.AddBallAsync(50.0f, 50.0f, 5.0f, 0.0f, 10.0f, "Red");
            await logic.AddBallAsync(70.0f, 50.0f, -5.0f, 0.0f, 10.0f, "Blue");
            
            logic.UpdateBallPositions(0.1f);

            var balls = logic.GetBallsData().ToList();
            Assert.Equal(-5.0f, balls[0].Velocity.X, 1); 
            Assert.Equal(0.0f, balls[0].Velocity.Y, 1);
            Assert.Equal(5.0f, balls[1].Velocity.X, 1);
            Assert.Equal(0.0f, balls[1].Velocity.Y, 1);

            Assert.Equal(49.5f, balls[0].Position.X, 0);
            Assert.Equal(70.0f, balls[1].Position.X, 1);
        }

        [Fact]
        public void SetTableSizeTest()
        {
            var logic = new Logic();
            logic.SetTableSize(200.0f, 150.0f);

            Assert.Equal(new Vector2(200.0f, 150.0f), logic.TableSize);
        }

        [Fact]
        public void SetTableSizeNegativeTest()
        {
            var logic = new Logic();
            Assert.Throws<ArgumentException>(() => logic.SetTableSize(-100.0f, 50.0f));
            Assert.Throws<ArgumentException>(() => logic.SetTableSize(100.0f, -50.0f));
        }
    }
}