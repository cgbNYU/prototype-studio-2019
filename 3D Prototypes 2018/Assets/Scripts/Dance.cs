using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using Rewired;
using Unity.Collections;
using Debug = UnityEngine.Debug;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Dance : MonoBehaviour
{
    //Public 
    public float WalkForce;
    public float WalkFriction;
    public float MaxSpeed;
    public float JumpForce;
    public float JumpFriction;
    public float FallSpeed;
    public float SpinForce;
    public float FlipForce;
    public float SlamForce;
    
    //Private
    private Rigidbody _rb;
    private Vector3 _moveVector;
    private float _jumpPower;
    private float _jumpTimer;
    
    //Enumerator
    public enum PlayerState
    {
        Idle,
        Walking,
        Jumping,
        Falling,
        Flipping,
        Slamming
    }

    public PlayerState _playerState;
    
    //Rewired
    private Rewired.Player _rewiredPlayer;
    public int PlayerNum = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _rewiredPlayer = ReInput.players.GetPlayer(PlayerNum); //intialize controller
        _playerState = PlayerState.Idle; //set idle state
        _rb = GetComponent<Rigidbody>(); //get the rigidbody
    }

    // Update is called once per frame
    void Update()
    {
        switch (_playerState)
        {
            case PlayerState.Idle:
                Walk();
                Jump();               
                break;
            case PlayerState.Jumping:
                Jumping();
                Flip();
                Spin();
                //Slam();
                break;
            case PlayerState.Falling:
                Falling();
                Flip();
                Spin();
                break;
            case PlayerState.Flipping:                
                Spin();
                Slam();
                break;
            case PlayerState.Slamming:
                break;
            default:
                Debug.Log("State machine error");
                break;
        }
        UpdatePos();
    }

    //Use the analog stick to walk around
    private void Walk()
    {
        Vector3 moveVector = new Vector3(_rewiredPlayer.GetAxis("Horz"), 0, _rewiredPlayer.GetAxis("Vert"));
        moveVector.Normalize();
        if (Mathf.Abs(moveVector.magnitude) > 0)
        {
            Debug.Log("Walking");
            _moveVector += moveVector * WalkForce;
            _moveVector = Vector3.ClampMagnitude(_moveVector, MaxSpeed);
        }
        else
        {
            Debug.Log(("Slowing"));
            _moveVector += -_moveVector.normalized * WalkFriction * Time.deltaTime;
            if (_moveVector.magnitude <= .02)
            {
                _moveVector = Vector3.zero;
            }
        }
    }
    
    //Left stick and Jump to flip in the air
    private void Flip()
    {                
        Vector3 moveVector = new Vector3(_rewiredPlayer.GetAxis("Flip_Horz"), 0, _rewiredPlayer.GetAxis("Flip_Vert"));
        moveVector.Normalize();
        Quaternion flipVector = Quaternion.Euler(moveVector.z * FlipForce, 0, -moveVector.x * FlipForce) * transform.localRotation;
        transform.localRotation = flipVector;
    }

    private void Flipping()
    {
        
    }

    //Press jump while on the ground to jump
    private void Jump()
    {
        if (_rewiredPlayer.GetButtonDown("Jump"))
        {
            _moveVector.y = JumpForce;
            _playerState = PlayerState.Jumping;           
        }
    }

    //Move up
    private void Jumping()
    {    
        _moveVector.y -= JumpFriction * Time.deltaTime;
        if (_moveVector.y < 0)
        {
            _playerState = PlayerState.Falling;
        }
    }

    //Move down
    private void Falling()
    {
        _moveVector.y -= FallSpeed * Time.deltaTime;
        RaycastHit boxRay;

        if (Physics.BoxCast(transform.position, new Vector3(transform.localScale.x/5, transform.localScale.y/5, transform.localScale.z/5), Vector3.down, out boxRay,
            transform.localRotation, transform.localScale.y/2))
        {
            if (boxRay.collider.CompareTag("Ground"))
            {
                Debug.Log("Landed");
                _moveVector.y = 0;
                transform.localRotation = Quaternion.Euler(Vector3.zero);
                _playerState = PlayerState.Idle;
            }
        }
    }

    //Press the triggers to spin in the corresponding direction
    private void Spin()
    {
        Vector3 spinVector = new Vector3(0, _rewiredPlayer.GetAxis("Spin"), 0);
        spinVector.Normalize();
        Quaternion spinQuaternion = Quaternion.Euler(0, spinVector.y, 0) * transform.localRotation;
        Debug.Log("spin vector " + spinVector);
        transform.localRotation = spinQuaternion;
    }

    //Press Slam in air to hit the ground hard
    private void Slam()
    {
        if (_rewiredPlayer.GetButtonDown("Slam"))
        {
            _playerState = PlayerState.Slamming;
            _rb.AddForce(Vector3.down * SlamForce);
        }
    }
    
    //Update the position after all calculations have been done to MoveVector
    private void UpdatePos()
    {
        transform.position += _moveVector * Time.deltaTime;        
    }
}
