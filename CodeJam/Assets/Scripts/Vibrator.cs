using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vibrator : MonoBehaviour
{
    // Code refrence https://www.youtube.com/watch?v=o6xVLzs1kVk&list=PL-tWl7XQF8U-RXQgjBzp3M_6hf2ZthnQy&index=2 

    // regular if-else statements conditions will be evaluated when the script runs, not during compilation. The preprocessor directives are evaluated during compilation.

#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif
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
    private static bool isAndroid() // Checks if the platform is Android
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }
}