using UnityEngine;
using UnityEngine.UI;

public class Tester : MonoBehaviour
{
    public void BeginvibrationNoAMP()
    {
        Vibrator.Vibrate(1000); // Vibrate for 1 second with the default amplitude
    }
    /*
    public void Beginvibration200()
    {
        Vibrator.Vibrate(1000, 200); // Vibrate for 1 second with an amplitude of 200
    }
    public void Beginvibration255()
    {
        Vibrator.Vibrate(1000, 255); // Vibrate for 1 second with the strongest amplitude
    }
    public void Stopvibration()
    {
        Vibrator.Cancel(); // Stop the vibration
    }
    public void Beginvibration100()
    {
        Vibrator.Vibrate(1000, 100); // Vibrate for 1 second with a weak amplitude
    }
    public void Beginvibration50()
    {
        Vibrator.Vibrate(1000, 50); // Vibrate for 1 second with a very weak amplitude
    }
    public void Beginvibration150()
    {
        Vibrator.Vibrate(1000, 150); // Vibrate for 1 second with a medium amplitude
    }
    */
}

