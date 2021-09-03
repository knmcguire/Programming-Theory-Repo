using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : Drone
{
    public Bolt()
    {
        massDrone = 0.06f; // in grams
    }

    protected override void HoverCommand()
    {
        Vector3 v = hoverStartPos;
        v.x += 0.1f * Mathf.Sin(Time.time * hoverOscillationSpeed);
        transform.position = v;
    }
}
