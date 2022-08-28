using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    [SerializeField] private int _weight;

    private int _layerDefault = 0;
    private int _layerInHole = 6;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Hole>(out Hole hole))
        {
            gameObject.layer = _layerInHole;
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
