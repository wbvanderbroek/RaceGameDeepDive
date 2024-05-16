using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarSelect : MonoBehaviour
{

    [SerializeField]
    TMP_Text colortxt;
    [SerializeField]
    GameObject[] carmodels;
    [SerializeField]
    string[] carcolor;
    [SerializeField]
    public GameObject currentcaar;
    [SerializeField]
    public GameObject lastcar;

    public void UpdateInt(int currentcarnumber)
    {
        currentcaar = carmodels[currentcarnumber];
        lastcar.SetActive(false);
        currentcaar.SetActive(true);
        lastcar.transform.rotation = Quaternion.identity;
        lastcar = currentcaar;
        colortxt.SetText(carcolor[currentcarnumber]);
    }
}