using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    [Header("Input Variables")]
    CarController carController;
    public float forwards;
    public float turn;
    public float braking;
    public float currentspeed;
    public bool canactivate;
    public float timeleft = 10f;

    [SerializeField]
    GameObject newlapbrake;


    [Header("Level Variables")]
    private Transform targetPositionTransform;

    private void Awake()
    {
        carController = GetComponent<CarController>();
        targetPositionTransform = carController.checkPoints[0].transform;
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = targetPositionTransform.position;
        float forwards = 0;
        float turn = 0;

        Vector3 directionToTarget = (targetPosition - transform.position);
        float dot = Vector3.Dot(transform.forward, directionToTarget);

        float distance = Vector3.Distance(transform.position, targetPosition);
        float minDistance = 10;

        if (distance > minDistance)
        {
            if (dot > 0)
            {
                forwards = 1;
            }
            else if (dot < 0)
            {
                forwards = -1;
            }
            float angle = Vector3.SignedAngle(transform.forward, directionToTarget, Vector3.up);
            if (angle > 5)
            {
                turn = 1;
            }
            if (angle < -5)
            {
                turn = -1;
            }
        }        
        else
        {
            targetPositionTransform = carController.NextCheckpoint().transform;
        }

        if (currentspeed < 18)
        {
            carController.DisableBrake(braking);
        }
        carController.ChangeSpeed(forwards);
        carController.Turn(turn);
        StartCoroutine(CalculateSpeed());
    }
    public void Update()
    {
        timeleft -= Time.deltaTime;
        if (timeleft < 1)
        {
            newlapbrake.active = true;
        }
    }
    IEnumerator CalculateSpeed()
    {
        Vector3 lastPosition = transform.position;
        yield return new WaitForFixedUpdate();
        currentspeed = (lastPosition - transform.position).magnitude / Time.deltaTime;
    }
}