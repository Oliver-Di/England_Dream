﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    public Image skillIcon;
    public float skillDuration;

    private bool skillOn;
    private float cd;
    private float timer;

    void Start()
    {
        timer = skillDuration;
    }

    void Update()
    {
        PressSkillButton();
        SkillData();
    }

    private void PressSkillButton()
    {
        if (Input.GetKeyDown(KeyCode.Q) &&
            !skillOn)
        {
            GameManager.instance.SlowDownTime();
            skillOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && skillOn ||
            timer <= 0 && skillOn) 
        {
            GameManager.instance.RecoveryTime();
            skillOn = false;
            cd = 3;
            timer = skillDuration;
        }
    }

    private void SkillData()
    {
        //技能CD
        if (!skillOn && cd > 0)
            cd -= Time.deltaTime;
        //技能CD显示
        skillIcon.fillAmount = 1 - cd / 3;
        //技能持续时间
        if (skillOn && timer > 0)
            timer -= Time.deltaTime;
    }
}
