using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region Singleton

    private static GameManager _instance;
    public static GameManager Instance => _instance;

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



    public bool isGameStarted { get; set; }
    public int Score { get; set; }

    public int lives { get; set; }
    public int tmpLives = 3;

    public int Level = 1;

    private void Start()
    {
        this.lives = this.tmpLives;
        Ball.OnBallDeath += OnBallDeath;
    }

    private void OnBallDeath(Ball obj)
    {
        if (BallsManager.Instance.Balls.Count <= 0)
        {
            this.lives--;

            if (this.lives < 1)
            {
                //Wczytaj ekran przegranej
            }
            else
            {
                ResetBallPosition();
                //Wczytaj ponownie poziom
            }
        }
    }

    private void ResetBallPosition()
    {
        BallsManager.Instance.ResetBalls();
        isGameStarted = false;
    }

    public void LoadNextLevel()
    {
        ResetBallPosition();
        CollectableManager.Instance.ResetCollectables();

        Level++;
        SceneManager.LoadScene("Level" + Level.ToString(), LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Level" + (Level - 1).ToString());
    }

    private void OnDisable()
    {
        Ball.OnBallDeath -= OnBallDeath;
    }

}
