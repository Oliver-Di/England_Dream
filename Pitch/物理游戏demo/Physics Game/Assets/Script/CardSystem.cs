using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : MonoBehaviour
{
    public static CardSystem instance;
    private void Awake()
    {
        //����
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
        //������ڿտ��ۣ���ʹ��
    }

    public void UseCard()
    {
        //���жϿ�������

        //�������͵���
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
