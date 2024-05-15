using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GearState
{
    Neutral,
    Running,
    CheckingChange,
    Changing
};
public class CarInputController : MonoBehaviour
{
    public float xAxis, gasInput, brakeInput, clutchInput;
    public int currentGear;

    CarController carController;

    public float Forwards;
    public float Steering;
    public float braking;
    public float handBrake;

    public float RPM;
    public float redLine;
    public float idleRPM;
    public TMP_Text rpmText;
    public TMP_Text gearText;
    public Slider rpmSlider;

    public float[] gearRatios;
    public float differentialRatio;
    private float currentTorque;
    public float clutch;
    public int isEngineRunning;
    private float wheelRPM;
    public AnimationCurve hpToRPMCurve;
    public GearState gearState;
    public float increaseGearRPM;
    public float decreaseGearRPM;
    public float changeGearTime = 0.5f;

    void Awake()
    {
        carController = GetComponent<CarController>();
    }

    void Update()
    {
        Forwards = Input.GetAxis("Vertical");
        Steering = Input.GetAxis("Horizontal");

        if (gearState != GearState.Changing)
        {
            if (gearState == GearState.Neutral)
            {
                clutch = 0f;
                if (Mathf.Abs(Forwards) > 0) gearState = GearState.Running;
            }
            else
            {
                clutch = Input.GetKey(KeyCode.LeftShift) ? 0 : Mathf.Lerp(clutch, 1, Time.deltaTime);
            }
        }
        else
        {
            clutch = 0;
        }

        rpmSlider.value = RPM;
        rpmText.text = RPM.ToString("0 000") + "rpm";
        if (currentGear == 1)
        {
            gearState = GearState.Neutral;
        }
        else
        {
            gearState = GearState.Running;
        }
        gearText.text = currentGear == 1 ? "N" : currentGear == 0 ? "R" : (currentGear -1).ToString();

        Forwards = Mathf.Clamp(Input.GetAxis("Vertical"),0,1);
        Steering = Input.GetAxis("Horizontal");


        currentTorque = CalculateTorque();
        carController.ChangeSpeed(currentTorque, Forwards);
        carController.Turn(Steering);

        if (brakeInput > 0f || Input.GetKey(KeyCode.Space))
        {
            carController.ActivateBrake(braking);
        }
        else
        {
            carController.DisableBrake(braking);
        }
    }

    float CalculateTorque()
    {
        float torque = 0;
        //if (RPM < idleRPM + 200 && Forwards == 0 && currentGear == 0)
        //{
        //    gearState = GearState.Neutral;
        //}
        if ((gearState == GearState.Running || gearState == GearState.Neutral)) //&& clutch > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && (RPM > increaseGearRPM || gearState == GearState.Neutral || currentGear == 0))
            {
                StartCoroutine(ChangeGear(1));
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl) && (RPM < decreaseGearRPM || gearState == GearState.Neutral ))
            {
                StartCoroutine(ChangeGear(-1));
            }
        }
        if (isEngineRunning > 0)
        {
            if (clutch < 0.1f)
            {
                RPM = Mathf.Lerp(RPM, Mathf.Max(idleRPM, redLine * Forwards) + Random.Range(-50, 50), Time.deltaTime);
            }
            else
            {
                foreach (var wheel in carController.wheels)
                {
                    wheelRPM = Mathf.Abs((wheel.Torque) / 2f) * gearRatios[currentGear] * differentialRatio;
                }
                RPM = Mathf.Lerp(RPM, Mathf.Max(idleRPM, redLine * Forwards) + Random.Range(-50, 50), Time.deltaTime);
                //RPM = Mathf.Lerp(RPM, Mathf.Max(idleRPM - 100, wheelRPM), Time.deltaTime * 3f);
                torque = (hpToRPMCurve.Evaluate(RPM / redLine) * carController.motorTorque / RPM) * gearRatios[currentGear] * differentialRatio * 5252f * clutch;
            }
        }

        return torque;
    }
    IEnumerator ChangeGear(int gearChange)
    {
        gearState = GearState.CheckingChange;
        if (currentGear + gearChange >= 0)
        {
            if (gearChange > 0)
            {
                gearState = GearState.Running;
                if (currentGear < gearRatios.Length - 1)
                {
                    gearState = GearState.Changing;
                    yield return new WaitForSeconds(changeGearTime);
                    currentGear += gearChange;
                }
                //increase the gear
            }
            if (gearChange < 0)
            {
                gearState = GearState.Running;

                if (currentGear > 0)
                {
                    gearState = GearState.Changing;
                    yield return new WaitForSeconds(changeGearTime);
                    currentGear += gearChange;
                }
                //decrease the gear
            }
        }

        if (gearState != GearState.Neutral)
            gearState = GearState.Running;
    }
}
