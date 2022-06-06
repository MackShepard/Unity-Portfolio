using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// —лушатель SO событи€, когда событеи вызываетс€, то слушатели выполн€ют свой код, в данном случае код назначаетс€ через UnityEvent
/// </summary>
public class GameEventListener : MonoBehaviour, IGameEventListener
{
    [Tooltip("Event to register with.")]
    [SerializeField] private GameEvent _eventSO;
    [Tooltip("Response to invoke when Event is raised.")]
    [SerializeField] private UnityEvent _response;
    [SerializeField] private UnityEvent _response2;
    public GameEvent eventSO {
        get { return _eventSO; }
        set { _eventSO = value; }
    }
    private event Action RegisterListenerAction;
    private event Action UnregisterListenerAction;

    private void Start()
    {
        RegisterListenerAction += RegisterListener;
        UnregisterListenerAction += UnregisterListener;
    }

    public void InvokeActionRegist()
    {
        RegisterListenerAction?.Invoke();
    }
    public void InvokeActionUnregist()
    {
        UnregisterListenerAction?.Invoke();
    }

    private void OnDisable()
    {
        RegisterListenerAction -= RegisterListener;
        UnregisterListenerAction -= UnregisterListener;

    }

    private void RegisterListener()
    {
        if (_eventSO != null) _eventSO.RegisterListener(this);
    }

    private void UnregisterListener()
    {
        _eventSO.UnregisterListener(this);
    }


    public void OnEventRaised()
    {
        switch (_eventSO.eventName) { // придумать что-то лучше
            case "Take":
                _response?.Invoke();
                break;
            case "Watch":
                _response2?.Invoke();
                break;
        }
    }
}



