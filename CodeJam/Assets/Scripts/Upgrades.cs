using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Assuming you're using TextMesh Pro

public class Upgrades : MonoBehaviour
{
    public int upgradeCostStrength; // Base cost for strength upgrades
    public int upgradeCostBait; // Base cost for upgrades
    public int money; // Player's current money
    public int upgradeableStrength; // Flag to indicate if upgrades are available
    public int upgradeableBait; // Flag to indicate if upgrades are available
    public TMP_Text goldText; // Text object to display money (using Text with capital T)
    public TMP_Text baitCostText;
    public TMP_Text strengthCostText;
    private int upgradeLevelStrength;
    private int upgradeLevelBait;
    public Button upgradeStrength; // Button to upgrade strength
    public Button upgradeBait; // Button to upgrade bait
    // Start is called before the first frame update
    void Start()
    {
        money = 10; // Starting money
        goldText.text = "Gold: " + money.ToString();
        strengthCostText.text = "Cost: " + upgradeCostStrength.ToString();
        baitCostText.text = "Cost: " + upgradeCostBait.ToString();
    }
    void update()
    {
        // Check if upgrades are available
        if (money >= upgradeCostStrength)
        {
            upgradeBait.gameObject.SetActive(true);
        }
        else
        {
            upgradeBait.gameObject.SetActive(false);
        }

        if (money >= upgradeCostBait)
        {
            upgradeStrength.gameObject.SetActive(true);
        }
        else
        {
            upgradeStrength.gameObject.SetActive(false);
        }
    }
    public void UpgradeStrength() // Renamed for clarity
    {

        // Check if enough money and upgrade is available
        if (money >= upgradeCostStrength && upgradeableStrength > 0)
        {
            upgradeLevelStrength++;  // Increase strength level
            money -= upgradeCostStrength;  // Deduct cost from money
            upgradeCostStrength *= 2;     // Increase cost for next upgrade
            upgradeableStrength--;     // Reduce available upgrades (optional)
            goldText.text = "Gold: " + money.ToString();
            strengthCostText.text = "Cost: " + upgradeCostStrength.ToString();
            Debug.Log("Strength upgraded to level " + upgradeLevelStrength);
            return;
        }
        else
        {
            // Not enough money or no more upgrades
            Debug.Log("Not enough money or no more upgrades");
            return;
        }
    }

    public void UpgradeBait() // Renamed for clarity
    {
        // Check if enough money and upgrade is available
        if (money >= upgradeCostBait && upgradeableBait > 0)
        {
            upgradeLevelBait++; // Increase bait level
            money -= upgradeCostBait; // Deduct cost from money
            upgradeCostBait *= 2;    // Increase cost for next upgrade
            upgradeableBait--;     // Reduce available upgrades (optional)
            goldText.text = "Gold: " + money.ToString();
            baitCostText.text = "Cost: " + upgradeCostBait.ToString();
            Debug.Log("Bait upgraded to level " + upgradeLevelBait);
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
