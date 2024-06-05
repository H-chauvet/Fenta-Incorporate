using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HijsiAbilities : MonoBehaviour, IMonsterAbilities
{
    public float dashTime = 1.0f;
    public float dashSpeed = 10.0f;
    public float dashCooldown = 2.0f;

    private float currentDashTime;
    private bool isDashing = false;
    private Vector3 moveDirection;

    private GameObject parent;

    private float currentCooldownTime;
    private bool canDash = true;

    void Start()
    {
        parent = transform.parent.gameObject;
    }

    void Update()
    {
        if (isDashing)
        {
            HandleDash();
        }
        else
        {
            HandleCooldown();
        }
    }

    public void MainAbilityInteraction()
    {
        Dash();
    }

    public void SecondaryAbilityInteraction()
    {
        // Special dash
    }

    private void Dash()
    {
        if (!canDash || isDashing) return;

        isDashing = true;
        currentDashTime = 0.0f;
        moveDirection = parent.transform.forward;
    }

    private void HandleDash()
    {
        currentDashTime += Time.deltaTime;
        if (currentDashTime < dashTime)
        {
            parent.transform.Translate(moveDirection * dashSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            isDashing = false;
            moveDirection = Vector3.zero;
            canDash = false;
            currentCooldownTime = 0.0f;
        }
    }

    private void HandleCooldown()
    {
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
