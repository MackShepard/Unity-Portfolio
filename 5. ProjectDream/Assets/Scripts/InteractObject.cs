using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Грузим данные об интерактивном объекте
/// </summary>
public class InteractObject : Abstract_InteractObject, Interface_ObjectReaction
{
    [SerializeField] private SO_InteractObject _data; // Данные для загрузки
    private Interface_Dialog interface_Dialog;

    private void Start()
    {
        itemName = _data.itemName;
        interface_Dialog = player.transform.root.GetComponentInChildren<Interface_Dialog>();
    }

    public void BaseReact(GameObject go)
    {
        if (go.CompareTag("Take"))
        {
            _data.inkStory.ChoosePathString(_data.itemTake, false);
            interface_Dialog.StartShowDialog(_data);
            _playerInventory.Inventory.Add(this);
            Destroy(this.gameObject);
        }
        if (go.CompareTag("Watch"))
        {
            _data.inkStory.ChoosePathString(_data.itemWatch, false);
            interface_Dialog.StartShowDialog(_data);
        }
        if (go.CompareTag("Use"))
        {
            _data.inkStory.ChoosePathString(_data.itemUse, false);
            interface_Dialog.StartShowDialog(_data);
            if (go.transform.root.GetComponentInChildren<Door>() != null)
                go.transform.root.GetComponentInChildren<Door>().OpenDoor();

        }
    }


    #region Inky Player

    public static event Action<Story> OnCreateStory;
    public Story story;

    void Awake()
    {
        StartStory();
    }

    void StartStory()
    {
        story = _data.inkStory;
        if (OnCreateStory != null) OnCreateStory(story);
        RefreshView();
    }

    void RefreshView()
    {

        // Read all the content until we can't continue any more
        while (story.canContinue)
        {
            // Continue gets the next line of the story
            string text = story.Continue();
            // This removes any white space from the text.
            text = text.Trim();
        }

    }
    #endregion

}




