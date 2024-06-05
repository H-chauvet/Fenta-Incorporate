using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [HideInInspector] public Animator _animator;
    private PlayerMovement playerMovement;
    private static readonly int Jumping = Animator.StringToHash("Jumping");
    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Falling = Animator.StringToHash("Falling");

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        PlayerSwitchScript.CharacterSwitch += OnCharacterSwitch;
    }

    private void OnCharacterSwitch(int whichCharacter, GameObject character)
    {
        _animator = character.GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool(Jumping, playerMovement.isJumping);
        _animator.SetBool(Walking, playerMovement.isWalking);
        _animator.SetBool(Falling, playerMovement.isFalling);
    }
}
