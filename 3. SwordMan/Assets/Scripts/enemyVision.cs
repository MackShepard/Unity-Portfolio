using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyVision : MonoBehaviour
{
    [SerializeField] private GameObject currentHitObject; // хранит игровой объект с которым столкнулся рейкаст
    [SerializeField] private float circleRadius; // радиус круга который рисует рейкаст
    [SerializeField] private float maxDistance; // конечная точка рейкаста
    [SerializeField] private LayerMask layerMask; // слой который будет виден для рейкаста

    private Vector2 _currentEnemyPosition; // изначальная позиция противника
    private Vector2 _direction; // направление
    private float _currentHitDistance; // расстояние от противника до объекта
    private enemyController _enemyController;
    private void Start()
    {
        _enemyController = GetComponent<enemyController>();
       
    }
    private void Update()
    {
        
        _currentEnemyPosition = transform.position; // присваем изначальную позицию противника каждый кадр



        if (_enemyController.IsFacingRight)
        {
            _direction = Vector2.right; // направление 
        }
        else {
            _direction = Vector2.left; // направление 
        }
            RaycastHit2D hit = Physics2D.CircleCast(_currentEnemyPosition, circleRadius, _direction, maxDistance, layerMask); // рисует рейкаст //создай мне луч от точки pC, с радиусом cR, в направлении d, с макимальной дистанцией mD, что бы она обраща внимания только на lM 
        if(hit) // если противник встретился с чем-то
        {
            currentHitObject = hit.transform.gameObject; // сохраняем объект с которыми столкнулись
            _currentHitDistance = hit.distance; // сохраняем дистанцию до объекта
            if (currentHitObject.CompareTag("Player")){ // если у этого объекта тэг
                _enemyController.StartChasingPlayer();
            }

        } else { // иначе сбрасываем переменные
            currentHitObject = null;
            _currentHitDistance = maxDistance;
        }
    }
    private void OnDrawGizmos() // визулизируем рейкаст
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_currentEnemyPosition, _currentEnemyPosition + _direction * _currentHitDistance);
        Gizmos.DrawWireSphere(_currentEnemyPosition + _direction * _currentHitDistance, circleRadius);


    }
}
