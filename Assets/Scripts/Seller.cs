using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Seller : MonoBehaviour
{
    [SerializeField] private Init _init;
    [SerializeField] private GameObject _panelSeller;

    private int priceItem = 30;
    private Panel _panel;

    public event UnityAction<int> ChangedMoney;

    private void Awake()
    {
        _panel = _init.GetPanel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Wallet>(out Wallet wallet))
        {
            _panel.OpenPanel(_panelSeller);
        }
    }

    public void IncreaseMoney(int multiplicator)
    {
        int income = priceItem * multiplicator;
        ChangedMoney?.Invoke(income);
        _panel.ClosePanel(_panelSeller);        
    }
}
