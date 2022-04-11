using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTaker : MonoBehaviour
{
    [SerializeField] RayCast rayCast;
    [SerializeField] Item itemToAdd;
    [SerializeField] Inventory targetInventory;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            if (rayCast.lastHit != null)
                if (rayCast.lastHit.gameObject.CompareTag("Axe")) { 
                    targetInventory.AddItem(itemToAdd);
                    Destroy(rayCast.lastHit.gameObject);
                }
    }
}
