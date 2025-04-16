using Data;
using System.Numerics;

namespace DataTest
{
    public class BallRepositoryTests
    {
        [Fact]
        public void AddBallTest()
        {
            var repo = new BallRepository();
            
            Assert.Empty(repo.GetBalls());
            repo.AddBall(10.0f, 20.0f, 5.0f, 6.0f, 15.0f, "Red");
            Assert.Single(repo.GetBalls());
        }

        [Fact]
        public void ClearTest()
        {
            var repo = new BallRepository();
            repo.AddBall(10.0f, 20.0f, 5.0f, 6.0f, 15.0f, "Red");
            Assert.Single(repo.GetBalls());
            repo.Clear();
            Assert.Empty(repo.GetBalls());
        }

        [Fact]
        public void UpdateBallPositionTest()
        {
            var repo = new BallRepository();
            var ball = new Ball(10.0f, 20.0f, 5.0f, 6.0f, 15.0f, "Red");
            repo.AddBall(ball);
            var newPosition = new Vector2(30.0f, 40.0f);
            repo.UpdateBallPosition(ball, newPosition);
            Assert.Equal(newPosition, repo.GetBalls()[0].Position);
        }

        [Fact]
        public void UpdateBallVelocityTest()
        {
            var repo = new BallRepository();
            var ball = new Ball(10.0f, 20.0f, 5.0f, 6.0f, 15.0f, "Red");
            repo.AddBall(ball);
            var newVelocity = new Vector2(7.0f, 8.0f);
            repo.UpdateBallVelocity(ball, newVelocity);
            Assert.Equal(newVelocity, repo.GetBalls()[0].Velocity);
        }
    }
}