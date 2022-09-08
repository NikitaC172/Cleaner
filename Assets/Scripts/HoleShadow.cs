using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleShadow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _offsetDelay = 0.15f;

    private Vector3 _positionObject;
    private Vector3 _distanceSmoothing;

    private void FixedUpdate()
    {
        _positionObject = _target.transform.position;
        _distanceSmoothing = Vector3.Lerp(transform.position, _positionObject, _offsetDelay);
        transform.position = _distanceSmoothing;
    }
}
