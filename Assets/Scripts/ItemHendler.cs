using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemHendler : MonoBehaviour
{
    [SerializeField] private List<Item> _items = new List<Item>();

    public event UnityAction<int> ChangedItems;
    public event UnityAction<int> ChangedMaxItems;

    private void OnEnable()
    {
        float delayCall = 2;

        foreach (var item in _items)
        {
            item.Destroyed += ChangeItem;
        }

        Invoke(nameof(CallEvent), delayCall);
        //ChangedMaxItems?.Invoke(_items.Count);
    }

    private void OnDisable()
    {
        foreach (var item in _items)
        {
            item.Destroyed -= ChangeItem;
        }        
    }

    private void ChangeItem(int weight)
    {
        ChangedItems?.Invoke(weight);
    }

    private void CallEvent()
    {
        ChangedMaxItems?.Invoke(_items.Count);
    }
}
