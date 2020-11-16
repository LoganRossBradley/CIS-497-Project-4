    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    public float speed = 10;
    public float fuelEfficiency = 1f;

    public float turnSpeed = 50;
    private float horizontalInput;
    private float forwardInput;
    private FuelBar gasCheck;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        GameManager.usedFuel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //speed= FuelBar.isFuelEmpty(speed);
        gameOver = FuelBar.isFuelEmpty();

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
            GameManager.usedFuel += Time.deltaTime * fuelEfficiency;
        }
    }

}
