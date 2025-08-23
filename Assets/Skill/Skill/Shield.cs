using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : SkillBase
{
    //private float lastCastTime = -Mathf.Infinity;

    //public Shield(AIBase owner, SkillConfig skillConfig)
    //{
    //    this.owner = owner;
    //    this.skillConfig = skillConfig;
    //    duration = skillConfig.parramOnLevel[1];
    //}

    //public override void PlaySkill()
    //{
    //    //if (Time.time - lastCastTime > cooldown)
    //    //{
    //    //    Debug.Log("shield active");
    //    //    lastCastTime = Time.time;
    //    //    Player.instance.immortal = true;
    //    //}
    //    if (Time.time - lastCastTime > cooldown)
    //    {
    //        Debug.Log("shield active");
    //        lastCastTime = Time.time;

    //        // Activate immortality
    //        Player.instance.immortal = true;

    //        // Activate visual ShieldEffect
    //        Player.instance.transform.Find("ShieldEffect").gameObject.SetActive(true);

    //        // Schedule StopSkill() after duration
    //        Player.instance.StartCoroutine(DeactivateAfterDuration());
    //    }
    //}

    //public override void StopSkill()
    //{
    //    Player.instance.immortal = false;
    //    // Deactivate visual ShieldEffect
    //    Player.instance.transform.Find("ShieldEffect").gameObject.SetActive(false);
    //}

    //private IEnumerator DeactivateAfterDuration()
    //{
    //    yield return new WaitForSeconds(duration);
    //    StopSkill();
    //}

    private float lastCastTime = -Mathf.Infinity;
    private readonly GameObject effect; // injected once
    private bool isActive;

    public Shield(AIBase owner, SkillConfig skillConfig, GameObject effect)
    {
        this.owner = owner;
        this.skillConfig = skillConfig;
        this.effect = effect;

        // parramOnLevel: { cooldown, duration }
        cooldown = skillConfig.parramOnLevel[0];
        duration = skillConfig.parramOnLevel[1];
    }

    public override void PlaySkill()
    {
        // cooldown check
        if (Time.time - lastCastTime < cooldown) 
            return;

        lastCastTime = Time.time;
        isActive = true;

        Player.instance.immortal = true;

        if (effect) 
            effect.SetActive(true);
    }

    public override void StopSkill()
    {
        isActive = false;

        Player.instance.immortal = false;

        // VFX off
        if (effect) 
            effect.SetActive(false);
    }

    public override void Tick()
    {
        // no per-frame logic needed for shield right now
        if (!isActive) 
            return;
    }
}

public class ShieldConfig : SkillConfig
{
    public ShieldConfig()
    {
        codeName = "Shield";
        skillActiveCondition = SkillActiveCondition.OnAction;
        skillCastType = SkillCastType.Active;
        parramOnLevel = new float[] {20f, 10f }; // cooldown, duration
    }
}