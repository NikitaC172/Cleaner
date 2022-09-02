using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] private Transform _attractionPoint;

    public Transform GetAttractionPoint()
    {
        return _attractionPoint;
    }
}
