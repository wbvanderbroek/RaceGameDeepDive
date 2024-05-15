using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CarController : MonoBehaviour
{
    public Transform centerOfMass;
    public float motorTorque = 1000f;
    public float maxSteer = 20f;
    public float brakeTorque = 100f;

    public Rigidbody rb;
    public Wheel[] wheels;
    [SerializeField] private Light BackLight1;
    [SerializeField] private Light BackLight2;

    public float rpm = 0;
    public int idleRpm = 800;
    public int maxRpm = 7000;
    [SerializeField] private float accelerationRate = 500f;
    [SerializeField] private float decelerationRate = 1000f;

    [SerializeField] private int currentGear = 1;
    public float[] gearRatios;
    [SerializeField] private int gears = 1;
    [SerializeField] private int maxGears = 5;
    void Awake()
    {
        wheels = GetComponentsInChildren<Wheel>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
        rpm = idleRpm;
    }
    private void Update()
    {
        print((wheels[0].gameObject.GetComponent<WheelCollider>().rpm));
        //print((int)((wheels[0].gameObject.GetComponent<WheelCollider>().rpm + wheels[0].gameObject.GetComponent<WheelCollider>().rpm) / 2) * gearRatios[currentGear - 1] * 60f / (2f * Mathf.PI));
        //rpm = (int)((wheels[0].gameObject.GetComponent<WheelCollider>().rpm + wheels[1].gameObject.GetComponent<WheelCollider>().rpm) /2) * gearRatios[currentGear -1]* 60f / (2f * Mathf.PI);
    }
    public void ChangeRpm(float rpmToadd)
    {
        Mathf.Clamp(rpm, 0, maxRpm);
    }
    public void ChangeSpeed(float throttle)
    {
        foreach (var wheel in wheels)
        {
            wheel.Torque = throttle * motorTorque;
        }
    }
    public void Turn(float steer)
    {
        foreach (var wheel in wheels)
        {
            wheel.Steerangle = steer * maxSteer;
        }
    }
    public void ActivateBrake(float brake)
    {
        foreach (var wheel in wheels)
        {
            wheel.brakeForce = 700f;
        }
        BackLight1.enabled = true;
        BackLight2.enabled = true;
    }
    public void DisableBrake(float brake)
    {
        foreach (var wheel in wheels)
        {
            wheel.brakeForce = 0f;
        }
        BackLight1.enabled = false;
        BackLight2.enabled = false;
    }
}