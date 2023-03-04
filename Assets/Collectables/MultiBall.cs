using System.Collections.Generic;
using System.Linq;

public class MultiBall : Collectable
{
    public override void removeEffect()
    {

    }

    protected override void ApplyEffect()
    {
        List<Ball> balls = BallsManager.Instance.Balls.ToList();

        if (balls.Count > 10) return;

        foreach (Ball ball in balls)
        {
            BallsManager.Instance.SpawnBalls(ball.gameObject.transform.position, 2);
        }
    }
}