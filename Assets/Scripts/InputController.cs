using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    CarController carController;
    public float Throttleinput { get; private set; }
    public float SteerInput { get; private set; }

    public float Forwards;
    public float Steering;
    public float braking;
    void Awake()
    {
        carController = GetComponent<CarController>();
    }
    void Update()
    {
        Forwards = Input.GetAxis("Vertical");
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