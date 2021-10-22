using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    private float _timer;

    [Tooltip("AI config")]
    public float turnTime=2;
    public float force=10000;

    [Range(0, 45)]
    public float angleVariantPercent;
    [Range(0, 50)]
    public float forceVariantPercent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        _timer = turnTime;
    }

    void Update()
    {
        if (GameManager.instance.gameMode == GameManager.GameMode.Enemy)
            _timer -= Time.deltaTime;

        if (_timer < 0)
            Attack();
    }

    private void Attack()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;
        direction = direction.normalized;
        //direction是敌人指向我的单位向量，去除了y轴干扰
        var newAngle = Quaternion.AngleAxis(Random.Range(-angleVariantPercent, angleVariantPercent), Vector3.up);
        var baseAngle = Quaternion.LookRotation(direction, Vector3.up);
        var resultAngle = newAngle * baseAngle;
        //两个Quaternion相乘，就是把他们对应的角度依次变换，得到的是最终的角度Quaternion

        var realForce = force * (Random.Range(-forceVariantPercent, forceVariantPercent) / 100f + 1);
        //百分比调整后的力度
        var finalDirection = resultAngle * Vector3.forward;
        //resultAngle是一个Quaternion，乘以Vector3.forward，得到这个Quaternion的forward对应的箭头的向量
        rb.AddForce(finalDirection * force);

        GameManager.instance.gameMode = GameManager.GameMode.Player;
        _timer = turnTime;
    }
}
