using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    // we create TAU which is 2pi (complete lap around circle)
    const float TAU = Mathf.PI * 2;

    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // (NaN,Nan,Nan) error means we are dividing by 0
        // WHY DO WE DO MATHf.Epsilon?
        /*
            idea is that we want to avoid the Nan error so we initially
            would do something like if (period == 0f). This is BAD 
            to compare floats with 0. this is because floats are SO precies
            there may be chances where it does not equal 0 exact. 
            Mathf.Epsilon is the smallest or known value closest to 0.
            when checking against 0 for floats use <= Mathf.Epsilon :)
        */
        if(period <= Mathf.Epsilon) return;
        CalculatePosition();
    }

    void CalculatePosition(){
        float cycles = Time.time / period;
        float rawSinWave = Mathf.Sin(cycles * TAU);
        /*
            rawSinWave = -1 to 1. 
            Add 1 = 0 to 2 
            Divide 2 = 0 to 1
        */
        movementFactor = (rawSinWave + 1f) / 2f; //instead of -1 to 1 we want 0 to1
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    
    }
}

