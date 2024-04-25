using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    // Lists to store fish counts, TextMeshPro components, and sprite GameObjects
    private List<int> fishCounts = new List<int>();
    private List<TMP_Text> fishTexts = new List<TMP_Text>();
    private List<GameObject> fishSprites = new List<GameObject>();

    // References to TextMeshPro fields for fish counts and sprite GameObjects
    [SerializeField] private TMP_Text[] fishTextFields;
    [SerializeField] private GameObject[] fishSpritesObjects;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        InitializeFishData();
    }
    // Initializes fish data by populating lists with default values and references
    private void InitializeFishData()
    {
        for (int i = 0; i < fishTextFields.Length; i++)
        {
            // Add default count of 0 for each fish type
            fishCounts.Add(0);
            // Add references to TextMeshPro components for fish counts
            fishTexts.Add(fishTextFields[i]);
            // Add references to sprite GameObjects for fish
            fishSprites.Add(fishSpritesObjects[i]);
        }
    }

    // Adds a fish to the inventory and updates the count and display
    private void AddFish(FishType fishType)
    {
        // Get the index corresponding to the fish type
        int index = (int)fishType;

        // Increment the count for the specified fish type
        fishCounts[index]++;

        // Update the TextMeshPro component to display the new count
        fishTexts[index].text = "Caught: " + fishCounts[index].ToString();

        // If there is at least one fish of this type, show its sprite
        if (fishCounts[index] > 0)
        {
            fishSprites[index].GetComponent<Image>().color = Color.white;
        }
    }
}