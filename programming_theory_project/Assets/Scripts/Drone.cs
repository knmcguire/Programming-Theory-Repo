using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    protected float massDrone = 0.04f; // in grams
    [SerializeField] protected float totalFlightTime = 600.0f; // in seconds
    protected float currentFlightTime; // in seconds

    protected float flightHeight = 1.0f; // in meters

    protected float takeOffSpeed = 0.5f; // in meters/second

    enum DroneState {TakeOff, Hover, Land}

    DroneState currentState;

    protected Vector3 hoverStartPos;

    protected float hoverOscillationSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(massDrone);
        currentState = DroneState.TakeOff;
        currentFlightTime = totalFlightTime;
        takeOffSpeed = 0.02f / massDrone;
        Debug.Log(takeOffSpeed);
        hoverOscillationSpeed = 0.16f/  massDrone;
        Debug.Log(hoverOscillationSpeed);

    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {
            case DroneState.TakeOff:
                if(TakeOffCommand(flightHeight))
                {
                    hoverStartPos = transform.position;
                    currentState = DroneState.Hover;
                }
                break;
            case DroneState.Hover:
                HoverCommand();
                if (DrainBattery())
                    currentState = DroneState.Land;
                break;
            case DroneState.Land:
                LandCommand();
                break;
            default:
                break;

        }

    }

    bool TakeOffCommand(float preferredHeight)
    {
        if (transform.position.y < preferredHeight)
        {
            transform.Translate(Vector3.up * Time.deltaTime * takeOffSpeed);
            return false;
        }
        return true;
    }

    protected virtual void HoverCommand()
    {
        Vector3 v = hoverStartPos;
        v.y += 0.1f * Mathf.Sin(Time.time * hoverOscillationSpeed);
        transform.position = v;
    }

    void LandCommand()
    {
        CharacterController controller = GetComponent<CharacterController>();

        if (transform.position.y > 0.05)
        {
            transform.Translate(Vector3.down * Time.deltaTime * takeOffSpeed);
        }
    }

    bool DrainBattery()
    {
        currentFlightTime -= Time.deltaTime;
        if (currentFlightTime < 0)
            return true;
        else
            return false;

    }

}
