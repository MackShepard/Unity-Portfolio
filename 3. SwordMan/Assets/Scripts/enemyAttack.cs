using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private float timeToDamage = 1f;
    private float _timeToDamageTemp;
    private bool _canAttack = true;

    private void Start()
    {
        _timeToDamageTemp = timeToDamage;
    }

    private void Update()
    {
        if (!_canAttack)
        {
            _timeToDamageTemp -= Time.deltaTime;
            if (_timeToDamageTemp <= 0f)
            {
                _canAttack = true;
                _timeToDamageTemp = timeToDamage;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        playerHealth playerHealth = other.gameObject.GetComponent<playerHealth>();

        if (playerHealth != null && _canAttack)
        {
            playerHealth.ReduseHealth(damage);
            _canAttack = false;
        }
    }
}
