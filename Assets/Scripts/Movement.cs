using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float movementThrust = 1000f;
    float rotationThrust = 50f;

    Rigidbody rb;
    AudioSource audioSource;
    AudioClip thrust;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        thrust = Resources.Load<AudioClip>("Sounds/thrust");
    }

    void Update()
    {
        ProcessMovement();
    }

    void ProcessMovement(){
        // if thrust
        if (Input.GetKey(KeyCode.Space)){
            PlayAudio(false);
            rb.AddRelativeForce(Vector3.up * movementThrust * Time.deltaTime);
        } else {
            PlayAudio(true);
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

    void PlayAudio(bool stop){
        if (stop){
            audioSource.Stop();
        } else if (!audioSource.isPlaying){
            // at this point we know we want to play autio
            audioSource.PlayOneShot(thrust);
        }
    }
}
