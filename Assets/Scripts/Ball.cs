using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static event Action<Ball> OnBallDeath;
    public static int damage = 1;

    public Vector2 Velocity;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == this.tag || collision.gameObject.tag == "Bullet")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());               
        }
        if (collision.gameObject.tag == "Portal")
        {
            GameManager.Instance.LoadNextLevel();
        }
        if (collision.gameObject.tag == "Block" || collision.gameObject.tag == "Platform")
        {
            _audioSource.Play();
        }
    }

    public void Die()
    {
        OnBallDeath?.Invoke(this);
        Destroy(gameObject,1);

    }
}
