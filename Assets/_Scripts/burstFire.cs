using UnityEngine;

public class burstFire : WeaponBase
{
    [SerializeField] private GameObject coinFab;
    [SerializeField] private Transform shootSpot;
    protected override void Attack(float percent)
    {
        Invoke("shoot", 0);
        Invoke("shoot", .1f);
        Invoke("shoot", .2f);
    }

    private void shoot()
    {
        weaponHandler.Ammo--;
        GameObject bullet = Instantiate(coinFab);
        bullet.transform.position = shootSpot.position;
        bullet.transform.rotation = Camera.main.transform.rotation;
    }

}
