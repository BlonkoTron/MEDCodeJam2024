using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject inventoryButton;
    public GameObject FishingRod;

    public void OpenCloseInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        Debug.Log(inventoryPanel.activeSelf);
    }
    private void Update()
    {
         if (Upgrades.doDisplayUI)
        {
          inventoryButton.SetActive(true);
        }
         else
        {
          inventoryButton.SetActive(false);
        }
    }
}
