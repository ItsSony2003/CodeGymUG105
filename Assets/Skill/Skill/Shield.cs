using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : SkillBase
{
    private float lastCastTime = -Mathf.Infinity;

    public Shield(AIBase owner, SkillConfig skillConfig)
    {
        this.owner = owner;
        this.skillConfig = skillConfig;
        duration = skillConfig.parramOnLevel[1];
    }

    public override void PlaySkill()
    {
        if (Time.time - lastCastTime > cooldown)
        {
            Debug.Log("shield active");
            lastCastTime = Time.time;
            Player.instance.immortal = true;
        }
    }

    public override void StopSkill()
    {
        Player.instance.immortal = false;
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