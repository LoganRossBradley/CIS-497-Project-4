    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    public float speed = 10;
    private float turnSpeed = 50;
    private float horizontalInput;
    private float forwardInput;
    private FuelBar gasCheck;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        speed= FuelBar.isFuelEmpty(speed);
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        

        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        //can only turn if moving forward or backward
        if (forwardInput != 0)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
        }

        //using fuel while moving
        if (forwardInput == 1 || forwardInput == -1)
        {
            GameManager.usedFuel += Time.deltaTime;
        }
    }

}
