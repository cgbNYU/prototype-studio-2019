using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerKill : MonoBehaviour
{
    private GrabPellet _gPellet;

    private void Start()
    {
        _gPellet = GameObject.Find("Snake").GetComponent<GrabPellet>();
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Snake"))
        {
            _gPellet.Die();
        }
    }
}
