using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloetjeAbilities : MonoBehaviour, IMonsterAbilities
{

    public float bloodAirPowerCooldown = 1f;
    public float tailSmashPowerCooldown = 1f;

    public List<GameObject> bloodAirProjectile;
    public Vector3 bloodAirProjectilePositionOffset = new Vector3(0, 0, 0);
    public float bloodAirProjectileSpeed = 1f;

    // Remaining time for the power to be ready
    private float bloodAirPowerDuration = 1f;
    private float tailSmashDuration = 1f;

    private bool bloodAirPowerReady = true;
    private bool tailSmashPowerReady = true;

    private GameObject parent;


    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (bloodAirPowerReady == false)
        {
            bloodAirPowerDuration -= Time.deltaTime;
            if (bloodAirPowerDuration <= 0)
            {
                bloodAirPowerReady = true;
                bloodAirPowerDuration = bloodAirPowerCooldown;
            }
        }
        if (tailSmashPowerReady == false)
        {
            tailSmashDuration -= Time.deltaTime;
            if (tailSmashDuration <= 0)
            {
                tailSmashPowerReady = true;
                tailSmashDuration = tailSmashPowerCooldown;
            }
        }
    }
    
    public void MainAbilityInteraction(Animator animator)
    {
        BloodAirPower(animator);
    }

    public void SecondaryAbilityInteraction(Animator animator)
    {
        TailSmash(animator);
    }

    public void BloodAirPower(Animator anim)
    {
        if (bloodAirPowerReady == false)
        {
            return;
        }
        bloodAirPowerReady = false;
        anim.Play("Primary");
        
        int randomProjectile = Random.Range(0, bloodAirProjectile.Count);
        Vector3 localOffset = parent.transform.TransformDirection(bloodAirProjectilePositionOffset);

        Rigidbody parentRb = parent.GetComponent<Rigidbody>();
        Vector3 parentVelocity = parentRb != null ? parentRb.velocity : Vector3.zero;

        GameObject bloodAirProjectileInstance = Instantiate(bloodAirProjectile[randomProjectile], parent.transform.position + localOffset, Quaternion.identity);
        Rigidbody rb = bloodAirProjectileInstance.GetComponent<Rigidbody>();
        if (rb == null)
        {
            return;
        }
        Vector3 direction = parent.transform.forward;
        rb.AddForce(direction * bloodAirProjectileSpeed * bloodAirProjectileInstance.transform.localScale.y * 500 + parentVelocity, ForceMode.Impulse);
    }

    public void TailSmash(Animator anim)
    {
        if (tailSmashPowerReady == false)
        {
            return;
        }
        tailSmashPowerReady = false;
        anim.Play("Secondary");
    }

    
}
