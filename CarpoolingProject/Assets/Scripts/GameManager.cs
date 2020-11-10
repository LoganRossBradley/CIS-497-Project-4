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
    public bool gameOverWin = false;
    public bool gameOverLose = false;

    private string CurrentLevelName = "MainMenu";

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
        AsyncOperation ao = SceneManager.UnloadSceneAsync(CurrentLevelName);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to unload: " + CurrentLevelName);
        }
        LoadLevel(CurrentLevelName);
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
        Debug.Log("ran out of fuel");
    }
    public static void reachedEndGoal()
    {
        Debug.Log("you win");
    }
}
