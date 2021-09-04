using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainManager : MonoBehaviour
{
    public Text amountOfDronesText;
    public Text amountOfCollectedDronesText;
    
    // Start is called before the first frame update
    void Start()
    {


        amountOfDronesText.text = $"Amount of Drones : 0";
        amountOfCollectedDronesText.text = $"Amount of Collected Drones : 0";



    }


    // Update is called once per frame
    void Update()
    {

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
