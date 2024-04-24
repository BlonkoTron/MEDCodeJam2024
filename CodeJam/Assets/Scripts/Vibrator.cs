using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vibrator : MonoBehaviour
{
    // Code refrence https://www.youtube.com/watch?v=o6xVLzs1kVk&list=PL-tWl7XQF8U-RXQgjBzp3M_6hf2ZthnQy&index=2 

#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif
    /* When using Amp it dosent work in the build version ;(
    public static void Vibrate(long milliseconds = 250, int amplitude = 255) // Vibrate for a given number of milliseconds with a given amplitude
    {
        if (isAndroid())
        {
            vibrator.Call("vibrate", milliseconds, amplitude);
            Debug.Log(Debug.isDebugBuild ? milliseconds.ToString() : "");
        }
        else
        {
            Handheld.Vibrate(); 
        }
    }
    */
    public static void Vibrate(long milliseconds = 250) // Vibrate for a given number of milliseconds
    {
        if (isAndroid())
        {
            vibrator.Call("vibrate", milliseconds);
        }
        else
        {
            Handheld.Vibrate();
        }
    }


    public static void Cancel() // Cancels the vibration
    {
        if (isAndroid())
        {
            vibrator.Call("cancel");
        }
    }
    public static bool isAndroid() // Checks if the platform is Android
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }
    /*
    private int convertToMilliseconds = 1000;
    Vibrator.Vibrate(currentCatch.catchInSeconds* convertToMilliseconds);  // This Needs to be tested
    Debug.Log(currentCatch.catchInSeconds);
    Vibrator.Cancel();
    */
}