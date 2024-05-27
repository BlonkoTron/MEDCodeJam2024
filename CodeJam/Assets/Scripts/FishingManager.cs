using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public enum FishType //13 types
{
    normal = 0,
    boot = 12,
    shrimp = 10,
    duck = 5,
    clown = 8,
    sword = 7,
    blob = 2,
    puffer = 9,
    crab = 11,
    chips = 6,
    cat = 1,
    flat = 4,
    rainbow = 3
}

[System.Serializable]
public class Fish
{
    public GameObject fishPrefab;
    public float spawnChance;
    public FishType type;
    public int catchInSeconds;
    public int gold;
}

public class FishingManager : MonoBehaviour
{ 
    [SerializeField] private List<Fish> possibleCatches = new();
    private List<Fish> Catches = new();

    public GameObject presentationFlair;
    [HideInInspector] public GameObject catchPresentationObject;


    public AudioClip vibrateSound;

    public GameObject bobber;

    private bool isFishing = false;
    public bool isDisplaying = false;
    private bool isCatchable = false;

    public float maxCatchTime=10;

    private float catchingTimer = 0f;

    private int convertToMilliseconds = 1000;

    private Fish currentCatch;

    public bool usingTestControls = false;

    public TMP_Text fishCounterText;
    private int fishCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        //subscribe to events
        ThrowRod.instance.OnUsingRod += StartFishing;
        ThrowRod.instance.OnPullRod += TryCatch;

        presentationFlair.SetActive(false);

        for (int i = 0; i < possibleCatches.Count; i++)
        {
            int numOfItems = Mathf.RoundToInt(possibleCatches[i].spawnChance * 100f);

            for (int j = 0; j < numOfItems; j++)
            {
                Catches.Add(possibleCatches[i]);
            }

        }

    }

    //unsubscribe from events to prevent memory mess
    private void OnDisable()
    {
        ThrowRod.instance.OnUsingRod -= StartFishing;
        ThrowRod.instance.OnPullRod -= TryCatch;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCatchable)
        {
            catchingTimer += Time.deltaTime;

            if (catchingTimer >= currentCatch.catchInSeconds && !isDisplaying)
            {
                Debug.Log("Took too long! The fish escaped!");
                Reset();
            }
        }

        if (usingTestControls)
        {
            if (Input.GetKeyDown(KeyCode.W)) //replace this with whatever initiates the fishing sequence
                                             //(i.e. when the bob is out on the water waiting for fish)
            {
                StartFishing();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                TryCatch();
            }
        }

        if (isDisplaying && Input.GetMouseButtonDown(0))
        {
            Reset();
        }

        if (isFishing)
        {
            Upgrades.doDisplayUI = false;
        }
        else
        {
            Upgrades.doDisplayUI = true;
        }

    }

    void StartFishing()
    {
        Reset(); ///Huhh?
        if (!isFishing)
        {

            Debug.Log("Fishing initiated");
            StartCoroutine(Wait());
        }
        else {
            //waitText.text = "Already fishing.";
            Debug.Log("Already fishing.");
        }
    }

    IEnumerator Wait()
    {
        isFishing = true;
        Debug.Log("Waiting...");
        //wait for 2-10 seconds: adjust the timing if needed
        yield return new WaitForSeconds(Random.Range(2, maxCatchTime));
        ReadyToCatch();
        yield break;
    }

    void ReadyToCatch()
    {
        var myBobber = bobber.GetComponent<Shake>();
        myBobber.ShakeMe();
        currentCatch = possibleCatches[Random.Range(0, possibleCatches.Count - 1)];
        Vibrator.Vibrate(currentCatch.catchInSeconds* convertToMilliseconds);  // This Needs to be tested
        AudioManager.PlaySound(vibrateSound);
        isCatchable = true;
    }

    private void TryCatch()
    {
        var myBobber = bobber.GetComponent<Shake>();
        myBobber.StopShake();
        Vibrator.Cancel();
        if (isCatchable)
        {
            Debug.Log($"Caught a {currentCatch.type}!");
            // this is just a stand-in; connect this to D's code and update the inventory

            if (Inventory.instance == null)
                Debug.LogWarning("there�s no inventory instance :(");
            else
                Inventory.instance.AddFish(currentCatch.type);           

            fishCounter++;
            Upgrades.money += currentCatch.gold;
            fishCounterText.text = "Fish acquired: " + fishCounter.ToString();
            DisplayCatch(currentCatch.fishPrefab);
        }
        else
        {
            Debug.Log("Nothing to catch yet :)");
            Reset();
        }
            

    }

    public void DisplayCatch(GameObject fishDisplayPrefab)
    {
        isDisplaying = true;

        //activate the triumphant display

        catchPresentationObject = Instantiate(fishDisplayPrefab, Vector3.zero, Quaternion.identity);

        presentationFlair.SetActive(true);

    }

    private void Reset()
    {
        Debug.Log("Resetting...");
        presentationFlair.SetActive(false);
        Destroy(catchPresentationObject);
        isFishing = false;
        isDisplaying = false;
        
        isCatchable = false;
        catchingTimer = 0f;
        StopAllCoroutines();

        //waitText.text = "Reset.";
        Debug.Log("Fishing has reset.");

    }
}