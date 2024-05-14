using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsandLaps : MonoBehaviour
{
    [Header("Checkpoints")]
    public GameObject start;
    public GameObject end;
    public GameObject[] checkpoints;

    [Header("Settings")]
    public float Laps = 1;

    [Header("Information")]
    private float currentCheckpoint;
    private float currentLap;
    private bool started;
    private bool finished;

    private void Start()
    {
        currentCheckpoint = 0;
        currentLap = 1;

        started = false;
        finished = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            GameObject thisCheckpoint = other.gameObject;

            // Started race
            if (thisCheckpoint == start && !started)
            {
                print("Started");
                started = true;
            }
            // Ended Lap / Race
            else if (thisCheckpoint == end && started)
            {
                // if all laps are finished, end race
                if (currentLap == Laps)
                {
                    if (currentCheckpoint == checkpoints.Length)
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
                else if (currentLap < Laps)
                {
                    if (currentCheckpoint == checkpoints.Length)
                    {
                        currentLap++;
                        currentCheckpoint = 0;
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
                if (thisCheckpoint == checkpoints[i] && i == currentCheckpoint)
                {
                    print("YYEEEEEEEEEEEEEEEEESSSS");
                    currentCheckpoint++;
                }
                // if the checkpoint is incorrect
                else if (thisCheckpoint == checkpoints[i] && i != currentCheckpoint)
                {
                    print("NOOOOOOOOOOOOOOOOOOOOOO WROOOOOOOOONG");
                }
            }
        }
    }
}
