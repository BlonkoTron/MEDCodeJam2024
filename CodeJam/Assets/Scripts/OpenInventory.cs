using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject inventoryButton;
    public GameObject FishingRod;
    public FishingManager fishingManager;
    private void Start()
    {
        ThrowRod.instance.OnUsingRod += CloseInventory;
    }

    public void OpenCloseInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        Debug.Log(inventoryPanel.activeSelf);
    }
    private void Update()
    {
        if (fishingManager.upgradeManager.doDisplayUI)
        {
          inventoryButton.SetActive(true);
        }
        else
        {
          inventoryButton.SetActive(false);
        }
    }
    private void CloseInventory()
    {
        inventoryPanel.SetActive(false);
    }
    private void OnDestroy()
    {
        ThrowRod.instance.OnUsingRod -= CloseInventory;

    }
}
