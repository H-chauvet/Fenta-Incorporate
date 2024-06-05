using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour, IMonsterAbilities
{
    public float dashTime = 1.0f;
    public float dashSpeed = 10.0f;
    public float dashCooldown = 2.0f;

    private float currentDashTime;
    private float currentSpeed = 1.0f;

    private bool isDashing = false;
    private Vector3 moveDirection;

    private GameObject parent;

    private float currentCooldownTime;
    private bool canDash = true;

    void Start()
    {
        parent = transform.parent.gameObject;
        currentDashTime = dashTime;
    }

    void Update()
    {
        if (isDashing)
        {
            currentDashTime += Time.deltaTime;
            if (currentDashTime < dashTime)
            {
                parent.transform.Translate(moveDirection * dashSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                isDashing = false;
                currentDashTime = dashTime;
                moveDirection = Vector3.zero;
                canDash = false;
                currentCooldownTime = 0.0f;
            }
        }
        else
        {
            parent.transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);

            if (!canDash)
            {
                currentCooldownTime += Time.deltaTime;
                if (currentCooldownTime >= dashCooldown)
                {
                    canDash = true;
                }
            }
        }
    }

    public void MainAbilityInteraction()
    {
        if (canDash)
        {
            Dash();
        }
    }

    private void Dash()
    {
        if (isDashing) return;

        isDashing = true;
        currentDashTime = 0.0f;
        moveDirection = parent.transform.forward;
        currentSpeed = dashSpeed;
    }

    public void SecondaryAbilityInteraction()
    {
        // Implémentez une autre capacité secondaire si nécessaire
    }
}
