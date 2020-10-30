using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class MarkProjectileManager : MonoBehaviour
{
    [SerializeField] float ShotsPerSecond = 1.0f;
    [SerializeField] float ShotPower = 500f;
    [SerializeField] float DestoryObjectTimeSeconds = 6.0f;
    //private GameObject _boss;
    private List<MarkProjectile> childProjectiles = new List<MarkProjectile>();
    private MarkProjectile _currentChildProjectile;
    private float _nextTimeAllowedToFire = 0.0f;
    private GameObject _crossHair;

    private void Awake()
    {
        //_boss = GameObject.Find("Boss");
        //Assert.IsNotNull(_boss);
        _crossHair = GameObject.Find("Crosshair");
        Assert.IsNotNull(_crossHair);

        MarkProjectile[] projectiles = GetComponentsInChildren<MarkProjectile>(true); // include in
        if ( projectiles != null )
        {
            projectiles.ToList().ForEach(p => {
                childProjectiles.Add(p);
                p.gameObject.SetActive(false);
                ConfigureProjectileForHolding(p);
            });
        }
        Debug.Log("A total of [" + childProjectiles.Count + "] child projectiles were found");
    }

    private void ConfigureProjectileForHolding(MarkProjectile projectile)
    {
        projectile.GetComponent<Rigidbody>().isKinematic = true;
        projectile.TurnOffCollision();
    }

    private void ConfigureProjectileForFiring(MarkProjectile projectile)
    {
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        projectile.TurnOnCollision();
    }

    void Update()
    {
        bool isAllowedToFire = Time.unscaledTime > _nextTimeAllowedToFire;
        bool isFireTriggerHeld = Input.GetAxis("Right Trigger") == 1.0f; // fully pressed
        if (_currentChildProjectile != null && isAllowedToFire && isFireTriggerHeld)
        {
            LaunchProjectile();
            _nextTimeAllowedToFire = (1.0f / ShotsPerSecond) + Time.unscaledTime;
        }
    }

    private void LaunchProjectile()
    {
        // dup the current held projectile and fire it. then hide the held projectile previously
        //GameObject duplicateToFire = Instantiate(_currentChildProjectile.gameObject);
        

        Transform shootFromLocation = _currentChildProjectile.transform;
        Vector3 aimLookat = getCrossHairTargetLookAt();
        shootFromLocation.transform.LookAt(aimLookat);
        GameObject shootObject = Instantiate(_currentChildProjectile.gameObject, shootFromLocation.position, shootFromLocation.rotation);
        ConfigureProjectileForFiring(shootObject.GetComponent<MarkProjectile>());
        Rigidbody shootObjectRB = shootObject.GetComponent<Rigidbody>();
        shootObjectRB.AddForce(shootFromLocation.forward * ShotPower);

        Destroy(shootObject, DestoryObjectTimeSeconds); // destroy after a few seconds

        // Deactivate the previous child
        DeactivateCurrentProjectile();

        Debug.Log("Launched projectile from player called " + shootObject.name);
    }

    public void OnProjectileHitPlayer(MarkProjectile projectile)
    {
        if ( !projectile.CanPlayerGrab)
        {
            return;
        }
        if ( _currentChildProjectile != null )
        {
            return; 
        }

        string projectileName = projectile.name.Replace("(Clone)", "");
        int indexOfSpace = projectileName.IndexOf(" ");
        if (indexOfSpace > -1)
        {
            projectileName = projectileName.Substring(0, indexOfSpace);
        }
        Debug.Log("Searching for projectile with name: " + projectileName);
        // make sure we can find this projectile in the children
        MarkProjectile foundChildProjectile = childProjectiles.Find(cp => cp.gameObject.name.Equals(projectileName));
        if ( foundChildProjectile == null )
        {
            return;
        }

        DeactivateCurrentProjectile();
        ActivateProjectile(foundChildProjectile);
    }

    private Vector3 getCrossHairTargetLookAt()
    {
        Ray crossHairRay = Camera.main.ScreenPointToRay(_crossHair.transform.position); // ray to the crosshair
        RaycastHit hit;
        if (Physics.Raycast(crossHairRay, out hit))
        {
            Debug.Log("Hit something with raycast. Distance: " + hit.distance + ", Name: " + hit.collider.gameObject.tag);
            return hit.point; // We hit something, aim at that
        }
        else
        {
            Debug.Log("Did not hit anything with raycast");
            return crossHairRay.GetPoint(100f); // pick a distance to aim at
        }
    }

    private void DeactivateCurrentProjectile()
    {
        if (_currentChildProjectile != null)
        {
            _currentChildProjectile.gameObject.SetActive(false); // hide it
            _currentChildProjectile = null; 
        }
    }

    private void ActivateProjectile(MarkProjectile projectile)
    {
        _currentChildProjectile = projectile;
        ActivateProjectile();
    }

    private void ActivateProjectile()
    {
        if (_currentChildProjectile != null)
        {
            _currentChildProjectile.gameObject.SetActive(true);
        }
    }
}
