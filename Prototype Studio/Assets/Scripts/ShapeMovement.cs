using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ShapeMovement : MonoBehaviour
{
    
    //Public
    public int PlayerNum;
    public float CircleSpeed;
    public float CircleFriction;
    
    //Private
    private Rewired.Player rewiredPlayer;
    private Vector3 moveVector;
    
    //Enumerator
    enum PlayerShape
    {
        Circle,
        Square,
        Triangle
    }

    private PlayerShape shape;
    
    // Start is called before the first frame update
    void Start()
    {
        shape = PlayerShape.Circle;
        rewiredPlayer = ReInput.players.GetPlayer(PlayerNum); //assing player number in inspector
    }

    // Update is called once per frame
    void Update()
    {
        switch (shape)
        {
            case PlayerShape.Circle:
                CircleMove();
                break;
            case PlayerShape.Square:
                break;
            case PlayerShape.Triangle:
                break;
        }
    }

    void CircleMove()
    {
        moveVector += new Vector3(rewiredPlayer.GetAxisRaw("Horizontal"), rewiredPlayer.GetAxisRaw("Vertical"), 0);
        transform.position += moveVector * CircleSpeed * Time.deltaTime;
        Vector3 friction = -moveVector.normalized * CircleFriction * Time.deltaTime;
        moveVector += friction;
        Debug.Log("Move Vector = " + moveVector);
    }
}
