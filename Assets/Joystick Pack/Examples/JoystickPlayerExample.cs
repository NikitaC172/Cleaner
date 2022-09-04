using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    [SerializeField] private Init _init;
    public float speed;
    public Joystick variableJoystick;
    public Rigidbody rb;

    private Upgrader _upgrader;

    private void Awake()
    {
        _upgrader = _init.GetUpgrader();
    }

    private void OnEnable()
    {
        _upgrader.ChangedSpeed += ChangeSpeed;
    }

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    private void OnDisable()
    {
        _upgrader.ChangedSpeed -= ChangeSpeed;
    }

    private void ChangeSpeed(float deltaSpeed)
    {
        speed += deltaSpeed;
    }
}