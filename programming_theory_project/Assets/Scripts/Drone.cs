using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Drone : MonoBehaviour
{

    protected float massDrone = 0.04f; // in grams
    protected float totalFlightTime = 600.0f; // in seconds
    public float currentFlightTime {get; protected set;} // in seconds

    protected float flightHeight = 1.0f; // in meters

    protected float takeOffSpeed = 0.5f; // in meters/second

    enum DroneState {TakeOff, Hover, HoverAroundPlayer, Land}

    DroneState currentState;

    protected Vector3 hoverStartPos;

    protected float hoverOscillationSpeed = 2.0f;

    protected float timeStartHover;

    protected GameObject Player;

    // for swarming
    float randomPi;
    float randomRadius;
    float randomHeight;
    float angleHeight;
    float circleSpeed =3.0f;
    // Start is called before the first frame update

    GameObject MainManager;
    

    static int amountOfDrones = 0;
    static int amountOfDronesCollected = 0;


    public bool isCollected = false;
    void Start()
    {
        IntializeDroneState();
        MainManager = GameObject.Find("MainManager");
        CommunicateChangeTotalAmountOfDrones(true);
        FindObjectOfType<PlayerController>().batteryCatchEvent += GotBattery;

    }

    // Update is called once per frame
    void Update()
    {
        HandleStateDrone();
    }

    void CommunicateChangeTotalAmountOfDrones(bool increase)
    {
        if(increase)
            amountOfDrones++;
        else
            amountOfDrones--;

        MainManager.GetComponent<MainManager>().UpdateAmountOfDrones(amountOfDrones);
    }

    void CommunicateChangeAmountCollectedDrones(bool increase)
    {
        if(increase)
            amountOfDronesCollected++;
        else
            amountOfDronesCollected--;

        MainManager.GetComponent<MainManager>().UpdateAmountOfCollectedDrones(amountOfDronesCollected);
    }


    void IntializeDroneState()
    {
        currentState = DroneState.TakeOff;
        currentFlightTime = totalFlightTime;
        takeOffSpeed = 0.02f / massDrone;
        hoverOscillationSpeed = 0.16f/  massDrone;
        RandomizeValuesCircle();
        
    }

    void HandleStateDrone()
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
                if (DrainBattery())
                    currentState = DroneState.Land;
                break;
            case DroneState.Land:
                if(LandCommand())
                {
                    
                    CommunicateChangeTotalAmountOfDrones(false);
                    if(isCollected)
                    {
                        isCollected = false;
                        CommunicateChangeAmountCollectedDrones(false);

                    }
                    Destroy(gameObject);
                }
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
        if(other.gameObject.CompareTag("Player") && !isCollected)
        { 
            Player = other.gameObject;
            currentState = DroneState.HoverAroundPlayer;
            isCollected = true;
            CommunicateChangeAmountCollectedDrones(true);
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

    public void RandomizeValuesCircle()
    {
        randomPi = Random.Range(0, 2*Mathf.PI); 
        randomRadius = Random.Range(0.5f, 1.0f);
        randomHeight = Random.Range(0.5f, 1.0f);
        angleHeight = Random.Range(0, 2*Mathf.PI);
    }
    Vector3 GetCircleAroundPlayerPosition()
    {
        Vector3 CirclePosition = RandomMovement();
        return new Vector3(Player.transform.position.x + CirclePosition.x, Player.transform.position.y + CirclePosition.y, Player.transform.position.z + CirclePosition.z );

    }

    void GotBattery()
    {
        if(isCollected)
        {
            
            currentFlightTime += totalFlightTime / (float)amountOfDronesCollected;
            if(currentFlightTime > totalFlightTime)
            {
                currentFlightTime = totalFlightTime;
            }
        }
    }

    

}
