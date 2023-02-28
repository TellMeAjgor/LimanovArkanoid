using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdditionalLife : Collectable
{
    protected override void ApplyEffect()
    {
        if (GameManager.Instance.lives < 5)
        {
            GameManager.Instance.lives++;
        }
        TextManager.Instance.updateLivesText();
    }
}
