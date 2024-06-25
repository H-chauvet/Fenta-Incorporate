using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Interaction
{
    [CreateAssetMenu(menuName = "New Ability")]
    public class AbilitySO : ScriptableObject
    {
        public MonsterType monsterType;
        public Abilities ability;
        public AbilityType abilityType;
        public Sprite abilityIcon;
        public Sprite monsterIcon;
    }

    public enum AbilityType
    {
        Primary,
        Secondary
    }
}