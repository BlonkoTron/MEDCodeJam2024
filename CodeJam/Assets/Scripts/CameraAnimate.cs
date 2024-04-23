using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimate : MonoBehaviour
{
    public AnimationCurve moveCurve;
    public float moveTimer;
    public Transform MoveToPosition;

    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    public void MoveToCatch()
    {
        StartCoroutine(MoveCam(moveTimer, transform.position, MoveToPosition.position));
    }

    private IEnumerator MoveCam(float time, Vector3 start, Vector3 end)
    {
        float journey = 0f;
        while (true)
        {
            journey = journey + Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, moveCurve.Evaluate(journey));
            yield return null;
        }

    }
}
