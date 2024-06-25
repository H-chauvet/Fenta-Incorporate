using System;
using System.Collections.Generic;
using _Scripts.Interaction;
using UnityEngine;
using UnityEngine.UI;

public class CurrentMonsterUI : MonoBehaviour
{
    public static Action<MonsterType> onCharacterSwitch;
    
    [SerializeField] private Image monsterImage;
    [SerializeField] private Image abilityImage;
    [SerializeField] private List<AbilitySO> monsterData;

    private readonly Dictionary<MonsterType, AbilitySO> MonsterLookup = new();

    private void OnEnable()
    {
        onCharacterSwitch += UpdateUI;
    }

    private void Start()
    {
        foreach (var monster in monsterData)
        {
            MonsterLookup.Add(monster.monsterType, monster);
        }
    }

    private void UpdateUI(MonsterType monsterType)
    {
        monsterImage.sprite = MonsterLookup[monsterType].monsterIcon;
        abilityImage.sprite = MonsterLookup[monsterType].abilityIcon;
    }
}
