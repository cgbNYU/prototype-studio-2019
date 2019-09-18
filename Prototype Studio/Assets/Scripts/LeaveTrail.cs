using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;


public class LeaveTrail : MonoBehaviour
{
    
    //Public
    public GameObject BlueTrail;
    public GameObject RedTrail;
    public GameObject Skull;
    public MeshRenderer ColorMat;
    public Material RedMat;
    public Material BlueMat;
    public PlayerColor StartingColor;
    public float DropTime;
    public int PlayerNum;
    
    //Private
    private float timer;
    private FourWayMove fourMove;
    private Rewired.Player rewiredPlayer;
    
    //Enum
    public enum PlayerColor
    {
        Red,
        Blue
    }

    private PlayerColor color;
    
    // Start is called before the first frame update
    void Start()
    {
        color = StartingColor;
        timer = DropTime;
        fourMove = GetComponent<FourWayMove>();
        rewiredPlayer = ReInput.players.GetPlayer(PlayerNum); //assing player number in inspector
    }

    // Update is called once per frame
    void Update()
    {
        switch (color)
        {
            case PlayerColor.Blue:
                BlueTrailDrop();
                break;
            case PlayerColor.Red:
                RedTrailDrop();
                break;
        }
        HitCheck();
        ColorSwitch(color);
    }

    private void BlueTrailDrop()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = DropTime;
            Instantiate(BlueTrail, transform.position, transform.rotation);
        }
    }

    private void RedTrailDrop()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = DropTime;
            Instantiate(RedTrail, transform.position, transform.rotation);
        }
    }

    private void HitCheck()
    {
        /*RaycastHit2D[] hitList = Physics2D.RaycastAll(transform.position, fourMove.MoveDir, .1f);
        foreach (RaycastHit2D hit in hitList)
        {
            bool isSafe = false;
            bool canDie = false;
            if (color == PlayerColor.Red && hit.collider.gameObject.CompareTag("BlueTrail"))
            {
                isSafe = true;
            }
            else if (color == PlayerColor.Blue && hit.collider.gameObject.CompareTag("RedTrail"))
            {
                isSafe = true;
            }
            if (color == PlayerColor.Red && hit.collider.gameObject.CompareTag("RedTrail"))
            {
                canDie = true;
            }
            else if (color == PlayerColor.Blue && hit.collider.gameObject.CompareTag("BlueTrail"))
            {
                canDie = true;
            }
            Debug.Log("safe? " + isSafe);
            if (canDie && !isSafe)
            {
                Debug.Log("Die");
            }
        }*/
        RaycastHit2D hit = Physics2D.Raycast(transform.position, fourMove.MoveDir, .1f);
        if (hit == true)
        {
            if (hit.collider.gameObject.CompareTag("BlueTrail") && color == PlayerColor.Blue)
            {
                Die();
            }
            else if (hit.collider.gameObject.CompareTag("RedTrail") && color == PlayerColor.Red)
            {
                Die();
            }
        }
    }

    private void ColorSwitch(PlayerColor currentColor)
    {
        if (rewiredPlayer.GetButtonDown("ColorSwitch"))
        {
            if (currentColor == PlayerColor.Blue)
            {
                color = PlayerColor.Red;
                ColorMat.material = RedMat;
            }
            else if (currentColor == PlayerColor.Red)
            {
                color = PlayerColor.Blue;
                ColorMat.material = BlueMat;
            }
        }
    }

    private void Die()
    {
        Instantiate(Skull, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death"))
        {
            Die();
        }
    }
}
