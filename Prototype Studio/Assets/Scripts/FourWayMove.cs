using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using Rewired;
using Unity.Collections;

public class FourWayMove : MonoBehaviour
{
    
    //Public
    public float Speed;
    public Vector3 StartDir;
    public int PlayerNum;

    public Vector3 MoveDir => moveDir;

    //Private
    private Vector3 moveDir;
    private Player rewiredPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        moveDir = StartDir;
        rewiredPlayer = ReInput.players.GetPlayer(PlayerNum); //assing player number in inspector
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (rewiredPlayer.GetAxis("Horizontal") > 0 && moveDir != Vector3.left)
        {
            moveDir = Vector3.right;
        }
        else if (rewiredPlayer.GetAxis("Horizontal") < 0 && moveDir != Vector3.right)
        {
            moveDir = Vector3.left;
        }
        else if (rewiredPlayer.GetAxis("Vertical") > 0 && moveDir != Vector3.down)
        {
            moveDir = Vector3.up;
        }
        else if (rewiredPlayer.GetAxis("Vertical") < 0 && moveDir != Vector3.up)
        {
            moveDir = Vector3.down;
        }
        transform.position += moveDir * Speed * Time.deltaTime;
    }
}
