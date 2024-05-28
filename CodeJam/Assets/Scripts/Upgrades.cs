using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Assuming you're using TextMesh Pro

public class Upgrades : MonoBehaviour
{
    public int upgradeCostBait; // Base cost for upgrades
    public static int Gold; // Player's current money

    public int money; // Placeholder for current money
    public int upgradeableBait; // Flag to indicate if upgrades are available (optional)

    public TMP_Text goldText; // Text object to display money (using Text with capital T)
    public TMP_Text baitCostText; // Text object to display upgrade cost
    public TMP_Text fishingrodLevelText; // Text object to display upgrade level

    public GameObject UICanvas; // Reference to the UI Canvas 

    private int upgradeLevelBait; // How many upgrades you have left before you reach max level

    public Button upgradeBait; // Button to upgrade bait
    public AudioClip upgradeSound; // Sound to play when upgrading

    [HideInInspector]
    public static bool doDisplayUI = true; // Flag to show/hide UI

    private List<string> levelNames = new List<string>  // List of level names
  {
    "SUPERSONIC",
    "EPIIIC",
    "Great",
    "Better",
    "Normal",
  };

    // Reference to the FishingManager script
    public  FishingManager fishingManager;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().clip = upgradeSound; // Assuming there's an AudioSource component attached

        upgradeBait.gameObject.SetActive(false); // Hide upgrade button at start
    }

    void Update()
    {
        // Check if enough money and upgrades are available (optional)

        bool canUpgradeBait = money >= upgradeCostBait || upgradeableBait == 0; // Assuming non-nullable

        if (doDisplayUI) // Check if UI should be displayed
        {
            UICanvas.SetActive(true); // Show UI
            if (canUpgradeBait) // Check if bait can be upgraded
            {
                upgradeBait.gameObject.SetActive(true); // Show upgrade button
            }
            else
            {
                upgradeBait.gameObject.SetActive(false); // Hide upgrade button
            
            }
        }
        else
        {
            UICanvas.SetActive(false); // Hide UI
        }

        /*This if statement is to change the money variable to the static variable Gold, 
        but also let you be able to change the money variable in the inspector 
        as long as you do it before the game starts. This is useful for testing purposes. */
        if (Gold != 0) // Check if Gold has changed away from default value
        {
            money = Gold; // Changes the money variable's value to that of Gold and it updates money from static variable
        }
   
        baitCostText.text = "Cost: " + upgradeCostBait.ToString(); // Display upgrade cost
        goldText.text = "Gold: " + money.ToString(); // Display money
        fishingrodLevelText.text = $"Fishing rod level: {levelNames[upgradeableBait]}"; // Display upgrade level of fishing rod
        if (upgradeableBait == 0) // Check if max level is reached
        {
            baitCostText.text = "Max"; // Display max level
        }

    }

    public void UpgradeBait() // Renamed for clarity
    {
        // Check if enough money and upgrade is available
        if (money >= upgradeCostBait && upgradeableBait > 0)
        {
            upgradeLevelBait++; // Increase bait level
            money -= upgradeCostBait; // Deduct cost from money
            upgradeCostBait *= 2; // Increase cost for next upgrade
            upgradeableBait--; // Reduce available upgrades (optional)
            Debug.Log("Bait upgraded to level " + upgradeLevelBait); // Log upgrade level
            // Play upgrade sound using AudioSource
            fishingManager.maxCatchTime -= 2; // Decrease maxCatchTime by 2 seconds from FishingManager
            GetComponent<AudioSource>().Play(); // Assuming there's an AudioSource component attached
            return;
        }
        else
        {
            // Not enough money or no more upgrades
            Debug.Log("Not enough money or no more upgrades");
            return;
        }
    }
}
