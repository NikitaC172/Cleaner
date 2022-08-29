using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    [SerializeField] private int _weight;

    private int _layerDefault = 0;
    private int _layerInHole = 6;

    public event UnityAction<int> Destroyed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Hole>(out Hole hole))
        {
            gameObject.layer = _layerInHole;
        }

        if (other.TryGetComponent<PlaneDestroer>(out PlaneDestroer plane))
        {
            Destroyed?.Invoke(_weight);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Hole>(out Hole hole))
        {
            gameObject.layer = _layerDefault;
        }
    }
}
