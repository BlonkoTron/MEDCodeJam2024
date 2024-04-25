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

    //adjust these based on the probabilities we want
    private int possibleFishes = 15;
    private int possibleBoots = 3;

    //the visual representation, assign in inspector
    public GameObject restSprite;
    public GameObject bouncingSprite;

    //public Text waitText;

    public GameObject presentationFlair;
    public GameObject catchPresentationObject;

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

    //this is just a stand in until the real inventory is added
    //public List<Catch> inventory = new();

    public bool usingTestControls = false;

    public TMP_Text fishCounterText;
    private int fishCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        //subscribe to events
        ThrowRod.instance.OnUsingRod += StartFishing;
        ThrowRod.instance.OnPullRod += TryCatch;

        //disappear the fishing bob until fishing starts
        restSprite.SetActive(false);
        bouncingSprite.SetActive(false);

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
        /*
        for (int i = possibleBoots; i>=0; i--)
        {
            possibleCatches.Add(new Boot());
        }
        for (int i = possibleFishes; i >= 0; i--)
        {
            possibleCatches.Add(new Fish());
        }
        */
        //waitText = FindObjectOfType<Text>();
        //waitText.text = "Started the system, ready to fish...";
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
        //waitText.text = "Starting fishing...";
        if (!isFishing)
        {
            //Deploy the fishing bob in restful animation
            restSprite.SetActive(true);
            //restSprite.GetComponent<SpriteRenderer>().color = Color.white;

            Debug.Log("Fishing initiated");
            //waitText.text = "Fishing initiated, trying Coroutine...";
            StartCoroutine(Wait());
        }
        else {
            //waitText.text = "Already fishing.";
        }
    }

    IEnumerator Wait()
    {
        Debug.Log("Starting to wait...");
        //waitText.text = "Starting to wait...";
        isFishing = true;
        //wait for 2-10 seconds: adjust the timing if needed
        yield return new WaitForSeconds(Random.Range(2, maxCatchTime));

        Debug.Log("Something caught :)");
        //waitText.text = "Something caught!";
        ReadyToCatch();
        yield break;
    }

    void ReadyToCatch()
    {
        //waitText.text = "Ready to catch";
        Debug.Log("something has bit onto the fishing rod..");
        
        currentCatch = possibleCatches[Random.Range(0, possibleCatches.Count - 1)];
        //waitText.text = "trying to vibrate";
        Vibrator.Vibrate(currentCatch.catchInSeconds* convertToMilliseconds);  // This Needs to be tested
        //Debug.Log(currentCatch.catchInSeconds);

        //instead of this, replace the "resting" fishing bob with the bouncing one
        //to indicate something having bitten
        //restSprite.GetComponent<SpriteRenderer>().color = Color.red;
        //waitText.text = "trying to change sprites";
        restSprite.SetActive(false);
        bouncingSprite.SetActive(true);

        isCatchable = true;
    }

    private void TryCatch()
    {
        Vibrator.Cancel();
        //waitText.text = "trying to catch";
        if (isCatchable)
        {
            // this is just a stand-in; connect this to D's code and update the inventory
            Inventory.instance.AddFish(currentCatch.type);

            //waitText.text = "Success!";
            Debug.Log($"Caught a {currentCatch.type}!");

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
            //waitText.text = "No catch or not catchable :)";
            Reset();
        }
            

    }

    public void DisplayCatch(Catch type)
    {
        //waitText.text = "trying to display";
        isDisplaying = true;

        //remove the fishing bob
        bouncingSprite.SetActive(false);

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
                catchPresentationObject = Instantiate(normalFishPrefab, Vector3.zero, Quaternion.identity);
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
        //waitText.text = "Resetting...";
        Debug.Log("Resetting...");
        presentationFlair.SetActive(false);
        Destroy(catchPresentationObject);
        isFishing = false;
        isDisplaying = false;
        
        isCatchable = false;
        catchingTimer = 0f;

        restSprite.SetActive(false);
        bouncingSprite.SetActive(false);
        StopAllCoroutines();

        //waitText.text = "Reset.";
        Debug.Log("Fishing has reset.");

    }
}

public enum FishType
{
    normal,
    boot,
    shrimp,
    duck,
    clown,
    sword,
    blob,
    puffer,
    crab,
    chips,
    cat,
    flat,
    rainbow
}
