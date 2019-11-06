using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Alien : MonoBehaviour
{

    private GameObject _player;

    private bool _isRunning;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("FPSController");
        EventManager.Instance.AddHandler<Events.GunShot>(OnGunShot);
        _isRunning = false;
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveHandler<Events.GunShot>(OnGunShot);
    }

    public void OnGunShot(Events.GunShot evt)
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, _player.transform.position, out hit))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Vector3 dist = _player.transform.position - transform.position;
                transform.position += dist.normalized * 1 * Time.deltaTime;
                if (dist.magnitude <= 15)
                {
                    _isRunning = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, _player.transform.position, out hit))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Vector3 dist = _player.transform.position - transform.position;
                if (!_isRunning)
                {
                    transform.position += dist.normalized * 1 * Time.deltaTime;
                }
                else
                {
                    transform.position += dist.normalized * 8 * Time.deltaTime;
                }
                
                if (dist.magnitude <= 1)
                {
                    Debug.Log("KillPlayer");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }
}
