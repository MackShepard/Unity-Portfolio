using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.UI;
/// <summary>
/// ������ ������ �� ������������� �������
/// </summary>
public enum HeroPos
{
    near,
    far
}

public class ContextMenuUI : MonoBehaviour // ������������ ������ � ������
{
    #region Variable
    [Header("����� �� ����� (����������� �����������)")]
    [SerializeField] private HeroPos _heroPos;
    [Header("����� � ������ ��������� ����")]
    [SerializeField] private GameObject _dot;
    [Header("����������� ����")] 
    [SerializeField] private GameObject _contextMenyUI;
    [Header("���� ��� ������������")]
    [SerializeField] private GameObject _circle;
    [Header("������")]
    [SerializeField] private RectTransform _canvas;

    [Header("��������� ����������� ����")]
    [SerializeField] private float _dotDistance;
    [Header("��������� ������������ ����������� ���� (��� ����)")]
    [SerializeField] private float _contextMenuVisibleRadius;

    private Vector3 _dotPos; // ���������� ��� ����� � ����
    private bool _dotMovingCondition = true, _contextMenuVisibleCondition = true; // ������ ��� ���������

    #endregion
    #region MonoBehaviour
    void Start()
    {     
        _heroPos = HeroPos.far;
        _circle.SetActive(false);
        
        StartCoroutine(DotMoving());
        StartCoroutine(�ontextMenuVisible());
    }
    
    private void OnTriggerEnter(Collider other)
    {
        SelectorVisibleTrue(); // ���� ����� ������ � ��������� �������
    }

    private void OnTriggerExit(Collider other)
    {
        SelectorVisibleFalse(); // ���� ����� ������� �� ���������� �������
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
    // ����������� ����� ������ ����������� ����
    IEnumerator DotMoving()
    {
        while (_dotMovingCondition)
        {
            yield return null;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(_canvas, Input.mousePosition, Camera.main, out _dotPos);  // �������������� ������� ����� � ��������� �������      
            _dot.transform.position = _dotPos;  // ������� ����� ��������� ������� ����
            
            if (Vector3.Distance(_circle.transform.position, _dotPos) > _dotDistance)
            { // ���� ��������� �� ������ ����� �� ������ ���� ������ �������� dotDistance
                _dot.transform.position = _circle.transform.position + (_dotPos - _circle.transform.position).normalized * _dotDistance; // ����� �������� � ������� dotDistance
            }

        }
    }

    // ������������ ����������� ���� ���� ����� ���� ��� ����� ����
    IEnumerator �ontextMenuVisible()
    {
        while (_contextMenuVisibleCondition)
        {
            yield return null;
            if (_heroPos == HeroPos.near) // ���� ����� �����
            {
                if (Vector3.Distance(_circle.transform.position, _dotPos) > _contextMenuVisibleRadius) // � ���� ��������� ����� ������ � �������� ���� ������ �������� dialogVisibleDistance
                {
                    _contextMenyUI.SetActive(false); // ������ ����������� ����
                }
                else // ����� ������ ����� ����� � ���� ��������� � ����������� ����
                {
                    _contextMenyUI.SetActive(true); // ���������� ����������� ����
                }
            }
            else if (_heroPos == HeroPos.far)
            { // ���� ����� �� �����
                _contextMenyUI.gameObject.SetActive(false); // ������ ����������� ����

            }
        }
    }

    #endregion
}
