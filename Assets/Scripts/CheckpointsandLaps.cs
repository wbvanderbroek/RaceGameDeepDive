using UnityEngine;

public class CheckpointsandLaps : MonoBehaviour
{
    [Header("Settings")]
    public float laps = 1;
    public float maxDistance = 10f; // Max distance allowed from the most recent checkpoint
    public Rigidbody rb;

    [Header("Information")]
    public int currentLap;
    public bool started;
    public bool finished;

    [Header("Level Variables")]
    public GameObject[] checkpoints;
    public GameObject currentCheckpoint;
    public int checkpointCounter = 0;

    private Timer timer;
    private GameObject previousCheckpoint;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        timer = FindObjectOfType<Timer>();

        currentCheckpoint = checkpoints[0];
        currentLap = 1;

        started = false;
        finished = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint") && other.gameObject == currentCheckpoint)
        {
            previousCheckpoint = currentCheckpoint; // Update the most recent checkpoint

            if (TryGetComponent<PlayerCheckpoints>(out PlayerCheckpoints playerCheckpoints))
            {
                playerCheckpoints.NextCheckpoint();
            }
            else
            {
                NextCheckpoint();
            }
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
                        if (timer != null)
                        {
                            timer.TouchLastCheckpoint();
                        }
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
                    print("Correct checkpoint");
                    NextCheckpoint();
                }
                else if (thisCheckpoint == checkpoints[i] && checkpoints[i] != currentCheckpoint)
                {
                    print("Wrong checkpoint");
                }
            }
        }
    }

    public GameObject NextCheckpoint()
    {
            checkpointCounter++;

        if (checkpointCounter == checkpoints.Length)
        {
            checkpointCounter = 0;
        }

        currentCheckpoint = checkpoints[checkpointCounter];
        return currentCheckpoint;
    }
}