using System;
using System.Collections.Generic;
using _Scripts.Interaction;
using _Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private List<AbilitySO> abilities = new();
    
    [SerializeField] private GameObject interactionIndicator;
    [SerializeField] private Image abilityImage;
    [SerializeField] private TMP_Text button;

    private readonly Dictionary<Abilities, AbilitySO> _abilityLookup = new();

    private Camera _camera;

    private void Awake()
    {
        PlayerInteraction.OnIndicatorSetActive += IndicatorSetActive;
        PlayerInteraction.OnSetIndicator += SetIndicator;
        _camera = Camera.main;
    }

    private void Start()
    {
        foreach (var ability in abilities)
        {
            _abilityLookup.Add(ability.ability, ability);
        }
    }

    private void SetIndicator(ObjectInteraction interactable, Vector3 position)
    {
        interactionIndicator.transform.position = _camera.WorldToScreenPoint(position);
        var info = _abilityLookup[interactable.ability];
        abilityImage.sprite = info.icon;
        button.text = info.type switch
        {
            AbilityType.Primary => "B",
            AbilityType.Secondary => "Y",
            _ => button.text
        };
    }

    private void IndicatorSetActive(bool state)
    {
        interactionIndicator.SetActive(state);
    }
}
