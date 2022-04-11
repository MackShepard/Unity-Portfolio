using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacterControl : MonoBehaviour
{

    [SerializeField] float speedX;
    [SerializeField] float jumpForce;

    private Rigidbody2D _rb;

    private float _horizontal;
    private bool _isJump;
    private bool _isGround;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.W))
        {
            Jump(); 
        }

        if (_horizontal > 0 && (whereIsLooking != -1))
            Flip();
        else if (_horizontal < 1 && (whereIsLooking != 1))
            Flip();

    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * speedX * (Time.deltaTime * 50f), _rb.velocity.y); // ходьба по x

        if (_isJump)
        {
            _rb.velocity = Vector2.up * jumpForce; // прыжок
            _isJump = false;
        }
        
    }

    private void Jump()
    {   
        if (_isGround) 
        _isJump = true;
    }

    private void Flip()
    {
        Vector2 flipTemp = this.transform.localScale;
        flipTemp.x *= -1;
        this.transform.localScale = flipTemp;
    }

    private float whereIsLooking {
        get {
            return (this.transform.localScale.x);
        }
    }


    private void OnCollisionEnter2D(Collision2D obj)
    {
        switch(obj.transform.tag)
        {
            case ("Ground"):
                _isGround = true;
                break;

        }

    }

    private void OnCollisionExit2D(Collision2D obj)
    {
        switch (obj.transform.tag)
        {
            case ("Ground"):
                _isGround = false;
                break;

        }

    }
}
