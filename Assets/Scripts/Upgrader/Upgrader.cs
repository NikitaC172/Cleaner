using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Upgrader : MonoBehaviour
{
    [SerializeField] private Init _init;
    [SerializeField] private UpgraderAbility _upgradeSize;
    [SerializeField] private UpgraderAbility _upgradeSpeed;
    [SerializeField] private UpgraderAbility _upgradeCapacity;
    [SerializeField] private UpgraderAbility _upgradeIncome;

    private Wallet _wallet;

    public event UnityAction<float> ChangedSize;
    public event UnityAction<float> ChangedSpeed;

    private void Awake()
    {
        _wallet = _init.GetWallet();
    }

    private void OnEnable()
    {
        _upgradeSize.TryedBuy += ChangeSize;
        _upgradeSpeed.TryedBuy += ChangeSpeed;
    }

    private void OnDisable()
    {
        _upgradeSize.TryedBuy -= ChangeSize;
        _upgradeSpeed.TryedBuy -= ChangeSpeed;
    }

    private bool TryBuy(int price)
    {
        return price < _wallet.Money;
    }

    private void ChangeSize(int price, float deltaSize)
    {
        if (TryBuy(price))
        {
            ChangedSize?.Invoke(deltaSize);
            _upgradeSize.Buy();
            Debug.LogWarning("OkSize");
        }
    }

    private void ChangeSpeed(int price, float deltaSpeed)
    {
        if (TryBuy(price))
        {
            ChangedSpeed?.Invoke(deltaSpeed);
            _upgradeSpeed.Buy();
            Debug.LogWarning("OkSpeed");
        }
    }
}
