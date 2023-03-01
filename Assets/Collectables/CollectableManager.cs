using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    #region Singleton

    private static CollectableManager _instance;
    public static CollectableManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    public List<Collectable> Available;
    public List<Collectable> Spawned;

    [Range(0, 100)]
    public float Chance;

    public void ResetCollectables()
    {
        foreach (var collectable in this.Spawned.ToList())
        {
            collectable.removeEffect();
            Destroy(collectable.gameObject);
        }

        Spawned.Clear();
    }
}
