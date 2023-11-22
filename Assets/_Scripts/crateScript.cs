using System;
using UnityEngine;

public class crateScript : MonoBehaviour
{
    [Flags]
    public enum crateType
    {
        ammo = 1,
        health = 2,
        money = 4,
    }
    public crateType CrateType;

    [SerializeField] private GameObject coinFab;

    [SerializeField] private int ammoGain;
    [SerializeField] private int healthGain;
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;


        if (((int)CrateType & (int)crateType.ammo) == (int)crateType.ammo)
            AmmoCrate();


        if (((int)CrateType & (int)crateType.health) == (int)crateType.health)
            HealthCrate();


        if (((int)CrateType & (int)crateType.money) == (int)crateType.money)
            MoneyCrate();


        Destroy(gameObject);
    }

    private void AmmoCrate()
    {
        Debug.Log("Ammo Crate");
        weaponHandler.ReserveAmmo += ammoGain;
    }
    private void HealthCrate()
    {
        Debug.Log("Health Crate");
        healthManager.GainHealth?.Invoke(healthGain);
    }
    private void MoneyCrate()
    {
        Debug.Log("Money Crate");
        for (int i = 0; i < 10; i++)
        {
            GameObject coin = Instantiate(coinFab);
            coin.transform.position = transform.position;
            coin.transform.eulerAngles = new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
        }
    }
}
