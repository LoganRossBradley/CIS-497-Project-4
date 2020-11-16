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

    public WheelCollider frontDriverW = new WheelCollider(), frontPassW = new WheelCollider();
    public WheelCollider rearDriverW = new WheelCollider(), rearPassW = new WheelCollider();
    public Transform frontDriverT, frontPassT;
    public Transform rearDriverT, rearPassT;
    public float maxSteerAngle = 30;
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
        forwardInput = Input.GetAxis("Vertical");
        brakeInput = Input.GetKey("space");
        if (forwardInput == 1 || forwardInput == -1)
        {
            GameManager.usedFuel += Time.deltaTime * fuelEfficiency;
        }

    }

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

    private void Accelerate()
    {

        if(brakeInput)
        {
            frontDriverW.brakeTorque = brake;
            frontPassW.brakeTorque = brake;

        }
        else
        {
            Debug.Log(frontDriverW.motorTorque);
 
            frontDriverW.motorTorque = forwardInput * motorForce;
            frontPassW.motorTorque = forwardInput * motorForce;
            frontDriverW.brakeTorque = 0;
            frontPassW.brakeTorque = 0;
        }
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(frontPassW, frontPassT);
        UpdateWheelPose(rearPassW, rearPassT);

    }

    private void UpdateWheelPose(WheelCollider collider, Transform transform)
    {
        Vector3 pos = transform.position;
        Quaternion quat = transform.rotation;

        collider.GetWorldPose(out pos, out quat);

        transform.position = pos;
        transform.rotation = quat;
    }

    void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
        gameOver = FuelBar.isFuelEmpty();
        if (frontDriverW.rpm > 500)
            frontDriverW.motorTorque = 0;
    }

}
