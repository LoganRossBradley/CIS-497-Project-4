using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleUI : MonoBehaviour
{
    public GameObject subtitleText;
    string[] subtitleArray = new string[10];

    private void Start()
    {
        subtitleArray[0] = "Hey buddy, how are you doing?";
        subtitleArray[1] = "Thanks for picking me up.";
        subtitleArray[2] = "You finally showed up!";
        subtitleArray[3] = "Alright, let's go get the others.";
        subtitleArray[4] = "Hello Acquantaince, it's nice to see you.";
        subtitleArray[5] = "Be careful, you dont want to run out of gas.";
        subtitleArray[6] = "I'm so glad you got here!";
        subtitleArray[7] = "Thanks, now lets get moving.";
        subtitleArray[8] = "I hope we don't run out of gas";
        subtitleArray[9] = "Try not to hit anything would you?";
    }
    private string PickupSpeech()
    {
        System.Random r = new System.Random();
        int result = r.Next(0, 9);
        return subtitleArray[result];
    }
    IEnumerator DisplaySubtitle()
    {
        subtitleText.GetComponent<Text>().text = PickupSpeech();
        yield return new WaitForSeconds(4f);
        subtitleText.GetComponent<Text>().text = "";
    }
    public void startIEnumerator()
    {
        StartCoroutine(DisplaySubtitle());
    }
}
