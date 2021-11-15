using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    public GameObject[] headBullets;
    public Transform throwPoint;
    public Vector2 throwSpeed;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        ThrowHead();
    }

    private void ThrowHead()
    {
        if(GetComponent<PlayerPickup>().headSlot[0]!=null&&
            Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("throw");
        }
    }

    public void CreateHead()
    {
        int num = GetComponent<PlayerPickup>().headSlot[0].GetComponent<Head>().headInt;
        GameObject head = ObjectPool.Instance.GetObject(headBullets[num]);
        float dir = transform.localScale.x;
        //生成投掷物
        head.transform.position = throwPoint.position;
        head.GetComponent<Rigidbody2D>().velocity = new Vector2(throwSpeed.x * dir, throwSpeed.y);
        //删除储存
        GetComponent<PlayerPickup>().headSlot[0] = null;
        GetComponent<PlayerPickup>().RefreshHead();
    }
}
