using UnityEngine;
using UnityEngine.UI;

public class Tester : MonoBehaviour
{
    public void BeginvibrationNoAMP()
    {
        Vibrator.Vibrate(1000); // Vibrate for 1 second with the default amplitude
    }
}

