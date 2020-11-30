using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float angleOffset;
    public float followSpeed = 10;
    public float lookSpeed = 10;

    private void PointCamera()
    {
        //Find spot to center slightly above player
        Vector3 playerDirection = player.position;
        playerDirection.y = playerDirection.y + angleOffset;
        Vector3 lookDirection = playerDirection - transform.position;

        //find and set look angle
        Quaternion orientation = Quaternion.LookRotation(lookDirection, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, orientation, lookSpeed * Time.deltaTime);
    }

    private void FollowTarget()
    {
        //find location to put camera
        Vector3 pos = player.position +
            player.forward * -offset.x +
            player.right * offset.z +
            player.up * offset.y;

        //Minimum so camera doesnt clip through ground
        if (pos.y <= 1.5)
            pos.y = 1.5f;

        //set position
        transform.position = Vector3.Lerp(transform.position, pos, followSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PointCamera();
        FollowTarget();
    }

}

