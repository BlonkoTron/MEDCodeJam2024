using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{ 
    private Catch[] possibleCatches;

    //adjust these based on the probabilities we want
    private int possibleFishes = 5;
    private int possibleBoots = 3;

    public SpriteRenderer SpriteRenderer;
    //private bool initiateFishing = false;

    private bool isFishing = false;
    private bool hasCatch = false;

    private Catch currentCatch;

    //this is just a stand in until the real inventory is added
    public List<Catch> inventory = new();

    // Start is called before the first frame update
    void Start()
    {
        //the sprite renderer is just for testing; instead connect to the actual
        //fishing bob
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.color = Color.white;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //replace this with whatever initiates the fishing sequence
                                            //(i.e. when the bob is out on the water waiting for fish)
        {
            if (!isFishing)
            {
                //Deploy the fishing bob in restful animation

                Debug.Log("Fishing initiated");
                StartCoroutine(Wait());
            }
            else if (hasCatch) 
            {
                // this is just a stand -in; connect this to D's code and update the inventory
                inventory.Add(currentCatch);

                //remove the fishing bob
                SpriteRenderer.color = Color.white;

                Debug.Log($"Caught a {currentCatch.name}!");
                isFishing = false;

                //then triumphantly display the catch and return to the
                //"not actively fishing" screen (before the fishing rod is cast out)
            }
        }   
    }

    IEnumerator Wait()
    {
        Debug.Log("Starting to wait...");
        isFishing = true;
        //wait for 2-10 seconds: adjust the timing if needed
        yield return new WaitForSeconds(Random.Range(2, 10));
        Debug.Log("Something caught :)");
        ReadyToCatch();
    }

    void ReadyToCatch()
    {
        Debug.Log("something has bit onto the fishing rod..");
        hasCatch = true;
        currentCatch = possibleCatches[Random.Range(0, possibleCatches.Length - 1)];


        //instead of this, replace the "resting" fishing bob with the bouncing one
        //to indicate something having bitten
        SpriteRenderer.color = Color.red;
        

    }
}
