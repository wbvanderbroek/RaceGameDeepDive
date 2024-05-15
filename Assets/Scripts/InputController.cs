using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    CarController carController;
    public float Throttleinput { get; private set; }
    public float SteerInput { get; private set; }

    public float Forwards;
    public float Steering;
    public float braking;

    PlayerInput input;
    void Awake()
    {
        input = FindObjectOfType<PlayerInput>();
        carController = GetComponent<CarController>();
    }
    void Update()
    {
        Forwards = input.actions["Vertical"].ReadValue<float>();
        Steering = Input.GetAxis("Horizontal");

        carController.ChangeSpeed(Forwards);
        carController.Turn(Steering);        
        if(Input.GetKey(KeyCode.Space))
        {
            carController.activatebrake(braking);
        }
        else
        {
            carController.disablebrake(braking);
        }
    }
}