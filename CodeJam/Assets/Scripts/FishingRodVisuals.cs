using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRodVisuals : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject bobber;
    [SerializeField] private GameObject watersplash;
    private Vector3 bobberStartPos;
    [SerializeField] private AnimationCurve moveCurve;
    [SerializeField] private FishingManager fishManager;
    private CameraAnimate camAnim;
    private SpriteRenderer spr;
    [SerializeField] private AudioClip throwSound, reelInSound, hitwaterSound;

    void Start()
    {
        watersplash.SetActive(false);
        spr = GetComponent<SpriteRenderer>();
        bobberStartPos = bobber.transform.position;
        bobber.SetActive(false);
        anim = GetComponent<Animator>();
        camAnim = Camera.main.GetComponent<CameraAnimate>();
        ThrowRod.instance.OnUsingRod += ThrowingRod;
        ThrowRod.instance.OnPullRod += PullBack;
    }
    private void Update()
    {
        if (fishManager.isDisplaying)
        {
            spr.enabled = false;
        }
        else
        {
            spr.enabled = true;
        }
    }
    private void ThrowingRod()
    {
        AudioManager.PlaySound(throwSound);
        anim.SetTrigger("Throw");
        bobber.SetActive(true);
        Vector3 endPos = new Vector3(camAnim.catchPosition.x, camAnim.catchPosition.y, transform.position.z);
        StartCoroutine(Move(bobber.transform, camAnim.moveTimer, transform.position, endPos));
        StartCoroutine(WaitToTrigger(camAnim.moveTimer));
    }
    private void PullBack()
    {
        AudioManager.PlaySound(reelInSound);
        bobber.GetComponent<Animator>().SetTrigger("bobberChange");
        watersplash.SetActive(false);
        StartCoroutine(Move(bobber.transform, camAnim.moveTimer, bobber.transform.position, bobberStartPos));
        StartCoroutine(WaitToHide(camAnim.moveTimer));
    }
    private void OnDestroy()
    {
        ThrowRod.instance.OnUsingRod -= ThrowingRod;
        ThrowRod.instance.OnPullRod -= PullBack;
    }
    private IEnumerator Move(Transform tr,float time, Vector3 start, Vector3 end)
    {
        float journey = 0f;
        while (journey <= time)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / time);
            float curvePercent = moveCurve.Evaluate(percent);
            tr.position = Vector3.LerpUnclamped(start, end, curvePercent);
            yield return null;
        }
    }
    private IEnumerator WaitToTrigger(float time)
    {
        yield return new WaitForSeconds(time);
        bobber.GetComponent<Animator>().SetTrigger("bobberChange");
        watersplash.SetActive(true);
        AudioManager.PlaySound(hitwaterSound);
    }
    private IEnumerator WaitToHide(float time)
    {
        yield return new WaitForSeconds(time);
        bobber.SetActive(false);
    }
}
