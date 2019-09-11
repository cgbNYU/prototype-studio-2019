using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Events;

public class NPCScript : MonoBehaviour
{
    
    //Public
    public int HP;
    public GameObject SkullIcon;
    
    //Private
    private bool mateable;
    
    //Enum
    public enum NPCShape
    {
        Square,
        Triangle,
        Circle,
        Dead
    }

    public NPCShape Shape;

    public enum NPCColor
    {
        Red,
        Yellow,
        Blue
    }

    public NPCColor Color;
    
    // Start is called before the first frame update
    void Start()
    {
        mateable = false;
        EventManager.Instance.AddHandler<PlayerChange>(OnPlayerChange);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveHandler<PlayerChange>(OnPlayerChange);
    }

    private void OnPlayerChange(PlayerChange evt)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (Shape)
        {
            case NPCShape.Square:
                break;
            case NPCShape.Circle:
                break;
            case NPCShape.Triangle:
                break;
            case NPCShape.Dead:
                break;
        } 
    }

    public void Hit()
    {
        HP--;
        if (HP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        SkullIcon.SetActive(true);
        Shape = NPCShape.Dead;
    }
}
