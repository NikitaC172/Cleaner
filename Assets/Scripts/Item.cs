using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    [SerializeField] private int _weight;
    [SerializeField] private Collider _collider;
    [SerializeField] private float _timeForMovePoint = 0.5f;
    [SerializeField] private float _delay = 1.5f;
    [SerializeField] private float _forceImpulse = 2.0f;

    private Rigidbody _rigidbody;
    private int _layerDefault = 0;
    private int _layerInHole = 6;
    private bool _isMove = false;

    public event UnityAction<int> Destroyed;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Hole>(out Hole hole) && _isMove == false)
        {
            _isMove = true;
            gameObject.layer = _layerInHole;
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
            _collider.isTrigger = false;
            Vector3 _position = hole.GetAttractionPoint().position - transform.position;
            _rigidbody.AddForce((_position) * _forceImpulse, ForceMode.VelocityChange);
            _rigidbody.AddTorque(_position*10.3f, ForceMode.Impulse);            
            Invoke(nameof(ResetLayer), _delay);
        }

        if (other.TryGetComponent<PlaneDestroer>(out PlaneDestroer plane))
        {
            Destroyed?.Invoke(_weight);
        }
    }

    private void ResetLayer()
    {
        gameObject.layer = _layerDefault;
        _isMove = false;
    }
}
