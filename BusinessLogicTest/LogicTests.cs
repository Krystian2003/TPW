using Xunit;
using BusinessLogic;
using Data;
using System.Numerics;


namespace BusinessLogicTest
{
    public class LogicTests
    {
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