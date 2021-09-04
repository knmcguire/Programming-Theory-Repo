using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{



    private float turnSpeed = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(Vector3.up);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.up, turnSpeed * Time.deltaTime);

    }



    
}
