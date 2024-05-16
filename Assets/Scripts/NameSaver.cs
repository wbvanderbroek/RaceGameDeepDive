using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameSaver : MonoBehaviour
{
    public TMP_InputField nameInput;

    public void SaveName()
    {
        string playerName = nameInput.text;
        PlayerPrefs.SetString("playerName", playerName);
    }
}
