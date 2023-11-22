using System;
using UnityEngine;

public class healthManager : MonoBehaviour
{
    public static int Health;
    public static int MaxHealth = 100;
    public static Action<int> GainHealth;
    public static Action<int> LoseHealth;

    private void Awake()
    {
        Health = 50;
    }
    private void OnEnable()
    {
        GainHealth += _gainHealth;
        LoseHealth += _loseHealth;
    }

    private void OnDisable()
    {
        GainHealth -= _gainHealth;
        LoseHealth -= _loseHealth;
    }

    private void _gainHealth(int healthGain)
    {
        if (Health > MaxHealth - healthGain)
        {
            var realGain = MaxHealth - Health;
            Health += realGain;
            return;
        }

        Health += healthGain;
    }

    private void _loseHealth(int healthLoss)
    {
        Health -= healthLoss;
    }

}
