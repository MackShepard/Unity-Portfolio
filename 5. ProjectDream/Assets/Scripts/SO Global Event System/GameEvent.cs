using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// SO Шаблон для ивентов
/// </summary>
[CreateAssetMenu(menuName = "Scriptble Objects/Event")]
public class GameEvent : ScriptableObject
{
    [SerializeField] private string _eventName;
    public string eventName => _eventName;
    private readonly List<IGameEventListener> _eventListeners =
        new List<IGameEventListener>(); // узнать зачем тут интерфейс?

    public void Raise()
    {
        for (int i = _eventListeners.Count - 1; i >= 0; i--)
            _eventListeners[i].OnEventRaised();
        
    }

    public void RegisterListener(IGameEventListener listener)
    {
        if (!_eventListeners.Contains(listener))
            _eventListeners.Add(listener);
    }

    public void UnregisterListener(IGameEventListener listener)
    {
        if (_eventListeners.Contains(listener))
            _eventListeners.Remove(listener);
    }

    public void ClearAllListener()
    {
        _eventListeners.Clear();
    }
}
