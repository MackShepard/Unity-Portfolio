using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    [SerializeField] private float totalHealth = 200f;
    [SerializeField] private Animator _animator;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private AudioSource hitSound;
    private float _health;

    private void Start()
    {
        _health = totalHealth;
        InitHealth();
    }

    public void ReduseHealth(float damage)
    {
        hitSound.Play();
        _animator.SetTrigger("takeDamage");
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
        gameOverCanvas.SetActive(true);
    }

    private void InitHealth()
    {
        healthSlider.value = _health / totalHealth;
    }
}
