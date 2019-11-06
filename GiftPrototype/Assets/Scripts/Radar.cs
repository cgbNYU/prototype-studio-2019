using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{
    public Image Cd1;
    public Image Cd2;
    public Image Cd3;

    public Image R1D1;
    public Image R1D2;
    public Image R1D3;

    public Image R2D1;
    public Image R2D2;
    public Image R2D3;

    public Image L1D1;
    public Image L1D2;
    public Image L1D3;

    public Image L2D1;
    public Image L2D2;
    public Image L2D3;

    public float MaxDist;
    public float LongDist;
    public float ShortDist;

    public GameObject CenterCastOb;
    public GameObject L1CastOb;
    public GameObject L2CastOb;
    public GameObject R1CastOb;
    public GameObject R2CastOb;

    // Start is called before the first frame update
    void Start()
    {
        Cd1.enabled = false;
        Cd2.enabled = false;
        Cd3.enabled = false;
        R1D1.enabled = false;
        R1D2.enabled = false;
        R1D3.enabled = false;
        R2D1.enabled = false;
        R2D2.enabled = false;
        R2D3.enabled = false;
        L1D1.enabled = false;
        L1D2.enabled = false;
        L1D3.enabled = false;
        L2D1.enabled = false;
        L2D2.enabled = false;
        L2D3.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Cast();
    }

    private void Cast()
    {
        //Center
        RaycastHit CenterHit;
        Debug.DrawRay(transform.position, CenterCastOb.transform.position);
        if (Physics.Linecast(transform.position, CenterCastOb.transform.position, out CenterHit))
        {
            if (CenterHit.transform.CompareTag("Alien") || CenterHit.transform.CompareTag("Machine"))
            {
                Vector3 dist = CenterHit.transform.position - transform.position;
                if (dist.magnitude >= LongDist)
                {
                    Cd1.enabled = true;
                    Cd2.enabled = false;
                    Cd3.enabled = false;
                }
                else if (dist.magnitude >= ShortDist)
                {
                    Cd1.enabled = false;
                    Cd2.enabled = true;
                    Cd3.enabled = false;
                }
                else
                {
                    Cd1.enabled = false;
                    Cd2.enabled = false;
                    Cd3.enabled = true;
                }
            }
        }
        else
        {
            Cd1.enabled = false;
            Cd2.enabled = false;
            Cd3.enabled = false;
        }
        
        //L1
        RaycastHit L1Hit;
        if (Physics.Linecast(transform.position, L1CastOb.transform.position, out L1Hit))
        {
            if (L1Hit.transform.CompareTag("Alien") || L1Hit.transform.CompareTag("Machine"))
            {
                Vector3 dist = L1Hit.transform.position - transform.position;
                if (dist.magnitude >= LongDist)
                {
                    L1D1.enabled = true;
                    L1D2.enabled = false;
                    L1D3.enabled = false;
                }
                else if (dist.magnitude >= ShortDist)
                {
                    L1D1.enabled = false;
                    L1D2.enabled = true;
                    L1D3.enabled = false;
                }
                else
                {
                    L1D1.enabled = false;
                    L1D2.enabled = false;
                    L1D3.enabled = true;
                }
            }
        }
        else
        {
            L1D1.enabled = false;
            L1D2.enabled = false;
            L1D3.enabled = false;
        }
        //L2
        RaycastHit L2Hit;
        if (Physics.Linecast(transform.position, L2CastOb.transform.position, out L2Hit))
        {
            if (L2Hit.transform.CompareTag("Alien") || L2Hit.transform.CompareTag("Machine"))
            {
                Vector3 dist = L2Hit.transform.position - transform.position;
                if (dist.magnitude >= LongDist)
                {
                    L2D1.enabled = true;
                    L2D2.enabled = false;
                    L2D3.enabled = false;
                }
                else if (dist.magnitude >= ShortDist)
                {
                    L2D1.enabled = false;
                    L2D2.enabled = true;
                    L2D3.enabled = false;
                }
                else
                {
                    L2D1.enabled = false;
                    L2D2.enabled = false;
                    L2D3.enabled = true;
                }
            }
        }
        else
        {
            L2D1.enabled = false;
            L2D2.enabled = false;
            L2D3.enabled = false;
        }
        
        //R1
        RaycastHit R1Hit;
        if (Physics.Linecast(transform.position, R1CastOb.transform.position, out R1Hit))
        {
            if (R1Hit.transform.CompareTag("Alien") || R1Hit.transform.CompareTag("Machine"))
            {
                Vector3 dist = R1Hit.transform.position - transform.position;
                if (dist.magnitude >= LongDist)
                {
                    R1D1.enabled = true;
                    R1D2.enabled = false;
                    R1D3.enabled = false;
                }
                else if (dist.magnitude >= ShortDist)
                {
                    R1D1.enabled = false;
                    R1D2.enabled = true;
                    R1D3.enabled = false;
                }
                else
                {
                    R1D1.enabled = false;
                    R1D2.enabled = false;
                    R1D3.enabled = true;
                }
            }
        }
        else
        {
            R1D1.enabled = false;
            R1D2.enabled = false;
            R1D3.enabled = false;
        }
        
        //R2
        RaycastHit R2Hit;
        if (Physics.Linecast(transform.position, R2CastOb.transform.position, out R2Hit))
        {
            if (R2Hit.transform.CompareTag("Alien") || R2Hit.transform.CompareTag("Machine"))
            {
                Vector3 dist = R2Hit.transform.position - transform.position;
                if (dist.magnitude >= LongDist)
                {
                    R2D1.enabled = true;
                    R2D2.enabled = false;
                    R2D3.enabled = false;
                }
                else if (dist.magnitude >= ShortDist)
                {
                    R2D1.enabled = false;
                    R2D2.enabled = true;
                    R2D3.enabled = false;
                }
                else
                {
                    R2D1.enabled = false;
                    R2D2.enabled = false;
                    R2D3.enabled = true;
                }
            }
        }
        else
        {
            R2D1.enabled = false;
            R2D2.enabled = false;
            R2D3.enabled = false;
        }
    }
}
