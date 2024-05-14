using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject firstPerson;
    public GameObject thirdPerson;
    public GameObject hoodView;
    public GameObject backView;
    
    void Start()
    {
        firstPerson.SetActive(false);
        thirdPerson.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && firstPerson.activeSelf == true)
        {
            firstPerson.SetActive(false);
            hoodView.SetActive(true);
            Debug.Log("To hood-view");
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && thirdPerson.activeSelf == true)
        {
            thirdPerson.SetActive(false);
            firstPerson.SetActive(true);
            Debug.Log("To first-person");
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && hoodView.activeSelf == true)
        {
            hoodView.SetActive(false);
            backView.SetActive(true);
            Debug.Log("To back-view");
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && backView.activeSelf == true)
        {
            backView.SetActive(false);
            thirdPerson.SetActive(true);
            Debug.Log("To third-person");
        }
    }
}
