using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    public float fuelEfficiency = 1f;
    public float horizontalInput;
    public bool brakeInput;
    public float steerAngle;
    public float forwardInput;
    private bool gameOver = false;

    // wheel collider allows car to drive using it's wheels
    public WheelCollider frontDriverW = new WheelCollider(), frontPassW = new WheelCollider();
    public WheelCollider rearDriverW = new WheelCollider(), rearPassW = new WheelCollider();
    private Transform frontDriverT, frontPassT;
    private Transform rearDriverT, rearPassT;
    //steering angle and force it can exert on the car
    public float maxSteerAngle = 30;
    public float motorForce = 700;
    public float brake = 500000;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        GameManager.usedFuel = 0;

        frontDriverT = frontDriverW.GetComponent<Transform>();
        frontPassT = frontPassW.GetComponent<Transform>();
        rearDriverT = rearDriverW.GetComponent<Transform>();
        rearPassT = rearPassW.GetComponent<Transform>();

    }

    public void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // when break is called causes the car to lose momentum 
        brakeInput = Input.GetKey("space");
        if (forwardInput == 1 || forwardInput == -1)
        {
            // drains fuel gauge as you hold down your movement button
            GameManager.usedFuel += Time.deltaTime * fuelEfficiency;
        }

    }

    // allows you to turn. Cannot turn while you are not moving, but you can hold down the button to turn the wheel
    private void Steer()
    {
        steerAngle = maxSteerAngle * horizontalInput;

        if (!brakeInput)
        {
            frontDriverW.steerAngle = steerAngle;
            frontPassW.steerAngle = steerAngle;

        }
        else
        {
            frontDriverW.steerAngle = -steerAngle;
            frontPassW.steerAngle = -steerAngle;
        }
    }

    //makes the car go faster every second
    private void Accelerate()
    {

        if(brakeInput)
        {
            frontDriverW.brakeTorque = brake;
            frontPassW.brakeTorque = brake;

        }
        else
        { 
            frontDriverW.motorTorque = forwardInput * motorForce;
            frontPassW.motorTorque = forwardInput * motorForce;
            frontDriverW.brakeTorque = 0;
            frontPassW.brakeTorque = 0;
        }
    }

    private void UpdateWheelPoses()
    {
        //makes the wheel actually turn
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(frontPassW, frontPassT);
        UpdateWheelPose(rearPassW, rearPassT);

    }

    private void UpdateWheelPose(WheelCollider collider, Transform transform)
    {
        //makes the wheel actually turn

        Vector3 pos = transform.position;
        Quaternion quat = transform.rotation;

        collider.GetWorldPose(out pos, out quat);

        transform.position = pos;
        transform.rotation = quat;
    }

    void FixedUpdate()
    {
        //all runs in fixed for the physics engine to work properly
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
        gameOver = FuelBar.isFuelEmpty();
        if (frontDriverW.rpm > 500)
            frontDriverW.motorTorque = 0;
    }

}
