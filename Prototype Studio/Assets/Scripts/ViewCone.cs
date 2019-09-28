using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCone : MonoBehaviour
{
    private GuardSpot _guard;
    
    // Start is called before the first frame update
    void Start()
    {
        _guard = GetComponentInParent<GuardSpot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Dog"))
        {
           // RaycastHit2D hit = Physics2D.Raycast(transform.position, other.transform.position);
            //if (hit.transform.gameObject.CompareTag("Player") || hit.transform.gameObject.CompareTag("Dog"))
            //{
                _guard.ViewCheck(other.transform);
            //}
        }
    }
}
