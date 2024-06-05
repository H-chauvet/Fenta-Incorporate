using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour, IMonsterAbilities
{
    public float maxDashTime = 1.0f;
    public float dashSpeed = 10.0f;
    public float dashStoppingSpeed = 0.1f;

    private float currentDashTime;
    private bool isDashing = false;
    private Vector3 moveDirection;

    // Vitesse normale du joueur
    public float normalSpeed = 5.0f;
    private float currentSpeed;

    void Start()
    {
        currentDashTime = maxDashTime;
        currentSpeed = normalSpeed;
    }

    void Update()
    {
        if (isDashing)
        {
            currentDashTime += Time.deltaTime;
            if (currentDashTime < maxDashTime)
            {
                transform.Translate(moveDirection * dashSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                isDashing = false;
                currentDashTime = maxDashTime;
                moveDirection = Vector3.zero;
                currentSpeed = normalSpeed;
            }
        }
        else
        {
            transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);
        }
    }

    public void MainAbilityInteraction()
    {
        Dash();
    }

    private void Dash()
    {
        if (isDashing) return;

        isDashing = true;
        currentDashTime = 0.0f;
        moveDirection = transform.forward;
        currentSpeed = dashSpeed; // Mettre à jour la vitesse du dash
    }

    public void SecondaryAbilityInteraction()
    {
        // Implémentez une autre capacité secondaire si nécessaire
    }
}
