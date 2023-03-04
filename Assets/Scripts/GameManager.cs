using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;
using Unity.VisualScripting;

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

    public int Level;

    public void GetLevel(int _level)
    {
        print("_level1= " + _level+ " Level1="+Level);
        Level = _level;
        print("_level2= " + _level + " Level2=" + Level);
    }

    private void Start()
    {

        this.lives = this.tmpLives;
        Ball.OnBallDeath += OnBallDeath;
        Physics2D.IgnoreLayerCollision(6, 7);
        Physics2D.IgnoreLayerCollision(6, 6);
        Laser.projectiles.Clear();
    }

    private void OnBallDeath(Ball obj)
    {
        if (BallsManager.Instance.Balls.Count <= 0)
        {
            this.lives--;
            TextManager.Instance.updateLivesText();
            foreach (var collectable in CollectableManager.Instance.Active)
            {
                collectable.removeEffect();
            }

            if (this.lives < 1)
            {
                //Wczytaj ekran przegranej
                SceneManager.LoadSceneAsync("LoseScreen");
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

        resetValues();
        Level++;      
        /*try {*/
        SceneManager.LoadScene("Level" + (Level+1).ToString(), LoadSceneMode.Additive);

        print("Level before unload :"+Level);
        SceneManager.UnloadSceneAsync("Level" + (Level).ToString());
        print("Level before after :" + Level);
        /*}*/
        /* catch
         {
             SceneManager.LoadSceneAsync("WinScreen");
         }*/

    }

    // Resetting values when changing level
    public void resetValues()
    {
        /*Level++;*/
        lives = tmpLives;
        Score = 0;
        PlatformScript.Instance.speedMultiplier = 1;
        TextManager.Instance.updateLivesText();
        TextManager.Instance.updatescoreText();
    }

    private void OnDisable()
    {
        Ball.OnBallDeath -= OnBallDeath;
    }

    
}
