using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{

    #region Singleton

    private static PlatformScript _instance;
    public static PlatformScript Instance => _instance;

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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector2 minScreenBounds = Camera.main.ScreenToWorldPoint(new UnityEngine.Vector2(0, 0));
        UnityEngine.Vector2 maxScreenBounds = Camera.main.ScreenToWorldPoint(new UnityEngine.Vector2(Screen.width, Screen.height));

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new UnityEngine.Vector2(Mathf.Clamp(mousePosition.x, minScreenBounds.x + 1, maxScreenBounds.x - 1), transform.position.y);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Rigidbody2D ballRB = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = collision.contacts[0].point;
            Vector3 platformCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

            float difference = hitPoint.x - platformCenter.x;

            ballRB.AddForce(new Vector2(difference * 200, 0));

            ballRB.velocity = new Vector2(Mathf.Clamp(ballRB.velocity.x, -5, 5), ballRB.velocity.x);
            ballRB.velocity = new Vector2(ballRB.velocity.x, Mathf.Sqrt(60 - Mathf.Pow(ballRB.velocity.x, 2)));
        }
    }
}
