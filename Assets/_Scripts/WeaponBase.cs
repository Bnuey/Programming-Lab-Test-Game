using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Weapon Base States")]
    [SerializeField] protected float timeBetweenAttacks;
    [SerializeField] protected float chargeUpTime;
    [SerializeField] public int clipSize;
    [SerializeField, Range(0f, 1f)] protected float minChargePercent;
    [SerializeField] private bool isFullAuto;

    private Coroutine currentFireTimer;
    private bool isOnCooldown;
    private float currentChargeTime;

    

    private WaitForSeconds coolDownWait;
    private WaitUntil coolDownEnforce;

    private void Start()
    {
        coolDownWait = new WaitForSeconds(timeBetweenAttacks);
        coolDownEnforce = new WaitUntil(() => !isOnCooldown);
    }

    public void StartShooting()
    {
        currentFireTimer = StartCoroutine(ReFireTimer());
    }

    public void StopShooting()
    {
        StopCoroutine(currentFireTimer);

        float percent = currentChargeTime / chargeUpTime;
        if (percent != 0) TryAttack(percent);
    }

    private IEnumerator CoolDownTimer()
    {
        isOnCooldown = true;
        yield return coolDownWait;
        isOnCooldown = false;
    }

    private IEnumerator ReFireTimer()
    {
        Debug.Log("Waiting for cooldown");
        yield return coolDownEnforce;
        Debug.Log("Post Cooldown");

        while(currentChargeTime < chargeUpTime)
        {
            currentChargeTime += Time.deltaTime;
            yield return null;
        }

        TryAttack(1);

    }

    private void TryAttack(float percent, bool isRefire = true)
    {
        currentChargeTime = 0;
        if (!CanAttack(percent)) return;

        Attack(percent);

        StartCoroutine(CoolDownTimer());

        if (isFullAuto && percent >= 1) currentFireTimer = StartCoroutine(ReFireTimer());
    }

    protected virtual bool CanAttack(float percent)
    {
        return !isOnCooldown && percent >= minChargePercent;
    }

    protected abstract void Attack(float percent);
}
