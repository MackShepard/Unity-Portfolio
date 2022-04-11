using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> inventoryItems = new List<Item>();

    public void AddItem(Item item)
    {
        inventoryItems.Add(item);
    }
}
