using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float verticalOffset;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + verticalOffset, player.position.z);

        if(this.CompareTag("Icon"))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, player.transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}
