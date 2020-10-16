using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the bullet object")] [SerializeField] private float destroyTimeSecondsBullet = 5f;
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimeSecondsCasing = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;

    [SerializeField] AudioSource GunShotAudio;
    [SerializeField] float ShotsPerSecond = 1.0f;
    private float _nextTimeAllowedToFire = 0.0f;
    private GameObject _crossHair;

    void Start()
    {
        if (barrelLocation == null)
        {
            barrelLocation = transform;
        }

        if (gunAnimator == null)
        {
            gunAnimator = GetComponentInChildren<Animator>();
        }
        _crossHair = GameObject.Find("Crosshair");
        Assert.IsNotNull(_crossHair);
    }

    void Update()
    {
        //If you want a different input, change it here
        if (Time.unscaledTime > _nextTimeAllowedToFire && Input.GetAxis("Right Trigger") == 1.0f) // fully pressed
         {
            //Calls animation on the gun that has the relevant animation events that will fire
            gunAnimator.SetTrigger("Fire");
            if (GunShotAudio != null )
            {
                GunShotAudio.Play();
            }
            _nextTimeAllowedToFire = (1.0f / ShotsPerSecond) + Time.unscaledTime;
        }
    }

    //This function creates the bullet behavior
    void Shoot()
    {
        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimeSecondsCasing);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Get the crosshair ray
        // Create a bullet and add force on it in direction of the barrel in the direction of the crosshair
        Vector3 aimLookat = getCrossHairTargetLookAt();

        GameObject bullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        barrelLocation.transform.LookAt(aimLookat); // look at the target (crosshair) and shoot towards that direction
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(barrelLocation.forward * shotPower);
        Destroy(bullet, destroyTimeSecondsBullet); // destroy after a few seconds
    }

    private Vector3 getCrossHairTargetLookAt()
    {
        Ray crossHairRay = Camera.main.ScreenPointToRay(_crossHair.transform.position); // ray to the crosshair
        RaycastHit hit;
        if (Physics.Raycast(crossHairRay, out hit))
        {
            return hit.point; // We hit something, aim at that
        }
        else
        {
            return crossHairRay.GetPoint(100f); // pick a distance to aim at
        }


    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimeSecondsCasing);
    }

}
