using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCheckpoints : MonoBehaviour
{
    CarController carController;
    void Start()
    {
        carController = GetComponent<CarController>();
        carController.checkPoints[carController.checkPointCounter].GetComponent<MeshRenderer>().enabled = true;
    }
    private void Update()
    {
        foreach (var cp in carController.checkPoints)
        {
            cp.SetActive(false);
        }
        carController.checkPoints[carController.checkPointCounter].SetActive(true);
    }   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            carController.NextCheckpoint();
        }
    }
}
