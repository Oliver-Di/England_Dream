using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header("跳跃属性")]
    public bool isJumping;
    public bool isOnGround;
    public float jumpF;
    public float fallMultiplier;
    public float jumpMultiplier;

    [Header("地面检测")]
    public Vector2 pointOffset;
    public Vector2 size;
    public LayerMask groundLayerMask;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (GetComponent<PlayerGetHit>().isDead == false &&
            GetComponent<PlayerGetHit>().isVertigo == false &&
            GetComponent<PlayerAttack>().isAttack == false)  
        {
            NormalJump();
        }
        isOnGround = OnGround();
    }

    private void NormalJump()
    {
        float dt = MyTime.deltaTime;

        if (Input.GetAxisRaw("Jump") == 1 && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpF);
            isJumping = true;
        }
        if (isOnGround && Input.GetAxisRaw("Jump") == 0)
        {
            isJumping = false;
            //设置动画
            anim.SetBool("drop", false);
            anim.SetBool("rise", false);
        }
        //玩家上升
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (jumpMultiplier - 1) * dt;
        }
        //玩家下坠
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * dt;
            isJumping = true;
        }
        //设置动画
        if (!isOnGround)
        {
            float latestY = transform.position.y;
            if (rb.velocity.y > 0)
            {
                //设置动画
                anim.SetBool("drop", false);
                anim.SetBool("rise", true);
            }
            else if (rb.velocity.y < 0)
            {
                //设置动画
                anim.SetBool("drop", true);
                anim.SetBool("rise", false);
            }
        }
    }
    //地面检测碰撞器
    bool OnGround()
    {
        Collider2D Coll = Physics2D.OverlapBox((Vector2)transform.position + pointOffset,
            size, 0, groundLayerMask);

        return Coll != null;
    }

    //显示地面碰撞器
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + pointOffset, size);
    }
}