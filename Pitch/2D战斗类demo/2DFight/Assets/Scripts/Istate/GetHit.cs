using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHit : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public float maxHp;
    public float hp;
    public bool isVertigo;

    public static GetHit instance;
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
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    //受击掉血且击退
    public void GetHitBack(float damage, Vector3 dir, float force)
    {
        //造成伤害
        hp -= damage;
        //进入受击状态

        //闪白
        StartCoroutine(HurtShader());
        //后退
        rb.AddForce(-dir * force);
        //判断死亡
        if (hp <= 0)
            Dead();

        Debug.Log("hitback");

        //判断变身
        //if (parameter.hp <= 0.5f * parameter.maxHp && parameter.isChanged == false)
        //{
        //    ChangeToRed();
        //    parameter.isChanged = true;
        //}
    }

    public void GetVertigo(float damage)
    {
        //眩晕状态
        FSM.instance.TransitionState(StateType.Vertigo);
        isVertigo = true;
        //眩晕动画

        Debug.Log("vertigo");
    }

    public void GetExecute(float damage)
    {
        hp -= damage;
        //回到追击状态
        FSM.instance.parameter.target = FSM.instance.parameter.player;
        FSM.instance.TransitionState(StateType.Chase);

        Debug.Log("execute!!!");
    }

    //受击闪白
    IEnumerator HurtShader()
    {
        sr.material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(0.1f);
        sr.material.SetFloat("_FlashAmount", 0);
    }

    void Dead()
    {
        anim.Play("dead");

        //enabled = false;
        //transform.gameObject.layer = LayerMask.NameToLayer("Dead");
    }
}
