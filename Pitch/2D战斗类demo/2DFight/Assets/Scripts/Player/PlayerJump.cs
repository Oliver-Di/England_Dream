using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private int jumpCount;

    [Header("跳跃属性")]
    public bool isJumping;
    public bool isOnGround;
    public float jumpF;
    public float fallMultiplier;
    public float jumpMultiplier;
    public int jumpNum;

    [Header("地面检测")]
    public LayerMask groundLayerMask;
    public Transform leftLeg;
    public Transform rightLeg;
    public float distance;

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

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < jumpNum) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpF);
            isJumping = true;
            jumpCount++;
        }
        if (isOnGround && Input.GetAxisRaw("Jump") == 0)
        {
            isJumping = false;
            jumpCount = 0;
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
        RaycastHit2D leftRay = Physics2D.Raycast(leftLeg.position, Vector2.down, distance, groundLayerMask);
        RaycastHit2D rightRay = Physics2D.Raycast(rightLeg.position, Vector2.down, distance, groundLayerMask);
        if (leftRay.collider != null)
            Debug.DrawLine(leftLeg.position, leftRay.point, Color.red);
        else
            Debug.DrawLine(leftLeg.position, leftLeg.position + Vector3.down*distance, Color.green);
        if (rightRay.collider != null)
            Debug.DrawLine(rightLeg.position, rightRay.point, Color.red);
        else
            Debug.DrawLine(rightLeg.position, rightLeg.position + Vector3.down*distance, Color.green);

        if (leftRay.collider != null || rightRay.collider != null)
            return true;
        else
            return false;
    }
}