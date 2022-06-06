using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// Скрипт для получения информации о UI
/// </summary>
public class StandaloneModule : StandaloneInputModule
{
    public PointerEventData GetPointerEventData(int id)
    {
        return GetLastPointerEventData(id);
    }
}
