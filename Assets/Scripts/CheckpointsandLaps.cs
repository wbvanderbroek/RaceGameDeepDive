using UnityEngine;

public class CheckpointsandLaps : MonoBehaviour
{
    [Header("Settings")]
    public float laps = 1;

    [Header("Information")]
    private int currentLap;
    private bool started;
    private bool finished;

    [Header("Level Variables")]
    public GameObject[] checkpoints;
    public GameObject currentCheckpoint;
    public int checkpointCounter = 0;

    private Timer timer;

    private void Start()
    {
        timer = FindObjectOfType<Timer>();

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
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