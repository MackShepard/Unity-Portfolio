using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.UI;
/// <summary>
/// Грузим данные об интерактивном объекте
/// </summary>
public enum HeroPos
{
    near,
    far
}

public class ContextMenuUI : MonoBehaviour // Присваивание метода к ивенту
{
    #region Variable
    [Header("Рядом ли игрок (првоеряется коллайдером)")]
    [SerializeField] private HeroPos _heroPos;
    [Header("Точка в центре диалогово окна")]
    [SerializeField] private GameObject _dot;
    [Header("Контекстное меню")] 
    [SerializeField] private GameObject _contextMenyUI;
    [Header("Круг для исследования")]
    [SerializeField] private GameObject _circle;
    [Header("Канвас")]
    [SerializeField] private RectTransform _canvas;

    [Header("Дистанция ограничения дота")]
    [SerializeField] private float _dotDistance;
    [Header("Дистанция исчезновения диалогового окна (для мыши)")]
    [SerializeField] private float _contextMenuVisibleRadius;

    private Vector3 _dotPos; // Переменная для точки и мыши
    private bool _dotMovingCondition = true, _contextMenuVisibleCondition = true; // Флажки для корутинов

    #endregion
    #region MonoBehaviour
    void Start()
    {     
        _heroPos = HeroPos.far;
        _circle.SetActive(false);
        
        StartCoroutine(DotMoving());
        StartCoroutine(СontextMenuVisible());
    }
    
    private void OnTriggerEnter(Collider other)
    {
        SelectorVisibleTrue(); // Если игрок входит в коллайдер объекта
    }

    private void OnTriggerExit(Collider other)
    {
        SelectorVisibleFalse(); // Если игрок выходит из коллайдера объекта
    }
    #endregion
    #region Func
    private void SelectorVisibleTrue()
    {
        _contextMenyUI.SetActive(true);
        _circle.SetActive(true);
        _heroPos = HeroPos.near;
    }
    private void SelectorVisibleFalse()
    {
        _contextMenyUI.SetActive(false);
        _circle.SetActive(false);
        _heroPos = HeroPos.far;
    }
    #endregion
    #region Coroutine
    // Перемещения точки внутри диалогового окна
    IEnumerator DotMoving()
    {
        while (_dotMovingCondition)
        {
            yield return null;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(_canvas, Input.mousePosition, Camera.main, out _dotPos);  // Интерпретируем позицию мышки в плоскость канваса      
            _dot.transform.position = _dotPos;  // Позиция точки равняется позиции мыши
            
            if (Vector3.Distance(_circle.transform.position, _dotPos) > _dotDistance)
            { // Если дистанция от центра круга до позици мыши больше величины dotDistance
                _dot.transform.position = _circle.transform.position + (_dotPos - _circle.transform.position).normalized * _dotDistance; // Точка остается в радиусе dotDistance
            }

        }
    }

    // Исчезновение диалогового окна если игрок ушел или вывел мышь
    IEnumerator СontextMenuVisible()
    {
        while (_contextMenuVisibleCondition)
        {
            yield return null;
            if (_heroPos == HeroPos.near) // Если игрок рядом
            {
                if (Vector3.Distance(_circle.transform.position, _dotPos) > _contextMenuVisibleRadius) // И если дистанция между кругом и позицией мыши больше величины dialogVisibleDistance
                {
                    _contextMenyUI.SetActive(false); // Прячем контекстное меню
                }
                else // Иначе значит игрок рядом и мышь находится в контекстном меню
                {
                    _contextMenyUI.SetActive(true); // Показываем контекстное меню
                }
            }
            else if (_heroPos == HeroPos.far)
            { // Если игрок не рядом
                _contextMenyUI.gameObject.SetActive(false); // Прячем контекстное меню

            }
        }
    }

    #endregion
}
