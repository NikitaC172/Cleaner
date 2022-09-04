using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationBagUI : MonoBehaviour
{
    [SerializeField] private Init _init;

    private SliderWeight _sliderWeight;
    private Animator _animator;

    private const string PulseAnim = "Pulse";
    private const string IdleAnim = "Idle";

    private void Awake()
    {
        _sliderWeight = _init.GetSliderWeight();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _sliderWeight.Fulled += ActivatePulseAnimation;
        _sliderWeight.Emted += ActivateIdleAnimation;
    }

    private void ActivatePulseAnimation()
    {
        _animator.Play(PulseAnim);
    }

    private void ActivateIdleAnimation()
    {
        _animator.Play(IdleAnim);
    }
}
