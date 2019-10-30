using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hit space to flap
public class Flap : MonoBehaviour
{
    //Public
    public float FlapForce;
    
    //Private
    private Rigidbody2D _rb;
    private bool _isDead;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isDead)
        {
            _rb.AddForce(Vector3.up * FlapForce, ForceMode2D.Impulse);
        }   
    }

    public void Die()
    {
        _rb.AddForce(Vector3.left * 1000);
        _rb.AddTorque(1000);
        _isDead = true;
    }
}
