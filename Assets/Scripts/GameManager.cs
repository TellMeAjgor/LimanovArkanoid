using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Singleton

    private static GameManager _instance;
    public static GameManager Instance=>_instance;

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    public bool isGameStarted { get;set; }

    public int lives { get; set; }
    public int tmpLives = 3;

    private void Start()
    {
        this.lives = this.tmpLives;
        Ball.OnBallDeath += OnBallDeath;
    }

    private void OnBallDeath(Ball obj)
    {
        if(BallsManager.Instance.Balls.Count <= 0)
        {
            this.lives--;

            if(this.lives<1)
            {
                //Wczytaj ekran przegranej
            }
            else
            {
                BallsManager.Instance.ResetBalls();
                isGameStarted = false;
                //Wczytaj ponownie poziom
            }
        }
    }

    private void OnDisable()
    {
        Ball.OnBallDeath -= OnBallDeath;
    }

}
