using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDude : MonoBehaviour
{
    void Start()
    {
        ThrowRod.instance.OnUsingRod += DisableMe;
    }

    private void DisableMe()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        ThrowRod.instance.OnUsingRod -= DisableMe;
    }
}
