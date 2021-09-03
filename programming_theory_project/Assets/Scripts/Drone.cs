using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    public float mass = 0.02f; // in grams
    public int flightTime = 600; // in seconds

    public float flightHeight = 1.0f; // in meters

    public float takeOffSpeed = 0.5f; // in meters/second

    enum DroneState {TakeOff, Hover, Land}

    DroneState currentState;

    Vector3 hoverStartPos;

    float hoverOscillationSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentState = DroneState.TakeOff;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case DroneState.TakeOff:
                if(TakeOffCommand(flightHeight))
                {
                    hoverStartPos = hoverStartPos = transform.position;
                    currentState = DroneState.Hover;
                }
                break;
            case DroneState.Hover:
                HoverCommand();
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

    void HoverCommand()
    {
        Vector3 v = hoverStartPos;
        v.y += 0.1f * Mathf.Sin(Time.time * hoverOscillationSpeed);
        transform.position = v;
    }
}
