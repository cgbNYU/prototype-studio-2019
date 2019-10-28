using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{

    public GameObject[] Obstacles;

    private float _timer;

    public float SpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        _timer = SpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            Spawn();
            _timer = SpawnTime;
        }
    }

    private void Spawn()
    {
        int random = Random.Range(0, Obstacles.Length - 1);
        Instantiate(Obstacles[random], transform.position, transform.rotation);
    }
}
