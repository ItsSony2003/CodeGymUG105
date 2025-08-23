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
    private Dictionary<SkillBase, Coroutine> activeSkillCoroutines = new Dictionary<SkillBase, Coroutine>();
    public bool isCooldown = false;

    void Start()
    {
        var ai = GetComponent<AIBase>();

        SkillBase skillMagnet = new Magnet(ai, new MagnetConfig(), magnetEffect);
        SkillBase skillShield = new Shield(ai, new ShieldConfig(), shieldEffect);
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

        skills[slotIndex].PlaySkill();

        if (skills[slotIndex].skillConfig.skillActiveCondition == SkillActiveCondition.OnAction)
        {
            // Skill may be on cooldown and refuse to activate; that’s OK:
            // Tick() in Magnet is guarded by isActive, so it won’t pull coins if not active.
            var coro = StartCoroutine(SkillCooldownCoroutine(slotIndex));
            activeSkillCoroutines[skills[slotIndex]] = coro;
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
        activeSkillCoroutines.Remove(skills[slot]);
    }

    public void StopSkill(SkillBase skill)
    {
        if (activeSkillCoroutines.ContainsKey(skill))
        {
            StopCoroutine(activeSkillCoroutines[skill]);
            skill.StopSkill();
            activeSkillCoroutines.Remove(skill);
        }
    }

    public void StopAllSkills()
    {
        foreach (var kvp in activeSkillCoroutines)
        {
            StopCoroutine(kvp.Value);
            kvp.Key.StopSkill();
        }
        activeSkillCoroutines.Clear();
    }
}
