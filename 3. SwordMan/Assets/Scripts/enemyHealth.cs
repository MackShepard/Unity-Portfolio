using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour
{
    [SerializeField] private float totalHealth = 100f;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Animator animator;
    private float _health;

    private void Start()
    {
        _health = totalHealth;
        InitHealth();
    }

    public void ReduseHealth(float damage)
    {
        animator.SetTrigger("takeDamage");
        _health -= damage;
        InitHealth();
        if (_health <= 0f)
        {
            Die();     
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
    private void InitHealth()
    {
        healthSlider.value = _health / totalHealth;
    }
}
