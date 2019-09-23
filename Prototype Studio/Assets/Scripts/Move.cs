using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

//Generic movement class that can move a character around either in 3D or 2D space
//2D space can be a side scroller or top down
public class Move : MonoBehaviour
{
    
    //Public
    public float Speed;
    public int PlayerNum;
    
    //Private
    private Rewired.Player _rewiredPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        _rewiredPlayer = ReInput.players.GetPlayer(PlayerNum); //initialize rewired
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVector = new Vector3(_rewiredPlayer.GetAxis("Horizontal"), _rewiredPlayer.GetAxis("Vertical"), 0);
        transform.position += moveVector * Speed * Time.deltaTime;
    }
    
    
}
