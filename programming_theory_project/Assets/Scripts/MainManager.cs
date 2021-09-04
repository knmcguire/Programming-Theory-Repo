using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainManager : MonoBehaviour
{
    public Text amountOfDronesText;
    public Text amountOfCollectedDronesText;
    public Text averageFlightTimeText;

    // Start is called before the first frame update
    void Start()
    {


        amountOfDronesText.text = $"Amount of Drones : 0";
        amountOfCollectedDronesText.text = $"Amount of Collected Drones : 0";
        averageFlightTimeText.text=  $"Average Flight Time Left : 0";


    }


    // Update is called once per frame
    void Update()
    {
         UpdateAverageFlightTime();
    }

    public void UpdateAverageFlightTime()
    {
        float totalFlightTime = 0;
        int totalCollectedDrones = 0;
        var foundCrazyflieObjects =FindObjectsOfType<Crazyflie>();

        foreach(Crazyflie crazyflie in foundCrazyflieObjects)
        {
            if(crazyflie.isCollected)
            {
                totalFlightTime += crazyflie.currentFlightTime;
                totalCollectedDrones ++;
            }
        }

        var foundBoltObjects =FindObjectsOfType<Bolt>();

        foreach(Bolt bolt in foundBoltObjects)
        {
            if(bolt.isCollected)
            {
                totalFlightTime += bolt.currentFlightTime;
                totalCollectedDrones ++;
            }
        }


        if( foundCrazyflieObjects.Length != 0)
        {
            float averageFlightTime = totalFlightTime / (float)totalCollectedDrones;
            Debug.Log(averageFlightTime);
            averageFlightTimeText.text = $"Average Flight Time Left : {averageFlightTime}";
        }

    }


    public void UpdateAmountOfDrones(int amountOfDrones)
    {
        amountOfDronesText.text = $"Amount of Drones : {amountOfDrones}";
    }

    public void UpdateAmountOfCollectedDrones(int amountOfDrones)
    {
        amountOfCollectedDronesText.text = $"Amount of Collected Drones : {amountOfDrones}";
    }
}
