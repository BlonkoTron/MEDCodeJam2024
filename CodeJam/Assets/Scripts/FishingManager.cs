using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    private float timer;
    private Catch[] possibleCatches;
    private int possibleFishes = 5;
    private int possibleBoots = 3;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        possibleCatches = new Catch[possibleFishes + possibleBoots];
        for (int i = possibleCatches.Length; i>0; i--)
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
        
    }
}
