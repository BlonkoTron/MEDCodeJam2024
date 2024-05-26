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
    if (Input.GetKeyDown(KeyCode.T) && !isThrown)
    {
        OnUsingRod?.Invoke();
        Debug.Log("Thrown!");
        isThrown = true;
        ignoreInputUntil = Time.time + ignoreInputDuration; // Set the time until which to ignore input
    }
    else if (Input.GetKeyDown(KeyCode.T) && isThrown)
        {
        isThrown = false;
        OnPullRod?.Invoke();
        Debug.Log("Pulled in!");
        ignoreInputUntil = Time.time + ignoreInputDuration; // Set the time until which to ignore input
    }

}



}