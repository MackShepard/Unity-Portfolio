using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    private Rigidbody2D _rb; 
    [SerializeField] private float speedX = 5f; 
    //[Range(1,10)]
    [SerializeField] private float jumpForce;
    [SerializeField] private Animator animator; 
    [SerializeField] private Transform playerModelTransform;
    [SerializeField] private AudioSource jumpSound;
    private FixedJoystick _fixedJoystick;

    const float SPEEDXMULTIPLAYER = 50f; 
    private float _horizontal; 
    private bool _isGround = false; 
    private bool _isJump = false;
    private bool _isFacingRight = true; 
    private Finish _finish; 
    private bool _isFinish = false; 
    private bool _isLeverArm = false; 
    private LeverArm _leverArm; 


    void Start()
    {
        
        _rb = GetComponent<Rigidbody2D>();
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>(); //для левелов
        _leverArm = FindObjectOfType<LeverArm>(); //для левелов
        // _fixedJoystick = GameObject.FindGameObjectWithTag("FixedJoystick").GetComponent<FixedJoystick>(); моб контроль
    }
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal"); // присваиваем переменной направление движения (-1:1) для клавы
        // _horizontal = _fixedJoystick.Horizontal; мобильный контроль
        animator.SetFloat("speedX", System.Math.Abs(_horizontal)); // задает значение переменной в аниматоре // анимация бега

        if (Input.GetKeyDown(KeyCode.W)) // если нажата клавиша W и персонаж на земле, то совершается прыжок
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.F)){ 
            Interact();
        }
        
    }
    void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * speedX * Time.fixedDeltaTime * SPEEDXMULTIPLAYER, _rb.velocity.y); // каждые 0.02 секунды в зависимости от переменной horizontal двигает героя в определенном направлении (направление движения * скорость * 1(коротко))
        
        if (_isJump) { 
            _rb.velocity = Vector2.up * jumpForce;
            //_rb.AddForce(new Vector2(0f, jumpForce)); // толкаем его по оси Y
            _isGround = false; 
            _isJump = false; 
        }
        if (_horizontal > 0f && !_isFacingRight){ 
            Flip(); 
        } else if (_horizontal < 0f && _isFacingRight){ 
            Flip(); 
        }
    }

    void Flip() 
    {
        _isFacingRight = !_isFacingRight; 
        Vector3 playerScale = playerModelTransform.localScale; 
        playerScale.x *= -1; 
        playerModelTransform.localScale = playerScale; 
    }
    public void Jump()
    {
        if (_isGround) { 
        _isJump = true; 
        jumpSound.Play();
        }
    }
    public void Interact()
    {
        if (_isFinish) 
            _finish.FinsishLevel();
        if (_isLeverArm) 
            _leverArm.ActivatedLeverArm(); 
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Ground"))
        { 
            _isGround = true; 
        }

    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        LeverArm leverArmTemp = other.GetComponent<LeverArm>(); 

        if (other.CompareTag("Finish")) 
        {
            _isFinish = true; 
        }
        if (leverArmTemp != null){ 
            _isLeverArm = true; 
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        LeverArm leverArmTemp = other.GetComponent<LeverArm>(); 
        if (other.CompareTag("Finish"))
        {
            _isFinish = false; 
        }
        if (leverArmTemp != null) 
        {
            _isLeverArm = false; 
        }
    }
   
}
