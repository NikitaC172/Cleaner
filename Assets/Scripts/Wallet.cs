using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private Init _init;

    private Seller _seller;
    private int _money = 0;

    public int Money => _money;

    private void Awake()
    {
        _seller = _init.GetSeller();
    }

    private void OnEnable()
    {
        _seller.ChangedMoney += TakeMoney;
    }

    private void OnDisable()
    {
        _seller.ChangedMoney -= TakeMoney;
    }

    public void TakeMoney(int money)
    {
        _money += money;
    }
}
