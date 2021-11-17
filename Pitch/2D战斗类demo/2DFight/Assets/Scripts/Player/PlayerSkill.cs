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
        SkillCD();
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

    private void SkillCD()
    {
        if (!skillOn && cd > 0)
            cd -= Time.deltaTime;

        skillIcon.fillAmount = 1 - cd / 3;
    }
}
