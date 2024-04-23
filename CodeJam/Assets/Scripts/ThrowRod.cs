using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRod : MonoBehaviour
{
    public float threshold = 1.0f; // Set your threshold value here

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 acceleration = Input.acceleration;
        Debug.Log("Acceleration: " + acceleration);

        if (acceleration.sqrMagnitude > threshold * threshold) // Compare squares for performance
        {
            Debug.Log("Acceleration exceeded threshold!");
            // Add your code here to handle the acceleration exceeding the threshold
        }
    }
}