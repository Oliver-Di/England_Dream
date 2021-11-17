using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    public float mp;
    public float maxMp;
    public Image skillIcon;

    private bool skillOn;
    private float cd;

    void Start()
    {
        
    }

    void Update()
    {
        PressSkillButton();
        SkillData();
    }

    private void PressSkillButton()
    {
        if(Input.GetKeyDown(KeyCode.Q)&&
            mp>0&&
            cd<=0&&
            !skillOn)
        {
            GameManager.instance.SlowDownTime();
            skillOn = true;
        }
        else if(Input.GetKeyDown(KeyCode.Q) && skillOn)
        {
            GameManager.instance.RecoveryTime();
            skillOn = false;
            cd = 3;
        }
    }

    private void SkillData()
    {
        //技能CD
        if (!skillOn && cd > 0)
            cd -= Time.deltaTime;
        //技能耗蓝
        if (skillOn)
        {
            mp -= 0.01f;
            GameManager.instance.RefreshMp();
        }
        else
        {
            //自动回蓝
            if (mp < maxMp)
            {
                mp += 0.0005f;
                GameManager.instance.RefreshMp();
            }
        }
        //技能CD显示
        skillIcon.fillAmount = 1 - cd / 3;
    }

    public void KillRewardMp(float _mp)
    {
        if (mp < maxMp)
        {
            mp += _mp;
            GameManager.instance.RefreshMp();
        }
        if (mp > maxMp)
        {
            mp = maxMp;
            GameManager.instance.RefreshMp();
        }
    }
}
