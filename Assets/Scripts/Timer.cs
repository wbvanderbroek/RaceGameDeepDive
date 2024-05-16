using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Timertext;
    [SerializeField] float RemainingTime;
    float ElapsedTime;
    bool isLastCheckpointReached = false;
    [SerializeField] private TextMeshProUGUI goText;

    void Update()
    {
        if (!isLastCheckpointReached)
        {
            if (RemainingTime > 0.00000001)
            {
                RemainingTime -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(RemainingTime / 60);
                int seconds = Mathf.FloorToInt(RemainingTime % 60);
                Timertext.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                if (GetComponent<CarController>().allowDrive == false)
                {
                    StartCoroutine(ShowGoUI());
                }
                GetComponent<CarController>().allowDrive = true;
                ElapsedTime += Time.deltaTime;
                int minutes = Mathf.FloorToInt(ElapsedTime / 60);
                int seconds = Mathf.FloorToInt(ElapsedTime % 60);
                Timertext.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
     
    }

    public void TouchLastCheckpoint()
    {
        isLastCheckpointReached = true;
        Debug.Log("Last checkpoint reached. Timer stopped.");
    }
    private IEnumerator ShowGoUI()
    {
        goText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        goText.gameObject.SetActive(false);
    }
}