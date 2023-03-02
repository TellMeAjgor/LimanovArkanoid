using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag=="Platform")
        {
            if(this is StrongerBall || this is WiderPlatform | this is SlowSpeed)
            {
                CollectableManager.Instance.Active.Add(this);
            }
            this.ApplyEffect();
        }

        if(collision.tag == "Platform" || collision.tag == "DeathWall")
        {
            if(CollectableManager.Instance.Spawned.Count>0)
            {
                CollectableManager.Instance.Spawned.Remove(this);
            }
            Destroy(this.gameObject);
        }
    }

    protected abstract void ApplyEffect();
    public abstract void removeEffect();
}
