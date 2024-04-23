using UnityEngine;
using UnityEngine.UI;

public class Tester : MonoBehaviour
{
    public void Beginvibration()
    {
        Vibrator.Vibrate(500, 150); // Vibrate for 500 milliseconds with amplitude 150
    }
    public void BeginLongVibration()
    {
        Vibrator.Vibrate(10000, 255); // vibrate for 10 second with amplitude 255
    }
    public void Stopvibration()
    {
        Vibrator.Cancel(); // Stop the vibration
    }
}
