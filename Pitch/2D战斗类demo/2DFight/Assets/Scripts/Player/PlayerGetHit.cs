using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHit : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator anim;
    private float timer;
    private bool hpIncreasing;

    public float maxHp;
    public float hp;
    public bool isDead;
    public bool isVertigo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        BloodReturn();

        if (timer > 0)
            timer -= Time.deltaTime;
    }

    //受击掉血且击退
    public void GetHitBack(float damage, Vector3 dir, float force)
    {
        hp -= damage;
        //修正回血等待时间
        timer = 5;
        PlayerHpBar.instance.RefreshHp();
        //闪白
        StartCoroutine(HurtShader());
        //后退
        rb.AddForce(-dir * force);
        //修正状态
        GetComponent<PlayerAttack>().isAttack = false;
        GetComponent<PlayerExecute>().isExecute = false;
        isVertigo = false;
        
        //判断死亡
        if (hp <= 0)
            Dead();
        else
            anim.SetTrigger("hurt");
    }

    //受击闪白
    IEnumerator HurtShader()
    {
        sr.material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(0.1f);
        sr.material.SetFloat("_FlashAmount", 0);
    }

    private void BloodReturn()
    {
        if (timer <= 0 &&
            hp < maxHp &&
            !hpIncreasing) 
        {
            StartCoroutine(ContinuousBloodReturn());
            hpIncreasing = true;
            BuffIcon.instance.StartBuff(4, 0.5f);
        }
    }

    IEnumerator ContinuousBloodReturn()
    {
        hp += 0.01f;
        PlayerHpBar.instance.RefreshHp();
        yield return new WaitForSeconds(0.5f);
        hpIncreasing = false;
    }

    public void Vertigo()
    {
        isVertigo = true;
        rb.velocity = Vector2.zero;
        anim.SetTrigger("vertigo");
        BuffIcon.instance.StartBuff(0, 1);
    }

    public void VertigoEnd()
    {
        isVertigo = false;
    }

    public void DebuffBloodLoss(float damage,int num)
    {
        StartCoroutine(ContinuousBloodLoss(damage));
        BuffIcon.instance.StartBuff(num, 5);
    }

    IEnumerator ContinuousBloodLoss(float damage)
    {
        for (int i = 0; i < 5; i++)
        {
            hp -= damage;
            //修正回血等待时间
            timer = 5;
            PlayerHpBar.instance.RefreshHp();
            yield return new WaitForSeconds(1);
        }
    }

    public void Dead()
    {
        anim.SetTrigger("dead");
        isDead = true;
        transform.gameObject.layer = LayerMask.NameToLayer("Dead");
    }
}
