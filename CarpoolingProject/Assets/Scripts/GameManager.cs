/*
 * Benjamin Schuster
 * Carpool Sim [Project 4+]
 * Manages menu setings
 */
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //to keep track of score count and fuel usage
    public static int score = 0;
    public static float usedFuel = 0;

    public GameObject pauseMenu;
    public bool isPaused = false;

    //endgame conditions
    public GameObject winScreen;
    public GameObject loseScreen;

    public static string CurrentLevelName = "MainMenu";

    public static GameManager instance;

    private GameObject[] friendList;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //Make sure game manager is global
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("Trying to instantiate second singleton");
        }
    }
    
    private void Update()
    {
        //pauses and resumes on P press. Cannot pause on main menu screen
        if (Input.GetKeyDown(KeyCode.P) && !isPaused && !CurrentLevelName.Equals("MainMenu"))
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.P) && isPaused && !CurrentLevelName.Equals("MainMenu"))
        {
            UnPause();
        }

        if (isPaused)
            Time.timeScale = 0f;
    }

    //load and unload levels
    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to load: " + levelName);
            return;
        }
        CurrentLevelName = levelName;
    }

    public void UnLoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to unload: " + levelName);
        }
        CurrentLevelName = "MainMenu";
    }

    public void UnloadCurrentLevel()
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(CurrentLevelName);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to unload: " + CurrentLevelName);
        }
        CurrentLevelName = "MainMenu";
    }

    public void ReloadCurrentLevel()
    {
        usedFuel = 0f;
        AsyncOperation ao = SceneManager.UnloadSceneAsync(CurrentLevelName);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to unload: " + CurrentLevelName);
        }

        LoadLevel(CurrentLevelName);
        UnPause();
    }

    //pausing and unpausing
    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        isPaused = true;
    }
    public void UnPause()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    //win conditions
    public static void outOfFuel()
    {
        Debug.Log("Lose");
        GameManager.instance.isPaused = true;
        Time.timeScale = 0f;
        GameManager.instance.loseScreen.SetActive(true);
    }
    public static void reachedEndGoal()
    {
        Debug.Log("you win");
        GameManager.instance.isPaused = true;
        Time.timeScale = 0f;
        GameManager.instance.winScreen.SetActive(true);
    }
}
