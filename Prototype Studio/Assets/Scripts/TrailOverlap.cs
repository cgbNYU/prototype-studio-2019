using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TrailOverlap : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("RedTrail") && other.gameObject.CompareTag("BlueTrail"))
        {
            Debug.Log("Trailhit");
            if (other.gameObject.transform.position.z < gameObject.transform.position.z)
            {
                Debug.Log("RedTrail Overlapped");
                Destroy(gameObject.GetComponent<BoxCollider2D>());
            }
        }
        else if (gameObject.CompareTag("BlueTrail") && other.gameObject.CompareTag("RedTrail"))
        {
            if (other.gameObject.transform.position.z < gameObject.transform.position.z)
            {
                Destroy(gameObject.GetComponent<BoxCollider2D>());
            }
        }
    }
}
