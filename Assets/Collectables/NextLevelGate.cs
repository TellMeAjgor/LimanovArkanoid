using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NextLevelGate : Collectable
{
    public GameObject portal;
    public GameObject createdPortal;

    public override void removeEffect()
    {
        restoreValues();
    }

    protected async override void ApplyEffect()
    {
        if (NextLevelGateUsed.nlg_object is NextLevelGate) return;
        NextLevelGateUsed.nlg_object = this;

        float pos_y = Random.Range(-2f, 3f);
        createdPortal = Instantiate(portal, new Vector2(9f, pos_y), Quaternion.identity);
    }

    public void restoreValues()
    {
        NextLevelGateUsed.nlg_object = null;
        Destroy(createdPortal);
    }
}

public static class NextLevelGateUsed
{
    public static NextLevelGate nlg_object;
}
