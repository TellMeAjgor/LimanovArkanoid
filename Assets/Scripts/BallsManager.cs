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
    public Ball ballPrefab;
     
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

        if(PlatformScript.Instance.ballCatched)
        {               
                Vector3 platformPosition = PlatformScript.Instance.gameObject.transform.position;
                Vector3 ballPosition = new Vector3(platformPosition.x, platformPosition.y + 0.45f, 0);
                PlatformScript.Instance.catchedBall.transform.position = ballPosition;

                if (Input.GetMouseButtonDown(0) && PlatformScript.Instance.ballCatched)
                {
                    Rigidbody2D catchedBallRb = PlatformScript.Instance.catchedBall.GetComponent<Rigidbody2D>();
                    catchedBallRb.isKinematic = false;
                    catchedBallRb.AddForce(new Vector2(2, BallsManager.Instance.initialBallSpeed));
                    PlatformScript.Instance.ballCatched = false;               
                }
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
        Vector3 startingPosition = new Vector3(platformPosition.x, platformPosition.y+10f, 0);
        initialBall = Instantiate(ballPrefab, startingPosition,Quaternion.identity);
        initialBallRb = initialBall.GetComponent<Rigidbody2D>();

        this.Balls = new List<Ball>
        {
            initialBall
        };
    }

    public void SpawnBalls(Vector3 position, int count)
    {
        bool biggerBalls = isBiggerBallsActivated();

        for (int i = 0; i < count; i++)
        {
            Ball spawnedBall = Instantiate(ballPrefab, position, Quaternion.identity) as Ball;

            if (biggerBalls)
            {
                spawnedBall.transform.localScale = new Vector3(1.3f, 1.3f, 1);
            }

            Rigidbody2D spawnedBallRb = spawnedBall.GetComponent<Rigidbody2D>();
            spawnedBallRb.isKinematic = false;
            spawnedBallRb.AddForce(new Vector2(0, initialBallSpeed * PlatformScript.Instance.speedMultiplier));
            this.Balls.Add(spawnedBall);
        }
    }

    private bool isBiggerBallsActivated()
    {
        foreach (Collectable powerup in CollectableManager.Instance.Active)
        {
            if (powerup is StrongerBall) return true;
        }
        return false;
    }
}

