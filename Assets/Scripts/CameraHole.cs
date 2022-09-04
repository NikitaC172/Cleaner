using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHole : MonoBehaviour
{
    [SerializeField] private Init _init;
    [SerializeField] private Camera _camera;

    private Upgrader _upgrader;

    private void Awake()
    {
        _upgrader = _init.GetUpgrader();
    }

    private void OnEnable()
    {
        _upgrader.ChangedSize += ChangeFieldView;
    }

    private void OnDisable()
    {
        _upgrader.ChangedSize -= ChangeFieldView;
    }

    private void ChangeFieldView(float deltaView)
    {
        _camera.fieldOfView += deltaView;
    }
}
