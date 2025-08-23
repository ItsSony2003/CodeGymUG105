using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSkillButton : MonoBehaviour
{
    public void OnClickSkill_1()
    {
        UIManager.instance.SlotSkill_1();
    }

    public void OnClickSkill_2()
    {
        UIManager.instance.SlotSkill_2();
    }

    public void OnClickSkill_3()
    {
        UIManager.instance.SlotSkill_3();
    }

    public void OnClickSkill_4()
    {
        UIManager.instance.SlotSkill_4();
    }
}
