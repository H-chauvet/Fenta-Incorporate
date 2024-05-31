using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private static readonly int Jumping = Animator.StringToHash("Jumping");
    private static readonly int Walking = Animator.StringToHash("Walking");

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        GetComponent<PlayerSwitchScript>().CharacterSwitch += OnCharacterSwitch;
    }

    private void OnCharacterSwitch(GameObject character)
    {
        _animator = character.GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool(Jumping, _playerMovement.isJumping);
        _animator.SetBool(Walking, _playerMovement.isWalking);
    }
}
