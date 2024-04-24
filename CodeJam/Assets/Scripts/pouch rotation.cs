using UnityEngine;

public class GyroAndAccelRotation : MonoBehaviour
{
    public float sensitivity = 1.0f; // Adjust sensitivity for responsiveness

    private Quaternion referenceRotation;
    private Vector3 gravity;

    void Start()
    {
        // Enable gyroscope and compensate for sensor inaccuracies
        Input.compensateSensors = true;
        Input.gyro.enabled = true;

        // Store initial device orientation and gravity
        referenceRotation = Quaternion.Inverse(Input.gyro.attitude);
        gravity = Input.acceleration;
    }

    void Update()
    {
        // Get current device orientation and gravity
        Quaternion currentRotation = Input.gyro.attitude;
        Vector3 currentGravity = Input.acceleration;

        // Calculate relative rotation since the reference point
        Quaternion relativeRotation = referenceRotation * currentRotation;

        // Extract Z-axis rotation and scale it by sensitivity
        float zRotation = relativeRotation.eulerAngles.z * sensitivity;

        // Check if phone is down based on adjusted gravity vector
        bool isPhoneDown = Mathf.Abs(currentGravity.y) < Mathf.Abs(currentGravity.z) && currentGravity.y < 0;

        // Combine rotations if phone is down
        if (isPhoneDown)
        {
            // Rotate the object 180 degrees around the global X-axis to align down directions
            transform.localRotation = Quaternion.Euler(180f, 0f, 0f) * transform.localRotation;
        }
        else
        {
            // Apply only the Z-axis rotation from the gyroscope
            transform.localRotation = Quaternion.Euler(0, 0, zRotation);
        }
    }
}
