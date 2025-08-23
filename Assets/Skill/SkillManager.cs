using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [Header("Effect References")]
    [SerializeField] private GameObject magnetEffect;
    [SerializeField] private GameObject shieldEffect;
    [SerializeField] private GameObject freezeEffect;


    public List<SkillBase> skills = new List<SkillBase>();
    private Dictionary<int, Coroutine> activeSkillCoroutines = new Dictionary<int, Coroutine>();
    public bool isCooldown = false;

    void Start()
    {
        var ai = GetComponent<AIBase>();

        SkillBase skillMagnet = new Magnet(ai, new MagnetConfig(), magnetEffect);
        SkillBase skillShield = new Shield(ai, new ShieldConfig(), shieldEffect);
        SkillBase skillJumBoost = new JumpBoost(ai, new JumpBoostConfig(), shieldEffect);

        skills.Add(skillMagnet);
        skills.Add(skillShield);
        //skills.Add(skillJumBoost);
        
        SkillBase skillFreeze = new Freeze(ai, new FreezeConfig(), freezeEffect);

        skills.Add(skillMagnet);
        skills.Add(skillShield);
        skills.Add(skillFreeze);
        
        foreach (var skill in skills) Debug.LogWarning(skill);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) UseSkill(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) UseSkill(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) UseSkill(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) UseSkill(3);
    }

    public void UseSkill(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= skills.Count || skills[slotIndex] == null) return;

        if (activeSkillCoroutines.ContainsKey(slotIndex)) return;

        skills[slotIndex].PlaySkill();

        if (skills[slotIndex].skillConfig.skillActiveCondition == SkillActiveCondition.OnAction)
        {
            var coro = StartCoroutine(SkillCooldownCoroutine(slotIndex));
            activeSkillCoroutines[slotIndex] = coro;
        }

    }

    private IEnumerator SkillCooldownCoroutine(int slot)
    {
        float duration = skills[slot].duration;
        float timer = 0f;

        while (timer < duration)
        {
            skills[slot].Tick();
            timer += Time.deltaTime;
            yield return null;
        }

        skills[slot].StopSkill();
        Debug.Log("goij stop");
        activeSkillCoroutines.Remove(slot);
    }

    //public void StopSkill(SkillBase skill)
    //{
    //    if (activeSkillCoroutines.ContainsKey(skill))
    //    {
    //        StopCoroutine(activeSkillCoroutines[skill]);
    //        skill.StopSkill();
    //        activeSkillCoroutines.Remove(skill);
    //    }
    //}

    //public void StopAllSkills()
    //{
    //    foreach (var kvp in activeSkillCoroutines)
    //    {
    //        StopCoroutine(kvp.Value);
    //        kvp.Key.StopSkill();
    //    }
    //    activeSkillCoroutines.Clear();
    //}
}
