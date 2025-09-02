using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost: SkillBase
{
    private float lastCastTime = -Mathf.Infinity;
    private readonly GameObject effect; // injected once
    private bool isActive;

    public JumpBoost(AIBase owner, SkillConfig skillConfig, GameObject effect)
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

        Debug.Log("jumppp");

        lastCastTime = Time.time;
        isActive = true;

        Player.instance.gridSize = 2;

        if (effect)
            effect.SetActive(true);
    }

    public override void StopSkill()
    {
        isActive = false;

        Player.instance.gridSize = 1;

        // VFX off
        if (effect)
            effect.SetActive(false);
    }

    public override void Tick()
    {
        // no per-frame logic needed for right now
        if (!isActive)
            return;
    }
}

public class JumpBoostConfig : SkillConfig
{
    public JumpBoostConfig()
    {
        codeName = "Shield";
        skillActiveCondition = SkillActiveCondition.OnAction;
        skillCastType = SkillCastType.Active;
        parramOnLevel = new float[] { 20f, 10f }; // cooldown, duration
    }
}

