using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//When colliding with Pellets, add one to the snake
public class GrabPellet : MonoBehaviour
{
    
    //Publiuc
    public GameObject Pellet;

    private GameObject _lastSegment;
    private Rigidbody2D _lastSegmentRb;

    void Start()
    {
        _lastSegment = gameObject; //starts as this object
        _lastSegmentRb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pellet"))
        {
            Destroy(other.gameObject);
            Vector3 newLocation = new Vector3(_lastSegment.transform.position.x - _lastSegment.transform.localScale.x, _lastSegment.transform.position.y, _lastSegment
            .transform.position.z);
            GameObject newSegment =
                Instantiate(Pellet, newLocation, _lastSegment.transform.rotation);
            newSegment.GetComponent<HingeJoint2D>().connectedBody = _lastSegmentRb;
            _lastSegment = newSegment;
            _lastSegmentRb = newSegment.GetComponent<Rigidbody2D>();
        }
    }
}
