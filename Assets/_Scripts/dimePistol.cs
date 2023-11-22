using UnityEngine;

public class dimePistol : WeaponBase
{
    [SerializeField] private GameObject coinFab;
    [SerializeField] private Transform shootSpot;
    protected override void Attack(float percent)
    {
        weaponHandler.Ammo--;
        GameObject bullet = Instantiate(coinFab);
        bullet.transform.position = shootSpot.position;
        bullet.transform.rotation = Camera.main.transform.rotation;
    }
}
