using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Light Flash;

    private bool _isShot;

    private float timer;

    private AudioSource source;

    public AudioClip shotSound;
    // Start is called before the first frame update
    void Start()
    {
        _isShot = false;
        Flash.enabled = false;
        timer = .15f;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isShot)
        {
            EventManager.Instance.Fire(new Events.GunShot());
            source.PlayOneShot(shotSound);
            _isShot = true;
            Flash.enabled = true;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 15))
            {
                if (hit.transform.CompareTag("Alien"))
                {
                    Debug.Log("Kill Alien");
                    Destroy(hit.transform.gameObject);
                }
            }
        }

        if (_isShot)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = .15f;
                Flash.enabled = false;
                _isShot = false;
            }
        }
    }
}
