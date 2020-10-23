using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarkGunManager : MonoBehaviour
{
    [SerializeField] MarkRandomAudioPlayer WeaponPickupPlayer;
    [SerializeField] GameObject[] Guns;

    public void ActivateGun(string name)
    {
        Debug.Log("Attempting to activate the gun with name: " + name);
        if ( Guns == null )
        {
            return;
        }

        // find the one we need to actiavte if we can
        GameObject gunToActivate = Guns.ToList().FirstOrDefault(g => g.name.Equals(name));
        if ( gunToActivate == null )
        {
            Debug.Log("There's no gun with name " + name + " to activate");
            return;
        }

        // deactivate all
        Guns.ToList().ForEach(g => g.SetActive(false));

        // activate the one found
        gunToActivate.SetActive(true);
        WeaponPickupPlayer.PlayRandomIfAllowed();
    }
}
