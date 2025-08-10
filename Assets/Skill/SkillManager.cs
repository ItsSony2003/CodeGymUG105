using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<SkillBase> skills = new List<SkillBase>();
    public bool isCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        SkillBase skillMagnet = new Magnet(gameObject.GetComponent<AIBase>(), new MagnetConfig());
        skills.Add(skillMagnet);
        foreach(var skill in skills)
        {
            Debug.Log(skill);
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

    void UseSkill(int slotIndex)
    {
        if (skills[slotIndex] != null)
        {
            skills[slotIndex].PlaySkill();

            if(skills[slotIndex].skillConfig.skillActiveCondition == SkillActiveCondition.OnAction)
            {
                StartCoroutine(SkillCooldownCoroutine(slotIndex));
            }
        }
    }


    private IEnumerator SkillCooldownCoroutine(int slot)
    {
        float duration = skills[slot].skillConfig.parramOnLevel[2];
        float timer = 0f;

        while (timer < duration)
        {
            skills[slot].Tick();
            timer += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Skill ended!");
    }
}
