using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject walkerPrefab;
    public GameObject walker_greenPrefab;
    public GameObject walker_redPrefab;
    public GameObject player;
    public Image hpBar;
    public Image mpBar;
    public GameObject bloodPointPrefab;
    public GameObject magicPointPrefab;
    public GameObject bloodOverlay;

    private float hp;
    private float maxHp;
    private float mp;
    private float maxMp;
    private Animator anim;
    private bool blood;

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
        //红屏
        if (hp <= 0.25 * maxHp)
        {
            bloodOverlay.GetComponent<Animator>().SetTrigger("bloodin");
            blood = true;
        }
        else if (blood && hp > 0.25 * maxHp)
        {
            bloodOverlay.GetComponent<Animator>().SetTrigger("bloodout");
            blood = false;
        }
    }

    public void RefreshMp()
    {
        mp = player.GetComponent<PlayerSkill>().mp;
        maxMp = player.GetComponent<PlayerSkill>().maxMp;
        //蓝条随蓝量变动
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

        Camera.main.GetComponent<PostProcessVolume>().enabled = true;
        Debug.Log("SlowDownTime_0.5x");
    }

    //public void SlowDownTime()
    //{
    //    Time.timeScale = 0.2f;
    //    MyTime.timescale = 5;
    //    player.GetComponent<PlayerMove>().speed = 25;
    //    player.GetComponent<PlayerJump>().jumpF = 38;
    //    player.GetComponent<PlayerJump>().fallMultiplier = 9.5f;
    //    player.GetComponent<PlayerJump>().jumpMultiplier = 7.5f;
    //    player.GetComponent<Rigidbody2D>().gravityScale = 10;
    //    SetAnimatorSpeed(anim, 2);
    //    Debug.Log("SlowDownTime_0.2x");
    //}

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

        Camera.main.GetComponent<PostProcessVolume>().enabled = false;
        Debug.Log("RecoveryTime_1");
    }

    private void SetAnimatorSpeed(Animator anim, float speed)
    {
        if (null == anim) return;
        anim.speed = speed;
    }

    public void CreateBloodPoint(Vector3 pos, int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject bead = ObjectPool.Instance.GetObject(bloodPointPrefab);
            bead.transform.position = pos;
            float rand1 = Random.Range(-1.5f, 1.5f);
            float rand2 = Random.Range(3, 6);
            bead.GetComponent<Rigidbody2D>().velocity = new Vector2(rand1, rand2);
            Debug.Log(i);
        }
    }

    public void CreateMagicPoint(Vector3 pos, int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject bead = ObjectPool.Instance.GetObject(magicPointPrefab);
            bead.transform.position = pos;
            float rand1 = Random.Range(-1.5f, 1.5f);
            float rand2 = Random.Range(3, 6);
            bead.GetComponent<Rigidbody2D>().velocity = new Vector2(rand1, rand2);
        }
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
