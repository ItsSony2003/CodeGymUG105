using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatConfigBase
{
    protected StatType statType;
    protected float baseValue;
    protected float basePercentValue;
    protected float otherValue;
    protected float allPercentValue;


    public StatConfigBase(StatType statType, float baseValue, float basePercentValue, float otherValue, float allPercentValue)
    {
        this.statType = statType;
        this.baseValue = baseValue;
        this.basePercentValue = basePercentValue;
        this.otherValue = otherValue;
        this.allPercentValue = allPercentValue;
    }

    public float GetValueStat()
    {
        return (baseValue * basePercentValue + otherValue) * allPercentValue;
    }
}
