using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedCalculator : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private TMP_Text speedText;

    private void Awake()
    {
        speedText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        speedText.text = Mathf.Round(rb.velocity.magnitude * 3.6f) + "KM/H";
    }
}