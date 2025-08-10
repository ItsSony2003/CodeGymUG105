using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Magnet : SkillBase
{
    private float lastCastTime = -Mathf.Infinity;
    private float cooldown => skillConfig.parramOnLevel[1];
    private float radius => skillConfig.parramOnLevel[0];
    private float duration;
    private LayerMask targetLayer = LayerMask.GetMask("Coin");

    public Magnet(AIBase owner, SkillConfig skillConfig)
    {
        this.owner = owner;
        this.skillConfig = skillConfig;
        duration = skillConfig.parramOnLevel[2];
    }

    public override void PlaySkill()
    {
        if (Time.time - lastCastTime > cooldown)
        {
            Debug.Log("magnet active");
            lastCastTime = Time.time;
        }
    }

    void MagnetCoinInRadius()
    {
        Collider[] colliders = Physics.OverlapSphere(owner.transform.position, radius, targetLayer);

        foreach (Collider coin in colliders)
        {
            Vector3 direction = (owner.transform.position - coin.transform.position).normalized;

            coin.transform.position += direction * 5f * Time.deltaTime;
        }
    }


    public override void Tick()
    {


        MagnetCoinInRadius();
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
