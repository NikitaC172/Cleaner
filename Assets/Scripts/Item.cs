using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    [SerializeField] private int _weight;
    //[SerializeField] private Collider _colliderTriger;
    [SerializeField] private Collider _collider;//_colliderForRigigdbody
    [SerializeField] private float _timeForMovePoint = 0.5f;

    //private Collider _collider;
    private Rigidbody _rigidbody;
    private int _layerDefault = 0;
    private int _layerInHole = 6;
    private float delay = 1.5f;
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
            /*gameObject.layer = _layerInHole;
            //_collider.isTrigger = false;
            _collider.enabled = true;
            _rigidbody.useGravity = true;*/

            _isMove = true;
            gameObject.layer = _layerInHole;
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
            _collider.isTrigger = false;
            Vector3 _position = hole.GetAttractionPoint().position - transform.position;
            _rigidbody.AddForce((_position) *2f, ForceMode.VelocityChange);
            //_rigidbody.rotation =  new Quaternion(_position.x,_position.y,_position.z, 0f);
            _rigidbody.AddTorque(_position, ForceMode.Impulse);
            Invoke(nameof(ResetLayer), delay);
            //_rigidbody.rotation =  new Quaternion(0f,1f,0f, 0f);
            //StartCoroutine(MoveToPoint(hole.GetAttractionPoint()));
        }

        if (other.TryGetComponent<PlaneDestroer>(out PlaneDestroer plane))
        {
            Destroyed?.Invoke(_weight);
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Hole>(out Hole hole))
        {
            //gameObject.layer = _layerDefault;
            //_rigidbody.useGravity = false;
            //_collider.isTrigger = true;
        }
    }*/

    private void ResetLayer()
    {
        gameObject.layer = _layerDefault;
        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
        _collider.isTrigger = true;
        _isMove = false;
    }

    private IEnumerator MoveToPoint(Transform _point)
    {
        float time = 0;
        gameObject.layer = _layerInHole;
        _rigidbody.useGravity = true;
        _collider.isTrigger = false;

        //while (time < _timeForMovePoint-0.2f)
        while (transform.position != _point.position)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, _point.position, time / _timeForMovePoint);
            yield return null;
        }

        
    }
}
