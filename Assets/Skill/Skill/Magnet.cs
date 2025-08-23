using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Magnet : SkillBase
{
    //private float lastCastTime = -Mathf.Infinity;
    //private float radius => skillConfig.parramOnLevel[0];
    //private LayerMask targetLayer = LayerMask.GetMask("Coin");

    //public Magnet(AIBase owner, SkillConfig skillConfig)
    //{
    //    this.owner = owner;
    //    this.skillConfig = skillConfig;
    //    duration = skillConfig.parramOnLevel[2];
    //    cooldown = skillConfig.parramOnLevel[1];
    //}

    //public override void PlaySkill()
    //{
    //    if (Time.time - lastCastTime > cooldown)
    //    {
    //        Debug.Log("magnet active");
    //        lastCastTime = Time.time;

    //        // Activate visual ShieldEffect
    //        Player.instance.transform.Find("MagnetEffect").gameObject.SetActive(true);

    //        // Schedule StopSkill() after duration
    //        Player.instance.StartCoroutine(DeactivateAfterDuration());
    //    }
    //}

    //private IEnumerator DeactivateAfterDuration()
    //{
    //    yield return new WaitForSeconds(duration);
    //    StopSkill();
    //}

    //public override void StopSkill()
    //{
    //    Debug.Log("magnet expired");

    //    // Deactivate visual MagnetEffect
    //    Player.instance.transform.Find("MagnetEffect").gameObject.SetActive(false);
    //}

    //void MagnetCoinInRadius()
    //{
    //    Collider[] colliders = Physics.OverlapSphere(owner.transform.position, radius, targetLayer);

    //    foreach (Collider coin in colliders)
    //    {
    //        Vector3 direction = (owner.transform.position - coin.transform.position).normalized;

    //        coin.transform.position += direction * 5f * Time.deltaTime;
    //    }
    //}


    //public override void Tick()
    //{
    //    MagnetCoinInRadius();
    //}
    private float lastCastTime = -Mathf.Infinity;
    private float radius => skillConfig.parramOnLevel[0];
    private LayerMask targetLayer = LayerMask.GetMask("Coin");

    private readonly GameObject effect;   // injected once
    private bool isActive;                // guard Tick when not active

    public Magnet(AIBase owner, SkillConfig skillConfig, GameObject effect)
    {
        this.owner = owner;
        this.skillConfig = skillConfig;
        this.effect = effect;

        duration = skillConfig.parramOnLevel[2];
        cooldown = skillConfig.parramOnLevel[1];
    }

    public override void PlaySkill()
    {
        // cooldown gate
        if (Time.time - lastCastTime < cooldown) 
            return;

        lastCastTime = Time.time;
        isActive = true;

        if (effect) 
            effect.SetActive(true);
        
    }

    public override void StopSkill()
    {
        isActive = false;
        if (effect) 
            effect.SetActive(false);
    }

    public override void Tick()
    {
        if (!isActive) 
            return;
        MagnetCoinInRadius();
    }

    private void MagnetCoinInRadius()
    {
        var colliders = Physics.OverlapSphere(owner.transform.position, radius, targetLayer);
        foreach (var coin in colliders)
        {
            var dir = (owner.transform.position - coin.transform.position).normalized;
            coin.transform.position += dir * 5f * Time.deltaTime;
        }
    }
}

public class MagnetConfig : SkillConfig
{
    public MagnetConfig()
    {
        codeName = "Magnet";
        skillActiveCondition = SkillActiveCondition.OnAction;
        skillCastType = SkillCastType.Active;
        parramOnLevel = new float[] { 5f, 20f, 10f}; //radius, cooldown, duration
    }
}
