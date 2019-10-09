using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilGod : MonoBehaviour
{
    //Public
    public GameObject MouseFollow;
    public Lightning LightningScript;
    public float LightningForce;
    
    //Private
    private Camera _cam;
    
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousPos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousPos.z = 0;
        MouseFollow.transform.position = mousPos;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D ray = Physics2D.Raycast(MouseFollow.transform.position, Vector2.down, 100000f);
            if (ray.collider.CompareTag("Brick"))
            {
                Vector2 impulseDir = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
                LightningScript.ZapTarget(ray.collider.gameObject);
                ray.collider.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(LightningForce * Vector2.down, ForceMode2D.Impulse);
            }
        }
    }
}
