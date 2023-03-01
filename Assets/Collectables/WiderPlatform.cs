using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class WiderPlatform : Collectable
{
    // duration in millisecondc
    public int duration;

    protected async override void ApplyEffect()
    {
        WiderPlatformTimer.lastCollected = this;
        GameObject platform = GameObject.FindGameObjectWithTag("Platform");

        platform.transform.localScale = new Vector3(1.5f, 1, 1);

        await Task.Delay(duration);

        if (WiderPlatformTimer.lastCollected == this)
        {
            platform.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}

public static class WiderPlatformTimer
{
    public static WiderPlatform lastCollected;
} 
