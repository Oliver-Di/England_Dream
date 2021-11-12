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
    public GameObject blood8Prefab;
    public GameObject blood4Prefab;
    public GameObject blood7Prefab;
    public GameObject blood9Prefab;
    public bool isVertigo;

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

        //判断变身
        //if (parameter.hp <= 0.5f * parameter.maxHp && parameter.isChanged == false)
        //{
        //    ChangeToRed();
        //    parameter.isChanged = true;
        //}
    }

    public void GetVertigo(float damage)
    {
        //掉血
        hp -= damage;
        //眩晕状态
        GetComponent<FSM>().TransitionState(StateType.Vertigo);
        isVertigo = true;
        //眩晕动画

        //判断死亡
        if (hp <= 0)
            Dead();

        Debug.Log("vertigo");
    }

    public void GetExecute(float damage)
    {
        hp -= damage;
        BloodVFX1();

        //发现敌人
        GetComponent<FSM>().parameter.target = GetComponent<FSM>().parameter.player;

        Debug.Log("execute!!!");
    }

    public void BloodVFX1()
    {
        //喷血特效8
        GameObject blood8 = ObjectPool.Instance.GetObject(blood8Prefab);
        blood8.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
    }

    public void BloodVFX2()
    {
        //喷血特效4
        GameObject blood4 = ObjectPool.Instance.GetObject(blood4Prefab);
        blood4.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
        //喷血特效7
        GameObject blood7 = ObjectPool.Instance.GetObject(blood7Prefab);
        blood7.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
        //喷血特效9
        GameObject blood9 = ObjectPool.Instance.GetObject(blood9Prefab);
        blood9.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
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
        GetComponent<FSM>().TransitionState(StateType.Dead);

        //enabled = false;
        //transform.gameObject.layer = LayerMask.NameToLayer("Dead");
    }

    public void Explode()
    {
        //判断死亡
        if (hp <= 0)
        {
            BloodVFX2();
            //生成残肢

            gameObject.SetActive(false);
        }
    }
}
