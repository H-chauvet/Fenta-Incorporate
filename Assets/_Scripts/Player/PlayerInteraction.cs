using System;
using _Scripts.Interaction;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        public static event Action<ObjectInteraction, Vector3> OnSetIndicator;
        public static event Action<bool> OnIndicatorSetActive;
        
        [SerializeField] private float interactionDistance = 0.5f;
        private void LateUpdate()
        {
            bool hitSomething = false;
            var hits = Physics.OverlapSphere(transform.position, interactionDistance);
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(typeof(IInteractable), out var hitInteractable))
                {
                    var interactable = hitInteractable as ObjectInteraction;
                    if (interactable != null && interactable.ability == Abilities.None) return;
                    hitSomething = true;
                    OnSetIndicator?.Invoke(interactable, hit.transform.position);
                };
                
                OnIndicatorSetActive?.Invoke(hitSomething);
            }
        }
    }
}