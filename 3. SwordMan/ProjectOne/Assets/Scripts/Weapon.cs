using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private AudioSource enemyHitSound;
    private AttackController attackController;

    private void Start()
    {
        attackController = transform.root.GetComponent<AttackController>(); 

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        enemyHealth enemyHealth = other.GetComponent<enemyHealth>();

        if (enemyHealth != null && attackController.IsAttack)
        {
            enemyHealth.ReduseHealth(damage);
            enemyHitSound.Play();
           
        }
    }
}
