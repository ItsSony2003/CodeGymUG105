using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<SkillBase> skills = new List<SkillBase>();
    private Dictionary<SkillBase, Coroutine> activeSkillCoroutines = new Dictionary<SkillBase, Coroutine>();
    public bool isCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        SkillBase skillMagnet = new Magnet(gameObject.GetComponent<AIBase>(), new MagnetConfig());
        SkillBase skillShield = new Shield(gameObject.GetComponent<AIBase>(), new ShieldConfig());
        skills.Add(skillMagnet);
        skills.Add(skillShield);
        foreach(var skill in skills)
        {
            Debug.LogWarning(skill);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            UseSkill(0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            UseSkill(1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            UseSkill(2);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            UseSkill(3);
        
    }

    public void UseSkill(int slotIndex)
    {
        if (skills[slotIndex] != null)
        {
            skills[slotIndex].PlaySkill();

            if(skills[slotIndex].skillConfig.skillActiveCondition == SkillActiveCondition.OnAction)
            {
                Coroutine coro = StartCoroutine(SkillCooldownCoroutine(slotIndex));
                activeSkillCoroutines[skills[slotIndex]] = coro;
            }
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

        Debug.Log("Skill ended!");
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
            Debug.Log($"Skill {skill} stopped manually!");
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
        Debug.Log("All skills stopped!");
    }
}
