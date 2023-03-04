using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdditionalLife : Collectable
{
    public override void removeEffect()
    {
        
    }

    protected override void ApplyEffect()
    {
        if (GameManager.Instance.lives < 5)
        {
            GameManager.Instance.changeLives(GameManager.Instance.lives + 1);
        }
    }
}
