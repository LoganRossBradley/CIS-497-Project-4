﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpoolController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //reset distance for compass, needed otherwise doesn't change target when picking up a friend
            other.gameObject.GetComponentInChildren<Compass>().distance = 99999f;

            GameManager.score += 1;
            Debug.Log("Destroy nurse");
            Destroy(gameObject);
        }
    }
}
