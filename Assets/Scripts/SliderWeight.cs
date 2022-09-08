using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderWeight : MonoBehaviour
{
    [SerializeField] private Init _init;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _panelWithNumbers;
    [SerializeField] private GameObject _panelMax;
    [SerializeField] private GameObject _panelGotoSell;

    private Slider _slider;
    private Panel _panel;
    private ItemHendler _itemHendler;
    private Hole _hole;
    private int _currentWeight = 1;
    private int _maxWeight = 30;
    private bool _isFull = false;

    public event UnityAction Fulled;
    public event UnityAction Emted;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _itemHendler = _init.GetItemHandler();
        _hole = _init.GetHole();
        _panel = _init.GetPanel();
    }

    private void OnEnable()
    {
        _itemHendler.ChangedItems += ChangeVolumeSlider;
        _hole.Reseted += ResetWeight;
    }

    private void OnDisable()
    {
        _itemHendler.ChangedItems -= ChangeVolumeSlider;
        _hole.Reseted -= ResetWeight;
    }

    private void ResetWeight(int currentWeight)
    {
        _currentWeight = currentWeight;
        _panel.ClosePanel(_panelMax);
        _panel.ClosePanel(_panelGotoSell);
        _panel.OpenPanel(_panelWithNumbers);
        ChangeVolumeSlider(0);
        Emted?.Invoke();
        _isFull = false;
    }

    private void ChangeVolumeSlider(int weight)
    {
        _currentWeight += weight;
        _slider.maxValue = _maxWeight;
        float value = (float)_maxWeight - _currentWeight;
        _slider.value = value;
        _text.text = $"{_currentWeight}";

        if (value <= 0 && _isFull == false)
        {
            Fulled?.Invoke();
            _isFull = true;
            _panel.ClosePanel(_panelWithNumbers);
            _panel.OpenPanel(_panelMax);
            _panel.OpenPanel(_panelGotoSell);
        }
    }
}
