using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoints : MonoBehaviour
{
    private CheckpointsandLaps checkpointsandLaps;


    void Start()
    {
        checkpointsandLaps = GetComponent<CheckpointsandLaps>();
        foreach (var checkpoint in checkpointsandLaps.checkpoints)
        {
            checkpointsandLaps.checkpoints[checkpointsandLaps.checkpointCounter].GetComponent<MeshRenderer>().enabled = false;
        }
        checkpointsandLaps.checkpoints[0].GetComponent<MeshRenderer>().enabled = true;

        checkpointsandLaps.checkpoints[checkpointsandLaps.checkpointCounter].GetComponent<MeshRenderer>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            NextCheckpoint();
        }
        if (other.CompareTag("Checkpoint"))
        {
            GameObject thisCheckpoint = other.gameObject;

            // Started race
            if (thisCheckpoint == checkpointsandLaps.checkpoints[0] && !checkpointsandLaps.started)
            {
                print("Started");
                checkpointsandLaps.started = true;
            }
            // Ended Lap / Race
            else if (thisCheckpoint == checkpointsandLaps.checkpoints[checkpointsandLaps.checkpoints.Length - 1] && checkpointsandLaps.started)
            {
                // if all laps are finished, end race
                if (checkpointsandLaps.currentLap == checkpointsandLaps.laps)
                {
                    if (checkpointsandLaps.currentCheckpoint == checkpointsandLaps.checkpoints[checkpointsandLaps.checkpoints.Length - 1])
                    {
                        checkpointsandLaps.finished = true;
                        print("Finished");
                    }
                    else
                    {
                        print("You missed 1 or more checkpoints!");
                    }
                }
            }

            // Loop through the checkpoints and compare and check which one the player passed through
            for (int i = 0; i < checkpointsandLaps.checkpoints.Length; i++)
            {
                if (checkpointsandLaps.finished)
                    return;

                // if the checkpoint is correct
                if (thisCheckpoint == checkpointsandLaps.checkpoints[i] && checkpointsandLaps.checkpoints[i] == checkpointsandLaps.currentCheckpoint)
                {
                    print("YYEEEEEEEEEEEEEEEEESSSS");
                    NextCheckpoint();
                }
                // if the checkpoint is incorrect
                else if (thisCheckpoint == checkpointsandLaps.checkpoints[i] && checkpointsandLaps.checkpoints[i] != checkpointsandLaps.currentCheckpoint)
                {
                    print("NOOOOOOOOOOOOOOOOOOOOOO WROOOOOOOOONG");
                }
            }
        }

    }

    public GameObject NextCheckpoint()
    {
        checkpointsandLaps.checkpoints[checkpointsandLaps.checkpointCounter].SetActive(false);
        checkpointsandLaps.checkpointCounter++;


        if (checkpointsandLaps.checkpointCounter == checkpointsandLaps.checkpoints.Length)
        {
            checkpointsandLaps.checkpointCounter = 0;
        }

        checkpointsandLaps.checkpoints[checkpointsandLaps.checkpointCounter].SetActive(true);
        checkpointsandLaps.currentCheckpoint = checkpointsandLaps.checkpoints  [checkpointsandLaps.checkpointCounter];
        return checkpointsandLaps.currentCheckpoint;
    }
}
