using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Управление анимациями
/// </summary>
public class PlayerAnimator : MonoBehaviour
{
    #region Variable
    [Header("ГО персонажа")]
    [SerializeField] private GameObject _character;
     
    private Animator _animator; // Аниматор персонажа   
    private PlayerMovement _characterControl; // Скрипт персонажа  

    private bool _setSpeedValueCondition = true; // Флажок для корутина SetSpeedValue
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        // Инициализация переменных
        if (_character != null)
        {           
            _animator = _character.transform.GetComponentInChildren<Animator>();
            _characterControl = _character.GetComponent<PlayerMovement>();
            StartCoroutine(SetSpeedValue());
        }
        else print("Заполните поля в инспекторе");
    }
    #endregion

    #region Coroutine
    // Передача значение speed аниматору
    IEnumerator SetSpeedValue()
    {
        while (_setSpeedValueCondition) { 
        yield return new WaitForFixedUpdate();
        _animator.SetFloat("speed", Mathf.Abs(_characterControl.Movement.magnitude));
        }
    }
    #endregion
}
