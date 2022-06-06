using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Управление персонажем
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour 
  {

    #region Variable
    [Header("Скорость игрока")]
    [SerializeField] private float _speed = 2f;
    [Header("Компонент rigidBody")]
    [SerializeField] private Rigidbody _physicsBody;
    [SerializeField] private GameObject _characterSprite;  // GO со спрайтом персонажа

    // Переменная для перемещения персонажа
    private Vector3 _movement; 
    public Vector3 Movement
    {
        get
        {
            return _movement;
        }
    }
 
    private Vector3 _characterFacing = Vector3.right; // Куда персонаж смотрит
    private Vector3 _characterLocalScale;             // Локалскейл персонажа
   
    private float _horizontalInput, _verticalInput;   // Переменные для горзонтального и вертикального инпута
    private bool _characterMoveCondition = true, _characterFlipCondition = true, _characterInputCondition = true; // Флаги для корутинов

    #endregion

    #region MonoBehaviour
    private void Start()
    {
        StartCoroutine(CharacterFlip());
        StartCoroutine(CharacterInput());
        // Инициализация переменных
        if (_physicsBody != null)
        {            
            StartCoroutine(CharacterMove());
        }
        else print("Заполните поля в инспекторе");
        _characterLocalScale = _characterSprite.transform.localScale;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    #endregion

    #region Coroutine
    // Движение персонажа
    IEnumerator CharacterMove()
    {
        while (_characterMoveCondition)
        {
            yield return new WaitForFixedUpdate();
            _movement = new Vector3(_horizontalInput, 0, _verticalInput).normalized;
            _movement = _movement * _speed * Time.fixedDeltaTime;
            _movement.y = _physicsBody.velocity.y;
            _physicsBody.velocity = _movement;
           
        }
    }

    // Поворот персонажа
    IEnumerator CharacterFlip()
    {
        while (_characterFlipCondition)
        {
            yield return new WaitForFixedUpdate();
            if (_horizontalInput > 0 && _characterFacing != Vector3.right || _horizontalInput < 0 && _characterFacing != Vector3.left)
            {
                _characterLocalScale.x *= -1;
                _characterSprite.transform.localScale = _characterLocalScale;
                _characterFacing *= -1;
            }
        }
    }

    // Инпуты 
    IEnumerator CharacterInput()
    {
        while (_characterInputCondition)
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
            yield return null;        
        }
    }
    #endregion
}
