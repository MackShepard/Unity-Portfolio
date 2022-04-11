using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJumpDemo : MonoBehaviour
{
    [SerializeField] private float fallMultiplayer = 2.5f;
    [SerializeField] private float lowJumpMultiplayer = 2.5f;
    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplayer - 1) * Time.deltaTime;
        } else if (_rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            _rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplayer - 1) * Time.deltaTime;
        }
        
    }
}
