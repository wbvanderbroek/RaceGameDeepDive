using UnityEngine;

public class Recover : MonoBehaviour
{
    private CarController carController;
    private GameObject spawnObject;
    [SerializeField] GameObject carObject;
    void Start()
    {
        carController = GetComponent<CarController>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            int lastCheckpoint = carController.checkPointCounter - 1;
            if (lastCheckpoint < 0)
            {
                lastCheckpoint = carController.checkPoints.Length - 1;
            }
            spawnObject = carController.checkPoints[lastCheckpoint];
            transform.position = spawnObject.transform.position;
            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation.x = 0;
            currentRotation.z = 0;
            transform.rotation = Quaternion.Euler(currentRotation);

        }
    }
}
