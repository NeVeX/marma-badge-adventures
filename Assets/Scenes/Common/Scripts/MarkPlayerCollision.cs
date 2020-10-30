using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkPlayerCollision : MonoBehaviour
{
    [SerializeField] float PlayerHealth = 100f;
    [SerializeField] float SecondsBetweenEnenmyHitHurt = 1.5f;
    [SerializeField] GameObject HurtOverlayPanel;
    [SerializeField] MarkRandomAudioPlayer EnemyKilledAudioPlayer;
    [SerializeField] MarkRandomAudioPlayer PlayerHurtAudioPlayer;
    [SerializeField] MarkRandomAudioPlayer PlayerHealthPickupAudioPlayer;
    [SerializeField] GameObject DeathMenu;


    private MarkProjectileManager _markProjectileManager;
    private float _nextTimeEnemyHitHurtAllowed = -1.0f;

    private void Start()
    {
        _markProjectileManager = FindObjectOfType<MarkProjectileManager>();
        if (HurtOverlayPanel != null)
        {
            HurtOverlayPanel.SetActive(false);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        string gameObjectTag = hit.gameObject.tag;
        if (gameObjectTag.Equals("Untagged"))
        {
            return;
        }
        //Debug.Log("Player collided with something via OnControllerColliderHit - " + gameObjectTag);
        OnPlayerHit(hit.gameObject);
        hit.transform.SendMessage("OnPlayerColliderHit", SendMessageOptions.DontRequireReceiver);
    }

    private void OnCollisionEnter(Collision collision)
    {
        string gameObjectTag = collision.gameObject.tag;
        if (gameObjectTag.Equals("Untagged"))
        {
            return;
        }
        Debug.Log("Player collided with something via OnCollisionEnter - " + gameObjectTag);
        OnPlayerHit(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        string gameObjectTag = other.gameObject.tag;
        if (gameObjectTag.Equals("Untagged"))
        {
            return;
        }
        Debug.Log("Player collided with something via OnTriggerEnter - " + gameObjectTag);
        OnPlayerHit(other.gameObject);
    }

    public float GetPlayerHealthRemaining()
    {
        return PlayerHealth;
    }

    public void AddHealth(float healthToAdd)
    {
        Debug.Log("Adding health amount [" + healthToAdd + "] to player");
        PlayerHealth += healthToAdd;
        if (PlayerHealthPickupAudioPlayer != null )
        {
            PlayerHealthPickupAudioPlayer.PlayRandomNow();
        }
    }

    
    private void OnPlayerHit(GameObject gameObject)
    {
        MarkDamageObject mdo = gameObject.GetComponent<MarkDamageObject>();
        if ( mdo != null )
        {
            OnPlayerHit(mdo);
        }

        MarkProjectile projectile = gameObject.GetComponent<MarkProjectile>();
        if (projectile != null )
        {
            OnPlayerHitWithProjectile(projectile);
        }
    }

    private void OnPlayerHit(MarkDamageObject mdo)
    {
        if ( !mdo.IsEnemy)
        {
            return;
        }
        if ( Time.unscaledTime <= _nextTimeEnemyHitHurtAllowed)
        {
            return; // not allowed to hit yet
        }

        _nextTimeEnemyHitHurtAllowed = Time.unscaledTime + SecondsBetweenEnenmyHitHurt;

        PlayerHealth -= mdo.DamageAmount;
        Debug.Log("Player damaged. New Health: " + PlayerHealth + ", Damage taken: " + mdo.DamageAmount);
        if ( PlayerHealth <= 0 || mdo.IsInstantKill)
        {
            PlayerHealth = 0; 
            OnPlayerDeath();
        } else
        {
            if (PlayerHurtAudioPlayer != null )
            {
                PlayerHurtAudioPlayer.PlayRandomIfAllowed();
            }
            StartCoroutine(ShowHurtOverlay());
        }
    }

    private void OnPlayerHitWithProjectile(MarkProjectile projectile)
    {
        if (_markProjectileManager != null)
        {
            _markProjectileManager.OnProjectileHitPlayer(projectile);
        } else
        {
            Debug.LogWarning("Projectile hit player, but there's not projectile manager");
        }
    }

    private IEnumerator ShowHurtOverlay()
    {
        if ( HurtOverlayPanel == null )
        {
            yield break;
        }
        // TODO: get the image and alpha is over time
        HurtOverlayPanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        HurtOverlayPanel.SetActive(false);
    }

    private void OnPlayerDeath()
    {
        if ( PlayerHurtAudioPlayer != null )
        {
            PlayerHurtAudioPlayer.StopPlaying();
        }
        if (EnemyKilledAudioPlayer != null)
        {
            EnemyKilledAudioPlayer.StopPlaying();
        }

        Debug.Log("Game over for the player");
        DeathMenu.SetActive(true);
    }

    public void OnKilledEnemy()
    {
        if (EnemyKilledAudioPlayer != null)
        {
            EnemyKilledAudioPlayer.PlayRandomIfAllowed();
        }
    }

    // These ON-STAY methods don't work; what the fuck unity - it half works, sometimes


    //private void OnTriggerStay(Collider other)
    //{
    //    Debug.Log("Player collided with something via OnTriggerStay - " + other.gameObject.tag);
    //    if (Time.unscaledTime > _nextOnStayTriggerAllowed)
    //    {
    //        OnTriggerEnter(other);
    //        _nextOnStayTriggerAllowed = Time.unscaledTime + ConsecutiveSecondsUntilOnStayAllowed;
    //    }
    //}

    //private void OnCollisionStay(Collision collision)
    //{
    //    Debug.Log("Tine: " + Time.unscaledTime + ", next: " + _nextOnStayCollisionAllowed);
    //    Debug.Log("Player collided with something via OnCollisionStay - " + collision.gameObject.tag);
    //    if (Time.unscaledTime > _nextOnStayCollisionAllowed)
    //    {
    //        OnCollisionEnter(collision);
    //        _nextOnStayCollisionAllowed = Time.unscaledTime + ConsecutiveSecondsUntilOnStayAllowed;
    //    }
    //}

}
