using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRodVisuals : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        ThrowRod.instance.OnUsingRod += ThrowingRod;
    }

    private void ThrowingRod()
    {
        anim.SetTrigger("Throw");
    }
    private void OnDisable()
    {
        ThrowRod.instance.OnUsingRod -= ThrowingRod;
    }
}
