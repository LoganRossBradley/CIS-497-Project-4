/*
 * Benjamin Schuster
 * Project 4-6
 * Controls compass for minimap
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    //goal is the temporary goal, final goal is the reference to the final goal object
    private Vector3 goal;
    public GameObject finalGoal;

    public GameObject arrow;
    public float distance = 99999f;

    public float viewDistance;
    
    private GameObject[] friendList;

    // Start is called before the first frame update
    void Start()
    {
        //Select the closest friend as goal target
        friendList = GameObject.FindGameObjectsWithTag("FriendGoal");
        for (int i = 0; i < friendList.Length; i++)
        {
            if (distance > Vector3.Distance(transform.position, friendList[i].transform.position))
                goal = friendList[i].transform.position;
        }

        distance = Vector3.Distance(transform.position, goal);
    }

    // Update is called once per frame
    void Update()
    {
        //Set the closest friend object as the target for the compass
        friendList = GameObject.FindGameObjectsWithTag("FriendGoal");
        if (friendList.Length > 0)
        {
            for (int i = 0; i < friendList.Length; i++)
            {
                if (distance > Vector3.Distance(transform.position, friendList[i].transform.position))
                    goal = friendList[i].transform.position;
            }
        }
        //Activate final goal when no more friends detected
        if(friendList.Length <= 0)
        {
            finalGoal.SetActive(true);
            goal = finalGoal.transform.position;
        }

        Vector3 temp;

        //Rotate compass to point towards the goal
        if(goal != null)
        {
            //prevents compass from pointing upwards
            temp = new Vector3(goal.y, transform.position.y, goal.z);
            transform.LookAt(temp);
        }
            

        //Have the arrow disappear if the player is close enough to goal, reappear if strays too far from goal
        distance = Vector3.Distance(transform.position, goal);
        if (goal != null && distance > viewDistance)
        {
            arrow.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            arrow.GetComponent<MeshRenderer>().enabled = false;
        }
    }

}
