using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Crazyflie : Drone
{
public Crazyflie()
  {
    massDrone = 0.02f; // in grams
    totalFlightTime = 60.0f;
  }

}
