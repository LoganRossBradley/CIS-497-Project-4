/*
 * Benjamin Schuster
 * Project 4-6
 * Allows player to swap which map they're using
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSwap : MonoBehaviour
{
    public GameObject minimap;
    public GameObject megamap;

    private bool minimapUsed;

    private void Start()
    {
        minimap.SetActive(true);
        megamap.SetActive(false);
        minimapUsed = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.M) && minimapUsed)
        {
            minimap.SetActive(false);
            megamap.SetActive(true);
            minimapUsed = false;
        }
        else if (Input.GetKeyDown(KeyCode.M) && !minimapUsed)
        {
            minimap.SetActive(true);
            megamap.SetActive(false);
            minimapUsed = true;
        }

    }
}
