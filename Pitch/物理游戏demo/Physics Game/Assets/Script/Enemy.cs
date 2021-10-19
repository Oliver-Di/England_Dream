using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;

    public float distance;
    public float force;
    public float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        timer = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameMode == GameManager.GameMode.Enemy)
            timer -= Time.deltaTime;

        if (timer < 0)
            Attack();
    }

    private void Attack()
    {
        Vector3 direction = player.transform.position - transform.position;
        distance = direction.magnitude;

        if (distance >= 20f)
        {
            rb.AddForce(direction.normalized * 20 * force);
        }
        else if (distance < 15f)
        {
            rb.AddForce(direction.normalized * 15 * force);
        }
        else
        {
            rb.AddForce(direction.normalized * distance * force);
        }

        GameManager.instance.gameMode = GameManager.GameMode.Player;
        timer = 2;
    }
}
