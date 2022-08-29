using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisabler : MonoBehaviour
{
    [SerializeField] private Item _item;

    private void OnEnable()
    {
        _item.Destroyed += Disable;
    }

    private void OnDisable()
    {
        _item.Destroyed -= Disable;
    }

    private void Disable(int weight)
    {
        gameObject.SetActive(false);
    }
}
