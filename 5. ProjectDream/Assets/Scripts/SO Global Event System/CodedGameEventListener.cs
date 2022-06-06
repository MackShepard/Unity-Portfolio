using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CodedGameEventListener : IGameEventListener
{
    [SerializeField] private GameEvent Event;

    private Action onResponse;
    /// <inheritdoc/>
    public void OnEventRaised()
    {
        onResponse?.Invoke();
        
    }
    public void OnEnable(Action response)
    {
        if (Event != null) Event.RegisterListener(this);
        onResponse = response;
    }

    public void OnDisable()
    {
        if (Event != null) Event.RegisterListener(this);
        onResponse = null;
    }
}

/* Пример как на объекте подписаться на событие
 * [SerializeField] private CodedGameEventListener codedGameEventListener;
    private void OnEnable()
    {
        codedGameEventListener?.OnEnable(SomethingHappen1Event);
    }
    private void OnDisable()
    {
        codedGameEventListener?.OnEnable(SomethingHappen1Event);
    }
    private void SomethingHappen1Event()
    {
        print("Появилась надпись" + gameObject.name);
    }
 */