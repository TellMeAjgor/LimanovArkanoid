using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Start is called before the first frame update
    public int HitPoints = 1;
    public int Points = 1;


    public ParticleSystem DestroyEffect;

    public static event Action<Block> OnBrickDestruction;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        ApplyCollisionLogic(ball);
    }

    private void ApplyCollisionLogic(Ball ball)
    {
        this.HitPoints--;

        if (this.HitPoints <= 0)
        {
            OnBrickDestruction?.Invoke(this);
            OnBrickDestroy();
            SpawnDestroyEffect();
            if (this.tag == "SilverBlock")
            {
                GameManager.Instance.Score += this.Points*GameManager.Instance.Level;
            }
            else
            {
                GameManager.Instance.Score += this.Points;
            }

            TextManager.Instance.updatescoreText();
            
            Destroy(this.gameObject);

            if(LevelFinished())
            {
                GameManager.Instance.LoadNextLevel();
            }
            
        }

    }
    
    private bool LevelFinished()
    {
        
        if(GameObject.FindGameObjectsWithTag("Block").Length > (this.tag=="Block"?1:0) || GameObject.FindGameObjectsWithTag("SilverBlock").Length > (this.tag == "SilverBlock" ? 1 : 0))
        {
            return false;
        }
        return true;
    }

    private void SpawnDestroyEffect()
    {
        Vector3 brickPos = gameObject.transform.position;
        Vector3 spawnPosition = new Vector3(brickPos.x, brickPos.y, brickPos.z - 0.2f);
        GameObject effect = Instantiate(DestroyEffect.gameObject, spawnPosition, Quaternion.identity);
        Destroy(effect, DestroyEffect.main.startLifetime.constant);
    }

    public void OnBrickDestroy()
    {
        float spawnChance = UnityEngine.Random.Range(0, 100f);


        if(spawnChance<CollectableManager.Instance.Chance)
        {
            Collectable buff = this.SpawnCollectable();
        }
    }

    private Collectable SpawnCollectable()
    {
        List<Collectable> collection;

        collection = CollectableManager.Instance.Available;

        int index= UnityEngine.Random.Range(0, collection.Count);
        Collectable prefab = collection[index];
        Collectable newCollectable = Instantiate(prefab, this.transform.position, Quaternion.identity) as Collectable;

        Rigidbody2D newCollectableRb = newCollectable.GetComponent<Rigidbody2D>();
        newCollectableRb.AddForce(new Vector2(0, -100));
        CollectableManager.Instance.Spawned.Add(newCollectable);

        return newCollectable;
    }


}
