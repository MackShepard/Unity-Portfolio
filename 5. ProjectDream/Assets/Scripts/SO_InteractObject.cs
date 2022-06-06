using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum ItemType
{
    Takeable,
    Interactable
}

[CreateAssetMenu(fileName = "InteractObject", menuName = "Scriptble Objects/InteractObject")]
public class SO_InteractObject : ScriptableObject
{
    [SerializeField] private string _itemName; // Название предмета
    private string _itemWatch;
    private string _itemTake;
    private string _itemUse;

    [SerializeField] private TextAsset _inkAsset;
    private static Story _inkStory;

    public string itemName => _itemName;
    public string itemWatch => _itemWatch;
    public string itemTake => _itemTake;
    public string itemUse => _itemUse;
    public Story inkStory => _inkStory;

    private void OnEnable()
    {
        _inkStory = new Story(_inkAsset.text);
        _itemTake = itemName + ".Take";
        _itemWatch = itemName + ".Watch";
        _itemUse = itemName + ".Use";

        _inkStory.onError += (msg, type) => // ловить ошибки в инке
        {
            {
                if (type == Ink.ErrorType.Warning)
                    Debug.LogWarning(msg);
                else
                    Debug.LogError(msg);
            }
        };
    }

    #region JsonConstructor

    #endregion

}






