using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//When colliding with Pellets, add one to the snake
public class GrabPellet : MonoBehaviour
{
    
    //Public
    public GameObject Pellet;
    public int TrailSizeMax;
    public int ScoreValue;

    private GameObject _lastSegment;
    private Rigidbody2D _lastSegmentRb;
    private List<GameObject> _segmentList;
    private GameObject _segmentParent;

    void Start()
    {
        _lastSegment = gameObject; //starts as this object
        _lastSegmentRb = GetComponent<Rigidbody2D>();
        _segmentList = new List<GameObject>();
        _segmentParent = (GameObject)Instantiate(Resources.Load("Prefabs/SegmentParent"));
    }

    private void Update()
    {
        
    }

    private void Combo()
    {
        int comboScore = _segmentList.Count * ScoreValue;
        Score.Instance.AddScore(comboScore);
        Destroy(_segmentParent);
        _segmentParent = (GameObject)Instantiate(Resources.Load("Prefabs/SegmentParent"));
        _lastSegment = gameObject;
        _lastSegmentRb = GetComponent<Rigidbody2D>();
        _segmentList = new List<GameObject>();
    }

    public void Die()
    {
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
            newSegment.transform.parent = _segmentParent.transform;
            _lastSegment = newSegment;
            _lastSegmentRb = newSegment.GetComponent<Rigidbody2D>();
            Score.Instance.AddScore(ScoreValue);
            _segmentList.Add(_lastSegment);
            if (_segmentList.Count >= TrailSizeMax)
            {
                Combo();
            }
        }
    }
}
