using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
    //with the way compass works, no need to check if friends remaining, as goal only becomes avaliable once all friends are grabbed
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.reachedEndGoal();
        }
    }
}
