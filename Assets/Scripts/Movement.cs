using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    const float MOVEMENT_THRUST = 1000f;
    const float ROTATION_THRUST = 50f;

    Rigidbody rb;
    AudioSource audioSource;
    AudioClip thrustSound;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        thrustSound = Resources.Load<AudioClip>("Sounds/thrust");
    }

    void Update()
    {
        ProcessThrustInput();
        ProcessRotationInput();
    }

    void ProcessThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(thrustSound);
                mainEngineParticles.Play();
            }
            rb.AddRelativeForce(Vector3.up * MOVEMENT_THRUST * Time.deltaTime);
        }
        else
        {
            StopThrustEffects();
        }
    }

    void ProcessRotationInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(ROTATION_THRUST);
            EnsureParticleEffect(rightThrusterParticles);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-ROTATION_THRUST);
            EnsureParticleEffect(leftThrusterParticles);
        }
        else
        {
            StopRotationEffects();
        }
    }

    private void EnsureParticleEffect(ParticleSystem particles)
    {
        if (!particles.isPlaying)
        {
            particles.Play();
        }
    }

    void ApplyRotation(float newRotation){
        rb.freezeRotation= true;
        transform.Rotate(Vector3.forward * newRotation * Time.deltaTime);
        rb.freezeRotation= false;
    }

    void StopThrustEffects()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    void StopRotationEffects()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }

}
