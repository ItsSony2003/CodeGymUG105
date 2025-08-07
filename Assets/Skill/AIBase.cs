using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : MonoBehaviour
{
    public RoleStats roleStat;
    public List<SkillBase> ownerkills;
    public List<EffectBase> effectsList;

    public void SetStat(StatType statType, StatConfigBase stat)
    {
        if(roleStat == null)
        {
            roleStat = new RoleStats();
        }
        roleStat.dictStats.Add(statType, stat);
    }
}
