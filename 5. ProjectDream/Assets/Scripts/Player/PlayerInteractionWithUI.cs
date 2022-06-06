using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// Взаимодействие с UI через мышь
/// Task: При старте он очищает инвентарь, это может где-то помешать, исправить
/// </summary>
public class PlayerInteractionWithUI : MonoBehaviour
{
    #region Variable 
    [SerializeField] private SO_Inventory _playerInventory; // Инвентарь игрока
    [SerializeField] private GameObject _currentClickedObject;
    private bool _getClickedObjectCondition = true; // флажок для корутина
    
    #endregion
    #region MonoBehaviour
    void Start()
    {
        StartCoroutine(GetClickedObject());
    }
    #endregion
    #region Func
    private GameObject PointerRaycast(Vector2 position) // функция для получения информации об объекте
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        List<RaycastResult> resultsData = new List<RaycastResult>();
        pointerData.position = position;
        EventSystem.current.RaycastAll(pointerData, resultsData);

        if (resultsData.Count > 0)
        {
            return resultsData[0].gameObject;
        }

        return null;
    }
    #endregion
    #region Coroutine
    IEnumerator GetClickedObject() // корутин для получения объекта при клике
    {
        while (_getClickedObjectCondition)
        {
            
            if (Input.GetButtonDown("Fire1")) //  Нажат пкм
            {
                var data = PointerRaycast(Input.mousePosition); // Получаем объект на который кликнул игрок

                if (data != null) 
                {
                    Interface_ObjectReaction interface_ObjectReaction = data.transform.root.GetComponentInChildren<Interface_ObjectReaction>(); // Ищем интерфейс                
                    if (interface_ObjectReaction != null)
                    {                       
                        interface_ObjectReaction.BaseReact(data);
                    }
                    _currentClickedObject = data;
                }

            }
            yield return null;         
        }
    }
    #endregion
}
