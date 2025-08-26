using UnityEngine;

public class Freeze : SkillBase
{
    private float lastCastTime = -Mathf.Infinity;
    private readonly GameObject effect; // optional VFX
    private bool isActive;

    // parramOnLevel: { cooldown, duration }
    public Freeze(AIBase owner, SkillConfig config, GameObject effect)
    {
        this.owner = owner;
        this.skillConfig = config;
        this.effect = effect;

        cooldown = config.parramOnLevel[0];
        duration = config.parramOnLevel[1];
    }

    public override void PlaySkill()
    {
        if (Time.time - lastCastTime < cooldown) return;

        lastCastTime = Time.time;
        isActive = true;

        FreezeManager.SetFrozen(true);
        if (effect) effect.SetActive(true);
    }

    public override void StopSkill()
    {
        isActive = false;

        FreezeManager.SetFrozen(false);
        if (effect) effect.SetActive(false);
    }

    public override void Tick() { /* nothing per-frame needed */ }
}

public class FreezeConfig : SkillConfig
{
    public FreezeConfig()
    {
        codeName = "Freeze";
        skillActiveCondition = SkillActiveCondition.OnAction;
        skillCastType = SkillCastType.Active;
        parramOnLevel = new float[] { 20f, 5f }; // cooldown, duration=5s
    }
}