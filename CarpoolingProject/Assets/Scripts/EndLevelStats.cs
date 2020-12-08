using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelStats : MonoBehaviour
{
    
    public static GameObject endLevelStats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        endLevelStats.GetComponent<Text>().text = "Stats:\nLevels Completed:" + GameManager.levelsCompleted + "\nBy Carpooling, You saved on " 
            + GameManager.score + " trips worth of gas!";
    }
}
