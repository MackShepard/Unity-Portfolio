using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject door;
    public bool isDoorOpen = false;
    public SO_Inventory _playerInventory;

    public void OpenDoor()
    {
        if (_playerInventory.Inventory != null)
            foreach (InteractObject item in _playerInventory.Inventory)
            {
                if (item.itemName == "Axe" || item.itemName == "Key")
                    isDoorOpen = true;
            }
        if (isDoorOpen)
            door.SetActive(false);
    }

   
}


