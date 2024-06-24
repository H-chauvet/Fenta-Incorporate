using UnityEngine;

namespace _Scripts.Interaction
{
    [CreateAssetMenu(menuName = "New Ability")]
    public class AbilitySO : ScriptableObject
    {
        public Abilities ability;
        public AbilityType type;
        public Sprite icon;
    }

    public enum AbilityType
    {
        Primary,
        Secondary
    }
}