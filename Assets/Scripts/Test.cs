using UnityEngine;

public class Test : MonoBehaviour
{
    [Header("Settings")]
    public float laps = 1;
    public float maxDistance = 10f; // Max distance allowed from the most recent checkpoint
    public Rigidbody rb;

    [Header("Information")]
    private int currentLap;
    private bool started;
    private bool finished;

    [Header("Level Variables")]
    public GameObject[] checkpoints;
    public GameObject currentCheckpoint;
    public int checkpointCounter = 0;

    private GameObject previousCheckpoint; // To store the most recent checkpoint interacted with

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentCheckpoint = checkpoints[0];
        currentLap = 1;
        started = false;
        finished = false;

        foreach (var checkpoint in checkpoints)
        {
            checkpoint.SetActive(false);
        }
        checkpoints[0].SetActive(true);
        checkpoints[checkpointCounter].GetComponent<MeshRenderer>().enabled = true;
    }

    private void Update()
    {
        if (previousCheckpoint != null)
        {
            float distanceFromCheckpoint = Vector3.Distance(transform.position, previousCheckpoint.transform.position);
            if (distanceFromCheckpoint > maxDistance)
            {
                Debug.Log("Teleporting to the last checkpoint due to exceeding max distance.");
                transform.position = previousCheckpoint.transform.position;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            previousCheckpoint = currentCheckpoint; // Update the most recent checkpoint
            NextCheckpoint();
            GameObject nextCheckpoint = currentCheckpoint;
            float distance = Vector3.Distance(previousCheckpoint.transform.position, nextCheckpoint.transform.position);
            Debug.Log($"Distance to next checkpoint: {distance}");
            rb.velocity = Vector3.zero;
        }

        if (other.CompareTag("Checkpoint"))
        {
            GameObject thisCheckpoint = other.gameObject;

            if (thisCheckpoint == checkpoints[0] && !started)
            {
                print("Started");
                started = true;
            }
            else if (thisCheckpoint == checkpoints[checkpoints.Length - 1] && started)
            {
                if (currentLap == laps)
                {
                    if (currentCheckpoint == checkpoints[checkpoints.Length - 1])
                    {
                        finished = true;
                        print("Finished");
                    }
                    else
                    {
                        print("You missed 1 or more checkpoints!");
                    }
                }
                else if (currentLap < laps)
                {
                    if (currentCheckpoint == checkpoints[checkpoints.Length - 1])
                    {
                        currentLap++;
                        currentCheckpoint = checkpoints[0];
                        print($"Started lap {currentLap}");
                    }
                }
                else
                {
                    print("You did not go through all the checkpoints");
                }
            }

            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (finished)
                    return;

                if (thisCheckpoint == checkpoints[i] && checkpoints[i] == currentCheckpoint)
                {
                    print("YYEEEEEEEEEEEEEEEEESSSS");
                    NextCheckpoint();
                }
                else if (thisCheckpoint == checkpoints[i] && checkpoints[i] != currentCheckpoint)
                {
                    print("NOOOOOOOOOOOOOOOOOOOOOO WROOOOOOOOONG");
                }
            }
        }
    }

    public GameObject NextCheckpoint()
    {
        checkpoints[checkpointCounter].SetActive(false);
        checkpointCounter++;

        if (checkpointCounter == checkpoints.Length)
        {
            checkpointCounter = 0;
        }

        checkpoints[checkpointCounter].SetActive(true);
        currentCheckpoint = checkpoints[checkpointCounter];
        return currentCheckpoint;
        

    }
}