using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    [SerializeField] private float walkDistance = 6f; // дистанция движения
    [SerializeField] private float patrolSpeed = 0.02f; // скорость движения
    [SerializeField] private float chaseSpeed = 0.06f; // скорость движения
    [SerializeField] private float timeToWait = 5f; // время в секундах ожидания при патрулировании
    [SerializeField] private float timeToChase = 3f;
    [SerializeField] private Transform enemyModelTransform;

    private Rigidbody2D _rb; // будет хранить компонент rigidbody
    private Vector2 _leftBoudaryPosition; // будет хранить крайнюю левую границу
    private Vector2 _rightBoudaryPosition; // будет хранить крайнюю правую границу
    private Transform _playerTransform; // будет хранить позицию игрока
    private Vector2 _nextPoint; // будет хранить движения игрока

    const float SPEEDXMULTIPLAYER = 50f; // хранит фиксированный множитель для deltaTime (50*0.02 =1)
    private bool _isFacingRight = true; // смотрит ли противник на право
    private bool _isWait = false; // ждет ли противник
    private float _waitTimeTemp; // временная переменная для таймера 
    private float _chaseTimeTemp;
    private bool _isChasingPlayer;  // в режиме преследования ли противник
    private bool _stopChasing = false;

    private float _currentSpeed;
    public bool IsFacingRight { // что бы передеть приват переменную в другой скрипт
        get => _isFacingRight;
    }
    public void StartChasingPlayer() // функция которая переключает переменную ответсвенную за режим преследования 
    {
        _isChasingPlayer = true; // переменная начать преследовать
        _chaseTimeTemp = timeToChase;
        _currentSpeed = chaseSpeed;
    }
    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); // передеать позицию игрока
        _rb = GetComponent<Rigidbody2D>(); // присваиваем компонент рб
        _leftBoudaryPosition = transform.position; // крайняя левая позиция = нынешняя позиция
        _rightBoudaryPosition = _leftBoudaryPosition + Vector2.right * walkDistance; // Vector2.right = new Vector 2 (1,0) // (x,y) + (1 * 6, 1 * 0) // крайняя правая позиция = крайняя левая позиция + дистанция движения 
        _waitTimeTemp = timeToWait; // врменная переменная = время ожидания
        _chaseTimeTemp = timeToChase;
        _currentSpeed = patrolSpeed;

    }
    private void Update()
    {
        if (_isChasingPlayer)
        {
            ChaseTimer();
        }
        if (_isWait && !_isChasingPlayer)  // если переменая противник ждет тру
            WaitTimer(); // вызываем функцию ждать

        if(ShouldWait()) // проверяет должен ли игрок ждать
            _isWait = true; // должен
    }

    private void FixedUpdate() {
        _nextPoint = Vector2.right * _currentSpeed * (Time.fixedDeltaTime * SPEEDXMULTIPLAYER); // отрезок на который будет перемещаться персонаж
   

        if (_stopChasing) //Math.Abs(DistanceToPlayer()) < minDistanceToPlayer
        {
            
            return; // выйти из функции
        }
        if (_isChasingPlayer) // если противник в режиме преследования
        {
            ChasePlayer(); // запустить соответствующую функцию
        }

        if (!_isWait && !_isChasingPlayer){ // если противник не ждет и не преследует, начать патруль
            Patrol(); // вызвать функцию патруль
        }
    }
    private void ChasePlayer()
    {

        float distanceBetweenPlayer = DistanceToPlayer();// высчитаваем расстояние до игрока

        if (distanceBetweenPlayer < 0) { // если игрок слева
        _nextPoint.x *= -1; // меняем направление движения
        }

        if (distanceBetweenPlayer > 0.2f && !_isFacingRight) // если игрок справа а я смотрю налево
        {
            Flip();
        } else if (distanceBetweenPlayer < 0.2f && _isFacingRight) // если игрок слева а я смотрю направо
        {
            Flip();
        }
        _rb.MovePosition((Vector2)transform.position + _nextPoint); // двигаем противника

    }
    private void Patrol()
    {
        if (!_isFacingRight)
        { // если смотрит на лево
            _nextPoint.x *= -1; // меняем направление движения
        }

        _rb.MovePosition((Vector2)transform.position + _nextPoint); // двигаем противника
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // линия красная
        Gizmos.DrawLine(_leftBoudaryPosition, _rightBoudaryPosition); // рисуем траекторию движения противника
    }
    void Flip() // функция поворота
    {
        _isFacingRight = !_isFacingRight; // переключает булеву переменную
        Vector3 enemyScale = enemyModelTransform.localScale; // присваиваем переменной значение скейл игрока
        enemyScale.x *= -1; // в этой переменной меняем скейл по иксу
        enemyModelTransform.localScale = enemyScale; // присваиваем скейлу игрока новое значение
    }
    private bool ShouldWait()
    {
        bool isOutOfRightBoundary = _isFacingRight && transform.position.x >= _rightBoudaryPosition.x; //если противник смотрит направо и достиг крайней правой позиции переменная становится тру
        bool isOutOfLeftBoundary = !_isFacingRight && transform.position.x <= _leftBoudaryPosition.x; //если противник смотрит налево и достиг крайней левой позиции переменная становится тру

        return isOutOfLeftBoundary || isOutOfRightBoundary;   // возвращаем уперся ли игрок или нет
    }
    private void WaitTimer()
    {
        _waitTimeTemp -= Time.deltaTime; // отсчитываем время
        if (_waitTimeTemp < 0f) // если таймер истек
        {
            _waitTimeTemp = timeToWait; // возобновляеем переменную временный таймер
            _isWait = false; // противник не ждет
            Flip(); // поворачиваем его
        }
    }
    private void ChaseTimer()
    {
        _chaseTimeTemp -= Time.deltaTime;
        if (_chaseTimeTemp < 0f) 
        {
            _chaseTimeTemp = timeToChase; 
            _isChasingPlayer = false;
            _currentSpeed = patrolSpeed;
        }
    }
    private float DistanceToPlayer()
    {
        return _playerTransform.position.x - transform.position.x; ;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        bool objectToСomparison = other.gameObject.CompareTag("Player");
        if (objectToСomparison)
        {
            _stopChasing = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        bool objectToСomparison = other.gameObject.CompareTag("Player");
        if (objectToСomparison)
        {
            _stopChasing = false;
        }
    }
}
