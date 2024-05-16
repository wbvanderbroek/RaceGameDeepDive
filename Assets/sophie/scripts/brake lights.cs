using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brakelights : MonoBehaviour
{
    [SerializeField]
    Light brakelight;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            brakelight.enabled = true;
        }
        else
        {
            brakelight.enabled = false;        }
    }
}
