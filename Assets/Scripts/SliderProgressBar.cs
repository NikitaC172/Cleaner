using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderProgressBar : MonoBehaviour
{
    [SerializeField] private Init _init;
    [SerializeField] private TMP_Text _text;

    private Slider _slider;
    private ItemHendler _itemHendler;
    private int _currentItemCollected = 0;
    private int _maxItem;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _itemHendler = _init.GetItemHandler();
    }

    private void OnEnable()
    {
        _itemHendler.ChangedMaxItems += SetMaxItem;
        _itemHendler.ChangedItems += ChangeVolumeSlider;
    }

    private void OnDisable()
    {
        _itemHendler.ChangedMaxItems -= SetMaxItem;
        _itemHendler.ChangedItems -= ChangeVolumeSlider;
    }

    private void SetMaxItem(int maxItem)
    {
        Debug.LogWarning(maxItem);
        _maxItem = maxItem;
    }

    private void ChangeVolumeSlider(int weight)
    {
        _currentItemCollected++;
        float value = (float)_currentItemCollected / _maxItem;
        float valuePercent = Mathf.RoundToInt(((float)_currentItemCollected / _maxItem)*100);
        _slider.value = value;
        _text.text = $"%{valuePercent}";
    }
}
