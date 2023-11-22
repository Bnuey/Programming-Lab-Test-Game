using System.Xml.Serialization;
using UnityEditor.Build;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    private static PlayerControls _actions;
    public static playerController PlayerController;
    public static weaponHandler WeaponHandler;
    public static void InitMovement(playerController script)
    {
        _actions = new PlayerControls();

        PlayerController = script;

        _actions.Player.Movement.performed += ctx => script.GetMove(ctx.ReadValue<Vector2>());
        _actions.Player.Jump.performed += ctx => script.Jump();
    }

    public static void InitWeapon(weaponHandler script)
    {
        WeaponHandler = script;

        _actions.Player.Shoot.performed += ctx => script.Shoot();
        _actions.Player.Reload.performed += ctx => script.ReloadPlease();

        _actions.Player.Weapon1.performed += ctx => script.SwitchWeapon(1);
        _actions.Player.Weapon2.performed += ctx => script.SwitchWeapon(2);

        PlayMode();
    }

    public static void PlayMode()
    {
        _actions.Player.Enable();
    }
}
