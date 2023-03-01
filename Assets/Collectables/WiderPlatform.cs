using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class WiderPlatform : Collectable
{
    // duration in milliseconds
    public int duration;

    protected async override void ApplyEffect()
    {
        WiderPlatformTimer.lastCollected = this;
        GameObject platform = GameObject.FindGameObjectWithTag("Platform");

        platform.transform.localScale = new Vector3(1.5f, 1, 1);

        await Task.Delay(duration);

        if (WiderPlatformTimer.lastCollected == this)
        {
            restoreValues();
        }
    }

    public void restoreValues()
    {
        GameObject platform = GameObject.FindGameObjectWithTag("Platform");
        platform.transform.localScale = new Vector3(1, 1, 1);
    }

    public override void removeEffect()
    {
        restoreValues();
    }
}

public static class WiderPlatformTimer
{
    public static WiderPlatform lastCollected;
} 
