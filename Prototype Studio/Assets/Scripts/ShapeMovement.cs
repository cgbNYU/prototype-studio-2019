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
    public float CircleDrag;
    public float SquareSpeed;
    public float SquareFriction;
    public float SquareDrag;
    public float SquareTime;
    public float TriangleSpeed;
    public float TriangleRotateSpeed;
    public float TriangleFriction;
    public float TriangleDrag;
    
    //Private
    private Rewired.Player rewiredPlayer;
    private Vector3 moveVector;
    public bool squarePressed;
    private float squareTimer;
    
    //Enumerators
    public enum PlayerShape
    {
        Circle,
        Square,
        Triangle
    }

    public PlayerShape shape;

    enum PlayerColor
    {
        Red,
        Yellow,
        Blue,
        Green,
        Purple,
        Orange
    }

    private PlayerColor color;
    
    // Start is called before the first frame update
    void Start()
    {
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
                SquareMove();
                break;
            case PlayerShape.Triangle:
                TriangleMove();
                break;
        }

        switch (color)
        {
            case PlayerColor.Red:
                Red();
                break;
            case PlayerColor.Blue:
                break;
            case PlayerColor.Yellow:
                break;
        }
    }

    void CircleMove()
    {
        moveVector += new Vector3(rewiredPlayer.GetAxisRaw("Horizontal"), rewiredPlayer.GetAxisRaw("Vertical"), 0);
        transform.position += moveVector * CircleSpeed * Time.deltaTime;
        Vector3 friction = -moveVector.normalized * CircleFriction * Time.deltaTime;
        moveVector += friction;
        Vector3 drag = -moveVector * CircleDrag * Time.deltaTime;
        moveVector += drag;
        Debug.Log("Move Vector = " + moveVector);
    }

    void SquareMove()
    {
        if (Mathf.Abs(rewiredPlayer.GetAxisRaw("Horizontal")) > 0 && !squarePressed)
        {
            moveVector += Vector3.right * rewiredPlayer.GetAxisRaw("Horizontal");
            squarePressed = true;
        }
        else if (Mathf.Abs(rewiredPlayer.GetAxisRaw("Vertical")) > 0 && !squarePressed)
        {
            moveVector += Vector3.up * rewiredPlayer.GetAxisRaw("Vertical");
            squarePressed = true;
        }

        transform.position += moveVector * SquareSpeed * Time.deltaTime;
        Vector3 friction = -moveVector.normalized * SquareFriction * Time.deltaTime;
        moveVector += friction;
        Vector3 drag = -moveVector * SquareDrag * Time.deltaTime;
        moveVector += drag;

        if (squarePressed)
        {
            squareTimer -= Time.deltaTime;
            if (squareTimer <= 0)
            {
                squarePressed = false;
                squareTimer = SquareTime;
            }
        }
    }

    void TriangleMove()
    {
        moveVector += transform.up * rewiredPlayer.GetAxisRaw("Vertical");
        transform.position += moveVector * TriangleSpeed * Time.deltaTime;
        transform.Rotate(Vector3.back * rewiredPlayer.GetAxisRaw("Horizontal") * TriangleRotateSpeed);
        Vector3 friction = -moveVector.normalized * TriangleFriction * Time.deltaTime;
        moveVector += friction;
        Vector3 drag = -moveVector * TriangleDrag * Time.deltaTime;
        moveVector += drag;
    }

    void Red()
    {
        
    }

    public void ShapeChange()
    {
        
    }

    public void ColorChange()
    {
        
    }
}
