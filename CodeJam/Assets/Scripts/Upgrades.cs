using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public int upgradeCost; // Base cost for upgrades
    private int upgradeLevelStrength; // Upgrade level for fishing strength
    private int upgradeLevelBait; // Upgrade level for bait effectiveness
    public int money; // Player's current money
    private int upgradeable; // Flag to indicate if upgrades are available

    // Start is called before the first frame update
    void Start()
    {
        money = 10; // Starting money
    }

    // Update is called once per frame
    void Update()
    {
        // Check for upgrade button press (replace with your actual button check)
        if (Input.GetKeyDown(KeyCode.U)) // Example: U key for upgrades
        {
            ShowUpgradeMenu(); // Open upgrade menu on button press
        }
    }

    // Function to display the upgrade menu
    void ShowUpgradeMenu()
    {
        // Implement your UI logic here to display upgrade options
        // Example: Create buttons for each upgrade type (Strength, Bait)
        // Update button text with current level and cost

        // Handle upgrade selection logic here (code inside buttons)
        if (UpgradeStrength())
        {
            upgradeLevelStrength++; // Increase strength level
            money -= upgradeCost; // Deduct cost from money
        }

        if (UpgradeBait())
        {
            upgradeLevelBait++; // Increase bait level
            money -= upgradeCost; // Deduct cost from money
        }
    }

    // Function for upgrading fishing strength
    bool UpgradeStrength()
    {
        // Check if enough money and upgrade is available
        if (money >= upgradeCost && upgradeable > 0)
        {
            // Upgrade successful
            upgradeable--; // Reduce available upgrades (optional)
            return true;
        }
        else
        {
            // Not enough money or no more upgrades
            return false;
        }
    }

    // Function for upgrading bait effectiveness
    bool UpgradeBait()
    {
        // Check if enough money and upgrade is available
        if (money >= upgradeCost && upgradeable > 0)
        {
            // Upgrade successful
            upgradeable--; // Reduce available upgrades (optional)
            return true;
        }
        else
        {
            // Not enough money or no more upgrades
            return false;
        }
    }

    // Function to update fishing mechanics based on upgrade levels
    public void ApplyUpgrades(ref float catchRate, ref float fishStrength)
    {
        // Increase catch rate based on bait upgrade level
        catchRate += upgradeLevelBait * 0.1f; // Example: 10% increase per level

        // Increase fish catch resistance based on strength upgrade level
        fishStrength += upgradeLevelStrength * 0.2f; // Example: 20% increase per level
    }
}
