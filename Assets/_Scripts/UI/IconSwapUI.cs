using System.Collections.Generic;
using UnityEngine;

public class IconSwapUI : MonoBehaviour
{
    [SerializeField] private MonsterType monsterType;
    [SerializeField] private GameObject activeIcon;
    [SerializeField] private GameObject inactiveIcon;
    
    private readonly Dictionary<MonsterType, string> _monsterTags = new()
    {
        {MonsterType.Bloetje, "Character1"},
        {MonsterType.Hijsi, "Character2"},
        {MonsterType.Robotmonster, "Character3"},
        {MonsterType.Elisa, "Character4"}
    };

    private void Awake()
    {
        PlayerSwitchScript.CharacterSwitch += OnCharacterSwitch;
    }

    private void OnCharacterSwitch(int whichMonster, GameObject monster)
    {
        if (monster.CompareTag(_monsterTags[monsterType]))
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }

    public void Activate()
    {
        activeIcon.SetActive(true);
        inactiveIcon.SetActive(false);
    }

    public void Deactivate()
    {
        activeIcon.SetActive(false);
        inactiveIcon.SetActive(true);
    }
}