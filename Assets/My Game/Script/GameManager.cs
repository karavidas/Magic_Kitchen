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
        GameOver,
        Beat
    }

    public gameStates gameState = gameStates.Playing;

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
