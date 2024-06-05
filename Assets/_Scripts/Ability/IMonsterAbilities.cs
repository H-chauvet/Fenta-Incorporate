using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IMonsterAbilities
{
    public void MainAbilityInteraction(Animator animator);
    public void SecondaryAbilityInteraction(Animator animator);
}