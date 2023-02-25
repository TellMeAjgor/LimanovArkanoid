using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallsManager : MonoBehaviour
{

    #region Singleton

    private static BallsManager _instance;
    public static BallsManager Instance => _instance;

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

    [SerializeField]
    private Ball ballPrefab;
     
    private Ball initialBall;

    private Rigidbody2D initialBallRb;

    private bool gameStarted = false;

    public float initialBallSpeed = 250;
    
    public List<Ball> Balls { get; set; }
  

    private void Start()
    {
        InitBall();
    }

    private void Update()
    {
        if(!GameManager.Instance.isGameStarted)
        {
            Vector3 platformPosition = PlatformScript.Instance.gameObject.transform.position;
            Vector3 ballPosition = new Vector3(platformPosition.x, platformPosition.y+0.45f, 0);
            initialBall.transform.position = ballPosition;
        } 
        
        if(Input.GetMouseButtonDown(0) && !GameManager.Instance.isGameStarted)
        {
            initialBallRb.isKinematic = false;
            initialBallRb.AddForce(new Vector2(2, initialBallSpeed));
            GameManager.Instance.isGameStarted = true;
            
        }
    }

    public void ResetBalls()
    {
        foreach (var ball in this.Balls.ToList())
        {
            Destroy(ball.gameObject);
        }

        InitBall();
    }

    private void InitBall()
    {
        Vector3 platformPosition = PlatformScript.Instance.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(platformPosition.x, platformPosition.y+0.45f, 0);
        initialBall = Instantiate(ballPrefab, startingPosition,Quaternion.identity);
        initialBallRb = initialBall.GetComponent<Rigidbody2D>();

        this.Balls = new List<Ball>
        {
            initialBall
        };
    }
}

 