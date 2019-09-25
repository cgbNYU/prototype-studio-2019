using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSpot : MonoBehaviour
{
    private Transform playerTransform;
    private Transform dogTransform;
    //Public
    public float ViewDist;
    public float ViewAngle;
    public float Speed;
    public float DeathSpeed;
    
    //Private
    private bool _isAttacking;
    private bool _isLooking;
    private Rigidbody2D _rb;
    public Collider2D _viewCone;

    private Vector3 _target;
    // Start is called before the first frame update
    void Start()
    {
        _isAttacking = false;
        _isLooking = true;
        playerTransform = GameObject.FindWithTag("Player").transform;
        dogTransform = GameObject.FindWithTag("Dog").transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (_isAttacking)
        {
            Attack();
        }
    }

    public void ViewCheck(Transform target)
    {
        if (_isLooking)
        {
            _isAttacking = true;
            _target = target.position;
        }
    }

    private void Attack()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isAttacking)
        {
            if (other.CompareTag("Player") || other.CompareTag("Dog"))
            {
                Debug.Log("Dead Player");
                Vector3 forceVector = other.transform.position - transform.position;
                other.GetComponent<Rigidbody2D>().AddForce(forceVector.normalized * DeathSpeed);
                other.GetComponent<Rigidbody2D>().AddTorque(1000);
            }
        }
        else if (other.CompareTag("Dog"))
        {
            Debug.Log("GuardDie");
            Vector3 forceVector = transform.position - other.transform.position;
            _rb.AddForce(forceVector.normalized * DeathSpeed);
            _rb.AddTorque(1000);
            _isAttacking = false;
            _isLooking = false;
        }
    }
}
