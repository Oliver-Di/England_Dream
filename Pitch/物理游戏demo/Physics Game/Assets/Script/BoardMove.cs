using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMove : MonoBehaviour
{
    private Vector3 targetPos;

    public float speed;

    void Start()
    {
        targetPos = transform.position + transform.position;
    }


    void Update()
    {
        Invoke("Move", 1);
    }

    private void Move()
    {
        if (GameManager.instance.gameMode != GameManager.GameMode.Ready)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);
        }
    }
}
