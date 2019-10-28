using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Constantly add force to the right
public class MoveRight : MonoBehaviour
{

    public float MoveForce;

    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.AddForce(Vector3.right * MoveForce);
    }
}
