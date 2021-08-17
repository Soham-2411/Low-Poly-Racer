using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform centerOfMass;

    public WheelCollider wheelColliderFrontLeft;
    public WheelCollider wheelColliderFrontRight;
    public WheelCollider wheelColliderBackLeft;
    public WheelCollider wheelColliderBackRight;

    public Transform wheelLeftFront;
    public Transform wheelRightFront;
    public Transform wheelRightBack;
    public Transform wheelLeftBack;

    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;


    public float motorTorque = 100f;
    public float maxSteer = 20f;


    public void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        steeringAngle = maxSteer * horizontalInput;
        wheelColliderFrontLeft.steerAngle = steeringAngle;
        wheelColliderFrontRight.steerAngle = steeringAngle;
    }

    private void Accelerate()
    {
        wheelColliderBackLeft.motorTorque = verticalInput * motorTorque;
        wheelColliderBackRight.motorTorque = verticalInput * motorTorque;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(wheelColliderFrontLeft, wheelLeftFront);
        UpdateWheelPose(wheelColliderFrontRight, wheelRightFront);
        UpdateWheelPose(wheelColliderBackLeft, wheelLeftBack);
        UpdateWheelPose(wheelColliderBackRight, wheelRightBack);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);
        _transform.position = _pos;
        _transform.rotation = _quat;

    }
    private Rigidbody _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }
}
