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
            thirdPerson.SetActive(true);
            Debug.Log("To back-view");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            backView.SetActive(true);
            firstPerson.SetActive(false);
            thirdPerson.SetActive(false);
            hoodView.SetActive(false);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            backView.SetActive(false);
            thirdPerson.SetActive(true);
        }
    }
}
