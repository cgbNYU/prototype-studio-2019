using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using Rewired;
using static Events;
using UnityEngine.UI;

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
    public float BasicSpeed;
    public float LifeTime = 60;
    public GameObject SkullIcon;
    public float MateRadius;
    public MeshRenderer Quad;
    public Material BlueCircle;
    public Material RedCircle;
    public Material YellowCircle;
    public Material BlueTriangle;
    public Material RedTriangle;
    public Material YellowTriangle;
    public Material BlueSquare;
    public Material RedSquare;
    public Material YellowSquare;
    public Text LifeText;
    public Text FoodText;
    
    //Private
    private Rewired.Player rewiredPlayer;
    private Vector3 moveVector;
    public bool squarePressed;
    private float squareTimer;
    public int food;
    private float lifeTimer;

    private MateDetect mDet;
    //Enumerators
    public enum PlayerShape
    {
        Circle,
        Square,
        Triangle,
        Basic,
        Dead
    }

    public PlayerShape shape;

    public enum PlayerColor
    {
        Red,
        Yellow,
        Blue,
        Green,
        Purple,
        Orange
    }

    public PlayerColor color;
    
    // Start is called before the first frame update
    void Start()
    {
        rewiredPlayer = ReInput.players.GetPlayer(PlayerNum); //assing player number in inspector
        lifeTimer = LifeTime;
        food = 0;
        mDet = GetComponentInChildren<MateDetect>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (shape)
        {
            case PlayerShape.Circle:
                CircleMate();
                CircleMove();
                LifeTiming();
                break;
            case PlayerShape.Square:
                SquareMove();
                SquareMate();
                LifeTiming();
                break;
            case PlayerShape.Triangle:
                TriangleMate();
                TriangleMove();
                LifeTiming();
                break;
            case PlayerShape.Basic:
                BasicMove();
                break;
            case PlayerShape.Dead:
                break;
        }

        switch (color)
        {
            case PlayerColor.Red:
                break;
            case PlayerColor.Blue:
                break;
            case PlayerColor.Yellow:
                break;
        }
        FoodText.text = food + "/3";
        LifeText.text = "Life: " + lifeTimer.ToString("#");
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

    void BasicMove()
    {
        moveVector = new Vector3(rewiredPlayer.GetAxisRaw("Horizontal"), rewiredPlayer.GetAxisRaw("Vertical"), 0);
        transform.position += moveVector * BasicSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (color == PlayerColor.Blue && other.gameObject.CompareTag("BlueBerry"))
        {
            Destroy(other.gameObject);
            food++;
        }
        else if (color == PlayerColor.Red && other.gameObject.CompareTag("RedBerry"))
        {    
            Destroy(other.gameObject);
            food++;
        }
        else if (color == PlayerColor.Yellow && other.gameObject.CompareTag("YellowBerry"))
        {
            Destroy(other.gameObject);
            food++;
        }
    }

    private void BerryEat(GameObject berry)
    {
        if (color == PlayerColor.Blue && berry.name == "BlueBerry")
        {
            Destroy(berry);
            food++;
        }
        else if (color == PlayerColor.Red && berry.name == "RedBerry")
        {
            Destroy(berry);
            food++;
        }
        else if (color == PlayerColor.Yellow && berry.name == "YellowBerry")
        {
            Destroy(berry);
            food++;
        }
        
    }

    void TriangleMate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && food >= 3)
        {
            if (mDet.mateTarget != null)
            {

                NPCScript script = mDet.mateTarget.GetComponent<NPCScript>();
                float randShape = Random.Range(-1, 1);
                float randColor = Random.Range(-1, 1);

                if (randShape >= 0) //take the other parent's shape
                {
                    if (script.Shape == NPCScript.NPCShape.Circle) //if they are a circle
                    {
                        shape = PlayerShape.Circle;
                        if (randColor >= 0) //take other parent's color
                        {
                            if (script.Color == NPCScript.NPCColor.Blue)
                            {
                                color = PlayerColor.Blue;
                                Quad.material = BlueCircle;
                            }
                            else if (script.Color == NPCScript.NPCColor.Red)
                            {
                                color = PlayerColor.Red;
                                Quad.material = RedCircle;
                            }
                            else
                            {
                                color = PlayerColor.Yellow;
                                Quad.material = YellowCircle;
                            }
                        }
                        else //take your color
                        {
                            if (color == PlayerColor.Blue)
                            {
                                Quad.material = BlueCircle;
                            }
                            else if (color == PlayerColor.Red)
                            {
                                Quad.material = RedCircle;
                            }
                            else
                            {
                                Quad.material = YellowCircle;
                            }
                        }
                    }

                    if (script.Shape == NPCScript.NPCShape.Triangle) //triangle
                    {
                        shape = PlayerShape.Triangle;
                        if (randColor >= 0) //take other parent's color
                        {
                            if (script.Color == NPCScript.NPCColor.Blue)
                            {
                                color = PlayerColor.Blue;
                                Quad.material = BlueTriangle;
                            }
                            else if (script.Color == NPCScript.NPCColor.Red)
                            {
                                color = PlayerColor.Red;
                                Quad.material = RedTriangle;
                            }
                            else
                            {
                                color = PlayerColor.Yellow;
                                Quad.material = YellowTriangle;
                            }
                        }
                        else //take your own color
                        {
                            if (color == PlayerColor.Blue)
                            {
                                Quad.material = BlueTriangle;
                            }
                            else if (color == PlayerColor.Red)
                            {
                                Quad.material = RedTriangle;
                            }
                            else
                            {
                                Quad.material = YellowTriangle;
                            }
                        }
                    }

                    if (script.Shape == NPCScript.NPCShape.Square) //square
                    {
                        shape = PlayerShape.Square;
                        if (randColor >= 0) //take other parent's color
                        {
                            if (script.Color == NPCScript.NPCColor.Blue)
                            {
                                color = PlayerColor.Blue;
                                Quad.material = BlueSquare;
                            }
                            else if (script.Color == NPCScript.NPCColor.Red)
                            {
                                color = PlayerColor.Red;
                                Quad.material = RedSquare;
                            }
                            else
                            {
                                color = PlayerColor.Yellow;
                                Quad.material = YellowSquare;
                            }
                        }
                        else //take your own color
                        {
                            if (color == PlayerColor.Blue)
                            {
                                Quad.material = BlueSquare;
                            }
                            else if (color == PlayerColor.Red)
                            {
                                Quad.material = RedSquare;
                            }
                            else
                            {
                                Quad.material = YellowSquare;
                            }
                        }
                    }
                }
                else //take your shape
                {
                    shape = PlayerShape.Triangle;
                    if (randColor >= 0) //take other parent's color
                    {
                        if (script.Color == NPCScript.NPCColor.Blue)
                        {
                            color = PlayerColor.Blue;
                            Quad.material = BlueTriangle;
                        }
                        else if (script.Color == NPCScript.NPCColor.Red)
                        {
                            color = PlayerColor.Red;
                            Quad.material = RedTriangle;
                        }
                        else
                        {
                            color = PlayerColor.Yellow;
                            Quad.material = YellowTriangle;
                        }
                    }
                    else //take your own color
                    {
                        if (color == PlayerColor.Blue)
                        {
                            Quad.material = BlueTriangle;
                        }
                        else if (color == PlayerColor.Red)
                        {
                            Quad.material = RedTriangle;
                        }
                        else
                        {
                            Quad.material = YellowTriangle;
                        }
                    }
                }

                lifeTimer = LifeTime;
                script.Die();
                food = 0;
            }
        }
    }

    void CircleMate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && food >= 3)
        {
            if (mDet.mateTarget != null)
            {

                NPCScript script = mDet.mateTarget.GetComponent<NPCScript>();
                float randShape = Random.Range(-1, 1);
                float randColor = Random.Range(-1, 1);

                if (randShape >= 0) //take the other parent's shape
                {
                    if (script.Shape == NPCScript.NPCShape.Circle) //if they are a circle
                    {
                        shape = PlayerShape.Circle;
                        if (randColor >= 0) //take other parent's color
                        {
                            if (script.Color == NPCScript.NPCColor.Blue)
                            {
                                color = PlayerColor.Blue;
                                Quad.material = BlueCircle;
                            }
                            else if (script.Color == NPCScript.NPCColor.Red)
                            {
                                color = PlayerColor.Red;
                                Quad.material = RedCircle;
                            }
                            else
                            {
                                color = PlayerColor.Yellow;
                                Quad.material = YellowCircle;
                            }
                        }
                        else //take your color
                        {
                            if (color == PlayerColor.Blue)
                            {
                                Quad.material = BlueCircle;
                            }
                            else if (color == PlayerColor.Red)
                            {
                                Quad.material = RedCircle;
                            }
                            else
                            {
                                Quad.material = YellowCircle;
                            }
                        }
                    }

                    if (script.Shape == NPCScript.NPCShape.Triangle) //triangle
                    {
                        shape = PlayerShape.Triangle;
                        if (randColor >= 0) //take other parent's color
                        {
                            if (script.Color == NPCScript.NPCColor.Blue)
                            {
                                color = PlayerColor.Blue;
                                Quad.material = BlueTriangle;
                            }
                            else if (script.Color == NPCScript.NPCColor.Red)
                            {
                                color = PlayerColor.Red;
                                Quad.material = RedTriangle;
                            }
                            else
                            {
                                color = PlayerColor.Yellow;
                                Quad.material = YellowTriangle;
                            }
                        }
                        else //take your own color
                        {
                            if (color == PlayerColor.Blue)
                            {
                                Quad.material = BlueTriangle;
                            }
                            else if (color == PlayerColor.Red)
                            {
                                Quad.material = RedTriangle;
                            }
                            else
                            {
                                Quad.material = YellowTriangle;
                            }
                        }
                    }

                    if (script.Shape == NPCScript.NPCShape.Square) //square
                    {
                        shape = PlayerShape.Square;
                        if (randColor >= 0) //take other parent's color
                        {
                            if (script.Color == NPCScript.NPCColor.Blue)
                            {
                                color = PlayerColor.Blue;
                                Quad.material = BlueSquare;
                            }
                            else if (script.Color == NPCScript.NPCColor.Red)
                            {
                                color = PlayerColor.Red;
                                Quad.material = RedSquare;
                            }
                            else
                            {
                                color = PlayerColor.Yellow;
                                Quad.material = YellowSquare;
                            }
                        }
                        else //take your own color
                        {
                            if (color == PlayerColor.Blue)
                            {
                                Quad.material = BlueSquare;
                            }
                            else if (color == PlayerColor.Red)
                            {
                                Quad.material = RedSquare;
                            }
                            else
                            {
                                Quad.material = YellowSquare;
                            }
                        }
                    }
                }
                else //take your shape
                {
                    shape = PlayerShape.Circle;
                    if (randColor >= 0) //take other parent's color
                    {
                        if (script.Color == NPCScript.NPCColor.Blue)
                        {
                            color = PlayerColor.Blue;
                            Quad.material = BlueCircle;
                        }
                        else if (script.Color == NPCScript.NPCColor.Red)
                        {
                            color = PlayerColor.Red;
                            Quad.material = RedCircle;
                        }
                        else
                        {
                            color = PlayerColor.Yellow;
                            Quad.material = YellowCircle;
                        }
                    }
                    else //take your own color
                    {
                        if (color == PlayerColor.Blue)
                        {
                            Quad.material = BlueCircle;
                        }
                        else if (color == PlayerColor.Red)
                        {
                            Quad.material = RedCircle;
                        }
                        else
                        {
                            Quad.material = YellowCircle;
                        }
                    }
                }

                lifeTimer = LifeTime;
                script.Die();
                food = 0;
            }
        }
    }
    
    void SquareMate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && food >= 3)
        {
            if (mDet.mateTarget != null)
            {

                NPCScript script = mDet.mateTarget.GetComponent<NPCScript>();
                float randShape = Random.Range(-1, 1);
                float randColor = Random.Range(-1, 1);

                if (randShape >= 0) //take the other parent's shape
                {
                    if (script.Shape == NPCScript.NPCShape.Circle) //if they are a circle
                    {
                        shape = PlayerShape.Circle;
                        if (randColor >= 0) //take other parent's color
                        {
                            if (script.Color == NPCScript.NPCColor.Blue)
                            {
                                color = PlayerColor.Blue;
                                Quad.material = BlueCircle;
                            }
                            else if (script.Color == NPCScript.NPCColor.Red)
                            {
                                color = PlayerColor.Red;
                                Quad.material = RedCircle;
                            }
                            else
                            {
                                color = PlayerColor.Yellow;
                                Quad.material = YellowCircle;
                            }
                        }
                        else //take your color
                        {
                            if (color == PlayerColor.Blue)
                            {
                                Quad.material = BlueCircle;
                            }
                            else if (color == PlayerColor.Red)
                            {
                                Quad.material = RedCircle;
                            }
                            else
                            {
                                Quad.material = YellowCircle;
                            }
                        }
                    }

                    if (script.Shape == NPCScript.NPCShape.Triangle) //triangle
                    {
                        shape = PlayerShape.Triangle;
                        if (randColor >= 0) //take other parent's color
                        {
                            if (script.Color == NPCScript.NPCColor.Blue)
                            {
                                color = PlayerColor.Blue;
                                Quad.material = BlueTriangle;
                            }
                            else if (script.Color == NPCScript.NPCColor.Red)
                            {
                                color = PlayerColor.Red;
                                Quad.material = RedTriangle;
                            }
                            else
                            {
                                color = PlayerColor.Yellow;
                                Quad.material = YellowTriangle;
                            }
                        }
                        else //take your own color
                        {
                            if (color == PlayerColor.Blue)
                            {
                                Quad.material = BlueTriangle;
                            }
                            else if (color == PlayerColor.Red)
                            {
                                Quad.material = RedTriangle;
                            }
                            else
                            {
                                Quad.material = YellowTriangle;
                            }
                        }
                    }

                    if (script.Shape == NPCScript.NPCShape.Square) //square
                    {
                        shape = PlayerShape.Square;
                        if (randColor >= 0) //take other parent's color
                        {
                            if (script.Color == NPCScript.NPCColor.Blue)
                            {
                                color = PlayerColor.Blue;
                                Quad.material = BlueSquare;
                            }
                            else if (script.Color == NPCScript.NPCColor.Red)
                            {
                                color = PlayerColor.Red;
                                Quad.material = RedSquare;
                            }
                            else
                            {
                                color = PlayerColor.Yellow;
                                Quad.material = YellowSquare;
                            }
                        }
                        else //take your own color
                        {
                            if (color == PlayerColor.Blue)
                            {
                                Quad.material = BlueSquare;
                            }
                            else if (color == PlayerColor.Red)
                            {
                                Quad.material = RedSquare;
                            }
                            else
                            {
                                Quad.material = YellowSquare;
                            }
                        }
                    }
                }
                else //take your shape
                {
                    shape = PlayerShape.Square;
                    if (randColor >= 0) //take other parent's color
                    {
                        if (script.Color == NPCScript.NPCColor.Blue)
                        {
                            color = PlayerColor.Blue;
                            Quad.material = BlueSquare;
                        }
                        else if (script.Color == NPCScript.NPCColor.Red)
                        {
                            color = PlayerColor.Red;
                            Quad.material = RedSquare;
                        }
                        else
                        {
                            color = PlayerColor.Yellow;
                            Quad.material = YellowSquare;
                        }
                    }
                    else //take your own color
                    {
                        if (color == PlayerColor.Blue)
                        {
                            Quad.material = BlueSquare;
                        }
                        else if (color == PlayerColor.Red)
                        {
                            Quad.material = RedSquare;
                        }
                        else
                        {
                            Quad.material = YellowSquare;
                        }
                    }
                }

                lifeTimer = LifeTime;
                script.Die();
                food = 0;
            }
        }
    }

    public void ShapeChange(PlayerShape newShape)
    {
        
    }

    public void ColorChange()
    {
        
    }

    void LifeTiming()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SkullIcon.SetActive(true);
        shape = PlayerShape.Dead;
    }
}
