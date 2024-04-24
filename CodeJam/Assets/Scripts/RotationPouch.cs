using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
    }

    void Update()
{
    // Get the device's attitude (rotation relative to the world)
    Quaternion deviceRotation = Input.gyro.attitude;

    // Convert the device's attitude to a 2D rotation
    float rotationZ = -deviceRotation.y * 90f;

    // Apply the rotation to the object
    transform.rotation = Quaternion.Euler(0, 0, rotationZ);
}
}
