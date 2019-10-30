using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hit space to flap
public class Flap : MonoBehaviour
{
    //Public
    public float FlapForce;
    public AudioClip FlapSound;
    
    //Private
    private Rigidbody2D _rb;
    private bool _isDead;
    private AudioSource _audio;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _isDead = false;
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isDead)
        {
            _rb.AddForce(Vector3.up * FlapForce, ForceMode2D.Impulse);
            _audio.PlayOneShot(FlapSound);
        }   
    }

    public void Die()
    {
        _rb.AddForce(Vector3.left * 1000);
        _rb.AddTorque(1000);
        _isDead = true;
    }
}
