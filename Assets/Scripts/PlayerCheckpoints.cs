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


    public GameObject NextCheckpoint()
    {
        checkpointsandLaps.checkpoints[checkpointsandLaps.checkpointCounter].GetComponent<MeshRenderer>().enabled = false;
        checkpointsandLaps.checkpointCounter++;


        if (checkpointsandLaps.checkpointCounter == checkpointsandLaps.checkpoints.Length)
        {
            checkpointsandLaps.checkpointCounter = 0;
        }

        checkpointsandLaps.checkpoints[checkpointsandLaps.checkpointCounter].GetComponent<MeshRenderer>().enabled = true;
        checkpointsandLaps.currentCheckpoint = checkpointsandLaps.checkpoints  [checkpointsandLaps.checkpointCounter];
        return checkpointsandLaps.currentCheckpoint;
    }
}
