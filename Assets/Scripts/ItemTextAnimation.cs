using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemTextAnimation : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private GameObject _objectWithTextAnimator;
    [SerializeField] private TMP_Text _text;

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
        _text.text = $"+{weight.ToString()}";
        _objectWithTextAnimator.SetActive(true);
    }
}
