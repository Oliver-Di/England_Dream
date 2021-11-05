using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attack;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSkillKey();
    }

    //传递攻击
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //造成伤害
        if (collision.CompareTag("Enemy"))
        {
            //获取interface
            InterFaces interfaces = collision.gameObject.GetComponent<InterFaces>();
            //计算方向
            Vector3 dir = transform.position - collision.transform.position;
            if (collision.GetComponent<FSM>().parameter.target == null &&
                collision.GetComponent<GetHit>().isVertigo == false) 
            {
                //击昏
                GetHit.instance.GetVertigo(attack);
            }
            else if (collision.GetComponent<GetHit>().isVertigo == true)
            {
                GetHit.instance.GetExecute(attack * 10);
            }
            else
            {
                //传递伤害
                GetHit.instance.GetHitBack(attack, dir, 500);
            }
        }
    }

    private void GetSkillKey()
    {
        if (Input.GetMouseButton(0))
            Attack1();
    }

    private void Attack1()
    {
        anim.SetTrigger("attack1");
    }
}
