using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdditionalLife : Collectable
{
    protected override void ApplyEffect()
    {
        GameManager.Instance.lives++;
        TextManager.Instance.updateLivesText();
    }
}
