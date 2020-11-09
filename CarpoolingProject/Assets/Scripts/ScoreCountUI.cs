using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCountUI : MonoBehaviour
{
    public Text scoreText;

    private void Update()
    {
        scoreText.text = "Score: " + GameManager.score;
    }
}
