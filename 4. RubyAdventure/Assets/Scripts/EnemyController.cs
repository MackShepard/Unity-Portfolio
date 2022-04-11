using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float timeToChangeDirection = 3f;

    float endCooldownChangeDirection;
    int facing;
    bool broken;

    Rigidbody2D _rb;
    Vector2[] _direction = {Vector2.left, Vector2.up, Vector2.right, Vector2.down};
    Animator animator;
    Vector2 position;
    public ParticleSystem smokeEffect;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        broken = true;
    }
    void Update()
    {
        if (!broken)
        {
            return;
        }
        if (Time.time > endCooldownChangeDirection)
            ChangeDirection();
       
    }
    private void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }
        position = transform.position;
        position += _direction[facing] * speed * Time.deltaTime;
        animator.SetFloat("Move X", _direction[facing].x);
        animator.SetFloat("Move Y", _direction[facing].y);
        _rb.MovePosition(position);
    }
    void ChangeDirection()
    {
        endCooldownChangeDirection = Time.time + timeToChangeDirection;
        facing = Random.Range(0, 4);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RubyController player = collision.gameObject.GetComponent<RubyController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }

    }
    public void Fix()
    {
        broken = false;
        _rb.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
    }
}
