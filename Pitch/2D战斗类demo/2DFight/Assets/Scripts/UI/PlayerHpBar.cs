using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    public static PlayerHpBar instance;

    public GameObject player;
    public Image hpBar;

    private float hp;
    private float maxHp;

    private void Awake()
    {
        //单例
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void RefreshHp()
    {
        hp = player.GetComponent<PlayerGetHit>().hp;
        maxHp = player.GetComponent<PlayerGetHit>().maxHp;
        //血条随血量变动
        hpBar.fillAmount = hp / maxHp;
    }
}
