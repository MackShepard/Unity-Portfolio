using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// �������������� � UI ����� ����
/// Task: ��� ������ �� ������� ���������, ��� ����� ���-�� ��������, ���������
/// </summary>
public class PlayerInteractionWithUI : MonoBehaviour
{
    #region Variable 
    [SerializeField] private SO_Inventory _playerInventory; // ��������� ������
    [SerializeField] private GameObject _currentClickedObject;
    private bool _getClickedObjectCondition = true; // ������ ��� ��������
    
    #endregion
    #region MonoBehaviour
    void Start()
    {
        StartCoroutine(GetClickedObject());
    }
    #endregion
    #region Func
    private GameObject PointerRaycast(Vector2 position) // ������� ��� ��������� ���������� �� �������
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
    IEnumerator GetClickedObject() // ������� ��� ��������� ������� ��� �����
    {
        while (_getClickedObjectCondition)
        {
            
            if (Input.GetButtonDown("Fire1")) //  ����� ���
            {
                var data = PointerRaycast(Input.mousePosition); // �������� ������ �� ������� ������� �����

                if (data != null) 
                {
                    Interface_ObjectReaction interface_ObjectReaction = data.transform.root.GetComponentInChildren<Interface_ObjectReaction>(); // ���� ���������                
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
