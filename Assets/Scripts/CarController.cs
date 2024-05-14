using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarController : MonoBehaviour
{
    public Transform centerOfMass;
    public float motorTorque = 500f;
    public float maxSteer = 20f;
    public float brakeTorque = 100f;
    public float currentspeed;

    public float BrakeForce;

    public float steer { get; set; }
    public float throttle { get; set; }
    private Rigidbody rb;
    private Wheel[] wheels;
    [SerializeField] private Light BackLight1;
    [SerializeField] private Light BackLight2;

    [SerializeField] private LapTimer finish;    

    [Header("Level Variables")]
    public GameObject[] checkPoints;
    public GameObject currentCheckPoint;
    public int checkPointCounter = 0;

    void Start()
    {
        checkPoints[0].SetActive(true);
        wheels = GetComponentsInChildren<Wheel>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
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
    public void activatebrake(float brake)
    {
        foreach (var wheel in wheels)
        {
            wheel.brakeForce = 700f;
        }
        BackLight1.enabled = true;
        BackLight2.enabled = true;
    }
    public void disablebrake(float brake)
    {
        foreach (var wheel in wheels)
        {
            wheel.brakeForce = 0f;
        }
        BackLight1.enabled = false;
        BackLight2.enabled = false;
    }

    void Update()
    {
        StartCoroutine(CalculateSpeed());
    }
    IEnumerator CalculateSpeed()
    {
        Vector3 lastPosition = transform.position;
        yield return new WaitForFixedUpdate();
        currentspeed = (lastPosition - transform.position).magnitude / Time.deltaTime;
    }
    
    public GameObject NextCheckpoint()
    {
        checkPointCounter++;

        if (checkPointCounter == checkPoints.Length)
        {
            LapTimer lapTimer = FindObjectOfType<LapTimer>();
            lapTimer.CanFinishOn();
            checkPointCounter = 0;
        }
        currentCheckPoint = checkPoints[checkPointCounter];
        return currentCheckPoint;
    }    
}