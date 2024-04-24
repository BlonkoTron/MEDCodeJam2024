using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class FishingManager : MonoBehaviour
{ 
    private List<Catch> possibleCatches = new();

    //adjust these based on the probabilities we want
    private int possibleFishes = 15;
    private int possibleBoots = 3;

    //the visual representation, assign in inspector
    public GameObject restSprite;
    public GameObject bouncingSprite;

    public GameObject waitText;

    public GameObject presentationFlair;
    public GameObject catchPresentationObject;

    public GameObject normalFishPrefab;
    public GameObject bootPrefab;
   

    private bool isFishing = false;
    private bool hasCatch = false;
    private bool isCatchable = false;

    private float catchingTimer = 0f;

    private Catch currentCatch;

    //this is just a stand in until the real inventory is added
    public List<Catch> inventory = new();
    public bool usingMouse = false;

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
        waitText.SetActive(false);
        
        /*
        //generate the list of possible things to catch
        possibleCatches = new Catch[possibleFishes + possibleBoots];
        for (int i = possibleCatches.Length-1; i>=0; i--)
        {
            if (possibleBoots > 0)
                possibleCatches[i] = new Boot();
            else
            {
                possibleCatches[i] = new Fish();
            }
        }
        */

        for (int i = possibleBoots; i>=0; i--)
        {
            possibleCatches.Add(new Boot());
        }
        for (int i = possibleFishes; i >= 0; i--)
        {
            possibleCatches.Add(new Boot());
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
        if (hasCatch && isCatchable)
        {
            catchingTimer += Time.deltaTime;

            if (catchingTimer >= currentCatch.catchInSeconds)
            {
                Debug.Log("Took too long! The fish escaped!");
                Reset();
            }
        }
        

        if (usingMouse)
        {

            if (Input.GetMouseButtonDown(0)) //replace this with whatever initiates the fishing sequence
                                             //(i.e. when the bob is out on the water waiting for fish)
            {
                /* old stuff
                if (!isFishing)
                {
                    //Deploy the fishing bob in restful animation
                    restSprite.SetActive(true);
                    restSprite.GetComponent<SpriteRenderer>().color = Color.white;

                    Debug.Log("Fishing initiated");
                    StartCoroutine(Wait());
                }
                */
                StartFishing();
            }

            /*
            if (hasCatch && isCatchable)
            {
                //replace the Mouse control with touch
                if (Input.GetMouseButtonDown(0))
                {
                    // this is just a stand -in; connect this to D's code and update the inventory
                    inventory.Add(currentCatch);

                    Debug.Log($"<b>Caught a {currentCatch.name}!</b>");

                    //then triumphantly display the catch and return to the
                    //"not actively fishing" screen (before the fishing rod is cast out)

                    Reset();
                }
                else
                {
                    catchingTimer += Time.deltaTime;
                }

                if (catchingTimer >= currentCatch.catchInSeconds)
                {
                    Debug.Log("Took too long! The fish escaped!");
                    Reset();
                }
            }
            */

            if (Input.GetMouseButtonDown(1))
            {
                TryCatch();
            }
        }

    }

    void StartFishing()
    {
        if (!isFishing)
        {
            //Deploy the fishing bob in restful animation
            restSprite.SetActive(true);
            //restSprite.GetComponent<SpriteRenderer>().color = Color.white;

            Debug.Log("Fishing initiated");
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        Debug.Log("Starting to wait...");
        waitText.SetActive(true);
        isFishing = true;
        //wait for 2-10 seconds: adjust the timing if needed
        yield return new WaitForSeconds(Random.Range(2, 10));
        Debug.Log("Something caught :)");
        ReadyToCatch();
    }

    void ReadyToCatch()
    {
        waitText.SetActive(false);
        Debug.Log("something has bit onto the fishing rod..");
        hasCatch = true;
        currentCatch = possibleCatches[Random.Range(0, possibleCatches.Count - 1)];
        Vibrator.Vibrate(currentCatch.catchInSeconds, 255);  // This Needs to be tested

        //instead of this, replace the "resting" fishing bob with the bouncing one
        //to indicate something having bitten
        //restSprite.GetComponent<SpriteRenderer>().color = Color.red;
        restSprite.SetActive(false);
        bouncingSprite.SetActive(true);

        isCatchable = true;
    }

    private void TryCatch()
    {
        if (hasCatch && isCatchable)
        {
            // this is just a stand-in; connect this to D's code and update the inventory
            inventory.Add(currentCatch);

            Debug.Log($"Caught a {currentCatch.name}!");

            //then triumphantly display the catch and return to the
            //"not actively fishing" screen (before the fishing rod is cast out)

            DisplayCatch(currentCatch);
        }
        else
            Debug.Log("Nothing to catch yet :)");
    }

    public void DisplayCatch(Catch type)
    {
        //remove the fishing bob
        bouncingSprite.SetActive(false);

        //activate the triumphant display
        presentationFlair.SetActive(true);
        switch (type.name)
        {
            case "normal fish":
                catchPresentationObject = Instantiate(normalFishPrefab, Vector3.zero, Quaternion.identity);
                break;
            case "old rotting boot":
                catchPresentationObject = Instantiate(bootPrefab, Vector3.zero, Quaternion.identity);
                break;
            default:
                Debug.Log("you messed something up and fished something that we haven't programmed yet :(");
                break;
        }


        Invoke("Reset", 3);
    }

    private void Reset()
    {
        Debug.Log("Resetting...");
        presentationFlair.SetActive(false);
        Destroy(catchPresentationObject);
        isFishing = false;
        hasCatch = false;
        isCatchable = false;
        catchingTimer = 0f;

        
        Debug.Log("Fishing has reset.");

    }
}
