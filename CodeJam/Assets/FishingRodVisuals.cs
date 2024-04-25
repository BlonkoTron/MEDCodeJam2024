using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRodVisuals : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject bobber;
    void Start()
    {
        bobber.SetActive(false);
        anim = GetComponent<Animator>();
        ThrowRod.instance.OnUsingRod += ThrowingRod;
    }

    private void ThrowingRod()
    {
        anim.SetTrigger("Throw");
        bobber.SetActive(true);
    }
    private void OnDisable()
    {
        ThrowRod.instance.OnUsingRod -= ThrowingRod;
    }
}
