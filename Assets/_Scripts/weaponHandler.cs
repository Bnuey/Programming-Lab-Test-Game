using DG.Tweening.Core.Easing;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class weaponHandler : MonoBehaviour
{
    private bool weaponShootToggle;

    [SerializeField] private WeaponBase[] _weapons;
    [SerializeField] private WeaponBase _currentWeapon;
    public static int Ammo;
    public static int ReserveAmmo;
    [SerializeField] private float _reloadTime;

    private Coroutine _reloadCoroutine;

    private bool _reloading;

    private void Awake()
    {
        Inputs.InitWeapon(this);
        Ammo = 10;
        ReserveAmmo = 40;
        _currentWeapon = _weapons[0];
    }
    public void Shoot()
    {
        if (_reloading) return;

        if (Ammo <= 0)
        {
            _reloadCoroutine =  StartCoroutine(Reload());
            return;
        }

        

        weaponShootToggle = !weaponShootToggle;
        if (weaponShootToggle) _currentWeapon.StartShooting();
        else _currentWeapon.StopShooting();
    }
    public IEnumerator Reload()
    {
        StopCoroutine(Reload());
        if (ReserveAmmo <= 0)
        {
            yield break;
        }


        _reloading = true;

        yield return new WaitForSeconds(_reloadTime);

        if (ReserveAmmo >= 10)
        {
            ReserveAmmo = ReserveAmmo - (_currentWeapon.clipSize - Ammo);
            Ammo = _currentWeapon.clipSize;
        }
        else
        {
            if ((_currentWeapon.clipSize - Ammo) <= ReserveAmmo)
            {
                ReserveAmmo = ReserveAmmo - (_currentWeapon.clipSize - Ammo);
                Ammo = _currentWeapon.clipSize;
            }
            else
            {
                Ammo += ReserveAmmo;
                ReserveAmmo = 0;
            }
        }



        _reloading = false;
    }

    public void SwitchWeapon(int key)
    {
        if (_reloadCoroutine != null) StopCoroutine(_reloadCoroutine);

        key--;

        foreach (WeaponBase Base in _weapons)
        {
            Base.gameObject.SetActive(false);
        }
        _weapons[key].gameObject.SetActive(true);
        _currentWeapon = _weapons[key];
    }

    public void ReloadPlease()
    {
        _reloadCoroutine = StartCoroutine(Reload());
    }
}
