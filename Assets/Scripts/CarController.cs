using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class CarController : MonoBehaviour
{
    public Transform centerOfMass;
    public float motorTorque = 1000f;
    public float maxSteer = 20f;
    public float brakeTorque = 100f;

    public Rigidbody rb;
    public Wheel[] wheels;

    [SerializeField] private TextMeshProUGUI speedText;
    public bool allowDrive =false;
    private float currentSpeed = 0;

    void Awake()
    {
        wheels = GetComponentsInChildren<Wheel>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
    }
    private void Update()
    {
        if (speedText == null)
            return;
        speedText.text = Mathf.Round(currentSpeed).ToString();
        StartCoroutine(CalculateSpeed());
    }
    public void ChangeSpeed(float throttle, float input)
    {
        if (allowDrive)
        {
            foreach (var wheel in wheels)
            {
                wheel.Torque = input * throttle;
            }
        }
    }
    IEnumerator CalculateSpeed()
    {
        Vector3 lastPosition = transform.position;
        yield return new WaitForFixedUpdate();
        currentSpeed = (lastPosition - transform.position).magnitude / Time.deltaTime * 4f;
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
    }
    public void DisableBrake(float brake)
    {
        foreach (var wheel in wheels)
        {
            wheel.brakeForce = 0f;
        }
    }
}