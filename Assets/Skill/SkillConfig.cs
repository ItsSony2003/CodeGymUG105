using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillConfig
{
    public string codeName;
    public SkillActiveCondition skillActiveCondition;
    public SkillCastType skillCastType;
    public List<EffectConfig> effects;
    public int[] levelUp;
    public float[][] parramOnLevel;
}
