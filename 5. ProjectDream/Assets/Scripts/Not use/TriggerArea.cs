using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TriggerArea : MonoBehaviour  // Вызов ивента через тригер
{
    
    private void OnTriggerEnter(Collider other)
    {
        GameEvents._instance.SelectorTriggerEnter(transform.parent.gameObject.GetInstanceID());
        
    }
    private void OnTriggerExit(Collider other)
    {
        GameEvents._instance.SelectorTriggerExit(transform.parent.gameObject.GetInstanceID());
    }


}
