using UnityEngine;
using UnityEngine.UI;

public class Tester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vibrator.Vibrate(1000); // Vibrate for 1 second
    }
    public void Beginvibration()
    {
        Vibrator.Vibrate(500); // vibrate for 0.5 seconds
    }
    public void BeginLongVibration()
    {
        Vibrator.Vibrate(10000); // vibrate for 10 second
    }
    public void Stopvibration()
    {
        Vibrator.Cancel(); // Stop the vibration
    }

}
