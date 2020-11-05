/*
 * Benjamin Schuster
 * Project 4+ 
 * Allows for tutorial to progress
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialProgressManager : MonoBehaviour
{
    public GameObject lastText;
    public GameObject nextText;

    private void OnTriggerEnter(Collider other)
    {
        //Enable the next tutorial message and disable the previous one, then destroy self
        if(other.CompareTag("Player"))
        {
            lastText.SetActive(false);
            nextText.SetActive(true);
            Destroy(gameObject);
        }
    }
}
