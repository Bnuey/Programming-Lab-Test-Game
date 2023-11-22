using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private TextMeshProUGUI _reserveAmmoText;
    [SerializeField] private TextMeshProUGUI _healthText;

    private void Update()
    {
        _ammoText.text = weaponHandler.Ammo.ToString();
        _reserveAmmoText.text = weaponHandler.ReserveAmmo.ToString();
        _healthText.text = healthManager.Health.ToString();
    }
}
