using Xunit;
using BusinessLogic;
using Data;
using System.Numerics;


namespace BusinessLogicTest
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
        }

        [Fact]
        public void UpdateBallPositionTest()
        {
            var logic = new Logic();
            logic.AddBallAsync(10.0f, 20.0f, 5.0f, 6.0f, 15.0f, "Red").Wait();

            logic.UpdateBallPositions(1.0f);

            var balls = logic.GetBallsData().ToList();
            Assert.Equal(new Vector2(15.0f, 26.0f), balls[0].Position);
        }

        [Fact]
        public void RightWallCollisionTest()
        {
            var logic = new Logic();
            logic.SetTableSize(100.0f, 100.0f);
            logic.AddBallAsync(95.0f, 50.0f, 10.0f, 0.0f, 5.0f, "Red").Wait();

            logic.UpdateBallPositions(1.0f);

            var balls = logic.GetBallsData().ToList();
            Assert.Equal(new Vector2(95.0f, 50.0f), balls[0].Position);
            Assert.Equal(new Vector2(-10.0f, 0.0f), balls[0].Velocity);
        }

        [Fact]
        public void TwoBallsCollisionTest()
        {
            var logic = new Logic();
            logic.SetTableSize(200.0f, 200.0f);

            logic.AddBallAsync(50.0f, 50.0f, 5.0f, 0.0f, 10.0f, "Red").Wait();
            logic.AddBallAsync(70.0f, 50.0f, -5.0f, 0.0f, 10.0f, "Blue").Wait();

            logic.UpdateBallPositions(1.0f);

            var balls = logic.GetBallsData().ToList();
            Assert.Equal(new Vector2(-5.0f, 0.0f), balls[0].Velocity);
            Assert.Equal(new Vector2(5.0f, 0.0f), balls[1].Velocity);
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
        }

        //    [Fact]
        //    private void AddBallsTest()
        //    {
        //        var mockRepo = new MockBallRepository();
        //        var logic = new Logic();

        //        Assert.Empty(mockRepo.GetBalls());
        //        logic.AddBall(10.0f, 20.0f, 5.0f, 6.0f, 15.0f, "Red");
        //        Assert.Single(mockRepo.GetBalls());
        //        Assert.Equal(10.0f, mockRepo.GetBalls()[0].Position.X);
        //        Assert.Equal(20.0f, mockRepo.GetBalls()[0].Position.Y);
        //        Assert.Equal(5.0f, mockRepo.GetBalls()[0].Velocity.X);
        //        Assert.Equal(6.0f, mockRepo.GetBalls()[0].Velocity.Y);
        //        Assert.Equal(15.0f, mockRepo.GetBalls()[0].Radius);
        //        Assert.Equal("Red", mockRepo.GetBalls()[0].Color);
        //    }

        //    [Fact] 
        //    private void UpdateBallPositionsTest()
        //    {
        //        var mockRepo = new MockBallRepository();
        //        var logic = new Logic(mockRepo);
        //        logic.AddBall(10.0f, 20.0f, 5.0f, 6.0f, 15.0f, "Red");
        //        logic.UpdateBallPositions(1.0f);
        //        Assert.Equal(15.0f, mockRepo.GetBalls()[0].Position.X);
        //        Assert.Equal(26.0f, mockRepo.GetBalls()[0].Position.Y);
        //    }

        //    [Fact]
        //    private void EdgeCollisionTest()
        //    {
        //        var mockRepo = new MockBallRepository();
        //        var logic = new Logic(mockRepo);
        //        logic.SetTableSize(100, 100);
        //        logic.AddBall(95.0f, 50.0f, 10.0f, 0.0f, 5.0f, "Red");
        //        logic.UpdateBallPositions(1.0f);
        //        Assert.Equal(105.0f, mockRepo.GetBalls()[0].Position.X);
        //        Assert.Equal(50.0f, mockRepo.GetBalls()[0].Position.Y);
        //        Assert.Equal(-10.0f, mockRepo.GetBalls()[0].Velocity.X);
        //    }

        //    [Fact]
        //    private void TimerElapsedTest()
        //    {
        //        var mockRepo = new MockBallRepository();
        //        var logic = new Logic(mockRepo);
        //        bool eventRaised = false;
        //        logic.PositionsUpdated += (s, e) => eventRaised = true;
        //        logic.OnTimerElapsed(null, null);
        //        Assert.True(eventRaised);
        //    }

        //    [Fact]
        //    private void SetTableSizeTest()
        //    {
        //        var mockRepo = new MockBallRepository();
        //        var logic = new Logic(mockRepo);
        //        logic.SetTableSize(200, 100);
        //        Assert.Equal(new Vector2(200, 100), logic.TableSize);
        //    }

        //    [Fact]
        //    private void SetTableSizeTestNegative()
        //    {
        //        var mockRepo = new MockBallRepository();
        //        var logic = new Logic(mockRepo);
        //        Assert.Throws<ArgumentException>(() => logic.SetTableSize(-200, 100));
        //        Assert.Throws<ArgumentException>(() => logic.SetTableSize(200, -100));
        //    }
    }
}