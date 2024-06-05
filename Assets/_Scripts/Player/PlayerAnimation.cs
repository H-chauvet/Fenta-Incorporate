using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private InputAction _move;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _move = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        var moveVector = _move.ReadValue<Vector2>();
        _animator.SetBool("isWalking", moveVector.magnitude > 0);
    }
}
