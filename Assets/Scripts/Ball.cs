using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static event Action<Ball> OnBallDeath;
    public static int damage = 1;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == this.tag)
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());               
        }
    }
    public void Die()
    {
        OnBallDeath?.Invoke(this);
        Destroy(gameObject,1);

    }
}
