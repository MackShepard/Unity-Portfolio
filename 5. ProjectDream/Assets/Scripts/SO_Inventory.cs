using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractObject", menuName = "Scriptble Objects/Inventory")]
public class SO_Inventory : ScriptableObject
{
    [SerializeField]private List<InteractObject> _inventory;

    public List<InteractObject> Inventory
    {
        get { return _inventory; }
    }

    private void OnEnable()
    {
        _inventory.Clear();
    }

}
