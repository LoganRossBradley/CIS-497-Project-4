using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpoolController : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //reset distance for compass, needed otherwise doesn't change target when picking up a friend
            collision.gameObject.GetComponentInChildren<Compass>().distance = 99999f;

            GameManager.score += 1;
            Destroy(gameObject);
        }
    }
}
