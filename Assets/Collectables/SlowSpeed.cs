using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SlowSpeed : Collectable
{
    // duration in milliseconds
    public int duration;

    public override void removeEffect()
    {
        restoreValues();
    }

    protected async override void ApplyEffect()
    {
        if (PlatformScript.Instance.speedMultiplier == 1)
        {
            PlatformScript.Instance.speedMultiplier = 0.5f;
            foreach (Ball ball in BallsManager.Instance.Balls)
            {
                ball.GetComponent<Rigidbody2D>().velocity *= new Vector2(0.5f, 0.5f);
            }
        }
        SlowTSpeedTimer.lastCollected = this;

        await Task.Delay(duration);

        if (SlowTSpeedTimer.lastCollected == this)
        {
            restoreValues();
        }
        CollectableManager.Instance.Active.Remove(this);
    }

    public void restoreValues()
    {
        PlatformScript.Instance.speedMultiplier = 1;
        foreach (Ball ball in BallsManager.Instance.Balls)
        {
            ball.GetComponent<Rigidbody2D>().velocity *= new Vector2(2, 2);
        }
        StrongerBallTimer.lastCollected = null;
    }
}

public static class SlowTSpeedTimer
{
    public static SlowSpeed lastCollected;
}

