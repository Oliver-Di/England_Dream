using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject walkerPrefab;
    public GameObject walker_greenPrefab;
    public GameObject walker_redPrefab;
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
        DontDestroyOnLoad(this);
    }

    public void CreateWalker(Vector3 pos)
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                GameObject walker = ObjectPool.Instance.GetObject(walkerPrefab);
                walker.transform.position = pos;
                break;
            case 1:
                GameObject walker1 = ObjectPool.Instance.GetObject(walker_greenPrefab);
                walker1.transform.position = pos;
                break;
            case 2:
                GameObject walker2 = ObjectPool.Instance.GetObject(walker_redPrefab);
                walker2.transform.position = pos;
                break;
        }
    }

    public void RefreshHp()
    {
        hp = player.GetComponent<PlayerGetHit>().hp;
        maxHp = player.GetComponent<PlayerGetHit>().maxHp;
        //血条随血量变动
        hpBar.fillAmount = hp / maxHp;
    }
}

public class MyTime
{
    public static float timescale = 1;
    public static float deltaTime
    {
        get
        {
            return Time.deltaTime * timescale;
        }
    }
}
