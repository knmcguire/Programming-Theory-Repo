using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Bolt : Drone
{
    public Bolt()
    {
        massDrone = 0.06f; // in grams
        totalFlightTime = 90.0f;
        maxHeightRand = 1.5f;
        minHeightRand = 0.5f;
        innerCircleRand = 1.0f;
        outerCircleRand = 1.5f;

    }

    // Polymorphism

    protected override void HoverCommand(Vector3 startPosition)
    {
        Vector3 v = startPosition;
        v.x += 0.1f * Mathf.Sin((Time.time-timeStartHover) * hoverOscillationSpeed);
        transform.position = v;
    }
}
