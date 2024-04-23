using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRod : MonoBehaviour
{
    public float threshold = 3.0f; // Set your threshold value here
    private bool isThrown = false;

    
    // Update is called once per frame
    void Update()
    {
        Vector3 acceleration = Input.acceleration;
        

        if (acceleration.sqrMagnitude > threshold * threshold) // Compare squares for performance
        {
            Debug.Log("Acceleration exceeded threshold!");
            isThrown = true;
            StartCoroutine(WaitAndMove());
            
        }
        if (acceleration.sqrMagnitude > threshold * threshold && isThrown == true)
        {
            Debug.Log("Rod is reeled in!");
            isThrown = false;
        }
    }
IEnumerator WaitAndMove()
{
    // Wait for a random amount of time between 1 and 20 seconds
    yield return new WaitForSeconds(Random.Range(1, 20));

    // Start the time slot
    float startTime = Time.time;
    float timeSlotDuration = 3.0f; // Set the duration of the time slot here

    // Wait until the end of the time slot
    while (Time.time - startTime < timeSlotDuration)
    {
        // Check if the action has been performed
        if (isThrown)
        {
            Debug.Log("Move action performed!");
            // Add your move code here
            break;
        }
        yield return null; // Wait for the next frame
    }

    // If the action wasn't performed during the time slot
    if (!isThrown)
    {
        Debug.Log("Time slot ended without action!");
        // Add your code here to handle the end of the time slot without action
    }
}

}