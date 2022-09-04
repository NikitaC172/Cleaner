using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    [SerializeField] private ItemHendler _itemHendler;
    [SerializeField] private SliderWeight _sliderWeight;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Hole _hole;
    [SerializeField] private Seller _seller;
    [SerializeField] private Panel _panel;
    [SerializeField] private Upgrader _upgrader;

    public ItemHendler GetItemHandler()
    {
        return _itemHendler;
    }

    public SliderWeight GetSliderWeight()
    {
        return _sliderWeight;
    }

    public Wallet GetWallet()
    {
        return _wallet;
    }

    public Hole GetHole()
    {
        return _hole;
    }

    public Seller GetSeller()
    {
        return _seller;
    }

    public Panel GetPanel()
    {
        return _panel;
    }

    public Upgrader GetUpgrader()
    {
        return _upgrader;
    }
}
