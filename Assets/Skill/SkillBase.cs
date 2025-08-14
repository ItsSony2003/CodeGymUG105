using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase
{ 
    protected AIBase owner;
    public SkillConfig skillConfig;
    protected List<EffectBase> effects;
    public float duration;
    public float cooldown;



    public virtual void PlaySkill()
    {

    }

    public virtual void StopSkill()
    {

    }

    public virtual void Tick()
    {

    }

    public virtual void ApplyEffectToTarget(AIBase target)
    {

    }
}
