using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    [SerializeField] private float waveSpeed=1;
    [SerializeField] private float waveAmount=1;

    void Update()
    {
        transform.localPosition = new Vector3(0, Mathf.Sin(Time.time * waveSpeed)*waveAmount, 0);
    }
}
