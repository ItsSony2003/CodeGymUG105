using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase
{
    protected AIBase owner;
    protected List<EffectBase> effects;

    public virtual void PlaySkill()
    {

    }

    public virtual void StopSkill()
    {

    }

    public virtual void ApplyEffectToTarget(AIBase target)
    {

    }
}
