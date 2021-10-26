using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Transform slowMotionTarget;

    private float velocityX;

    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        movement();
    }

    //2D移动
    void movement()
    {
        var dt = MyTime.deltaTime;
        if (Vector3.Distance(transform.position, slowMotionTarget.position) > 3)
        {
            dt = dt;
        }
        else
        {
            dt = dt * 0.25f;
        }

        //移动
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x,
                speed * dt * 60, ref velocityX, 0.1f), rb.velocity.y);
            //anim.SetBool("isMoving", true);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x,
                speed * dt * -60, ref velocityX, 0.1f), rb.velocity.y);
            //anim.SetBool("isMoving", true);
        }
        else
        {
            rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, 0, ref velocityX, 0.1f),
                rb.velocity.y);
            //anim.SetBool("isMoving", false);
        }


        //转向
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        if (horizontalMove != 0)
            transform.localScale = new Vector3(horizontalMove, 1, 1);
    }
}