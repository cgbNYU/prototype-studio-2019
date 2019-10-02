using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BounceHit : MonoBehaviour
{
    public float BounceDist;
    public float BounceSpeed;

    private Vector3 startRotation;
    private Vector3 startPosition;

    private void Start()
    {
        startRotation = transform.localEulerAngles;
        startPosition = transform.position;
    }

    public void Bounce()
    {
        Vector3 bounceVector = new Vector3(transform.position.x, transform.position.y - BounceDist, transform.position.z);
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOMove(bounceVector, BounceSpeed));
        mySequence.Append(transform.DOMove(startPosition, BounceSpeed));
    }

    public void CarBounce()
    {
        Vector3 bounceVector = new Vector3(BounceDist, startRotation.y, startRotation.z);
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DORotate(bounceVector, BounceSpeed));
        mySequence.Append(transform.DORotate(startRotation, BounceSpeed));
        
    }
}
