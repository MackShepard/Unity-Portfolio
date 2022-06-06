using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Кастомная ивент система
/// </summary>
public class GameEvents : MonoBehaviour 
{
    // Статичный экземпляр скрипта
    public static GameEvents _instance;
    public GameEvents Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(this);
        }
    }

    
    public event Action<int> onSelectorTriggerEnter;

    public event Action<int> onSelectorTriggerExit;

    public event Action<int> onPlayerTakeItem;

    public void SelectorTriggerEnter(int id)
    {
        if (onSelectorTriggerEnter != null)
        {
            onSelectorTriggerEnter(id);
        }
    }
    public void SelectorTriggerExit(int id)
    {
        if (onSelectorTriggerExit != null)
        {
            onSelectorTriggerExit(id);
        }

    }
    public void PlayerTakeItem(int id)
    {
        if (onPlayerTakeItem != null)
        {
            onPlayerTakeItem(id);
        }
    }
}
