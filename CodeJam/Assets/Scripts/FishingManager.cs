using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class FishingManager : MonoBehaviour
{ 
    private List<Catch> possibleCatches = new();

    public GameObject presentationFlair;
    [HideInInspector] public GameObject catchPresentationObject;

    public GameObject normalFishPrefab;
    public GameObject bootPrefab; 
    public GameObject shrimpPrefab;
    public GameObject duckPrefab;
    public GameObject clownFishPrefab;
    public GameObject swordFishFishPrefab;
    public GameObject blobFishPrefab;
    public GameObject pufferFishPrefab;
    public GameObject crabFishPrefab;
    public GameObject fishAndChipsPrefab;
    public GameObject catFishPrefab;
    public GameObject flatFishPrefab;
    public GameObject rainbowFishPrefab;

    private bool isFishing = false;
    public bool isDisplaying = false;
    private bool isCatchable = false;

    public float maxCatchTime=10;

    private float catchingTimer = 0f;

    private int convertToMilliseconds = 1000;

    private Catch currentCatch;

    public List<Catch> typesOfCatches = new List<Catch>
    {
        new BlobFish(),
        new Boot(),
        new CatFish(),
        new ClownFish(),
        new CrabFish(),
        new DuckFish(),
        new Fish(),
        new FishAndChips(),
        new FlatFish(),
        new PufferFish(),
        new RainbowFish(),
        new ShrimpFish(),
        new SwordFish()
    };

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


        foreach (Catch @catch in typesOfCatches)
        {
            for (int i = @catch.possibleElements; i>=0; i--)
            {
                possibleCatches.Add(@catch);
            }   
        }

        //debug the list of possible things
        foreach (Catch @catch in possibleCatches)
        {
            Debug.Log(@catch.type);
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
        if (!isFishing)
        {

            Debug.Log("Fishing initiated");
            StartCoroutine(Wait());
        }
        else {
            //waitText.text = "Already fishing.";
        }
    }

    IEnumerator Wait()
    {
        isFishing = true;
        //wait for 2-10 seconds: adjust the timing if needed
        yield return new WaitForSeconds(Random.Range(2, maxCatchTime));
        ReadyToCatch();
        yield break;
    }

    void ReadyToCatch()
    {
        Debug.Log("something has bit onto the fishing rod..");
        
        currentCatch = possibleCatches[Random.Range(0, possibleCatches.Count - 1)];
        Vibrator.Vibrate(currentCatch.catchInSeconds* convertToMilliseconds);  // This Needs to be tested

        isCatchable = true;
    }

    private void TryCatch()
    {
        Vibrator.Cancel();
        if (isCatchable)
        {
            Debug.Log($"Caught a {currentCatch.type}!");
            // this is just a stand-in; connect this to D's code and update the inventory

            if (Inventory.instance == null)
                Debug.LogWarning("there�s no inventory instnce :(");
            else
                Inventory.instance.AddFish(currentCatch.type);           

            //then triumphantly display the catch and return to the
            //"not actively fishing" screen (before the fishing rod is cast out)
            fishCounter++;
            Upgrades.money += currentCatch.gold;
            fishCounterText.text = "Fish acquired: " + fishCounter.ToString();
            DisplayCatch(currentCatch);
        }
        else
        {
            Debug.Log("Nothing to catch yet :)");
            Reset();
        }
            

    }

    public void DisplayCatch(Catch type)
    {
        isDisplaying = true;

        //activate the triumphant display
        presentationFlair.SetActive(true);
        switch (type.type)
        {
            case FishType.normal:
                catchPresentationObject = Instantiate(normalFishPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.boot:
                catchPresentationObject = Instantiate(bootPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.shrimp:
                catchPresentationObject = Instantiate(shrimpPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.duck:
                catchPresentationObject = Instantiate(duckPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.clown:
                catchPresentationObject = Instantiate(clownFishPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.sword:
                catchPresentationObject = Instantiate(swordFishFishPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.blob:
                catchPresentationObject = Instantiate(blobFishPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.puffer:
                catchPresentationObject = Instantiate(pufferFishPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.crab:
                catchPresentationObject = Instantiate(crabFishPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.chips:
                catchPresentationObject = Instantiate(fishAndChipsPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.cat:
                catchPresentationObject = Instantiate(catFishPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.flat:
                catchPresentationObject = Instantiate(flatFishPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.rainbow:
                catchPresentationObject = Instantiate(rainbowFishPrefab, Vector3.zero, Quaternion.identity);
                break;

            default:
                Debug.Log("you messed something up and fished something that we haven't programmed yet :(");
                break;
        }

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

public enum FishType
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
