using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HijsiAbilities : MonoBehaviour, IMonsterAbilities
{
    public float dashTime = 1.0f;
    public float dashSpeed = 10.0f;
    public float dashCooldown = 2.0f;
    public float angleDirectionOffset = 30f;

    public GameObject tornadoObject;

    private float currentDashTime;
    private bool isDashing = false;
    private Vector3 moveDirection;

    private GameObject parent;

    private float currentCooldownTime;
    private bool canDash = true;

    private Rigidbody parentRb;



    [HideInInspector]
    public bool isDashedControlled;
    private bool isSpecialDashing = false;
    private Vector3 initialPlayerPos;
    private Vector3 targetPlayerPos;
    private Vector3 targetDirection;

    private float TornadoDuration = 1f;
    private float TornadoCooldown = 3f;
    private bool TornadoReady = true;

    void Start()
    {
        parent = transform.parent.gameObject;
        parentRb = parent.GetComponent<Rigidbody>();
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
        if (isSpecialDashing)
        {
            HandleSpecialDash();
        }

        if (TornadoReady == false)
        {
            TornadoDuration -= Time.deltaTime;
            if (TornadoDuration <= 0)
            {
                TornadoReady = true;
                TornadoDuration = TornadoCooldown;
            }
        }
    }

    public void MainAbilityInteraction(Animator animator)
    {
        if (!isDashedControlled) 
        {
            Dash();
        }
    }

    public void SecondaryAbilityInteraction(Animator animator)
    {
        Tornado();
    }

    private void HandleSpecialDash()
    {
        Vector3 currentPlayerPos = parent.transform.position;
        float dotProduct = Vector3.Dot(targetDirection, targetPlayerPos - currentPlayerPos);


        if (dotProduct >= 0)
        {
             parent.transform.Translate(targetDirection * dashSpeed * Time.deltaTime, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            parent.transform.rotation = targetRotation;

        }
        else
        {
             isSpecialDashing = false;
            moveDirection = Vector3.zero;
            parentRb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        }
    }

    public void SpecialDash(GameObject other)
    {

        if (isSpecialDashing) return;

        initialPlayerPos = parent.transform.position;
        targetPlayerPos = other.transform.position;
        Vector3 playerForward = parent.transform.forward;
        targetDirection = (targetPlayerPos - initialPlayerPos).normalized;

        float dotProduct = Vector3.Dot(playerForward, targetDirection);
        float angleThresholdRadians = angleDirectionOffset * Mathf.Deg2Rad;
        float angleBetween = Mathf.Acos(dotProduct);

        if (angleBetween <= angleThresholdRadians)
        {
            if (parentRb != null)
            {
                parentRb.constraints |= RigidbodyConstraints.FreezePositionY;
            }
            
            isSpecialDashing = true;
        }
         else
        {
            Dash();
        }
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
    
            if (parentRb != null)
            {
                parentRb.constraints |= RigidbodyConstraints.FreezePositionY;
            }
            parent.transform.Translate(moveDirection * dashSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            isDashing = false;
            moveDirection = Vector3.zero;
            canDash = false;
            currentCooldownTime = 0.0f;
            parentRb.constraints &= ~RigidbodyConstraints.FreezePositionY;

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

    private void Tornado()
    {
        if (TornadoReady == false)
        {
            return;
        }
        TornadoReady = false;
        Vector3 forwardOffset = parent.transform.forward;
        GameObject tornadoTemp = Instantiate(tornadoObject, parent.transform.position + forwardOffset, Quaternion.identity);
    }
}