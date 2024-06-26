using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimate : MonoBehaviour
{
    public AnimationCurve moveCurve;
    public float moveTimer;
    public Transform catchPositionTransform;
    [HideInInspector] public Vector3 catchPosition;

    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        catchPosition = new Vector3(catchPositionTransform.position.x, catchPositionTransform.position.y, transform.position.z);
        ThrowRod.instance.OnUsingRod += MoveToCatch;
        ThrowRod.instance.OnPullRod += MoveToStart;
    }

    private void OnDisable()
    {
        ThrowRod.instance.OnUsingRod -= MoveToCatch;
        ThrowRod.instance.OnPullRod -= MoveToStart;
    }

    public void MoveToCatch()
    {
        StartCoroutine(MoveCam(moveTimer, transform.position, catchPosition));
    }

    public void MoveToStart()
    {
        StartCoroutine(MoveCam(moveTimer, transform.position, startPosition));

    }

    //
    public void PullBack(float pullAmount)
    {
        var pull = transform.position.y - pullAmount;
        if (pull<startPosition.y)
        {
            pull = startPosition.y;
        }
        MoveToPosition(new Vector3(0, pull, 0));
    }

    public void MoveToPosition(Vector3 newPos)
    {
        Vector3 movePos = new Vector3(newPos.x, newPos.y, transform.position.z);
        StartCoroutine(MoveCam(moveTimer,transform.position,movePos));
    }
    // https://medium.com/@rhysp/lerping-with-coroutines-and-animation-curves-4185b30f6002
    private IEnumerator MoveCam(float time, Vector3 start, Vector3 end)
    {
        float journey = 0f;
        while (journey <= time)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / time);
            float curvePercent = moveCurve.Evaluate(percent);
            transform.position = Vector3.LerpUnclamped(start, end, curvePercent);
            yield return null;
        }
    }
}
