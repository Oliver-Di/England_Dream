using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public float mp;
    public float maxMp;

    private bool skillOn;
    private float skillCD;

    void Start()
    {
        
    }

    void Update()
    {
        PressSkillButton();
    }

    private void PressSkillButton()
    {
        if(Input.GetKeyDown(KeyCode.Q)&&
            mp>0&&
            skillCD<=0&&
            !skillOn)
        {
            GameManager.instance.SlowDownTime();
            skillOn = true;
        }
        else if(Input.GetKeyDown(KeyCode.Q) && skillOn)
        {
            GameManager.instance.RecoveryTime();
            skillOn = false;
        }
    }
}
