using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHit : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator anim;

    public float maxHp;
    public float hp;
    public bool isDead;
    public bool isVertigo;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //受击掉血且击退
    public void GetHitBack(float damage, Vector3 dir, float force)
    {
        hp -= damage;
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
        {
            Dead();
        }
        else
        {
            anim.SetTrigger("hurt");
        }
    }

    //受击闪白
    IEnumerator HurtShader()
    {
        sr.material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(0.1f);
        sr.material.SetFloat("_FlashAmount", 0);
    }

    public void Vertigo()
    {
        isVertigo = true;
        rb.velocity = Vector2.zero;
        anim.SetTrigger("vertigo");
    }

    public void VertigoEnd()
    {
        isVertigo = false;
    }

    public void DebuffBloodLoss(float damage)
    {
        StartCoroutine(ContinuousBloodLoss(damage));
    }

    IEnumerator ContinuousBloodLoss(float damage)
    {
        for (int i = 0; i < 5; i++)
        {
            hp -= damage;
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
