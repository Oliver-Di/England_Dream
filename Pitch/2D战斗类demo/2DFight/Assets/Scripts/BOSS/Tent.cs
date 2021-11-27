using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tent : MonoBehaviour
{
    public float time;
    public float hp;

    private SpriteRenderer sr;
    private Animator anim;
    private bool isDead;
    private Transform player;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        TryToCreate();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        
    }

    private void TryToCreate()
    {
        StartCoroutine(CreateMonster());
    }

    IEnumerator CreateMonster()
    {
        if (Vector2.Distance(player.position, transform.position) <= 9)
            GameManager.instance.CreateWalker(transform.position);
        yield return new WaitForSeconds(time);
        TryToCreate();
    }

    //受击掉血
    public void GetHit(float damage)
    {
        //造成伤害
        hp -= damage;
        //闪白
        StartCoroutine(HurtShader());
        //判断死亡
        if (hp <= 0)
            Dead();
    }

    //受击闪白
    IEnumerator HurtShader()
    {
        sr.material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(0.1f);
        sr.material.SetFloat("_FlashAmount", 0);
    }

    private void Dead()
    {
        if (hp <= 0 && !isDead)
        {
            anim.SetTrigger("dead");
            isDead = true;
        }
    }

    private void DestroyThis()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
}
