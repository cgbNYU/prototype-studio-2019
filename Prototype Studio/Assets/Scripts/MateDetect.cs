using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MateDetect : MonoBehaviour
{

    public GameObject mateTarget;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            mateTarget = other.gameObject;
        }
    }
}
