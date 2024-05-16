using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public TMP_InputField nameInput;
    public void Track1()
    {
        SceneManager.LoadScene(2);
    }
    public void Track2()
    {
        SceneManager.LoadScene(3);
    }
    public void StartButton()
    {
        string playerName = nameInput.text;
        PlayerPrefs.SetString("playerName", playerName);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
}
