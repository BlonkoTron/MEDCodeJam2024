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
    public AudioClip vibrateSound;

    public GameObject bobber;

    private bool isFishing = false;
    public bool isDisplaying = false;
    private bool isCatchable = false;

    public float maxCatchTime=10;

    private float catchingTimer = 0f;

    private int convertToMilliseconds = 1000;

    private Catch currentCatch;

    public List<Catch> typesOfCatches = new List<Catch>
    {
        new Catch(FishType.blob),
        new Catch(FishType.boot),
        new Catch(FishType.cat),
        new Catch(FishType.clown),
        new Catch(FishType.crab),
        new Catch(FishType.duck),
        new Catch(FishType.normal),
        new Catch(FishType.chips),
        new Catch(FishType.flat),
        new Catch(FishType.puffer),
        new Catch(FishType.rainbow),
        new Catch(FishType.shrimp),
        new Catch(FishType.sword)
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
        fishCounterText.text = "Fish acquired: " + fishCounter.ToString();
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

            Upgrades.Gold += currentCatch.gold; // Add the gold value of the catch to the player's total gold in Upgrades.cs    
            fishCounter++; // Increase fish counter       
            fishCounterText.text = "Fish acquired: " + fishCounter.ToString(); // Update fish counter text
            
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
        
        switch (type.type)
        {
            case FishType.normal: //1
                catchPresentationObject = Instantiate(normalFishPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.boot: //2
                catchPresentationObject = Instantiate(bootPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.shrimp: //3
                catchPresentationObject = Instantiate(shrimpPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.duck: //4
                catchPresentationObject = Instantiate(duckPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.clown: //5
                catchPresentationObject = Instantiate(clownFishPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.sword: //6
                catchPresentationObject = Instantiate(swordFishFishPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.blob: //7
                catchPresentationObject = Instantiate(blobFishPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.puffer: //8
                catchPresentationObject = Instantiate(pufferFishPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.crab: //9
                catchPresentationObject = Instantiate(crabFishPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.chips: //10
                catchPresentationObject = Instantiate(fishAndChipsPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.cat: //11
                catchPresentationObject = Instantiate(catFishPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.flat: //12
                catchPresentationObject = Instantiate(flatFishPrefab, Vector3.zero, Quaternion.identity);
                break;
            case FishType.rainbow: //13
                catchPresentationObject = Instantiate(rainbowFishPrefab, Vector3.zero, Quaternion.identity);
                break;
        }

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