using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarkGunManager : MonoBehaviour
{
    [SerializeField] GameObject[] Guns;

    public void ActivateGun(string gunTagName)
    {
        if ( Guns == null )
        {
            return;
        }

        // find the one we need to actiavte if we can
        GameObject gunToActivate = Guns.ToList().First(g => g.CompareTag(gunTagName));
        if ( gunToActivate == null )
        {
            Debug.Log("There's no gun with tag name " + gunTagName + " to activate");
        }

        // deactivate all
        Guns.ToList().ForEach(g => g.SetActive(false));

        // activate the one found
        gunToActivate.SetActive(true);
    }
}
