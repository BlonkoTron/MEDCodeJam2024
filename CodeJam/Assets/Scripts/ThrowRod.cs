using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrowRod : MonoBehaviour
{
public float threshold = 3.0f; // Set your threshold value here
private bool isThrown = false;

private float ignoreInputUntil = 0f;
private float ignoreInputDuration = 1f; // Ignore input for 1 second

public static ThrowRod instance;

public delegate void UsingRod();

public event UsingRod OnUsingRod;


public event UsingRod OnPullRod;

void Awake()
{
    if (instance == null)
    {
        instance = this;
    }
}
void Update()
{
    if (Time.time < ignoreInputUntil)
    {
        return; // Ignore input
    }

    Vector3 acceleration = Input.acceleration;

    if (acceleration.sqrMagnitude > threshold * threshold && !isThrown)
    {
        OnUsingRod?.Invoke();
        Debug.Log("Thrown!");
        isThrown = true;
        ignoreInputUntil = Time.time + ignoreInputDuration; // Set the time until which to ignore input
    }
    else if (acceleration.sqrMagnitude > threshold * threshold && isThrown)
    {
        isThrown = false;
        OnPullRod?.Invoke();
        Debug.Log("Pulled in!");
        ignoreInputUntil = Time.time + ignoreInputDuration; // Set the time until which to ignore input
    }
}
IEnumerator WaitAndMove()
{
    // Wait for a random amount of time between 1 and 20 seconds before starting the time slot
    yield return new WaitForSeconds(Random.Range(1, 10));
    Debug.Log("The Fish is here!");
    // Start the time slot
    float startTime = Time.time;
    float timeSlotDuration = 3.0f; // Set the duration of the time slot here

    // Wait until the end of the time slot
    while (Time.time - startTime < timeSlotDuration)
    {
         // Check if the action has been performed
        if (isThrown == false)
        {
            Debug.Log("Pulled at the right time!");
            yield break; // Stop the coroutine here
        }
    yield return null; // Wait for the next frame
    }

    // If the action wasn't performed during the time slot
    if (isThrown)
    {
        Debug.Log("Pulled too late!");
        yield break; // Stop the coroutine here
        // Add your code here to handle the end of the time slot without action
    }
  
}

}