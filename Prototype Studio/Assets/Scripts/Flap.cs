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
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * FlapForce, ForceMode2D.Impulse);
        }   
    }
    
    
}
