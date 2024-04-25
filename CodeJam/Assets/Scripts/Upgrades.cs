using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Assuming you're using TextMesh Pro

public class Upgrades : MonoBehaviour
{
    //public int upgradeCostStrength; // Base cost for strength upgrades
    public int upgradeCostBait; // Base cost for upgrades
    public static int money; // Player's current money
    //public int upgradeableStrength; // Flag to indicate if upgrades are available (optional)
    public int upgradeableBait; // Flag to indicate if upgrades are available (optional)
    public TMP_Text goldText; // Text object to display money (using Text with capital T)
    public TMP_Text baitCostText;

    public GameObject UICanvas;

    //public TMP_Text strengthCostText;
    //private int upgradeLevelStrength;
    private int upgradeLevelBait;
    //public Button upgradeStrength; // Button to upgrade strength
    public Button upgradeBait; // Button to upgrade bait
    public AudioClip upgradeSound;

    [HideInInspector]
    public static bool doDisplayUI = true;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().clip = upgradeSound; // Assuming there's an AudioSource component attached
    }

    void Update()
    {
        // Check if enough money and upgrades are available (optional)
        //bool canUpgradeStrength = money >= upgradeCostStrength || upgradeableStrength == 0; // Assuming non-nullable

        bool canUpgradeBait = money >= upgradeCostBait || upgradeableBait == 0; // Assuming non-nullable

        //upgradeStrength.gameObject.SetActive(canUpgradeStrength);
        if (doDisplayUI)
        {
            UICanvas.SetActive(true);
            if (canUpgradeBait)
            {
                upgradeBait.gameObject.SetActive(true);
            }
            else
            {
                upgradeBait.gameObject.SetActive(false);
            }
        }
        else
        {
            UICanvas.SetActive(false);
        }

        // Update cost text if cost changes during gameplay
        //strengthCostText.text = "Cost: " + upgradeCostStrength.ToString();
        baitCostText.text = "Cost: " + upgradeCostBait.ToString();
        goldText.text = "Gold: " + money.ToString();
         /*+ fish.ToString()*/;
        /*if (upgradeableStrength == 0)
        {
            strengthCostText.text = "Max";
        }*/
        if (upgradeableBait == 0)
        {
            baitCostText.text = "Max";
        }

        
    }

    /*public void UpgradeStrength() // Renamed for clarity
    {

        // Check if enough money and upgrade is available
        if (money >= upgradeCostStrength && upgradeableStrength > 0)
        {
            upgradeLevelStrength++;  // Increase strength level
            money -= upgradeCostStrength;  // Deduct cost from money
            upgradeCostStrength *= 2;     // Increase cost for next upgrade
            upgradeableStrength--;     // Reduce available upgrades (optional)
            Debug.Log("Strength upgraded to level " + upgradeLevelStrength);
            // Play upgrade sound using AudioSource
            GetComponent<AudioSource>().Play(); // Assuming there's an AudioSource component attached
            return;
        }
        else
        {
            // Not enough money or no more upgrades
            Debug.Log("Not enough money or no more upgrades");
            return;
        }
    }*/

    public void UpgradeBait() // Renamed for clarity
    {
        // Check if enough money and upgrade is available
        if (money >= upgradeCostBait && upgradeableBait > 0)
        {
            upgradeLevelBait++; // Increase bait level
            money -= upgradeCostBait; // Deduct cost from money
            upgradeCostBait *= 2;    // Increase cost for next upgrade
            upgradeableBait--;     // Reduce available upgrades (optional)
            Debug.Log("Bait upgraded to level " + upgradeLevelBait);
            // Play upgrade sound using AudioSource
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

