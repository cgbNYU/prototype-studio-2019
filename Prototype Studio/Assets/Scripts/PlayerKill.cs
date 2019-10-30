using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerKill : MonoBehaviour
{
    public GrabPellet _gPellet;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Snake"))
        {
            _gPellet.Die();
        }
    }
}
