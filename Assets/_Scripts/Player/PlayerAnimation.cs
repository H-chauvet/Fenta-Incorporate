using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private static readonly int Jumping = Animator.StringToHash("Jumping");
    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Falling = Animator.StringToHash("Falling");

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        PlayerSwitchScript.CharacterSwitch += OnCharacterSwitch;
    }

    private void OnCharacterSwitch(int whichCharacter, GameObject character)
    {
        _animator = character.GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.Log(_animator);
        _animator.SetBool(Jumping, _playerMovement.isJumping);
        _animator.SetBool(Walking, _playerMovement.isWalking);
        _animator.SetBool(Falling, _playerMovement.isFalling);
    }
}
