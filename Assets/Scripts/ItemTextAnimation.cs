using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTextAnimation : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private GameObject _objectWithTextAnimator;

    private void OnEnable()
    {
        _item.Destroyed += Activate;
    }

    private void OnDisable()
    {
        _item.Destroyed -= Activate;
    }

    private void Activate(int weight)
    {
        _objectWithTextAnimator.SetActive(true);
    }
}
