using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Rewired;
using Unity.Collections;
using Debug = UnityEngine.Debug;

public class Dance : MonoBehaviour
{
    //Public 
    public float WalkForce;
    public float JumpForce;
    public float SpinForce;
    public float FlipForce;
    public float SlamForce;
    
    //Private
    private Rigidbody _rb;
    
    //Enumerator
    public enum PlayerState
    {
        Idle,
        Walking,
        Jumping,
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
    void FixedUpdate()
    {
        switch (_playerState)
        {
            case PlayerState.Idle:
                Walk();
                Jump();
                break;
            case PlayerState.Jumping:
                Flip();
                Spin();
                Slam();
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
    }

    //Use the analog stick to walk around
    private void Walk()
    {
        Vector3 moveVector = new Vector3(_rewiredPlayer.GetAxis("Horz"), 0, _rewiredPlayer.GetAxis("Vert"));
        moveVector.Normalize();
        _rb.AddForce(moveVector * WalkForce);
    }
    
    //Left stick and Jump to flip in the air
    private void Flip()
    {
        if (_rewiredPlayer.GetButtonDown("Jump"))
        {
            _playerState = PlayerState.Flipping;
            Vector3 moveVector = new Vector3(_rewiredPlayer.GetAxis("Horz"), 0, _rewiredPlayer.GetAxis("Vert"));
            moveVector.Normalize();
            _rb.AddTorque(moveVector * FlipForce);
            _rb.AddForce(Vector3.up * JumpForce);
        }
    }

    //Press jump while on the ground to jump
    private void Jump()
    {
        if (_rewiredPlayer.GetButtonDown("Jump"))
        {
            _playerState = PlayerState.Jumping;
            _rb.AddForce(Vector3.up * JumpForce);
        }
    }

    //Press the triggers to spin in the corresponding direction
    private void Spin()
    {
        if (_rewiredPlayer.GetAxis("Spin_L") > 0)
        {
            _rb.AddTorque(Vector3.left * SpinForce);
        }
        else if (_rewiredPlayer.GetAxis("Spin_R") > 0)
        {
            _rb.AddTorque(Vector3.right * SpinForce);
        }
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _playerState = PlayerState.Idle;
        }
    }
}
