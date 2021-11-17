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
    public Image mpBar;

    private float hp;
    private float maxHp;
    private float mp;
    private float maxMp;
    private Animator anim;

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

    private void Start()
    {
        anim = player.GetComponent<Animator>();
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

    public void RefreshMp()
    {
        hp = player.GetComponent<PlayerSkill>().mp;
        maxHp = player.GetComponent<PlayerSkill>().maxMp;
        //血条随血量变动
        mpBar.fillAmount = mp / maxMp;
    }

    public void SlowDownTime()
    {
        Time.timeScale = 0.5f;
        MyTime.timescale = 2;
        player.GetComponent<PlayerMove>().speed = 10;
        player.GetComponent<PlayerJump>().jumpF = 18;
        player.GetComponent<PlayerJump>().fallMultiplier = 3.8f;
        player.GetComponent<PlayerJump>().jumpMultiplier = 3;
        player.GetComponent<Rigidbody2D>().gravityScale = 4;
        SetAnimatorSpeed(anim, 2);
        Debug.Log("SlowDownTime");
    }

    public void RecoveryTime()
    {
        Time.timeScale = 1;
        MyTime.timescale = 1;
        player.GetComponent<PlayerMove>().speed = 5;
        player.GetComponent<PlayerJump>().jumpF = 10;
        player.GetComponent<PlayerJump>().fallMultiplier = 1.9f;
        player.GetComponent<PlayerJump>().jumpMultiplier = 1.5f;
        player.GetComponent<Rigidbody2D>().gravityScale = 2;
        SetAnimatorSpeed(anim, 1);
        Debug.Log("RecoveryTime");
    }

    public void SetAnimatorSpeed(Animator anim, float speed)
    {
        if (null == anim) return;
        anim.speed = speed;
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
