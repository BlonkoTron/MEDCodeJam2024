using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    private AudioSource soundPlayer;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        soundPlayer = GetComponentInChildren<AudioSource>();
    }
    public void PlaySound(AudioClip clip)
    {
        soundPlayer.PlayOneShot(clip);
    }
}
