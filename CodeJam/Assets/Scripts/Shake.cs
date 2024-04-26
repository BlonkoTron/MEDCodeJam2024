using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{

    [SerializeField] private float shakeSpeed;
    [SerializeField] private float shakeAmount;
    [SerializeField] private float shakeTime;

    public void ShakeMe()
    {
        StartCoroutine(DoShake(shakeTime));
    }
    public void StopShake()
    {
        StopAllCoroutines();
    }
    private IEnumerator DoShake(float time)
    {
        Vector3 startPos = transform.position;
        float journey = 0f;
        while (journey <= time)
        {
            journey = journey + Time.deltaTime;
            transform.position = startPos + new Vector3(0,Mathf.Abs( Mathf.Sin(Time.time * shakeSpeed) * shakeAmount), 0);
            yield return null;
        }

    }
}
