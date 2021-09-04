using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public delegate void batteryCatchDelegate();
    public event batteryCatchDelegate batteryCatchEvent;

    float rangeArena = 5;
    private float speed = 10.0f;
    private float turnSpeed = 90.0f;


    private float horizontalInput;
    private float forwardInput;

    // Start is called before the first frame update
    void Start()
    {
        if(batteryCatchEvent != null)
        {
            batteryCatchEvent();
        }   

    }

    // Update is called once per frame
    void Update()
    {
        KeepPlayerInArena();
        ControlPlayer();

    }

    // Keyboard input to control the player
    void ControlPlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
        
            }

// Check if player reached the end of the arena
    void KeepPlayerInArena()
    {
        if (transform.position.x < -rangeArena)
        {
            transform.position = new Vector3(-rangeArena, transform.position.y, transform.position.z);
        }
        if (transform.position.x > rangeArena)
        {
            transform.position = new Vector3(rangeArena, transform.position.y, transform.position.z);
        }
        if (transform.position.z < -rangeArena)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -rangeArena);
        }
        if (transform.position.z > rangeArena)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, rangeArena);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Battery"))
        { 
            batteryCatchEvent();
            Destroy(other.gameObject);
        }
    }
    
}
