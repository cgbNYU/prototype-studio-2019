using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCar : MonoBehaviour
{
    public float CarTime;

    public AudioSource Audio;
    public AudioClip Clip;

    public GameObject Car;

    private float timer;
  
    // Start is called before the first frame update
    void Start()
    {
        timer = CarTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            CarTime += 5;
            timer = CarTime;
            Audio.PlayOneShot(Clip);
            Instantiate(Car, transform.position, transform.rotation);
        }
    }
}
