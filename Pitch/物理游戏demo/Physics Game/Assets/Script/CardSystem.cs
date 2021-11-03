using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : MonoBehaviour
{
    public static CardSystem instance;
    private void Awake()
    {
        //单例
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void AchieveCard()
    {
        //如果存在空卡槽，则使用
    }

    public void UseCard()
    {
        //先判断卡牌类型

        //根据类型调用
    }

    public void HeavySkill()
    {

    }

    public void GhostSkill()
    {

    }

    public void SpecialSkill()
    {

    }
}
