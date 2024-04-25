using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]float movementThrust = 1000f;
    [SerializeField]float rotationThrust = 500f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovement();
    }

    void ProcessMovement(){
        // if thrust
        if (Input.GetKey(KeyCode.Space)){
            rb.AddRelativeForce(Vector3.up * movementThrust * Time.deltaTime);
        }
        // if rotation
        if (Input.GetKey(KeyCode.A)){
            ApplyRotation(rotationThrust);
        } else if (Input.GetKey(KeyCode.D)){
            ApplyRotation(-rotationThrust);
        }

    }

    void ApplyRotation(float newRotation){
        rb.freezeRotation= true;
        transform.Rotate(Vector3.forward * newRotation * Time.deltaTime);
        rb.freezeRotation= false;
    }
}
