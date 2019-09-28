using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class WhistleFollow : MonoBehaviour
{
    private Rewired.Player _rewiredPlayer;

    private GameObject _target;

    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        _rewiredPlayer = ReInput.players.GetPlayer(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_rewiredPlayer.GetButtonDown("Whistle"))
        {      
            _target = GameObject.FindGameObjectWithTag("Player");
        }

        if (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position,
                Speed * Time.deltaTime);
            if (transform.position == _target.transform.position)
            {
                _target = null;
            }
        }
    }
}
