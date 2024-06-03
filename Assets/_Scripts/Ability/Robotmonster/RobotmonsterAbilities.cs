using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotmonsterAbilities : MonoBehaviour, IMonsterAbilities
{

    public float lightningPowerCooldown = 5f;
    public float waterPowerCooldown = 0.01f;

    // List of all the water projectiles that can be spawned randomly
    public List<GameObject> waterProjectile;

    // Offset of the position of the water projectiles relative to the parent object orientation and position
    public Vector3 waterProjectilePositionOffset = new Vector3(0, 0, 0);

    // Speed of the water projectiles
    public float waterProjectileSpeed = 1f;

    // Percentage to add upwards direction to the water projectiles
    public float waterProjectileUpwardPercentage = 0.2f;

    // Random left/right, up/down offset for the water projectiles
    public float waterProjectileRandomDirectionOffset = 0.1f;

    // Range of amount of water projectiles to spawn
    public int waterProjectileMinAmount = 1;
    public int waterProjectileMaxAmount = 5;


    // Remaining time for the power to be ready
    private float lightningPowerDuration = 5f;
    private float waterPowerDuration = 0.01f;

    private bool lightningPowerReady = true;
    private bool waterPowerReady = true;

    private GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
    }

    void Update()
    {
        if (lightningPowerReady == false)
        {
            lightningPowerDuration -= Time.deltaTime;
            if (lightningPowerDuration <= 0)
            {
                lightningPowerReady = true;
                lightningPowerDuration = lightningPowerCooldown;
            }
        }
        if (waterPowerReady == false)
        {
            waterPowerDuration -= Time.deltaTime;
            if (waterPowerDuration <= 0)
            {
                waterPowerReady = true;
                waterPowerDuration = waterPowerCooldown;
            }
        }
    }

    public void MainAbilityInteraction()
    {
        LightningPower(parent);
    }

    public void SecondaryAbilityInteraction()
    {
        WaterPower();
    }

    public void LightningPower(GameObject newLocation)
    {
        if (lightningPowerReady == false)
        {
            return;
        }
        lightningPowerReady = false;
        parent.transform.position = newLocation.transform.position;
    }

    public void WaterPower()
    {
        if (waterPowerReady == false)
        {
            return;
        }
        waterPowerReady = false;
        
        Vector3 localOffset = parent.transform.position + parent.transform.TransformDirection(waterProjectilePositionOffset);

        Rigidbody parentRb = parent.GetComponent<Rigidbody>();
        Vector3 parentVelocity = parentRb != null ? parentRb.velocity : Vector3.zero;

        int randomAmount = Random.Range(waterProjectileMinAmount, waterProjectileMaxAmount);

        for (int i = 0; i < randomAmount; i++)
        {
            CreateWaterProjectile(localOffset, parentVelocity);
        }
    }

    // Create a water projectile with a random rotation and direction relative to the parent object orientation position and velocity
    private void CreateWaterProjectile(Vector3 localOffset, Vector3 parentVelocity)
    {
        int randomProjectile = Random.Range(0, waterProjectile.Count);
        Vector3 randomRotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

        GameObject newProjectile = Instantiate(waterProjectile[randomProjectile], localOffset, Quaternion.Euler(randomRotation));
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();

        float randomLeftRight = Random.Range(-waterProjectileRandomDirectionOffset, waterProjectileRandomDirectionOffset);
        float randomUpDown = Random.Range(-waterProjectileRandomDirectionOffset, waterProjectileRandomDirectionOffset);

        Vector3 direction = parent.transform.forward + Vector3.up * waterProjectileUpwardPercentage + parent.transform.right * randomLeftRight + parent.transform.up * randomUpDown;
        direction.Normalize();
        rb.AddForce(direction * waterProjectileSpeed * newProjectile.transform.localScale.y * 100 + parentVelocity, ForceMode.Impulse);
    }
}
