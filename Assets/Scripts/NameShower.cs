using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameShower : MonoBehaviour
{
    public TMP_Text playerNameText;

    void Start()
    {
        string playerName = PlayerPrefs.GetString("playerName");
        playerNameText.text = playerName;
    }
}
