using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public GameObject inventoryPanel;

    public void OpenCloseInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        Debug.Log(inventoryPanel.activeSelf);
    }
}
