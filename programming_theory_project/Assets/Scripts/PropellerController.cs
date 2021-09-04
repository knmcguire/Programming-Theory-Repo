using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerController : MonoBehaviour
{

    public bool clockWiseRotation = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 5000.0f;

        if (clockWiseRotation)
            speed = 5000.0f;
        else
            speed = -5000.0f;
        transform.Rotate(Vector3.up, 5000.0f * Time.deltaTime);

    }
}
