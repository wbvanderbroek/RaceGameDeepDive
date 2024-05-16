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
    [SerializeField] private Light BackLight1;
    [SerializeField] private Light BackLight2;

    [SerializeField] private TextMeshProUGUI speedText;
    private float currentSpeed = 0;

    void Awake()
    {
        wheels = GetComponentsInChildren<Wheel>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
    }
    private void Update()
    {
        speedText.text = Mathf.Round(currentSpeed).ToString();
        StartCoroutine(CalculateSpeed());
    }
    public void ChangeSpeed(float throttle, float input)
    {
        foreach (var wheel in wheels)
        {
            wheel.Torque = input * throttle;
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