using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    [SerializeField]
    GameObject timerstart;
    [SerializeField]
    GameObject endscren;
    float timertime = 0;
    bool timerRun = true;
    public TMP_Text timertext;

    void Update()
    {
        if (timertime >= 0)
        {
            timertime += Time.deltaTime;
        }
        Displaytime(timertime);
    }
    void Displaytime(float timercurrent)
    {
        timercurrent += 1;
        float minutes = Mathf.FloorToInt(timercurrent / 60);
        float seconds = Mathf.FloorToInt(timercurrent % 60);
        float milliseconds = (timercurrent % 1) * 1000;
        timertext.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
