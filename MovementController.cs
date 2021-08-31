using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(IsGround))]
public class MovementController : MonoBehaviour
{
    [Header("Parametrs")]
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _chrouchSpeed;
    [SerializeField] private float _chrouchJumpHeight;

    private IsGround _checker;

    private Vector3 _crouch;
    private Vector3 _startScale;
    
    private float _playerStartHeight;
    private float _startJumpHeight;
    private float _currentSpeed;
    private float _yRotation;

    private bool _isCrouch;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _checker = GetComponent<IsGround>();

        var localScale = transform.localScale;
        
        _startScale = localScale;
        _startJumpHeight = _jumpHeight;
        _crouch = new Vector3(localScale.x, 0.5f, localScale.z);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Move();
        CanCrouch();
        CanRun();

        if (CanJump())
            Jump();
    }

   private bool CanJump()
   {
        return IsGround.IsGrounded && Input.GetButtonDown("Jump");
   }
    
    private void Jump()
    {
        if(!_isCrouch)
            _jumpHeight = _startJumpHeight;

        _rigidBody.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
    }
    
    private Vector3 GetMoveDirection()
    {
        float horizontalspeed = Input.GetAxis("Horizontal");
        float verticalspeed = Input.GetAxis("Vertical");

        return new Vector3(horizontalspeed, 0f, verticalspeed);
    }

    private void Move()
    {
        //Extra Gravity
        _rigidBody.AddForce(Vector3.down * Time.deltaTime * 10);

        var direction = GetMoveDirection();
        var vector = transform.rotation * direction * _currentSpeed;

        _rigidBody.velocity = new Vector3(vector.x, _rigidBody.velocity.y, vector.z);
    }

    // Get's player input and say to controller crouch or not
    private void CanCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _isCrouch = true;
            Crouch();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            _isCrouch = false;
            GoUp();
        }
    }

    //
    private void Crouch()
    {
        _currentSpeed = _chrouchSpeed;
        _jumpHeight = _chrouchJumpHeight;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z);
        transform.localScale = _crouch;
    }

    private void GoUp()
    {
        _currentSpeed = _walkSpeed;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
        transform.localScale = _startScale;
    }

    private void CanRun()
    {
        if (IsGround.IsGrounded && Input.GetKey(KeyCode.LeftShift) && !_isCrouch)
            Run();
        
        else if (!_isCrouch)
            StopRun();
    }

    private void Run()
    {
        _currentSpeed = _runSpeed;
    }
    
    private void StopRun()
    {
        _currentSpeed = _walkSpeed;
    }
}
