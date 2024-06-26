using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class JackLevelCompletion : MonoBehaviour
{
    public List<ChestHandsSequence> hands = new();
    public UnityEvent onComplete;

    public void RemoveHand(ChestHandsSequence hand)
    {
        hands.Remove(hand);

        if (hands.Count <= 0)
        {
            onComplete.Invoke();
        }
    }
}
