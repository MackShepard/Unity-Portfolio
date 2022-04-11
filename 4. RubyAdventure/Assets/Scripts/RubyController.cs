using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public int maxHealth = 5;
    public float speed = 3f;
    public float timeInvincible = 2.0f;

    int currentHealth;
    float horizontal;
    float vertical;
    bool isInvincible;
    float invincibleDone;
    

    public GameObject projectilePrefab;
    public ParticleSystem itemUpEffect;
    public AudioClip throwsCog;
    public AudioClip rubyWasHitted;

    AudioSource audioSource;
    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 lookDirection = new Vector2(0, 1);
   
    public int CurrentHealth
    {
        get { return currentHealth; }
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvincible)
                return;
            PlaySound(rubyWasHitted);
            isInvincible = true;
            invincibleDone = Time.time + timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);

    }
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        PlaySound(throwsCog);
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);

    }

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Time.time >= invincibleDone)
        {
            isInvincible = false;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponentInChildren<NonPlayerCharacter>();
                if (character != null)
                    character.DisplayDialog();
            }
        }
    }
    void FixedUpdate()
    {
        Vector2 move = new Vector2(horizontal, vertical);
        Vector2 position = rigidbody2d.position;
        position += move * speed * Time.deltaTime;

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        rigidbody2d.MovePosition(position);

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

    }
    
    
}
