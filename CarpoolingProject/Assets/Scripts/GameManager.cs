/*
 * Benjamin Schuster
 * Carpool Sim [Project 4+]
 * Manages menu setings
 */
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    //to keep track of score count and fuel usage
    public static int levelsCompleted = 0;
    public static int score = 0;
    public static float usedFuel = 0;

    public GameObject pauseMenu;
    public bool isPaused = false;

    //endgame conditions
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject statsText;

    public static string CurrentLevelName = "MainMenu";

    public static GameManager instance;


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
        {
            Time.timeScale = 0f;
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

    public void loadRandomLevel()
    {
        //int nextLevel = Random.Range(0, 2);

        UnloadCurrentLevel();

        LoadLevel("Level 1");

        //uncomment this and above random when level 2 is ready
        //if (nextLevel == 0)
        //{
        //    LoadLevel("Level 1");
        //}
        //else if(nextLevel == 1)
        //{
        //    LoadLevel("Level 2");
        //}
        
        UnPause();
    }

    public void UnLoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        //GameManager.instance.endLevelStats.SetActive(false);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to unload: " + levelName);
        }
        CurrentLevelName = "MainMenu";
    }

    public void UnloadCurrentLevel()
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(CurrentLevelName);
        
        //GameManager.instance.endLevelStats.SetActive(false);
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
        //GameManager.instance.endLevelStats.SetActive(false);
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
        usedFuel = 0f;
        Time.timeScale = 0f;
        GameManager.instance.statsText.GetComponent<Text>().text = "Without carpooling, you and your friends would've used " + GameManager.score + " times more fuel across these " + GameManager.levelsCompleted + " levels! \nCarpool in real life to help save the enviorment, and your wallet!";
        GameManager.instance.isPaused = true;
        GameManager.instance.loseScreen.SetActive(true);
        score = 0;
        levelsCompleted = 0;
    }

    public static void reachedEndGoal()
    {
        levelsCompleted++;
        GameManager.instance.isPaused = true;
        Time.timeScale = 0f;
        //GameManager.instance.endLevelStats.SetActive(true);
        GameManager.instance.winScreen.SetActive(true);
    }
}
