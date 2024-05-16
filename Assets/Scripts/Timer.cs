using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class Timer : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI Timertext;
    [SerializeField] NetworkVariable<float> RemainingTime = new NetworkVariable<float>(10,NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    NetworkVariable<float> ElapsedTime = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    bool isLastCheckpointReached = false;
    [SerializeField] private TextMeshProUGUI goText;
    public bool allowDrive = false;

    void Update()
    {
        if (!isLastCheckpointReached)
        {
            if (RemainingTime.Value > 0)
            {
                int minutes = 0;
                int seconds = 0;
                if (IsServer)
                {
                    RemainingTime.Value -= Time.deltaTime;
                }
                minutes = Mathf.FloorToInt(RemainingTime.Value / 60);
                seconds = Mathf.FloorToInt(RemainingTime.Value % 60);
                Timertext.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                int minutes = 0;
                int seconds = 0;
                if (allowDrive == false)
                {
                    StartCoroutine(ShowGoUI());
                    allowDrive = true;
                }
                if (IsServer)
                {
                    ElapsedTime.Value += Time.deltaTime;
                }
                minutes = Mathf.FloorToInt(ElapsedTime.Value / 60);
                seconds = Mathf.FloorToInt(ElapsedTime.Value % 60);
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