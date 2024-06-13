using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElisaAbilities : MonoBehaviour, IMonsterAbilities
{
    public float growPlantsCooldown = 5f;
    public float icePowerCooldown = 5f;

    private float growPlantsDuration = 5f;
    private float icePowerDuration = 5f;

    private bool growPlantsReady = true;
    private bool icePowerReady = true;

    public List<GameObject> plantPrefabs;
    public float plantGrowDuration = 1f;
    public float plantGrowDelay = 0.5f;
    public float plantGrowDistance = 1f;


    // Update is called once per frame
    void Update()
    {
        if (growPlantsReady == false)
        {
            growPlantsDuration -= Time.deltaTime;
            if (growPlantsDuration <= 0)
            {
                growPlantsReady = true;
                growPlantsDuration = growPlantsCooldown;
            }
        }
        if (icePowerReady == false)
        {
            icePowerDuration -= Time.deltaTime;
            if (icePowerDuration <= 0)
            {
                icePowerReady = true;
                icePowerDuration = icePowerCooldown;
            }
        }
    }

    public void MainAbilityInteraction(Animator animator)
    {

    }

    public void SecondaryAbilityInteraction(Animator animator)
    {
        
    }

}
