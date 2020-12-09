using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    public float fuelEfficiency = 1f;

    private float verticalInput;
    private float horizontalInput;
    private float steeringAngle;
    private bool brakeInput = false;
    [SerializeField] private float currentSpeed;

    private bool gameOver = false;

    // wheel collider allows car to drive using it's wheels
    public WheelCollider frontDriverW = new WheelCollider(), frontPassW = new WheelCollider();
    public WheelCollider rearDriverW = new WheelCollider(), rearPassW = new WheelCollider();
    public Transform frontDriverT, frontPassT;
    public Transform rearDriverT, rearPassT;
    //steering angle and force it can exert on the car
    public float maxSteerAngle = 30;
    public float maxMotorForce = 1000;
    public float motorForce = 700;
    public float brake = 500000;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        GameManager.usedFuel = 0;
    }

    public void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // when break is called causes the car to lose momentum 
        
        brakeInput = Input.GetKey("space");
        //Drain fuel the faster you're moving
        if (currentSpeed > 10)
        {
            GameManager.usedFuel += Time.deltaTime * fuelEfficiency * (currentSpeed/maxMotorForce) ;
        }
        else if (currentSpeed < -10)
        {
            GameManager.usedFuel += Time.deltaTime * fuelEfficiency * (currentSpeed / maxMotorForce) * -1;
        }

    }

    // allows you to turn. Cannot turn while you are not moving, but you can hold down the button to turn the wheel
    private void Steer()
    {
        steeringAngle = maxSteerAngle * horizontalInput;

        if (currentSpeed < 10 && currentSpeed > -10)
        {
            frontDriverW.steerAngle = -steeringAngle;
            frontPassW.steerAngle = -steeringAngle;
        }
        else
        {
            frontDriverW.steerAngle = steeringAngle;
            frontPassW.steerAngle = steeringAngle;
        }

    }

    //makes the car go faster every second
    private void Accelerate()
    {

        if(brakeInput)
        {
            frontDriverW.brakeTorque = brake;
            frontPassW.brakeTorque = brake;
            
            //uncomment for easier, though less realistic, breaking. Uncomment with the comment below
            //rearDriverW.brakeTorque = brake;
            //rearPassW.brakeTorque = brake;


        }
        else if(frontDriverW.rpm >= maxMotorForce)
        {
            frontDriverW.motorTorque = 0;
            frontPassW.motorTorque = 0;
        }
        else
        {
            frontDriverW.motorTorque = verticalInput * motorForce;
            frontPassW.motorTorque = verticalInput * motorForce;

            //breaking
            frontDriverW.brakeTorque = 0;
            frontPassW.brakeTorque = 0;
            
            //uncomment for easier, though less realistic, breaking. Uncomment with the comment above
            //rearDriverW.brakeTorque = 0;
            //rearPassW.brakeTorque = 0;
        }
        //if (!brakeInput)
        //{
        //    frontDriverW.brakeTorque = 0;
        //    frontPassW.brakeTorque = 0;
        //    rearDriverW.brakeTorque = 0;
        //    rearPassW.brakeTorque = 0;


        //}
    }

    //Besides that our camera cant see the wheels turning so its unnessesary, its what caused the car controller to spaz out so badly
    //private void UpdateWheelPoses()
    //{
    //    //makes the wheel actually turn
    //    UpdateWheelPose(frontDriverW, frontDriverT);
    //    UpdateWheelPose(rearDriverW, rearDriverT);
    //    UpdateWheelPose(frontPassW, frontPassT);
    //    UpdateWheelPose(rearPassW, rearPassT);

    //}

    //private void UpdateWheelPose(WheelCollider collider, Transform transform)
    //{
    //    //makes the wheel actually turn

    //    Vector3 pos = transform.position;
    //    Quaternion quat = transform.rotation;

    //    collider.GetWorldPose(out pos, out quat);

    //    transform.position = pos;
    //    transform.rotation = quat;
    //}

    void FixedUpdate()
    {
        //all runs in fixed for the physics engine to work properly
        GetInput();
        Steer();
        Accelerate();
        currentSpeed = frontDriverW.rpm;
        gameOver = FuelBar.isFuelEmpty();
    }

}
