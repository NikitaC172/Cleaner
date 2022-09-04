using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hole : MonoBehaviour
{
    [SerializeField] private Init _init;
    [SerializeField] private Transform _attractionPoint;
    [SerializeField] private Collider _triggerCollider;
    [SerializeField] private GameObject _holeCilinder;
    [SerializeField] private GameObject _lockCilinder;
    [SerializeField] private GameObject _animationPrefab;

    private SliderWeight _sliderWeight;
    private Seller _seller;
    private Upgrader _upgrader;

    public event UnityAction<int> Reseted;

    private void Awake()
    {
        _sliderWeight = _init.GetSliderWeight();
        _seller = _init.GetSeller();
        _upgrader = _init.GetUpgrader();
    }

    private void OnEnable()
    {
        _sliderWeight.Fulled += DeactivateDropInHole;
        _seller.ChangedMoney += ActivateDropInHole;
        _upgrader.ChangedSize += ChangeSize;
    }

    private void OnDisable()
    {
        _sliderWeight.Fulled -= DeactivateDropInHole;
        _seller.ChangedMoney -= ActivateDropInHole;
        _upgrader.ChangedSize -= ChangeSize;
    }

    public Transform GetAttractionPoint()
    {
        return _attractionPoint;
    }

    private void DeactivateDropInHole()
    {
        _triggerCollider.enabled = false;
        _holeCilinder.SetActive(false);
        _lockCilinder.SetActive(true);
        _animationPrefab.SetActive(false);
        _animationPrefab.SetActive(true);
    }

    private void ActivateDropInHole(int money)
    {
        _triggerCollider.enabled = true;
        _lockCilinder.SetActive(false);
        _holeCilinder.SetActive(true);
        Reseted?.Invoke(0);
    }

    private void ChangeSize(float deltaSize)
    {
        Vector3 scale = new Vector3(transform.localScale.x + deltaSize, transform.localScale.y, transform.localScale.z + deltaSize);
        transform.localScale = scale;
    }
}
