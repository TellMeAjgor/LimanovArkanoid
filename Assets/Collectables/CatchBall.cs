using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBall : Collectable
{
    public override void removeEffect()
    {
        PlatformScript.Instance.catchedBall = null;
        PlatformScript.Instance.catchIsAvailable = false;
        PlatformScript.Instance.ballCatched = false;
    }

    protected override void ApplyEffect()
    {
        PlatformScript.Instance.catchIsAvailable = true;
    }
}
