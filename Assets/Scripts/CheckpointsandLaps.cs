using UnityEngine;

public class CheckpointsandLaps : MonoBehaviour
{

    [Header("Settings")]
    public float laps = 1;

    [Header("Information")]
    public int currentLap;
    public bool started;
    public bool finished;

    [Header("Level Variables")]
    public GameObject[] checkpoints;
    public GameObject currentCheckpoint;
    public int checkpointCounter = 0;

    private void Start()
    {
        currentCheckpoint = checkpoints[0];
        currentLap = 1;

        started = false;
        finished = false;
    }

    //private void Update()
    //{
    //    foreach (var cp in checkpoints)
    //    {
    //        cp.SetActive(false);
    //    }
    //    checkpoints[checkpointCounter].SetActive(true);
    //}


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint") && other.gameObject == currentCheckpoint)
        {
            if (TryGetComponent<PlayerCheckpoints>(out PlayerCheckpoints playerCheckpoints))
            {
                playerCheckpoints.NextCheckpoint();

            }
            else
            {
                NextCheckpoint();
            }
        }
        if (other.CompareTag("Checkpoint"))
        {
            GameObject thisCheckpoint = other.gameObject;

            // Started race
            if (thisCheckpoint == checkpoints[0] && !started)
            {
                print("Started");
                started = true;
            }
            // Ended Lap / Race
            else if (thisCheckpoint == checkpoints[checkpoints.Length-1] && started)
            {
                // if all laps are finished, end race
                if (currentLap == laps)
                {
                    if (currentCheckpoint == checkpoints[checkpoints.Length-1])
                    {
                        finished = true;
                        print("Finished");
                    }
                    else
                    {
                        print("You missed 1 or more checkpoints!");
                    }
                }
                //if all laps are not finished, start new lap.
                else if (currentLap < laps)
                {
                    if (currentCheckpoint == checkpoints[checkpoints.Length])
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

            // Loop through the checkpoints and compare and check which one the player passed through
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (finished)
                    return;

                // if the checkpoint is correct
                if (thisCheckpoint == checkpoints[i] && checkpoints[i] == currentCheckpoint)
                {
                    print("YYEEEEEEEEEEEEEEEEESSSS");
                    NextCheckpoint();
                }
                // if the checkpoint is incorrect
                else if (thisCheckpoint == checkpoints[i] && checkpoints[i] != currentCheckpoint)
                {
                    print("NOOOOOOOOOOOOOOOOOOOOOO WROOOOOOOOONG");
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