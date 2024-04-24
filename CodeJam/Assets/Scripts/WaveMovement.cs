using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    [Header("Y-axis")]
    [SerializeField] private float waveSpeedY=1;
    [SerializeField] private float waveAmountY=1;
    [Header("X-axis")]

    [SerializeField] private float waveSpeedX = 1;
    [SerializeField] private float waveAmountX = 1;
    void Update()
    {
        transform.localPosition = new Vector3(Mathf.Sin(Time.time * waveSpeedX) * waveAmountX, Mathf.Sin(Time.time * waveSpeedY)*waveAmountY, 0);
    }
}
