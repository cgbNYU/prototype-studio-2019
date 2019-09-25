using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FollowSausage : MonoBehaviour
{
    
    //Public
    public float Speed;
    
    //private
    private GameObject _target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _target = GameObject.FindWithTag("Sausage");
        if (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Speed * Time.deltaTime);
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sausage"))
        {
            Debug.Log("Sausage Eat");
            Destroy(other.gameObject);
        }
    }
}
