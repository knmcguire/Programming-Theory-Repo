using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    protected float massDrone = 0.04f; // in grams
    protected float totalFlightTime = 600.0f; // in seconds
    protected float currentFlightTime; // in seconds

    protected float flightHeight = 1.0f; // in meters

    protected float takeOffSpeed = 0.5f; // in meters/second

    enum DroneState {TakeOff, Hover, HoverAroundPlayer, Land}

    DroneState currentState;

    protected Vector3 hoverStartPos;

    protected float hoverOscillationSpeed = 2.0f;

    protected float timeStartHover;

    protected GameObject Player;

    float randomPi;
     float randomRadius;
      float randomHeight;
      float angleHeight;
    float circleSpeed =3.0f;
    // Start is called before the first frame update
    void Start()
    {
        currentState = DroneState.TakeOff;
        currentFlightTime = totalFlightTime;
        takeOffSpeed = 0.02f / massDrone;
        hoverOscillationSpeed = 0.16f/  massDrone;
        randomizeValues();
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
                    timeStartHover = Time.time;
                    currentState = DroneState.Hover;
                }
                break;
            case DroneState.Hover:
                HoverCommand(hoverStartPos);
                if (DrainBattery())
                    currentState = DroneState.Land;
                break;
            case DroneState.HoverAroundPlayer:
                HoverCommand(GetCircleAroundPlayerPosition());
                break;

            
            case DroneState.Land:
                if(LandCommand())
                    Destroy(gameObject);
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

    protected virtual void HoverCommand(Vector3 startPosition)
    {
        
        Vector3 v = startPosition;
        v.y += 0.1f * Mathf.Sin((Time.time - timeStartHover) * hoverOscillationSpeed);
        transform.position = v;
    }

    bool LandCommand()
    {
        CharacterController controller = GetComponent<CharacterController>();

        if (transform.position.y > 0.05)
        {
            transform.Translate(Vector3.down * Time.deltaTime * takeOffSpeed);
            return false;
        }
        return true;
    }

    bool DrainBattery()
    {
        currentFlightTime -= Time.deltaTime;
        if (currentFlightTime < 0)
            return true;
        else
            return false;

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
        if(other.gameObject.CompareTag("Player"))
        { 
            Player = other.gameObject;
            currentState = DroneState.HoverAroundPlayer;
            //Destroy(gameObject);
        }
    }

    public Vector3 RandomMovement()
    {       float time = Time.timeSinceLevelLoad;
            float currentPi = randomPi + circleSpeed * time;    
            float currentPiHeight = angleHeight + circleSpeed * time;
            float circleX = randomRadius*Mathf.Sin(currentPi);
            float circleY = randomRadius*Mathf.Cos(currentPi);
            float height = randomHeight + 0.5f*Mathf.Sin(currentPiHeight);
            

            return new Vector3(circleX, height ,circleY);
    }

    public void randomizeValues()
    {
        randomPi = Random.Range(0, 2*Mathf.PI); 
        randomRadius = Random.Range(1.00f, 2.00f);
        randomHeight = Random.Range(0.5f, 1.0f);
        angleHeight = Random.Range(0, 2*Mathf.PI);
    }
    Vector3 GetCircleAroundPlayerPosition()
    {
        Vector3 CirclePosition = RandomMovement();
        return new Vector3(Player.transform.position.x + CirclePosition.x, Player.transform.position.y + CirclePosition.y, Player.transform.position.z + CirclePosition.z );

    }

    

}
