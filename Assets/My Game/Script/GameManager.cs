using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Static variable GameManager
    public static GameManager instance;

    // GameState variable
    public enum gameStates
    {
        Playing,
        Pause,
        GameOver,
        Beat
    }

    public gameStates gameState = gameStates.Playing;

    [System.Serializable]
    public struct Level {
        public int scoreTarget;
        public float ratio;
    }

    public Level[] Levels;

    // Use this for initialization for components and Physics
    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        UIManager.instance.ShowStartUI();
    }
	
    // Update is called once per frame
    void Update()
    {
        if (gameState == gameStates.GameOver)
        {
            End();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause() {
        if (gameState == gameStates.Playing)
        {
            gameState = gameStates.Pause;
        }
        else
        {
            gameState = gameStates.Playing;
        }
    }

    public void Playing()
    {
        gameState = gameStates.Playing;
    }

    public float RatioLevel() {

        int indexRatio = 0;
        for (int i = Levels.Length -1; i >= 0; i--)
        {
            if (PlayerStats.instance.Gold >= Levels[i].scoreTarget)
            {
                indexRatio = i;
                break;
            }
        }

        return Levels[indexRatio].ratio;
    }

    // Activate UI
    public void End()
    {
        UIManager.instance.ShowEndUI();
        SpawnManager.instance.StopSpawning();
    }

    // Reload this scene
    public void ReloadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    // Quit the applcation
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}
