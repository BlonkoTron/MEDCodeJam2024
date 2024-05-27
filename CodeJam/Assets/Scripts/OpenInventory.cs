using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject inventoryButton;
    public GameObject FishingRod;
    private Upgrades upgrades;

    void Awake()
    {
        upgrades = GetComponent<Upgrades>();
    }
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
        if (upgrades.doDisplayUI)
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
