using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StrongerBall : Collectable
{
    // duration in milliseconds
    public int duration;

    public override void removeEffect()
    {
        restoreValues();
    }

    protected async override void ApplyEffect()
    {
        StrongerBallTimer.lastCollected = this;
        Ball.damage = 2;
        foreach(Ball ball in BallsManager.Instance.Balls)
        {
            ball.transform.localScale = new Vector3(1.3f, 1.3f, 1);
        }


        await Task.Delay(duration);

        if (StrongerBallTimer.lastCollected == this)
        {
            restoreValues();
        }
        CollectableManager.Instance.Active.Remove(this);
    }

    public void restoreValues()
    {
        Ball.damage = 1;
        foreach (Ball ball in BallsManager.Instance.Balls)
        {
            ball.transform.localScale = new Vector3(1, 1, 1);
        }
        StrongerBallTimer.lastCollected = null;
    }
}

public static class StrongerBallTimer
{
    public static StrongerBall lastCollected;
}
