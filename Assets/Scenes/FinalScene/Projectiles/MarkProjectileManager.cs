using System;
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

    public MarkProjectile GetCurrentlyHeldProjectile()
    {
        return _currentChildProjectile;
    }

    private void Awake()
    {
        //_boss = GameObject.Find("Boss");
        //Assert.IsNotNull(_boss);
        _crossHair = GameObject.Find("Crosshair");
        Assert.IsNotNull(_crossHair);

        MarkProjectile[] projectiles = GetComponentsInChildren<MarkProjectile>(true); // include inactives
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
        projectile.GetComponent<Collider>().enabled = false;
        projectile.TurnOffCollision();
    }

    private void ConfigureProjectileForFiring(MarkProjectile projectile)
    {
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        projectile.GetComponent<Collider>().enabled = true;
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
        //GameObject shootObject = Instantiate(_currentChildProjectile.gameObject, shootFromLocation.position, shootFromLocation.rotation);
        ConfigureProjectileForFiring(_currentChildProjectile.GetComponent<MarkProjectile>());
        Rigidbody shootObjectRB = _currentChildProjectile.GetComponent<Rigidbody>();
        shootObjectRB.AddForce(shootFromLocation.forward * ShotPower);
        _currentChildProjectile.transform.parent = null; // remove the parent again
        _currentChildProjectile.ShouldRotate = true;

        Debug.Log("Launched projectile from player called " + _currentChildProjectile.name);
        Destroy(_currentChildProjectile.gameObject, DestoryObjectTimeSeconds); // destroy after a few seconds

        // Deactivate the previous child
        DeactivateCurrentProjectile();
    }

    public bool OnProjectileHitPlayer(MarkProjectile projectile)
    {
        if ( !projectile.CanPlayerGrab)
        {
            return false;
        }

        bool shouldActivateMarmaBadger = _currentChildProjectile != null && _currentChildProjectile.name.StartsWith("Projectile-Fruit-Orange") && projectile.name.StartsWith("Projectile-Animal-Badger");

        if ( _currentChildProjectile != null && !shouldActivateMarmaBadger)
        {
            return false; 
        }

        string projectileName = projectile.name.Replace("(Clone)", "");
        int indexOfSpace = projectileName.IndexOf(" ");
        if (indexOfSpace > -1)
        {
            projectileName = projectileName.Substring(0, indexOfSpace);
        }

        if ( shouldActivateMarmaBadger )
        {
            projectileName = "Projectile-Animal-MarmaBadge"; // override
            Destroy(_currentChildProjectile.gameObject); // and remove it from the scene
            DeactivateCurrentProjectile();
        }
        
        // make sure we can find this projectile in the children
        MarkProjectile foundChildProjectile = childProjectiles.Find(cp => cp.gameObject.name.Equals(projectileName));
        if ( foundChildProjectile == null )
        {
            Debug.Log("Did not find projectile with name: " + projectileName);
            return false;
        }
        Debug.Log("Found projectile with name: " + projectileName);
        GameObject instanceObject = Instantiate(foundChildProjectile.gameObject, foundChildProjectile.transform.position, foundChildProjectile.transform.rotation);
        instanceObject.transform.parent = foundChildProjectile.transform.parent;
        MarkProjectile mp = instanceObject.GetComponent<MarkProjectile>();
        DeactivateCurrentProjectile();
        ConfigureProjectileForHolding(mp);
        ActivateProjectile(mp);
        return true;
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
            //_currentChildProjectile.gameObject.SetActive(false); // hide it
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
            if ( _currentChildProjectile.PlayerGrabAudioSource != null )
            {
                _currentChildProjectile.PlayerGrabAudioSource.Play();
            }
        }
    }
}
