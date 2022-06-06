using System;
using UnityEngine;

  public class CharacterControlTool : MonoBehaviour
  {
    #region Constants


    #endregion


    #region Variable


    [SerializeField]
    private float speed = 2f;
    [Header("Relations")]
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private Rigidbody physicsBody = null;

    private Vector3 _movement;
    private float horizontalInput, verticalInput;

    public float zBound;

    #endregion


    #region MonoBehaviour
    private void Start()
    {

    }

    private void Update ()
    {
        // инпуты движения           
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");        

        // поворот персонажа (в корутин?)
        if (horizontalInput > 0)
        {
            Vector3 tempLS = transform.localScale;
            tempLS.x = 1;
            transform.localScale = tempLS;
        }
        else if (horizontalInput < 0)
        {
            Vector3 tempLS = transform.localScale;
            tempLS.x = -1;
            transform.localScale = tempLS;
        }

        // передача аниматору значения переменной (в корутин?)
        animator.SetFloat("speed", Mathf.Abs(_movement.magnitude));
            
    }


    private void FixedUpdate ()
    {
        // движение персонажа
        _movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
        _movement = _movement* speed * Time.fixedDeltaTime;
        _movement.y = physicsBody.velocity.y;
        physicsBody.velocity = _movement;

        // ограничение движения вниз (может убрать в корутин?)
        if (transform.position.z < zBound)
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
    }

    #endregion
  }
