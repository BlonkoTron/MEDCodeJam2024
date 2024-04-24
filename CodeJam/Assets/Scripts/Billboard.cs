using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera targetCamera; // Reference to the camera
    public Axis lockAxis; // Choose which camera axis to lock to

    void Update()
    {
        if (targetCamera == null)
        {
            targetCamera = GameObject.Find("Pouch Camera").GetComponent<Camera>(); // Find camera by name
            if (targetCamera == null)
            {
                Debug.LogError("Camera 'Pouch Camera' not found!");
                return;
            }
        }

        // Get camera's forward vector (Z-axis by default)
        Vector3 cameraForward = targetCamera.transform.forward;

        // Calculate rotation based on lockAxis
        Vector3 newRotation;
        switch (lockAxis)
        {
            case Axis.X:
                newRotation = new Vector3(Mathf.Atan2(cameraForward.y, cameraForward.z) * Mathf.Rad2Deg, 0f, 0f);
                break;
            case Axis.Y:
                newRotation = new Vector3(0f, Mathf.Atan2(cameraForward.x, cameraForward.z) * Mathf.Rad2Deg, 0f);
                break;
            case Axis.Z:
                // No rotation needed for Z-axis locking (already facing forward)
                newRotation = Vector3.zero;
                break;
            default:
                Debug.LogError("Invalid lockAxis value!");
                return;
        }

        // Apply rotation (ignoring unwanted axis rotation)
        transform.rotation = Quaternion.Euler(newRotation.x, 0f, 0f);
    }

    public enum Axis
    {
        X,
        Y,
        Z
    }
}
