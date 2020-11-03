using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkProjectileWaveConfig : MonoBehaviour
{
    [SerializeField] public string Name;
    [SerializeField] public GameObject[] ProjectilesGood;
    [SerializeField] public GameObject[] ProjectilesBad;
    [SerializeField] public float PercentBad = 0.9f;

    [SerializeField] public float MinShotPower = 500f;
    [SerializeField] public float MaxShotPower = 900f;
    //[SerializeField] public float EjectPower = 150f;

    [SerializeField] public float ShotEverySecondsMin = 1.0f;
    [SerializeField] public float ShotEverySecondsMax = 1.1f;

    [SerializeField] public int LowHealthForNextStage;
    [SerializeField] public AudioClip AudioToPlayOnActivated;

    public GameObject GetNextProjectile()
    {
        double percentage = Random.Range(0.0f, 1.0f);
        // Debug.Log("Projectile percentage: " + percentage);
        if ( percentage <= PercentBad)
        {
            return ProjectilesBad[Random.Range(0, ProjectilesBad.Length)];
        } else
        {
            return ProjectilesGood[Random.Range(0, ProjectilesGood.Length)];
        }
    }

    public float GetShotPower()
    {
        return Random.Range(MinShotPower, MaxShotPower);
    }
}
