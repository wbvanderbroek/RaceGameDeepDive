using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour
{
    CarSelect carSelect;
    [SerializeField]
    GameObject carToRotate;
    [SerializeField]
    GameObject prevCar;

    private void Awake()
    {
        carSelect = GetComponent<CarSelect>();
    }
    void Update()
    {
        carToRotate = carSelect.currentcaar;
        carToRotate.transform.Rotate(0, 15f * Time.deltaTime, 0);
    }
}