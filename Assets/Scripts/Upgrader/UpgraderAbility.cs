using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgraderAbility : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private Init _init;  //
    [SerializeField] private List<int> _prices = new List<int>();
    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private float _stepUpgrade = 0.05f;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _textPrice;
    [SerializeField] private TMP_Text _textPercent;
    [SerializeField] private Button _buttonBuy;

    public event UnityAction<int, float> TryedBuy;

    private void OnEnable()
    {
        _buttonBuy.onClick.AddListener(TryBuy);
    }

    private void OnDisable()
    {
        _buttonBuy.onClick.RemoveListener(TryBuy);
    }

    public void Buy()
    {
        int percent = 100;
        _currentLevel++;
        _slider.value += _stepUpgrade;
        _textPrice.text = $"{_prices[_currentLevel - 1]}";
        float percentText = _slider.value * percent;
        _textPercent.text = $"%{percentText}";
    }

    private void TryBuy()
    {
        TryedBuy?.Invoke(_prices[_currentLevel - 1], _stepUpgrade);
    }
}
